using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Managers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MongoDB_Manager : Page
    {
        public MongoDB_Manager()
        {
            InitializeComponent();

   
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
 
            Frame.Navigate(typeof(MainPage));
        }

        private void ImportButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TreeViewPage));
        }


    }
}
