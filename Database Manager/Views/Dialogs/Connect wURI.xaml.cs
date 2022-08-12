using Database_Manager.Model;
using Database_Manager.Services;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Dialogs
{
    public sealed partial class Connect_wURI : UserControl
    {
        public Connect_wURI()
        {
            InitializeComponent();


            loadTypeButtons();

        }


        string databaseType = string.Empty;
        BsonDocument CardSettings_Bson = new BsonDocument();

        private void CancelButton(object sender, RoutedEventArgs e)

        => MainPage.MainPageContext.Popup_ConnectURI.IsOpen = false;


        private void BCreate_Wuri(object sender, RoutedEventArgs e)
        {


        

            
            CardSettings_Bson["CardType"] = databaseType;
            CardSettings_Bson["URI_Link"] = TURI_Link.Text;
            CardSettings_Bson["CardName"] = CardName.Text;



            var newCard = new Helper().GetDatabaseCard(CardSettings_Bson);

            


            if (CardName.Text != "" || TURI_Link.Text != "" || databaseType != "")
            {

                if (!new DatabaseStack_Service().AddNewCard(newCard))
                {

                    MainPage.MainPageContext.Popup_ConnectURI.IsOpen = false;
                    Database_stack.Database_stackContext.updateStack();

                }
                else
                {

                    _ = new DialogService()._DialogService("It already exists", "Please enter another URI");

                }

            }
            else
            {

                _ = new DialogService()._DialogService("Pease enter data", "Some fields are missing please go back and complete");
            }



        }

        public void loadTypeButtons() {

            var myButtons = Stack_typeButtons.Children;


            foreach (Button button in myButtons)
            {
                button.Click += typeButton_OnClick;
            }

        }

        private void typeButton_OnClick(object sender, RoutedEventArgs e)
        {
        
            var buttonType = sender.ToBsonDocument()["Content"].ToString();
            databaseType = buttonType;
            new localSettings_Service().NewCardType(buttonType);

            var myButtons = Stack_typeButtons.Children;

            foreach (Button button in myButtons)
            {

                if (button.Content.ToString() == buttonType)
                {
                    button.Background = new localSettings_Service().ConvertColorFromHexString("#001333");
                }
                else {

                    button.Background = new SolidColorBrush(Colors.Black);

                }

            }


        }

        internal class Helper{

            public DatabaseCard GetDatabaseCard(BsonDocument CardSettings ) {


                string CardType = CardSettings["CardType"].ToString();
                string URI_Link = CardSettings["URI_Link"].ToString();
                string CardName = CardSettings["CardName"].ToString();




                DatabaseCard newCard = new DatabaseCard
                {

                    Id = 01,
                    Name = CardName,
                    URI = URI_Link,
                    Type = CardType,
                    Status = "Stopped",
                    StorageSize = "0 kb"

                };

                return newCard;


            }
        
        }
    }
}
