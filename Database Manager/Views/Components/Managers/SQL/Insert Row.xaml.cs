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
        public Insert_Row()
        {
            InitializeComponent();

   

          
            //LoadTextBox();
     
        }

        private void LoadTextBox()
        {
            try
            {


                var TableNames = new List<string>();


                if (SQL_DataGrid.sQL_DataGridContext.dataGrid.Name == null) return;


                var columns = SQL_DataGrid.sQL_DataGridContext.dataGrid.Columns;

                foreach (var column in columns)
                {
                    TableNames.Add(column.Header.ToString());

                }

                var TableTitle = "People";

                var stringText = $"INSERT INTO {TableTitle} ({string.Join(", ", TableNames)})" +
                    $"VALUES({string.Join(", ", TableNames)})";

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
            var Grids = SQL_Manager.sQL_ManagerContext.SQL_GridTableContainer.Children;
            Grid containerGrid = null;
            foreach (Grid grid in Grids)
            {
                if (grid.Name == "documents_Container") 
                {
                    Debug.WriteLine($"contains?? ::: {grid.Children[0].ToJson()}");
                }
     
          
          
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
