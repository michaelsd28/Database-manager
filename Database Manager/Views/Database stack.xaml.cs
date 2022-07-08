using Database_Manager.Model;
using Database_Manager.Resource_dictionaries.Main_page;
using Database_Manager.Services;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views
{
    public sealed partial class Database_stack : UserControl
    {

        public static Database_stack Database_stackContext { get; set; }
        public Database_stack()
        {
            InitializeComponent();
            Database_stackContext = this;
            GridViewStack.ItemClick += ClickedONItem;
            updateStack();


        }






        public void updateStack(string filterText = "")
        {



            GridViewStack.Items.Clear();
 



            List<DatabaseCard> databaseCards = new DatabaseStack_Service().GetCards();


            databaseCards = databaseCards.FindAll(x => x.Name.Contains(filterText));

            foreach (var item in DatabaseStackRoot.Children)
            {

                if (item.ToString() == "Windows.UI.Xaml.Controls.TextBlock")
                {
                    DatabaseStackRoot.Children.Remove((TextBlock)item);
                }
            }



            if (databaseCards.Count < 1)
            {

          
                GridViewStack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;



                DatabaseStackRoot.Children.Add(PleaseAddCard_TextBlock());


                return;
            }

            DatabaseStackRoot.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;


            GridViewStack.Visibility = Windows.UI.Xaml.Visibility.Visible;


           

            databaseCards.ForEach(card =>
            {


                string icon = string.Empty;

                if (card.Status == "Stopped")
                {
                    icon = "/Assets/Mainpage/run database icon.png";
                }
                else
                {
                    icon = "/Assets/Mainpage/stop icon badge.png";
                }


                switch (card.Type)
                {

                    case "MongoDB":

                        GridViewStack.Items.Add(new Database_Card
                        {
                            ImageBadgeSRC = "/Assets/Mainpage/MongoDB Badge.png",
                            CTitleBackgroundColor = "#0F4F00",
                            TTitle = card.Name,
                            TSize = "Storage size: 200 Megabytes",
                            TDocument = "Documents: 6969 Docs",
                            TextURI = "URI: " + card.URI,
                            TLastUpdated = "Last updated: 1 min  ago",
                            RunDB_Icon = icon
                        });
                        break;

                    case "Redis":

                        GridViewStack.Items.Add(new Database_Card
                        {
                            ImageBadgeSRC = "/Assets/Mainpage/redis logo badge.png",
                            CTitleBackgroundColor = "#4D0000",
                            TTitle = card.Name,
                            TSize = "Storage size: 500 Megabytes",
                            TDocument = "Documents: 5470 Docs",
                            TextURI = "URI: " + card.URI,
                            TLastUpdated = "Last updated: 5 min  ago",
                            RunDB_Icon = icon
                        });
                        break;

                    case "SQL Server":

                        GridViewStack.Items.Add(new Database_Card
                        {
                            ImageBadgeSRC = "/Assets/Mainpage/sql logo badge.png",
                            CTitleBackgroundColor = "#2E2B00",
                            TTitle = card.Name,
                            TSize = "Storage size: 500 Megabytes",
                            TDocument = "Documents: 5470 Docs",
                            TextURI = "URI: " + card.URI,
                            TLastUpdated = "Last updated: 5 min  ago",
                            RunDB_Icon = icon
                        });
                        break;

                    default:
                        break;

                }
            });




        }



        private TextBlock PleaseAddCard_TextBlock() {

            TextBlock textBlock = new TextBlock();
            DatabaseStackRoot.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            textBlock.Margin = new Windows.UI.Xaml.Thickness(80);
            textBlock.FontSize = 20;
            textBlock.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            textBlock.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            textBlock.Margin = new Windows.UI.Xaml.Thickness(60);
            textBlock.Text = "Please add a card";
            textBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;

            return textBlock;

        }

        private void ClickedONItem(object sender, ItemClickEventArgs e)
        {

       



        }
    }
}
