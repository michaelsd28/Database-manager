using Database_Manager.Services;
using Database_Manager.Services.SQL;
using MySqlConnector;
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

namespace Database_Manager.Views.Components.Managers.SQL.tree
{
    public sealed partial class Database_tree : UserControl
    {






        public string DBName
        {
            get { return (string)GetValue(DBNameProperty); }
            set { SetValue(DBNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DBName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DBNameProperty =
            DependencyProperty.Register("DBName", typeof(string), typeof(Database_tree), new PropertyMetadata(string.Empty));


        public Database_tree(string MyDBName)
        {
            InitializeComponent();
            DBName = MyDBName;
            LoadTables();
        }





        public  void LoadTables() 

          =>  new SQL_Services().GetTables(DBName,stackpanel_Tables);



    }
}
