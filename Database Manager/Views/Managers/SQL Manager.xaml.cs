using Database_Manager.Services;
using Database_Manager.Services.SQL;
using Database_Manager.Views.Components.Managers.SQL;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Managers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SQL_Manager : Page
    {


        public static SQL_Manager sQL_ManagerContext { get; set; }

        public SQL_Manager()
        {

            sQL_ManagerContext = this;
          
            InitializeComponent();

        

            // testRedis();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);


                SizeChanged += SQL_Manager_SizeChanged;



        }

      

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await new SQL_Services().LoadDatabases();


            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                string uri = parametersPassed.Replace("URI: ", "");


            //   new SQLLocalSettings().UpdateURI(uri);




            }

        }



        private void SQL_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        
          =>  new SQLManagerService_Helper().LeftMenu_SizeChanged(LeftPopUp, LeftBar_GridContainer);
        

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));

        }

        private void InsertRow_Button(object sender, RoutedEventArgs e)
        {

          var isTableNull =  SQL_DataGrid.sQL_DataGridContext.ToJson();


            if (isTableNull.ToString() =="null") 
            
            {
                _ = new DialogService()._DialogService("Error", "Please select a table");
                return;
            }



            Insert_Row.insert_RowContext.LoadTextBox();
            new SQLManagerService_Helper().ToggleGrid(InsertRow_Popup);
        }



        

        private void BExpandLeftMenu(object sender, RoutedEventArgs e)
          =>  new SQLManagerService_Helper().TogglePopup(LeftPopUp);

        

        private async void BSearchValue(object sender, RoutedEventArgs e)
        {
            try
            {
                string TableName = new SQLLocalSettings().GetLocalSettings_Bson()["CurrentTable"].ToString();
                var searchText = TextBoxFilter.Text;

                if (searchText == "") {
                   await new SQL_Services().UpdateTableGrid(TableName);
                   return;
                };

                var SearchDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchText);

                var columnName = SearchDictionary.Keys.ToArray()[0];
                var searchValue = SearchDictionary[columnName];

                SearchDictionary.Add("SearchColumn", columnName);
                SearchDictionary.Add("SearchTerm", searchValue);

                await new SQL_Services().UpdateTableGrid(TableName, SearchDictionary);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Search value error", ex.Message);

            }

        }

        private async void RefreshTable_Button(object sender, RoutedEventArgs e)
        {
            var CurrentTable = new SQLLocalSettings().GetLocalSettings_Bson()["CurrentTable"].ToString();
           await new SQL_Services().UpdateTableGrid(CurrentTable);
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFilter.Text == "")
            {

                string TableName = new SQLLocalSettings().GetLocalSettings_Bson()["CurrentTable"].ToString();
                new SQL_Services().UpdateTableGrid(TableName);
                return;
            };
        }
    }
}


public  class SQLManagerService_Helper{


    public void ToggleGrid(Grid grid) 
    {

        if (grid.Visibility == Visibility.Visible)
        {
            grid.Visibility = Visibility.Collapsed;

            return;
        }

        grid.Visibility = Visibility.Visible;

    }

    public void TogglePopup(Popup popup)
    {


        if (!popup.IsOpen)
        {
            popup.IsOpen = true;

            return;
        }

        popup.IsOpen = false;
    }




    public void LeftMenu_SizeChanged (Popup LeftPopup, Grid LeftBar_GridContainer) 
    {
        //Get the current Windows Size
        var bounds = Window.Current.Bounds;
        double height = bounds.Height;

        double width = bounds.Width;


        if (width < 755)
        {


            LeftPopup.IsOpen = false;

        }
        else
        {

            LeftPopup.IsOpen = true;

        }

        LeftBar_GridContainer.Height = height - 230;


    }

}