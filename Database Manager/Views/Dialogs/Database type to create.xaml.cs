using Database_Manager.Views.Dialogs.Server_creation;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Dialogs
{
    public sealed partial class Database_type_to_create : UserControl
    {

        public static Database_type_to_create _Type_To_CreateContext { get; set; } 
        public Database_type_to_create()
        {



            InitializeComponent();

            _Type_To_CreateContext = this;

            //// add click to mongodb button
            CMongoDB_Button.AddHandler(PointerPressedEvent,
new PointerEventHandler(BMongoDB_Create), true);


        }

        private void BMongoDB_Create(object sender, PointerRoutedEventArgs e)
        {


            this.Content = new MongoDB_Server();

        }

        public static implicit operator Database_type_to_create(Settings_page v)
        {
            throw new NotImplementedException();
        }
    }
}
