﻿using Database_Manager.Services.SQL;
using System;
using System.Collections.Generic;
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
    public sealed partial class SQL_DataGrid : UserControl
    {
       public static SQL_DataGrid sQL_DataGridContext { get; set; }
        public SQL_DataGrid(string connString, string tableName)
        {
            sQL_DataGridContext = this;
            InitializeComponent();

            LoadDataGrid(connString, tableName);

  

          
        }

        public void LoadDataGrid(string connString, string tableName)
        
           => new SQL_Services().DisplayTable_DataGrid(connString, tableName, dataGrid);



   

    }
}
