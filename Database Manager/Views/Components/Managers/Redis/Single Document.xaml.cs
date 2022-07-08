using Database_Manager.Services;
using Database_Manager.Services.Redis;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.Redis
{
    public sealed partial class Single_Document : UserControl
    {



        public string KeyText
        {
            get { return (string)GetValue(KeyTextProperty); }
            set { SetValue(KeyTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyTextProperty =
            DependencyProperty.Register("KeyText", typeof(string), typeof(Single_Document), new PropertyMetadata(string.Empty));


        ///Assets/Managers/Redis/Data types/string icon.png"

        public string KeyType
        {
            get { return (string)GetValue(KeyTypeProperty); }
            set { SetValue(KeyTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyTypeProperty =
            DependencyProperty.Register("KeyType", typeof(string), typeof(Single_Document), new PropertyMetadata(string.Empty));



        public string ImageKey
        {
            get { return (string)GetValue(ImageKeyProperty); }
            set { SetValue(ImageKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageKeyProperty =
            DependencyProperty.Register("ImageKey", typeof(string), typeof(Single_Document), new PropertyMetadata("/Assets/Managers/Redis/Data types/string icon.png"));



        public static Single_Document RedisContextSingle_Document { get; set; }


        public Single_Document(string redisKeyType= null)
        {

            RedisContextSingle_Document = this;
            InitializeComponent();
      

           CheckKeyType(redisKeyType);

        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {

           string KeyText =  new RedisLocalSettings().GetCurrentKey();

            _ = new DialogService()._DialogService($"Do you want to delete *{KeyText}* ?", "Press 'sure' to continue", DeleteKey_Service);
        }

        private void DeleteKey_Service(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string KeyText = new RedisLocalSettings().GetCurrentKey();
            new RedisDB_Services().DelKey(KeyText);
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            
            string value = SingleTextBox.Text;
            string KeyText = new RedisLocalSettings().GetCurrentKey();


            new RedisDB_Services().SaveKeyValue(KeyText, value);

            _ = new DialogService()._DialogService($"Succefully saved to {KeyText}", "Press sure to continue");

        }

        private void BEdit_click(object sender, RoutedEventArgs e)
        {
       
        }


        public void CheckKeyType(string keyType) {

            var hashIcon = "/Assets/Managers/Redis/Data types/hash icon.png";
            var stringIcon = "/Assets/Managers/Redis/redis-key.png";
            var listIcon = "/Assets/Managers/Redis/Data types/list icon.png";
            var setIcon = "/Assets/Managers/Redis/Data types/sets icon.png";
            var sortedSetIcon = "/Assets/Managers/Redis/Data types/sorted set icon.png";


            switch (keyType)
            {
                case "String":
                    ImageKey = stringIcon;
                    break;

                case "Hash":
                    ImageKey = hashIcon;
                    break;
                case "List":
                    ImageKey = listIcon;
                    break;

                case "Set":
                    ImageKey = setIcon;
                    break;

                case "SortedSet":
                    ImageKey = sortedSetIcon;
                    break;

                default:
                    ImageKey = stringIcon;
                    break;
            }
        
        
        
        }



    }
}
