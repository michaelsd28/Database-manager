using Database_Manager.Services;
using Database_Manager.Views;
using Database_Manager.Views.Components.Managers.Redis;
using Database_Manager.Views.Dialogs;
using StackExchange.Redis;
using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
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
        public static ApplicationDataContainer localSettingsMain { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            MainPageContext = this;



            localSettingsMain = ApplicationData.Current.LocalSettings;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 500));



            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);


            ///start with no collection
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["CurrentCollection"] = string.Empty;

        }


 
    

 

        private void SettingsButton(object sender, RoutedEventArgs e)
        {
 
                Frame.Navigate(typeof(Settings_page));

      
        
        }

        private void TSearchDB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Database_stack.Database_stackContext.updateStack(TSearchDB.Text);
        }
    }
}