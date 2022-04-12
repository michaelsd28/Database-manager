﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components
{
    public sealed partial class IconButton : UserControl
    {



        public string ImageHeight
        {
            get { return (string)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(string), typeof(IconButton), new PropertyMetadata("20"));



        public string ForegroundText
        {
            get { return (string)GetValue(ForegroundTextProperty); }
            set { SetValue(ForegroundTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundTextProperty =
            DependencyProperty.Register("ForegroundText", typeof(string), typeof(IconButton), new PropertyMetadata(0));




        public string ImageIcon
        {
            get { return (string)GetValue(ImageIconProperty); }
            set { SetValue(ImageIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageIconProperty =
            DependencyProperty.Register("ImageIcon", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));




        public string BFontSize
        {
            get { return (string)GetValue(BFontSizeProperty); }
            set { SetValue(BFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BFontSizeProperty =
            DependencyProperty.Register("BFontSize", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));



        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));



        public string ButtonBackground
        {
            get { return (string)GetValue(ButtonBackgroundProperty); }
            set { SetValue(ButtonBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonBackgroundProperty =
            DependencyProperty.Register("ButtonBackground", typeof(string), typeof(IconButton), new PropertyMetadata("#FF252525"));





        public string ButtonPadding
        {
            get { return (string)GetValue(ButtonPaddingProperty); }
            set { SetValue(ButtonPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPaddingProperty =
            DependencyProperty.Register("ButtonPadding", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));



        public string ImageMargin
        {
            get { return (string)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));



        public IconButton()
        {
            this.InitializeComponent();
        }
    }
}
