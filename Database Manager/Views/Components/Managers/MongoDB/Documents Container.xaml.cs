using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Documents_Container : UserControl
    {

        public static Documents_Container Documents_ContainerContext { set; get; }
        public Documents_Container()
        {
            InitializeComponent();
            Documents_ContainerContext = this;

            if (DocumentContainer_StackPanel.Children.Count < 1)
            {

                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Please select a table/collection";
                textBlock.FontSize = 24;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Padding = new Thickness(30);
                DocumentContainer_StackPanel.Children.Add(textBlock);

            }


        }

        public void AddDocument(Single_Document single_Document)
        {


            DocumentContainer_StackPanel
             .Children
             .Add(single_Document);

            if (DocumentContainer_StackPanel.Children.Count < 1)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Please select a table/collection";
                textBlock.FontSize = 24;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Padding = new Thickness(30);
                DocumentContainer_StackPanel.Children.Add(textBlock);
            }








        }


    }
}
