using Database_Manager.Views.Components.Managers.SQL.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Database_Manager.Services.SQL
{
    internal class SQL_Service
    {

        public StackPanel GetLeftMenu()
        {
            var stackPanel = new StackPanel();   

            var database_Tree = new Database_tree();  
            stackPanel.Children.Add(database_Tree);





            return stackPanel;
        
        }
    }
}
