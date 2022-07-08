using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Database_Manager.Services;
using Database_Manager.Services.Redis;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Diagnostics;
using MongoDB.Bson;
using System;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis
{
    public sealed partial class Add_ValuePair : UserControl
    {

        public static Add_ValuePair valuePairContext { set; get; }
        public Add_ValuePair()
        {
            valuePairContext = this;
            InitializeComponent();

            ToggleButtonColors();

            new RedisLocalSettings().AddValue_ButtonType(null);
        }




        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Redis_Manager.Redis_ManagerContext.AddKey_Grid.Visibility = Visibility.Collapsed;
            ClearTextBoxes();
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {


            string value = "";
            Value_TextBox.Document.GetText(Windows.UI.Text.TextGetOptions.None, out value);

            

            new RedisDB_Services().SaveKeyValue(KeyValue_TextBox.Text, value, typeToAdd: new RedisLocalSettings().AddValue_ButtonType());

            Redis_Manager.Redis_ManagerContext.AddKey_Grid.Visibility = Visibility.Collapsed;
            ClearTextBoxes();

        }


        public void ClearTextBoxes()
        {

            KeyValue_TextBox.Text = "";
            Value_TextBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");

        }



        public void ToggleButtonColors()
        {

            foreach (Button child in ButtonType_StackPanel.Children)
            {

                child.Click += SetCurrentButton;

            }

        }



        private void SetCurrentButton(object sender, RoutedEventArgs e)
        {


            var contentText = sender.ToBsonDocument()["Content"].ToString();
            string selectedButton = new RedisLocalSettings().AddValue_ButtonType(contentText);



            foreach (Button child in ButtonType_StackPanel.Children)
            {

                string CurrentChild = child.Content.ToString();

                if (CurrentChild == selectedButton)
                {
                    child.Background = new SolidColorBrush(Color.FromArgb(255, 51, 14, 14));
                }
                else
                {
                    child.Background = new SolidColorBrush(Colors.Black);
                }



            }



        }
    }
}
