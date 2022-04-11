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
