using Database_Manager.Services;
using Database_Manager.Views.Managers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Import_database : UserControl
    {
        public Import_database()
        {
            this.InitializeComponent();
        }

        private async void ImportButton(object sender, RoutedEventArgs e)
        {
            if (TNameDB.Text == String.Empty || TCollectionName.Text == String.Empty)
            {

                await new DialogService()._DialogService("Please enter data", "Press ok to continue");

                return;
            }

            new MongoDB_DatabaseService().ImportCollection(TNameDB.Text, TCollectionName.Text);
            MongoDB_Manager.MongoDB_ManagerContext.ImportDB_POPUP.IsOpen = false;
        }


        private void CancelButton(object sender, RoutedEventArgs e)
          =>  MongoDB_Manager.MongoDB_ManagerContext.ImportDB_POPUP.IsOpen = false;
        
    }
}
