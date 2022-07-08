using Database_Manager.Services;
using Database_Manager.Services.Redis;
using Database_Manager.Views.Components.Managers.Redis.tree;
using FreeRedis;
using MongoDB.Bson;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis
{
    public sealed partial class Database_tree : UserControl
    {





        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(Database_tree), new PropertyMetadata(string.Empty));



        public Database_tree(string HeadText = null)
        {
            InitializeComponent();



            HeaderText = HeadText;

            string dbNumber_String = HeadText.Replace("My database ", "");

 
            LoadKeys(dbNumber_String);

    

        }



        public void LoadKeys(string HeaderNumber, string keyToSearch = "")
        {
            try {


                var settingSTR = "abortConnect=true,connectRetry=1,connectTimeout=500,allowAdmin=true";
                int dbNumber = int.Parse(HeaderNumber);



                var RedisURI = new RedisLocalSettings().RedisURI();

                var compleURI = RedisURI + "," + settingSTR;


                var indexServer = compleURI.ToString().IndexOf(",");

                var serverSTR = compleURI.ToString().Substring(0, indexServer);



                var redis = ConnectionMultiplexer.Connect(compleURI);


                var ListKeys = redis.GetServer(serverSTR).Keys(dbNumber);

                var db = redis.GetDatabase();




                RedisListBox.Items.Clear();


            bool isEmpty = true;

            foreach (var key in ListKeys)
            {


                    if (key.ToString().Contains(keyToSearch)) {

                        var keyType = db.KeyType(key.ToString()).ToString();

                    var myTreeKey = new Tree_Key(keyType) { KeyText = key};


                    myTreeKey.AddHandler(PointerPressedEvent,
                        new PointerEventHandler(DisplayKeyValue_Handler), true);


                    RedisListBox.Items.Add(myTreeKey);
                }


                isEmpty = false;

            }


            if (isEmpty) RedisExpander.IsExpanded = false;

            }
            catch (Exception ex) {


                _ = new DialogService()._DialogService("Unable to connect to server * local redis", ex.Message,GoBack);

  
            }

        }

        private void GoBack(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            MainPage.MainPageContext.Frame.GoBack();
        }

        private void DisplayKeyValue_Handler(object sender, PointerRoutedEventArgs e)
        {

            string KeyText = sender.ToBsonDocument()["KeyText"].ToString();
            int DBNumber = int.Parse(HeaderText.Replace("My database ", ""));

            new RedisDB_Services().DisplayKeyValue(KeyText, DBNumber);
            new RedisLocalSettings().UpdateDBNumber(HeaderText.Replace("My database ", ""));
        }

        private void AddKey_Button(object sender, RoutedEventArgs e)

           {
            string DBNumber = HeaderText.Replace("My database ", "");

            new RedisLocalSettings().UpdateDBNumber(DBNumber);
      
            
            Redis_Manager.Redis_ManagerContext.AddKey_Grid.Visibility = Visibility.Visible;
        }

    private void SearchKey_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

           string dbNumber_String = HeaderText.Replace("My database ", "");
           LoadKeys(dbNumber_String,SearchKey_TextBox.Text);

        
        }
    }
}
