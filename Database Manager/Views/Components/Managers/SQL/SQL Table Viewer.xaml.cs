using Database_Manager.Services;
using Database_Manager.Services.SQL;
using Microsoft.Toolkit.Uwp.UI.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis
{




    public sealed partial class SQL_Table_Viewer : UserControl
    {




        public SQL_Table_Viewer(string connString, string tableName)
        {

            InitializeComponent();
            //string connString = ""; string tableName ="";
            LoadDataGrid(connString,tableName);

        }




        
        public void LoadDataGrid(string connString, string tableName ) 
        
         =>   new SQL_Service().DisplayTable_DataGrid(connString,tableName,dataGrid);
     
        

        private void ToSourceCollection(DataTable dt, DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = dt.Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var sourceCollection = new ObservableCollection<object>();
            foreach (DataRow row in dt.Rows)
                sourceCollection.Add(row.ItemArray);

            dataGrid.ItemsSource = sourceCollection;
        }
    }
    }

