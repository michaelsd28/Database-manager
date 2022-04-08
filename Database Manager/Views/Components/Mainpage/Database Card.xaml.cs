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

namespace Database_Manager.Resource_dictionaries.Main_page
{
    public sealed partial class Database_Card : UserControl
    {


        public string TSize
        {
            get { return (string)GetValue(TSizeProperty); }
            set { SetValue(TSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TSizeProperty =
            DependencyProperty.Register("Sizeize", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));



        public string TDocument
        {
            get { return (string)GetValue(TDocumentProperty); }
            set { SetValue(TDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TDocumentProperty =
            DependencyProperty.Register("TDocument", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));



        public string TAverageSize
        {
            get { return (string)GetValue(TAverageSizeProperty); }
            set { SetValue(TAverageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TAverageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TAverageSizeProperty =
            DependencyProperty.Register("TAverageSize", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));




        public string TLastUpdated
        {
            get { return (string)GetValue(TLastUpdatedProperty); }
            set { SetValue(TLastUpdatedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TLastUpdated.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TLastUpdatedProperty =
            DependencyProperty.Register("TLastUpdated", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));



        public string TTitle
        {
            get { return (string)GetValue(TTitleProperty); }
            set { SetValue(TTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TTitleProperty =
            DependencyProperty.Register("TTitle", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));




        public string CTitleBackgroundColor
        {
            get { return (string)GetValue(CTitleBackgroundColorProperty); }
            set { SetValue(CTitleBackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CTitleBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CTitleBackgroundColorProperty =
            DependencyProperty.Register("CTitleBackgroundColor", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));



        public string ImageBadgeSRC
        {
            get { return (string)GetValue(ImageBadgeSRCProperty); }
            set { SetValue(ImageBadgeSRCProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageBadgeSRC.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageBadgeSRCProperty =
            DependencyProperty.Register("ImageBadgeSRC", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));




        public string DetailFontSize
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(string), typeof(Database_Card), new PropertyMetadata("16"));






        public Database_Card()
        {
            this.InitializeComponent();
        }
    }
}
