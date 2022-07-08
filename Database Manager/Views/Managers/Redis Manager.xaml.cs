using Database_Manager.Services;
using Database_Manager.Services.Redis;
using FreeRedis;
using MongoDB.Bson;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Components.Managers.Redis
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Redis_Manager : Page
    {
        public static Redis_Manager Redis_ManagerContext { get; set; }
     
        public Redis_Manager()
        {
            Redis_ManagerContext = this;

            InitializeComponent();

          ///  StartCode();
         new RedisDB_Services().UpdateLeftMenu();

           

           // testRedis();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);

            SizeChanged += Redis_Manager_SizeChanged;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string uri = String.Empty;
            bool isCloudURI = true;

            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                uri = parametersPassed.Replace("URI: ", "");


                new RedisLocalSettings().RedisURI(newURL: uri);

            }


            if (uri.Contains("127.0.0.1") || uri.Contains("localhost"))

            {
                isCloudURI = false;

            }
           
      


            new RedisDB_Services().UpdateLeftMenu(isCloudURI);

        }



        private void Redis_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
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

            try {
                var isPleaseSelect = Redis_documentContainer.Children[0].ToBsonDocument()["Text"].ToString();

                if (isPleaseSelect == "Please select a key")
                {
                    LeftPopUp.IsOpen = true;
                }

            } catch { }


            LeftBar_GridContainer.Height = height - 230;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));

        }

        private void InsertKeyValue_Modal(object sender, RoutedEventArgs e)
        {
            Redis_Manager.Redis_ManagerContext.AddKey_Grid.Visibility = Visibility.Visible;
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
                var sValue = Single_Document.RedisContextSingle_Document.SingleTextBox.Text;

                int selectStart = sValue.IndexOf(TextBoxFilter.Text);
                int selectEnd = TextBoxFilter.Text.Length;



                Single_Document.RedisContextSingle_Document.SingleTextBox.Select(selectStart, selectEnd);


                Single_Document.RedisContextSingle_Document.SingleTextBox.Select(selectStart, selectEnd);


                Single_Document.RedisContextSingle_Document.SingleTextBox.Focus(FocusState.Keyboard);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Select error", ex.Message);

            }

        }
    }
}
