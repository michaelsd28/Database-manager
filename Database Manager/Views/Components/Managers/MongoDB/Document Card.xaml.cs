using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Document_Card : UserControl
    {
        public Document_Card()
        {
            this.InitializeComponent();
            TextboxDocumentCard.Text = @" {
    ""name"": ""Tokyo"",
    ""country"": ""Japan"",
    ""continent"": ""Asia"",
    ""population"": 37.400
 }";
        }
    }
}
