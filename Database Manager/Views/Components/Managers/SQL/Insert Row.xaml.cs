using Database_Manager.Services;
using Database_Manager.Views.Managers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Insert_Row : UserControl
    {

        public static Insert_Row insert_RowContext { set; get; }
        public Insert_Row()
        {
            insert_RowContext = this;
            InitializeComponent();
      
        }

        public void LoadTextBox()
        {
            try
            {


                var TableNames = new List<string>();


                isTableSelected();


                    var columns = SQL_DataGrid.sQL_DataGridContext.dataGrid.Columns;

                foreach (var column in columns)
                {
                    TableNames.Add(column.Header.ToString());

                }

                var TableTitle = "People";

                var stringText = $"INSERT INTO {TableTitle} ({string.Join(", ", TableNames)})" +
                    $"\n VALUES ({string.Join(", ", TableNames)})";

                Value_TextBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, stringText);
                SQL_Manager.sQL_ManagerContext.InsertRow_Popup.IsOpen = false;

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error inserting row", ex.Message);
            }

        }

        private void InsertRow_Button(object sender, RoutedEventArgs e)
        {



        }


        public void isTableSelected() 
        {
     

      

         var dataGridContext =    SQL_DataGrid.sQL_DataGridContext.ToJson();
            Debug.WriteLine("Table****:: " + dataGridContext);
            if (dataGridContext =="")

            {

                Debug.WriteLine("Table****:: "+ dataGridContext);
                return;
        
            }



            


          
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {

        }

        private void Value_TextBox_Loaded(object sender, RoutedEventArgs e)
        {




        }
    }
}
