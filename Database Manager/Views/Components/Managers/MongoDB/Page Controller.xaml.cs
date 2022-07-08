using Database_Manager.Services;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Page_Controller : UserControl
    {

        public static int pagaFrom { get; set; } = 0;
        public static int pagaTo { get; set; } = 10;

        public Page_Controller()
        {
            this.InitializeComponent();
            PreviusButton.AddHandler(
                PointerPressedEvent,
                new PointerEventHandler(PreviusButton_PointerPressed), true);


            NextButton.AddHandler(
                PointerPressedEvent, new PointerEventHandler(NextButton_PointerPressed), true);
        }




        private void PreviusButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pagaFrom -= 10;
            pagaTo -= 10;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private void Page1Button(object sender, RoutedEventArgs e)
        {
            pagaFrom = 0;
            pagaTo = 10;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

                      if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private void Page2Button(object sender, RoutedEventArgs e)
        {

            pagaFrom = 10;
            pagaTo = 20;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);



            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);

        }

        private void Page3Button(object sender, RoutedEventArgs e)
        {
            pagaFrom = 20;
            pagaTo = 30;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private void Page4Button(object sender, RoutedEventArgs e)
        {
            pagaFrom = 30;
            pagaTo = 40;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private  void Page5Button(object sender, RoutedEventArgs e)
        {
            pagaFrom = 40;
            pagaTo = 50;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }
            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private void NextButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            pagaFrom += 10;
            pagaTo += 10;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(pagaFrom, pagaTo);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }

            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
        }

        private void TGoToPage_TextChanged(object sender, TextChangedEventArgs e)
        {

            try {

                if (TGoToPage.Text =="") {
                    Page1Button(sender,e);
                    return;
                }

                int pageNumber = int.Parse(TGoToPage.Text)*10;
                int fromTOPAGE = pageNumber-10;
                int goTOPAGE = pageNumber;

            List<object> RangedList = new MongoDB_DatabaseService().GetDocList_WRange(fromTOPAGE, goTOPAGE);

            if (RangedList == null || RangedList.Count == 0)
            {

                _ = new DialogService()._DialogService("No more documents", "please try search");
                return;
            }

            new MongoDB_DatabaseService().UpdateDocumentList(RangedList);
            }
            catch (Exception ex) {

                _ = new DialogService()._DialogService("Invalid page",ex.Message);
            }
        }
    }
}
