using System;
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
            this.SizeChanged += MongoDB_Manager_SizeChanged;

        }

        private void MongoDB_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Get the current Windows Size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;

            double width = bounds.Width;

            myTextBlock.Text = "Height: " +height +" Width: "+width;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
                LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));
        }

        private void ImportButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TreeViewPage));
        }

        private void TestButton(object sender, RoutedEventArgs e)
        {

            if (LeftPopUp.IsOpen)
            {
                LeftPopUp.IsOpen = false;
            }
            else {

                LeftPopUp.IsOpen = true;
            }

     
        }

   

        private void ExpandLeftBar(object sender, RoutedEventArgs e)
        {
            if (LeftPopUp.IsOpen)
            {
                LeftPopUp.IsOpen = false;
            }
            else
            {

                LeftPopUp.IsOpen = true;
            }

        }
    }
}
