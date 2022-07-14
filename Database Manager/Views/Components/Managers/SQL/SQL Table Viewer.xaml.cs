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



        
        public SQL_Table_Viewer()
        {
            InitializeComponent();
            LoadDataGrid("","");

        }




        
        public void LoadDataGrid(string connString, string tableName ) 
        {

             connString = @"Server=localhost;User ID=debian-sys-maint;Password=oYM7Qh9SqgL3RD7T;Database=mydb";
            tableName = "Persons";


            var query = $"SELECT * FROM {tableName};";



            DataTable dataTable = new DataTable();

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();

            // create data adapter
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);
            ToSourceCollection(dataTable, dataGrid);
            conn.Close();
            da.Dispose();
        }

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

