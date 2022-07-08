using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Dialogs.Server_creation
{
    public sealed partial class MongoDB_Server : UserControl
    {


        public MongoDB_Server()
        {

            InitializeComponent();

        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            //MainPage.MainPageContext.Popup_Create.IsOpen = false;
        }
    }
}
