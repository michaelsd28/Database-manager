﻿using Database_Manager.Views.Managers;
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

namespace Database_Manager.Views.Components.Managers.SQL.tree
{
    public sealed partial class Tree_header : UserControl
    {






        public string TextHeader
        {
            get { return (string)GetValue(TextHeaderProperty); }
            set { SetValue(TextHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHeaderProperty =
            DependencyProperty.Register("TextHeader", typeof(string), typeof(Tree_header), new PropertyMetadata(string.Empty));




        public Tree_header()
        {
            this.InitializeComponent();
        }

        private void BAddTable(object sender, RoutedEventArgs e)
        {

            ToggleGrid(SQL_Manager.sQL_ManagerContext.CreateTable_Popup);
      
        }


        private void ToggleGrid(Grid createTable_Popup) 
        {

            if (createTable_Popup.Visibility == Visibility.Collapsed)
            {
                createTable_Popup.Visibility = Visibility.Visible;

            }
            else {
                createTable_Popup.Visibility = Visibility.Collapsed;

            }
      
            


      

        }
      
    }
}
