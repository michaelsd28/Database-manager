using Database_Manager.Services;
using Database_Manager.Services.SQL;
using Microsoft.Toolkit.Uwp.UI.Controls;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq.Dynamic;
using System.Linq;
using System;
using MongoDB.Bson;
using System.Collections.Generic;
using Newtonsoft.Json;
using Database_Manager.Views.Managers;
using Windows.UI.Text;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.SQL
{
    public sealed partial class SQL_DataGrid : UserControl
    {
       public static SQL_DataGrid sQL_DataGridContext { get; set; }
        public SQL_DataGrid()
        {
            sQL_DataGridContext = this;
            InitializeComponent();


       //     LoadDataGrid(connString, tableName);

            //new SQL_Services().SearchValue("PersonID", "3",   dataGrid);
        }




        private void DeleteRowService(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => new SQL_Services().DelRow(GetDelString());

 



        private void DeleteRow_FlyOut(object sender, RoutedEventArgs e)
        {
   
            string rowFirstValue = "Value";
            string TableName = new SQLLocalSettings().GetLocalSettings_Bson()["CurrentTable"].ToString();

            string query = $"DELETE FROM {TableName} WHERE {GetDelString()} ;";

            _ = new DialogService()._DialogService
                (
                title: $"Do you want to delete row {dataGrid.SelectedIndex+1}?", 
                content: $"Executing: \n{query} \n*multiple rows could be deleted*",
                PrimaryButton: DeleteRowService
                );

        }







        public string GetDelString() {

            try { 

            var valueList = dataGrid.SelectedItem.ToJson().ToString();
            var keyList = dataGrid.Columns.Select(x => x.Header).Skip(1).ToArray();
            var deleteString = "";

            var valueListColl = JsonConvert.DeserializeObject<List<string>>(valueList).Skip(1).ToArray();

            for (int x = 0; x < keyList.Count(); x++)
            {

                if (keyList.Count() == 1) return $"{keyList[x]}='{valueListColl[x]}' ";

                deleteString += $"{keyList[x]}='{valueListColl[x]}' AND ";

            }

            deleteString = deleteString.Substring(0, deleteString.Length - 4);

            return deleteString;

            }
            catch (Exception ex) {
                _ = new DialogService()._DialogService("Error Selecting", ex.Message);
                return "";
            }

        }



        private void dataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {

            //Show the ascending icon when acending sort is done
            e.Column.SortDirection = DataGridSortDirection.Ascending;

            //Show the descending icon when descending sort is done
            e.Column.SortDirection = DataGridSortDirection.Descending;



            try {


            //Use the Tag property to pass the bound column name for the sorting implementation 
            if (e.Column.Tag.ToString() == "FirstName")
            {
                //Use the Tag property to pass the bound column name for the sorting implementation
                if (e.Column.Tag.ToString() == "FirstName")
                {
                    //Implement sort on the column "Range" using LINQ
                    if (e.Column.SortDirection == null || e.Column.SortDirection == DataGridSortDirection.Descending)
                    {
                        string sortTypeStr = "ASC"; // or DESC
                        string SortColumnName = "FirstName"; // Your column name
             

                       // dataGrid.ItemsSource = new ObservableCollection<dynamic>().OrderBy($"{SortColumnName} {sortTypeStr}");
                        e.Column.SortDirection = DataGridSortDirection.Ascending;
                    }
                    else
                    {
                        string sortTypeStr = "DESC"; // or DESC
                        string SortColumnName = "FirstName"; // Your column name

                   //     dataGrid.ItemsSource = new ObservableCollection<dynamic>().OrderBy($"{SortColumnName} {sortTypeStr}");

                        e.Column.SortDirection = DataGridSortDirection.Descending;
                    }
                }
                // add code to handle sorting by other columns as required

                // Remove sorting indicators from other columns
                foreach (var dgColumn in dataGrid.Columns)
                {
                    if (dgColumn.Tag.ToString() != e.Column.Header.ToString())
                    {
                        dgColumn.SortDirection = null;
                    }
                }
            }

            }
            catch (Exception ex) {

                _ = new DialogService()._DialogService    (    "Error sorting",      ex.Message    );
            }
        }

        private void UpdateRow_FlyOut(object sender, RoutedEventArgs e)
        {
            SQL_Manager.sQL_ManagerContext.UpdateRow_Popup.Visibility = Visibility.Visible;

            var updateString = Update_Row.update_RowContext.Value_TextBox_Loaded();

             Update_Row.update_RowContext.Value_TextBox.Document.SetText(TextSetOptions.None, updateString);
        }
    }
}
