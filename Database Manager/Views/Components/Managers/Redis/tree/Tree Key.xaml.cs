using Database_Manager.Services;
using Database_Manager.Services.Redis;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis.tree
{
    public sealed partial class Tree_Key : UserControl
    {





        public string ImageKeySRC
        {
            get { return (string)GetValue(ImageKeySRCProperty); }
            set { SetValue(ImageKeySRCProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageKeySRC.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageKeySRCProperty =
            DependencyProperty.Register("ImageKeySRC", typeof(string), typeof(Tree_Key), new PropertyMetadata("/Assets/Managers/Redis/redis-key.png"));








        public string KeyText
        {
            get { return (string)GetValue(KeyTextProperty); }
            set { SetValue(KeyTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyTextProperty =
            DependencyProperty.Register("KeyText", typeof(string), typeof(Tree_Key), new PropertyMetadata(string.Empty));

       
        public Tree_Key(string KeyType = null)
        {
            InitializeComponent();



           

            TreeKey_root.AddHandler(PointerPressedEvent,
                new PointerEventHandler(SaveCurrentKey),true);

            CheckImageType(KeyType);


        }

        private void SaveCurrentKey(object sender, PointerRoutedEventArgs e)
        {
          string keyToSave =  sender.ToBsonDocument()["KeyText"].ToString();

            new RedisLocalSettings().UpdateCurrentKey(keyToSave);

  

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
           _ = new DialogService()._DialogService($"Do you want to delete *{KeyText}* ?", "Press 'sure' to continue", DeleteKey_Service);

        }

        private void DeleteKey_Service(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        
         =>   new RedisDB_Services().DelKey(KeyText);



        private void CheckImageType(string KeyType) {

        
            var hashIcon = "/Assets/Managers/Redis/Data types/hash icon.png";
            var stringIcon = "/Assets/Managers/Redis/redis-key.png";
            var listIcon = "/Assets/Managers/Redis/Data types/list icon.png";
            var setIcon = "/Assets/Managers/Redis/Data types/sets icon.png";
            var sortedSetIcon = "/Assets/Managers/Redis/Data types/sorted set icon.png";


       

            switch (KeyType) 
            {

                case "String":
                    ImageKeySRC = stringIcon;
                    break;

                case "Hash":
                    ImageKeySRC = hashIcon;
                    break;

                case "List":
                    ImageKeySRC = listIcon;
                    break;

                case "Set":
                    ImageKeySRC = setIcon;
                    break;

                case "SortedSet":
                    ImageKeySRC = sortedSetIcon;
                    break;

                    

                default:
                    ImageKeySRC = "/Assets/Managers/Redis/redis-key.png";
                    break;
            
            
            }



           
        }


    }
}
