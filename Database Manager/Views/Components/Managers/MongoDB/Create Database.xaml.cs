using Database_Manager.Services;
using Database_Manager.Views.Managers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Create_Database : UserControl
    {
        public Create_Database()
        {
            this.InitializeComponent();
        }

        private async void CreateButton(object sender, RoutedEventArgs e)
        {
            if (TNameDB.Text != "" && TCollectionName.Text != "")
            {
                new MongoDB_DatabaseService().CreateDB_AND_Collection(TNameDB.Text, TCollectionName.Text);
                new MongoDB_DatabaseService().UpdateDBLeftMenu();
                MongoDB_Manager.MongoDB_ManagerContext.CreateMyDB_POPUP.IsOpen = false;
                return;
            }

            await new DialogService()._DialogService("Enter data to create dabase", "Press ok to continue");




        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            MongoDB_Manager.MongoDB_ManagerContext.CreateMyDB_POPUP.IsOpen = false;
        }
    }
}
