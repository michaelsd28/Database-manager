using Database_Manager.Views.Dialogs;
using Database_Manager.Views.Managers;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Database_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage MainPageContext { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            MainPageContext = this;



  
            /*
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Colors.Black;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Black;
            */

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 500));



            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
        Window.Current.SetTitleBar(AppTitleBar);

        }


        private void BCreateDB_Click(object sender, RoutedEventArgs e)
        {

            

            Database_type_to_create._Type_To_CreateContext.Content = new Database_type_to_create();

            if (Popup_Create.IsOpen)
            {

                Popup_Create.IsOpen = false;

            }
            else
            {

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