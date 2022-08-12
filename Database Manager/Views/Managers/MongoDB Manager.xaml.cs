using Database_Manager.Services;
using Database_Manager.Services.MongoDB;
using Database_Manager.Views.Components.Managers.MongoDB;
using Microsoft.Toolkit.Uwp.UI.Controls;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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
            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                string uri = parametersPassed.Replace("URI: ", "");



                new MongoDB_LocalSettings().UpdateURI(uri);
     


            }
         

            new MongoDB_DatabaseService().UpdateDBLeftMenu();

        }


        private void MongoDB_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
          => new Helper().HandleSizeChange_LeftPopUp(LeftPopUp,LeftBar_GridContainer);
        

        private void AddButton(object sender, RoutedEventArgs e)
           => new Helper().TogglePopUP(CreateMyDB_POPUP);

         


        







        private void ExpandLeftBar(object sender, RoutedEventArgs e)
           => new Helper().TogglePopUP(LeftPopUp);





        

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


           new Helper(). ToggleGrid_Visibility(InsertDocument_POPUP);





        }



        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
          =>  new Helper().Documents_FilterSearch(TextBoxFilter.Text);

        

        private void ExportButton(object sender, RoutedEventArgs e)
            =>new MongoDB_DatabaseService().ExportCollection();

     

        private void ImportButton(object sender, RoutedEventArgs e)
           => new Helper().TogglePopUP(ImportDB_POPUP);

 

        private void BViewAsJson(object sender, RoutedEventArgs e)
        {

        }

        private void BViewAsGrid(object sender, RoutedEventArgs e)
             =>   new MongoDB_DatabaseService().ViewCollectionAsGrid();

    


        internal class Helper {


            public void Documents_FilterSearch(string textSearch) {


                try
                {

                    if (textSearch == string.Empty)
                    {

                        new MongoDB_DatabaseService().UpdateDocumentList();
                        return;
                    }


                    var FilterDocs = BsonDocument.Parse(textSearch);

                    new MongoDB_DatabaseService().SearchInCollection(FilterDocs);

                }
                catch { }


            }


            public void ToggleGrid_Visibility(Grid grid) 
            {
                if (grid.Visibility == Visibility.Visible)
                {
                    grid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    grid.Visibility = Visibility.Visible;
                }


            }


            public void TogglePopUP(Popup myPopup) {

                if (myPopup.IsOpen == false)
                {
                    myPopup.IsOpen = true;
                }
                else
                {
                    myPopup.IsOpen = false;
                }

            }

            public void HandleSizeChange_LeftPopUp(Popup LeftPopUp,Grid LeftBar_GridContainer) 
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


        }
    }

}

/*    <controls:DataGrid 

    Background="{ThemeResource MainAcrylicB}"

CacheMode="BitmapCache"
ScrollViewer.BringIntoViewOnFocusChange="True"
VerticalScrollBarVisibility="Visible"
HorizontalScrollBarVisibility="Visible"
CanBeScrollAnchor="True"
VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
ScrollViewer.HorizontalScrollBarVisibility="Visible"
ScrollViewer.VerticalScrollBarVisibility="Visible"

AlternatingRowBackground="#1F1B00"
    x:Name="dataGrid"

    x:FieldModifier="Public"

Sorting="dataGrid_Sorting"


CanUserSortColumns="True"

>*/