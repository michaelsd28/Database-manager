using Database_Manager.Views.Components.Managers.SQL.tree;
using Database_Manager.Views.Managers;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Database_Manager.Services.SQL
{
    internal class SQL_Service
    {

        public StackPanel GetLeftMenu(string DBName = "")
        {
            var stackPanel = new StackPanel();   

            var database_Tree = new Database_tree(DBName)  ;  
            stackPanel.Children.Add(database_Tree);

            return stackPanel;
        
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

                                SQL_Manager.sQL_ManagerContext.DatabaseStackPanel.Children.Add(new SQL_Service().GetLeftMenu(dbName));
                 
                            }
               
                  

                        }
              
                }
            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);
            }



        }

        internal async void GetTables(string DBName,StackPanel stackPanel)
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

                                stackPanel.Children.Add(new Table_button() { TableName = tableName });

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
