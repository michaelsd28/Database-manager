﻿using Database_Manager.Views.Components.Managers.SQL.tree;
using Database_Manager.Views.Managers;
using Microsoft.Toolkit.Uwp.UI.Controls;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Diagnostics;
using MongoDB.Bson;
using Database_Manager.Views.Components.Managers.SQL;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Database_Manager.Services.SQL
{
    internal class SQL_Services
    {


        /// <summary>
        /// Insert row
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>

        public async Task ExecQuery(string query)
        {
            try
            {
                string connString = @"Server=localhost;User ID=root;Password=admin;Database=mydb";




                using (var conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();



                    // Retrieve all rows
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {



                            for (int x = 0; x < reader.FieldCount; x++)
                            {

                                var ReaderValue = reader.GetValue(x).ToString();

                                Debug.WriteLine("ReaderValue:: " + ReaderValue);

                            }

                        }
                }
            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);

            }

        }

        public async void DelRow(string DelString) 
        {
            string TableName = new SQLLocalSettings().GetLocalSettings()["CurrentTable"].ToString();
            string query = $"DELETE FROM {TableName} WHERE {DelString} ;";
            await ExecQuery(query);
            new SQL_Services().UpdateTableGrid(TableName);

        }


        public StackPanel GetLeftMenu(string DBName = "")
        {
            var stackPanel = new StackPanel();   

            var database_Tree = new Database_tree(DBName)  ;  
            stackPanel.Children.Add(database_Tree);

            return stackPanel;
        
        }

        public void DisplayTable_DataGrid(string connString, string tableName,DataGrid dataGrid,int limitNumber = 20) 
        {
            try
            {

                 connString = @"Server=localhost;User ID=root;Password=admin;Database=mydb";


                var query = $"SELECT * FROM {tableName} LIMIT {limitNumber};";
       

 
                DataTable dataTable = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand cmd = new MySqlCommand(query, conn);
           
                conn.Open();

                // create data adapter
                MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                sqlAdapter.Fill(dataTable);

                ToSourceCollection(dataTable, dataGrid);

                conn.Close();
                sqlAdapter.Dispose();

            }
            catch (Exception ex)
            {
                _ = new DialogService()._DialogService("Display grid error", ex.Message);
            }



        }


        internal void SearchValue(string ColumnName,string SearchValue) 
        {


          
            var columns = SQL_DataGrid.sQL_DataGridContext.dataGrid.Columns.Select(x => x.Header.ToString()).ToArray();
            var rows = SQL_DataGrid.sQL_DataGridContext.dataGrid.ItemsSource;
            var rowList = new List<string[]>();

            int searchValueIndex = 0;
            for (int i = 0; i < columns.Count(); i++)
            {

                if (columns[i] == ColumnName)
                    searchValueIndex = i;
                

            }


            foreach (var row in rows)
            {
                rowList.Add(JsonConvert.DeserializeObject<string[]>(row.ToJson().ToString()));
            }

            var filterList = rowList.Where(x => x[searchValueIndex] == SearchValue );

            var sourceCollection = filterList;




            SQL_DataGrid.sQL_DataGridContext.dataGrid.ItemsSource = sourceCollection;


        }

        internal void UpdateTableGrid(string currentTable)
        {

            SQL_Manager.sQL_ManagerContext.SQL_GridTableContainer.Children.Clear();
            Grid grid = new Grid { Name = "documents_Container" };


            var sqlGrid = new SQL_DataGrid("", currentTable);
            grid.Children.Add(sqlGrid);


            new SQLLocalSettings().UpdateCurrentTable(currentTable);
            SQL_Manager.sQL_ManagerContext.SQL_GridTableContainer.Children.Add(grid);
        
        }
        

        private void ToSourceCollection(DataTable dt, DataGrid dataGrid)
        {

      

            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;

            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "#",
                Binding = new Binding { Path = new PropertyPath("[" + 0.ToString() + "]") },


            });


            for (int i = 1; i < dt.Columns.Count+1; i++)
            {


                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = dt.Columns[i-1].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") },
                    Tag = dt.Columns[i-1].ColumnName,     
                    
                });
            }



            var sourceCollection = new ObservableCollection<object>();

    


            var index = 1;
            foreach (DataRow row in dt.Rows) {

      

                var arr = row.ItemArray.ToList();

                arr.Insert(0,index);

    
                sourceCollection.Add(arr);
                index++;
            }



    


            dataGrid.ItemsSource = sourceCollection;
        }


        
     


        internal async Task  LoadDatabases()
        {

            try
            {

                SQL_Manager.sQL_ManagerContext.DatabaseStackPanel.Children.Clear();

                string connString = @"Server=localhost;User ID=root;Password=admin;Database=mydb";
                //  string connString = @"Server=localhost;User ID=root;Password=";

                var query = "SHOW DATABASES;";


                using (var conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();



                    // Retrieve all rows
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())

                        while (await reader.ReadAsync()) {

                            for (int x = 0;x<reader.FieldCount; x++) {

                                var dbName = reader.GetString(x);

                                SQL_Manager.sQL_ManagerContext.DatabaseStackPanel.Children.Add(new SQL_Services().GetLeftMenu(dbName));
                 
                            }
               
                        }
                }
            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error LoadDatabases", ex.Message);
            }

        }




        internal async void GetTables(string DBName,GridView stackPanel)
        {
            try
            {
                string connString = @"Server=localhost;User ID=root;Password=admin;Database=mydb";
                //     var query = "SELECT * FROM Persons;";
                string query = $"SHOW TABLES FROM {DBName};";


                using (var conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();



                    // Retrieve all rows
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())

                        while (await reader.ReadAsync())
                        {

                            for (int x = 0; x < reader.FieldCount; x++)
                            {

                                var tableName = reader.GetString(x);
          


                                stackPanel.Items.Add(new Table_button() { TableName = tableName });

                            }

                        }

                }
            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);
            }

        }
    }
}
