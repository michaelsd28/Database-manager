using Database_Manager.Services;
using Database_Manager.Services.Redis;
using Database_Manager.Views.Components.Managers.Redis.tree;
using MongoDB.Bson;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
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


        public static Database_tree database_TreeContext { get; set; }
        public  Database_tree()
        {
            database_TreeContext = this;
            InitializeComponent();

        }





        public void DisplayKeyValue_Handler(object sender, PointerRoutedEventArgs e)
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
        //   LoadKeys(dbNumber_String,SearchKey_TextBox.Text);

        
        }
    }
}
