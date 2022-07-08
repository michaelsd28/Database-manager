using Database_Manager.Services;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Single_Document : UserControl
    {
        public static Single_Document single_DocumentContext { get; set; }
        public static string CurrentDocument { get; set; }
        public Single_Document()
        {
            InitializeComponent();

            single_DocumentContext = this;



        }

        private void SaveDocument_Button(object sender, RoutedEventArgs e)
        {

            string newDocument = SingleTextBox.Text;

            new MongoDB_DatabaseService().updateDocument(CurrentDocument, newDocument);
            SingleTextBox.IsEnabled = false;



        }

        private void EditDocument_Button(object sender, RoutedEventArgs e)
        {
            CurrentDocument = SingleTextBox.Text;
  
            SingleTextBox.IsEnabled = true;
        }

        private async void DeleteDocument_Button(object sender, RoutedEventArgs e)
        {
            await new DialogService()._DialogService("Do you want to delete this document?", "press ok to delete", DeleteThisDocuement);
        }

        private void DeleteThisDocuement(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            new MongoDB_DatabaseService().DeleteDocumentW_ID(SingleTextBox.Text);
        }
    }
}
