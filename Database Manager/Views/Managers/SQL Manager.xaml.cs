using Database_Manager.Services;
using Database_Manager.Services.SQL;
using Database_Manager.Views.Components.Managers.SQL;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

      

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            String uri = String.Empty;

            new SQL_Services().LoadDatabases();


            if (e.Parameter != null)
            {
                string parametersPassed = e.Parameter.ToString();
                uri = parametersPassed.Replace("URI: ", "");

            }

        }



        private void SQL_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        
          =>  new SQLManagerServices().LeftMenu_SizeChanged(LeftPopUp, LeftBar_GridContainer);
        

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
            new SQLManagerServices().ToggleGrid(InsertRow_Popup);
        }



        

        private void BExpandLeftMenu(object sender, RoutedEventArgs e)
        

          =>  new SQLManagerServices().TogglePopup(LeftPopUp);

        

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

        private void RefreshTable_Button(object sender, RoutedEventArgs e)

        {

            var CurrentTable = new SQLLocalSettings().GetLocalSettings()["CurrentTable"].ToString();
            new SQL_Services().UpdateTableGrid(CurrentTable);
        } 
        
    }
}


public  class SQLManagerServices{


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