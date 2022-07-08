using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components
{
    public sealed partial class Expander_Content : UserControl
    {



        public string CollectionText
        {
            get { return (string)GetValue(CollectionTextProperty); }
            set { SetValue(CollectionTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionTextProperty =
            DependencyProperty.Register("CollectionText", typeof(string), typeof(Expander_Content), new PropertyMetadata(string.Empty));






        public Expander_Content()
        {
            this.InitializeComponent();



        }

        private void CollectionButton_Method(object sender, PointerRoutedEventArgs e)
        {



        }
    }
}
