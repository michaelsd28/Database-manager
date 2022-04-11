using Database_Manager.Views;
using Database_Manager.Views.Dialogs;
using Database_Manager.Views.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Database_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       public static  MainPage MainPageContext { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            MainPageContext = this;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Colors.Black;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Black;

         
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(800,500));
         
        }

        private void BCreateDB_Click(object sender, RoutedEventArgs e)
        {



            Database_type_to_create._Type_To_CreateContext.Content = new Database_type_to_create();

            if (Popup_Create.IsOpen)
            {
       
                Popup_Create.IsOpen = false;
                
            }
            else {
            
                Popup_Create.IsOpen = true;
            }

           



        }

        private void BConnectDB_Click(object sender, RoutedEventArgs e)
        {

            Database_type_to_create._Type_To_CreateContext.Content = new Database_type_to_create();

            if (Popup_ConnectURI.IsOpen)
            {

                Popup_ConnectURI.IsOpen = false;

            }
            else
            {

                Popup_ConnectURI.IsOpen = true;
            }



        }

        private void BRefreshDB_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MongoDB_Manager));
       

        }
    }
}
