using Database_Manager.Services;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Managers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MongoDB_Manager : Page
    {

        public static MongoDB_Manager MongoDB_ManagerContext { get; set; }
        public MongoDB_Manager()
        {
            InitializeComponent();
            SizeChanged += MongoDB_Manager_SizeChanged;
            MongoDB_ManagerContext = this;


            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LeftPopUp.IsOpen = false;
            string uri = String.Empty;
            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                uri = parametersPassed.Replace("URI: ", "");

                var localSettings = ApplicationData.Current.LocalSettings;

                // Create a simple setting.
                localSettings.Values["CurrentURI"] = uri;

  

            }
            else CreateMyDB_POPUP.IsOpen = true;

            new MongoDB_DatabaseService().UpdateDBLeftMenu();

        }


        private void MongoDB_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Get the current Windows Size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;

            double width = bounds.Width;



            if (width < 755)
            {
                LeftPopUp.IsOpen = false;
            }
            else
            {

                LeftPopUp.IsOpen = true;
            }
            LeftBar_GridContainer.Height = height - 230;
        }



        private void AddButton(object sender, RoutedEventArgs e)
        {



            if (CreateMyDB_POPUP.IsOpen == true) CreateMyDB_POPUP.IsOpen = false;
            else CreateMyDB_POPUP.IsOpen = true;


        }







        private void ExpandLeftBar(object sender, RoutedEventArgs e)
        {


            if (LeftPopUp.IsOpen)
            {
                LeftPopUp.IsOpen = false;
            }
            else
            {

                LeftPopUp.IsOpen = true;
            }



        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));

        }

        private async void Insert_Button(object sender, RoutedEventArgs e)

        {


            var localSettings = ApplicationData.Current.LocalSettings;
            object currentCollect = localSettings.Values["CurrentCollection"];


            if (currentCollect.ToString() == string.Empty)
            {

                await new DialogService()._DialogService("Please select a collection", "press ok to continue");

                return;
            }



            if (InsertDocument_POPUP.Visibility == Visibility.Visible)
            {
                InsertDocument_POPUP.Visibility = Visibility.Collapsed;
            }
            else
            {
                InsertDocument_POPUP.Visibility = Visibility.Visible;
            }




        }



        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (TextBoxFilter.Text == string.Empty)
            {

                new MongoDB_DatabaseService().UpdateDocumentList();
                return;
            }

            new MongoDB_DatabaseService().SearchInCollection(TextBoxFilter.Text);

        }

        private void ExportButton(object sender, RoutedEventArgs e)
        {

            new MongoDB_DatabaseService().ExportCollection();

     

        }




        private void ImportButton(object sender, RoutedEventArgs e)
        {


            if (ImportDB_POPUP.IsOpen == false)
            {
                ImportDB_POPUP.IsOpen = true;
            }
            else
            {
                ImportDB_POPUP.IsOpen = false;
            }



        }
    }

}

