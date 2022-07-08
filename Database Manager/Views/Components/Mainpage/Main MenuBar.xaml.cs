using Database_Manager.Services;
using Database_Manager.Services.Redis;
using Database_Manager.Views.Components.Managers.Redis;
using Database_Manager.Views.Dialogs;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;



// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Mainpage
{
    public sealed partial class Main_MenuBar : UserControl
    {
        public Main_MenuBar()
        {
            InitializeComponent();

        

            RefreshButton.AddHandler(PointerPressedEvent,
            new PointerEventHandler(Refresh_PointerPressed), true);


            TerminalButton.AddHandler(PointerPressedEvent,
               new PointerEventHandler(Terminal_PointerPressed), true);
        }





        private void BCreateDB_Click(object sender, RoutedEventArgs e)
        {


            Database_type_to_create._Type_To_CreateContext.Content = new Database_type_to_create();

            if (MainPage.MainPageContext.Popup_Create.IsOpen)
            {

                MainPage.MainPageContext.Popup_Create.IsOpen = false;

            }
            else
            {

                MainPage.MainPageContext.Popup_Create.IsOpen = true;
            }

        }


        private void BConnectDB_Click(object sender, RoutedEventArgs e)
        {

            Database_type_to_create._Type_To_CreateContext.Content = new Database_type_to_create();

            if (MainPage.MainPageContext.Popup_ConnectURI.IsOpen)
            {
                MainPage.MainPageContext.Popup_ConnectURI.IsOpen = false;
            }
            else
            {
                MainPage.MainPageContext.Popup_ConnectURI.IsOpen = true;
            }


        }



        private void Refresh_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try {


                Database_stack.Database_stackContext.updateStack();

               


            } catch(Exception ex) {

                _ = new DialogService()._DialogService("Redis server was not found",ex.Message);
                return;
            
            }

}

private void Terminal_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
          
        }
    }
}
