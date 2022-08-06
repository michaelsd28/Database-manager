using Database_Manager.Services.SQL;
using Database_Manager.Views.Managers;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.SQL
{
    public sealed partial class Update_Row : UserControl
    {

        public static Update_Row update_RowContext { get; set; }
        public Update_Row()
        {
            update_RowContext = this;
            InitializeComponent();

        } 
        

        private void BCancel(object sender, RoutedEventArgs e)
                => SQL_Manager.sQL_ManagerContext.UpdateRow_Popup.Visibility = Visibility.Collapsed;

        private async void BUpdateRow(object sender, RoutedEventArgs e)
        {


            var currentTable = new SQLLocalSettings().GetLocalSettings()["CurrentTable"].ToString();


            Value_TextBox.Document.GetText(TextGetOptions.None, out string query );


            await new SQL_Services().ExecQuery(query);

            new SQL_Services().UpdateTableGrid(currentTable);
            SQL_Manager.sQL_ManagerContext.UpdateRow_Popup.Visibility = Visibility.Collapsed;
        }




        

        public  string Value_TextBox_Loaded()
        {


            var currentTable = new SQLLocalSettings().GetLocalSettings()["CurrentTable"].ToString();



            string query = $"UPDATE {currentTable} \n" +
                $"SET  {GetValuesRow(isSet: true)} \n" +
                $"WHERE {GetValuesRow( isSet: false)} ;";

            return query;

        }



        private string GetValuesRow( bool isSet= false) 
        {

            var columnNames = SQL_DataGrid.sQL_DataGridContext.dataGrid.Columns.Skip(1).ToArray();

            var SelectedRow = SQL_DataGrid.sQL_DataGridContext.dataGrid.SelectedItem.ToJson();
            var SelectedRowList = JsonConvert.DeserializeObject<List<string>>(SelectedRow);
            var RowValues = SelectedRowList.Skip(1).ToList().Select((value, index) =>
            $"{columnNames[index].Header}='{value}'");

            if (isSet)
            {


                return string.Join(" , ", RowValues);

            }
            else 
            {
                return string.Join(" AND ", RowValues);
            }
        }

 
    }
}
