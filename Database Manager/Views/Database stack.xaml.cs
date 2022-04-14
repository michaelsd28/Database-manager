using Database_Manager.Views.Managers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views
{
    public sealed partial class Database_stack : UserControl
    {
        public Database_stack()
        {
            InitializeComponent();
            MongoDB_Card.AddHandler(PointerPressedEvent,new PointerEventHandler(MongoDBCard_Click),true);
        }

        private void MongoDBCard_Click(object sender, PointerRoutedEventArgs e)
        {
      

       
           
            MainPage.MainPageContext.Frame.Navigate(typeof(MongoDB_Manager));
            MongoDB_Manager.MongoDB_ManagerContext.LeftPopUp.IsOpen = false;

        }
    }
}
