using Database_Manager.Services;
using Database_Manager.Views.Managers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components
{
    public sealed partial class Expander_Header : UserControl
    {



        public string TextHeader
        {
            get { return (string)GetValue(TextHeaderProperty); }
            set { SetValue(TextHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHeaderProperty =
            DependencyProperty.Register("TextHeader", typeof(string), typeof(Expander_Header), new PropertyMetadata(string.Empty));


        public Expander_Header()
        {
            this.InitializeComponent();

            DeleteButton.AddHandler(PointerPressedEvent,
            new PointerEventHandler(DeleteDatabase_PointerPressed), true);
        }

        private async void DeleteDatabase_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            await new DialogService()._DialogService($"Do you want to delete {TextHeader}?", "press ok to delete", DeleteDatabaseMethod);
        }

        private void DeleteDatabaseMethod(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            new MongoDB_DatabaseService().DeleteDatabase(TextHeader);

            MongoDB_Manager.MongoDB_ManagerContext.DatabaseStackPanel.Children.Clear();

            MongoDB_Manager.MongoDB_ManagerContext.DatabaseStackPanel.Children.Add(new MongoDB_DatabaseService().GetDatabase_StackPanel());



        }








    }
}
