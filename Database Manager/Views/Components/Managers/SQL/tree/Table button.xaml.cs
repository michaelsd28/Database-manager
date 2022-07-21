﻿using Database_Manager.Services.SQL;
using Database_Manager.Views.Components.Managers.Redis;
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
using Windows.UI;
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
    public sealed partial class Table_button : UserControl
    {



        public string TableName
        {
            get { return (string)GetValue(TableNameProperty); }
            set { SetValue(TableNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TableName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TableNameProperty =
            DependencyProperty.Register("TableName", typeof(string), typeof(Table_button), new PropertyMetadata(string.Empty));


        public Table_button()
        {
            InitializeComponent();
        }

        private void BTable_Click(object sender, RoutedEventArgs e)
       
           => new SQL_Services().UpdateTableGrid(TableName);
            
        

    }
}
