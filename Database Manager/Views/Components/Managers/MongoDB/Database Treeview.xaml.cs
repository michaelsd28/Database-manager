using Database_Manager.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Database_Treeview : UserControl
    {



        public string myTextHeader
        {
            get { return (string)GetValue(myTextHeaderProperty); }
            set { SetValue(myTextHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for myTextHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myTextHeaderProperty =
            DependencyProperty.Register("myTextHeader", typeof(string), typeof(Database_Treeview), new PropertyMetadata(string.Empty));




        public string CollectionText
        {
            get { return (string)GetValue(CollectionTextProperty); }
            set { SetValue(CollectionTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionTextProperty =
            DependencyProperty.Register("CollectionText", typeof(string), typeof(Database_Treeview), new PropertyMetadata(string.Empty));



        public static string HdabaseName { get; set; }
        public static List<string> ClistCollection { get; set; }



        public Database_Treeview(string headerName = null, List<string> listCollection = null)
        {
            InitializeComponent();

            HdabaseName = headerName;
            ClistCollection = listCollection;

            myTextHeader = headerName;


            foreach (var item in listCollection)
            {

                ContentExpander.Children.Add(GetCollecButton(item));
            }
        }

        private void collectionButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            try
            {
                PageController_Restart();

                string collectName = (string)sender.ToBsonDocument()["ButtonText"];
                List<object> dotNetObjList = new List<object>();
                dotNetObjList = new MongoDB_DatabaseService().GetDocumentList(myTextHeader, collectName, 10);


                var localSettings = ApplicationData.Current.LocalSettings;
                // Create a simple setting.
                localSettings.Values["CurrentCollection"] = collectName;
                localSettings.Values["CurrentDB"] = myTextHeader;



                //// clear stack 
                Documents_Container
                     .Documents_ContainerContext
                     .DocumentContainer_StackPanel
                     .Children.Clear();


                //// if error occurs
                if (dotNetObjList != null && dotNetObjList.Count < 1)
                {

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "Please select a table/collection";
                    textBlock.FontSize = 24;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Padding = new Thickness(30);


                    Documents_Container
                    .Documents_ContainerContext
                    .DocumentContainer_StackPanel
                    .Children
                    .Add(textBlock);

                    return;

                }



                /// add documents
                if (dotNetObjList != null)
                {
                    new MongoDB_DatabaseService().UpdateDocumentList();
                }

            }
            catch (Exception ex)
            {

                Documents_Container
                  .Documents_ContainerContext
                  .DocumentContainer_StackPanel
                  .Children.Clear();


                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"Error: {ex.ToString()}";
                textBlock.FontSize = 24;
                textBlock.Padding = new Thickness(30);

                Documents_Container
                 .Documents_ContainerContext
                 .DocumentContainer_StackPanel
                 .Children.Add(new TextBlock() { Text = ex.Message });


       

            }

        }




        public IconButton GetDeleteButton(string CollectName)
        {

            IconButton DeleteButton = new IconButton
            {

                ImageIcon = @"/Assets\Managers\MongoDB\minus icon.png",
                Margin = new Thickness(15, 15, 0, 15),
                ImageHeight = "8",
                ButtonPadding = "5,2,5,3",
                ButtonText = CollectName,
                MaxWidth = 20

            };



            DeleteButton.PointerPressed += DeleteCollection_PointerPressed;
            DeleteButton.AddHandler(PointerPressedEvent,
            new PointerEventHandler(DeleteCollection_PointerPressed), true);


            DeleteButton.HorizontalAlignment = HorizontalAlignment.Right;
            DeleteButton.HorizontalContentAlignment = HorizontalAlignment.Right;

            return DeleteButton;
        }

        public StackPanel GetCollecButton(string buttonText = null)
        {

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;



            IconButton CollectIcon_Button = new IconButton
            {
                MaxWidth = 150,
                ButtonText = buttonText,
                ButtonPadding = "7",
                BFontSize = "14",
                ButtonBackground = "#171717",
                ImageMargin = "0,0,6,0",
                ImageIcon = @"/Assets\Managers\MongoDB\table badge.png",


            };

            CollectIcon_Button.PointerPressed += collectionButton_PointerPressed;
            CollectIcon_Button.AddHandler(PointerPressedEvent,
            new PointerEventHandler(collectionButton_PointerPressed), true);


            stackPanel.Children.Add(CollectIcon_Button);
            stackPanel.Children.Add(GetDeleteButton(buttonText));

            return stackPanel;


        }




        public void PageController_Restart()
        {


            Page_Controller.pagaFrom = 0;
            Page_Controller.pagaTo = 10;

        }



        private async void DeleteCollection_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            string collectName = (string)sender.ToBsonDocument()["ButtonText"];

            /*  string title,
              string content,
               TypedEventHandler< ContentDialog, ContentDialogButtonClickEventArgs > PrimaryButton
            */


            await new DialogService()._DialogService("Do you want to delete " + collectName, "Press ok to delete", DeleteCollection);

        }

        private void DeleteCollection(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {


            string dialogTitle = (string)sender.ToBsonDocument()["Title"];
            string collectName = dialogTitle.Replace("Do you want to delete ", "");



            new MongoDB_DatabaseService().DropCollectionWithName(collectName, myTextHeader);
            ContentExpander.Children.Clear();

            new MongoDB_DatabaseService()
                .GetCollections_WithDB(myTextHeader)
                .ForEachAsync(collection =>
                {

                    ContentExpander.Children.Add(GetCollecButton(collection));

                });



        }
    }
}
