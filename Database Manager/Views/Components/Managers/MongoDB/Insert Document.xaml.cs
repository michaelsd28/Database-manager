using Database_Manager.Services;
using Database_Manager.Views.Managers;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.MongoDB
{
    public sealed partial class Insert_Document : UserControl
    {
        public static BsonDocument BsonTemplate { get; set; }
        public Insert_Document()
        {
            this.InitializeComponent();


            OnLoadTextBox();

        }



        public void OnLoadTextBox()
        {

            BsonTemplate = new BsonDocument();
            ObjectId objID = ObjectId.GenerateNewId();
            BsonTemplate.Add("_id", objID);

            string formatedJSON = JsonConvert.SerializeObject(BsonTemplate.ToJson(), Formatting.Indented);

            MyTextDocument.Document.SetText(Windows.UI.Text.TextSetOptions.None, BsonTemplate.ToJson());

        }





        private void CancelButton(object sender, RoutedEventArgs e)
        {

            MongoDB_Manager.MongoDB_ManagerContext.InsertDocument_POPUP.Visibility = Visibility.Collapsed;

            OnLoadTextBox();

        }

        private async void InsertDocument_Button(object sender, RoutedEventArgs e)
        {

            try
            {

       
                MyTextDocument.Document
                    .GetText(Windows.UI.Text.TextGetOptions.None, out string json);

                new MongoDB_DatabaseService().InsertDocument(BsonDocument.Parse(json));
                MongoDB_Manager.MongoDB_ManagerContext.InsertDocument_POPUP.Visibility = Visibility.Collapsed;


                OnLoadTextBox();

            }
            catch (Exception ex)
            {

                await new DialogService()._DialogService("Error inserting ", ex.Message);

            }

        }
    }
}
