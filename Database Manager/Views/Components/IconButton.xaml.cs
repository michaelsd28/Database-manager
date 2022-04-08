using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components
{
    public sealed partial class IconButton : UserControl
    {



        public string ImageIcon
        {
            get { return (string)GetValue(ImageIconProperty); }
            set { SetValue(ImageIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageIconProperty =
            DependencyProperty.Register("ImageIcon", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));




        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(IconButton), new PropertyMetadata("Button text"));



        public IconButton()
        {
            this.InitializeComponent();
        }
    }
}
