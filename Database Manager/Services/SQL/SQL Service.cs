using Database_Manager.Views.Components.Managers.SQL.tree;
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
                string connString = @"Server=localhost;User ID=debian-sys-maint;Password=oYM7Qh9SqgL3RD7T;Database=mydb";

    


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

                                var ReaderValue = reader.GetValue(x).ToString() ;

                            }

                        }
                }
            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);

            }

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

                connString = @"Server=localhost;User ID=debian-sys-maint;Password=oYM7Qh9SqgL3RD7T;Database=mydb";
        

                var query = $"SELECT * FROM {tableName} LIMIT {limitNumber};";
           var adsd =     "SELECT TO* , ROW_NUMBER()  over (order by (SELECT NULL))  as `#️⃣` from dbo.Persons ;";
                string query2 = "SELECT* , ROW_NUMBER() over (order by (SELECT NULL)) as `#️⃣` from " + tableName+";";

                Debug.WriteLine($"query2:: {query2}");
                DataTable dataTable = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand cmd = new MySqlCommand(query2, conn);
                conn.Open();

                // create data adapter
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                ToSourceCollection(dataTable, dataGrid);

                

                var enumerator = dataGrid.ItemsSource.GetEnumerator();
                while (enumerator.MoveNext()) 
                {
                    var element =enumerator.Current;

                  //  Debug.WriteLine($"dataGrid.ItemsSource:: {element.ToBsonDocument()["_v"]}");

                }
                conn.Close();
                da.Dispose();

            }
            catch (Exception ex)
            {


                _ = new DialogService()._DialogService("Display grid error", ex.Message);
            }



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
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = dt.Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") },
                    Tag = dt.Columns[i].ColumnName,     
                    
                });
            }

            var sourceCollection = new ObservableCollection<object>();
            foreach (DataRow row in dt.Rows)
                sourceCollection.Add(row.ItemArray);

            dataGrid.ItemsSource = sourceCollection;
        }


        internal async void LoadDatabases()
        {

            try
            {
                string connString = @"Server=localhost;User ID=debian-sys-maint;Password=oYM7Qh9SqgL3RD7T;Database=mydb";
                //     var query = "SELECT * FROM Persons;";
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

                _ = new DialogService()._DialogService("Error", ex.Message);
            }

        }




        internal async void GetTables(string DBName,GridView stackPanel)
        {
            try
            {
                string connString = @"Server=localhost;User ID=debian-sys-maint;Password=oYM7Qh9SqgL3RD7T;Database=mydb";
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
