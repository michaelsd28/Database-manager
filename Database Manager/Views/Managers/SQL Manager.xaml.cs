using Database_Manager.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Managers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SQL_Manager : Page
    {
        public SQL_Manager()
        {
            this.InitializeComponent();



            // testRedis();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);

            SizeChanged += SQL_Manager_SizeChanged;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string uri = String.Empty;
            bool isCloudURI = true;

            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                uri = parametersPassed.Replace("URI: ", "");


 

            }


            if (uri.Contains("127.0.0.1") || uri.Contains("localhost"))

            {
                isCloudURI = false;

            }




      

        }



        private void SQL_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Get the current Windows Size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;

            double width = bounds.Width;


            if (width < 755)
            {

                Debug.WriteLine($"width < 755):: {width < 755}");
                LeftPopUp.IsOpen = false;
        
            }
            else
            {
                Debug.WriteLine($"width < 755):: {width < 755}");
                LeftPopUp.IsOpen = true;

            }

     

            LeftBar_GridContainer.Height = height - 230;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));

        }

        private void InsertKeyValue_Modal(object sender, RoutedEventArgs e)
        {
    
        }








        private void BExpandLeftMenu(object sender, RoutedEventArgs e)
        {
            if (LeftPopUp.IsOpen)
            {
                LeftPopUp.IsOpen = false;
                return;
            }

            LeftPopUp.IsOpen = true;

        }



        private void BSearchValue(object sender, RoutedEventArgs e)
        {
            try
            {
          




            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Select error", ex.Message);

            }

        }
    }
}
