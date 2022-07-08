using Database_Manager.Services;
using Database_Manager.Services.Redis;
using MongoDB.Bson;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Database_Manager.Views.Components.Managers.Redis.tree
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Database_Cloud_Tree : Page
    {










        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(Database_Cloud_Tree), new PropertyMetadata(string.Empty));





        public Database_Cloud_Tree(string HeadText = "")
        {
            this.InitializeComponent();


            string SettingsString = "";


            HeaderText = HeadText;

            LoadKeys(SettingsString);
        }

        public void LoadKeys(string SettingsDB, string keyToSearch = "")
        {
            try
            {


                var settingSTR = "abortConnect=true,connectRetry=1,connectTimeout=500,allowAdmin=true";




                var RedisURI = new RedisLocalSettings().RedisURI();

                var compleURI = RedisURI + "," + settingSTR;


                var indexServer = compleURI.ToString().IndexOf(",");

                var serverSTR = compleURI.ToString().Substring(0, indexServer);



                var redis = ConnectionMultiplexer.Connect(compleURI);


                var ListKeys = redis.GetServer(serverSTR).Keys();

                var db = redis.GetDatabase();




                //            "name=michael,password=ZvXL6R0OCTdOuvl3BPuLzeKugnVFCvRY,allowAdmin=true";
                //redis-19535.c89.us-east-1-3.ec2.cloud.redislabs.com:19535









                foreach (var key in ListKeys)
                {


                    if (key.ToString().Contains(keyToSearch))
                    {

                        var keyType = db.KeyType(key.ToString()).ToString();

           

                        var myTreeKey = new Tree_Key(keyType) { KeyText = key };


                        myTreeKey.AddHandler(PointerPressedEvent,
                            new PointerEventHandler(DisplayKeyValue_Handler), true);


                        RedisCloudListBox.Items.Add(myTreeKey);
                    }


            

                }

    


            }

            catch (Exception ex)
            {
                _ = new DialogService()._DialogService("Unable to connect to server * cloud", ex.Message, GoBack);
            }


        }

        private void GoBack(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        =>    MainPage.MainPageContext.Frame.GoBack();
        

        private void DisplayKeyValue_Handler(object sender, PointerRoutedEventArgs e)
        {

            string KeyText = sender.ToBsonDocument()["KeyText"].ToString();
     

            new RedisDB_Services().DisplayKeyValue(KeyText, 0);
      
        }

        private void BAddKey(object sender, RoutedEventArgs e)

        {
            new RedisLocalSettings().UpdateDBNumber("0");
            Redis_Manager.Redis_ManagerContext.AddKey_Grid.Visibility = Visibility.Visible;
        }

        
    }
}
