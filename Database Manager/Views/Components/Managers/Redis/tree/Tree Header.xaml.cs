using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis
{
    public sealed partial class Tree_Header : UserControl
    {



        public string TextHeader
        {
            get { return (string)GetValue(TextHeaderProperty); }
            set { SetValue(TextHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHeaderProperty =
            DependencyProperty.Register("TextHeader", typeof(string), typeof(Tree_Header), new PropertyMetadata(string.Empty));


        public Tree_Header()
        {
            this.InitializeComponent();
        }
    }
}
