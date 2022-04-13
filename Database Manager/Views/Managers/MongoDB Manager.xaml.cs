using System;
using Windows.ApplicationModel.Core;
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
            SizeChanged += MongoDB_Manager_SizeChanged;


            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);

        }

        private void MongoDB_Manager_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Get the current Windows Size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;

            double width = bounds.Width;



            if (width < 755)
            {
              LeftPopUp.IsOpen = false;
            }
            else {

               LeftPopUp.IsOpen = true;
            }
            LeftBar_GridContainer.Height = height-230;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            
        }

        private void ImportButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TreeViewPage));
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

              LeftPopUp.IsOpen = false;
            Frame.Navigate(typeof(MainPage));

        }
    }
}
