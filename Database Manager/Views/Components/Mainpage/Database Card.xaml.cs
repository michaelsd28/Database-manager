using Database_Manager.Model;
using Database_Manager.Services;
using Database_Manager.Services.Redis;
using Database_Manager.Views.Components.Managers.Redis;
using Database_Manager.Views.Managers;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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





        public string TextURI
        {
            get { return (string)GetValue(TextURIProperty); }
            set { SetValue(TextURIProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextURI.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextURIProperty =
            DependencyProperty.Register("TextURI", typeof(string), typeof(Database_Card), new PropertyMetadata(string.Empty));








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




        public string RunDB_Icon
        {
            get { return (string)GetValue(RunDB_IconProperty); }
            set { SetValue(RunDB_IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RunDB_Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunDB_IconProperty =
            DependencyProperty.Register("RunDB_Icon", typeof(string), typeof(Database_Card), new PropertyMetadata("/Assets/Mainpage/run database icon.png"));



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



            BDelete_Card.AddHandler(PointerPressedEvent,
            new PointerEventHandler(BDelete_Card_PointerPressed), true);




            CardRoot.AddHandler(PointerPressedEvent,
                new PointerEventHandler(CardRoot_PointerPressed), true);
        }



        private void BDelete_Card_PointerPressed(object sender, PointerRoutedEventArgs e)

        =>  _ = new DialogService()._DialogService("Do you want to delete this card?", TextURI, deleteTheCard).ConfigureAwait(false);



        




        private void deleteTheCard(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            string uriToDelete = TextURI.Replace("URI: ", "");

           

            DatabaseCard delCard = new DatabaseCard
            {

                Id = 01,
                Name = "new card",
                URI = uriToDelete,
                Type = new localSettings_Service().NewCardType(),
                Status = "Stopped",
                StorageSize = "0 kb"

            };

            new DatabaseStack_Service().RemoveCard(delCard);
        }



        private void CardRoot_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            try
            {

                var RedisURI = sender.ToBsonDocument()["TextURI"].ToString().Replace("URI: ", "");
                new RedisLocalSettings().RedisURI(RedisURI);


 


                var location = sender.ToBsonDocument()["ImageBadgeSRC"].ToString();


                whereTO(location, URI: RedisURI);

    

     



            }
            catch (Exception ex) {

                _ = new DialogService()._DialogService("Cannot navigate", ex.Message);
            }
        }

        private void BType_Click(object sender, RoutedEventArgs e)
        {
            string uriToDelete = TextURI.Replace("URI: ", "");

            DatabaseCard runCard = new DatabaseCard
            {

                Id = 01,
                Name = TTitle,
                URI = uriToDelete,
                Type = "MongoDB",
                Status = "Stopped",
                StorageSize = "0 kb"
            };

            new DatabaseStack_Service().RunCard(runCard);


        }



        public void whereTO(string location, string URI = "") {

            location = location.Replace("/Assets/Mainpage/", "");

            switch (location) 
            {
                case "MongoDB Badge.png":

                        MainPage.MainPageContext.Frame.Navigate(typeof(MongoDB_Manager), TextURI);

                   
             
                    break;

                case "redis logo badge.png":
                    goRedisManager(URI);
                    break;

                        case "sql logo badge.png":
                    break;



                default:
                    _ = new DialogService()._DialogService("Not a valid Database");
                    break;
                        
            
            }


        
        }



        private void goRedisManager(string URI) {

            try
            {

                var settingSTR = "abortConnect=true,connectRetry=1,connectTimeout=500";


                new RedisLocalSettings().RedisURI(URI);


                var completeURI = URI + "," + settingSTR;

      

                ConnectionMultiplexer.Connect($"{URI},{settingSTR}");
                MainPage.MainPageContext.Frame.Navigate(typeof(Redis_Manager), URI);


            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Redis server was not found", ex.Message);
                return;

            }

        }
    }
}
