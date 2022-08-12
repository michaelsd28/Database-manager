using Database_Manager.Services.MongoDB;
using Database_Manager.Views.Components.Managers.MongoDB;
using Database_Manager.Views.Managers;
using Microsoft.Toolkit.Uwp.UI.Controls;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Database_Manager.Services
{
    internal class MongoDB_DatabaseService
    {

        public async void ImportCollection(string NameDB, string CollectName)
        {


            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

            MongoClient dbClient = new MongoClient(dbURI.ToString());
            var database = dbClient.GetDatabase(NameDB);
            var collection = database.GetCollection<BsonDocument>(CollectName); // initialize to the collection to read from



            // Open a text file.
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".json");

            StorageFile file = await open.PickSingleFileAsync();


            if (file != null)
            {
                using (IRandomAccessStream randAccStream =
                    await file.OpenAsync(FileAccessMode.Read))
                {

                    string reader = new StreamReader(randAccStream.AsStream()).ReadToEnd();


                    List<object> DocumentsToSave = JsonConvert.DeserializeObject<List<object>>(reader);

                    DocumentsToSave.ForEach(document =>
                    {

                        string current = document.ToString();



                        collection.InsertOneAsync(BsonDocument.Parse(current));

                    });






                }
                UpdateDBLeftMenu();

            }




        }

        public async void ExportCollection()
        {

            try
            {


                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();



                MongoClient dbClient = new MongoClient(dbURI.ToString());
                var database = dbClient.GetDatabase(currentDB.ToString());
                ///   var collection = database.GetCollection<BsonDocument>(myCollect.ToString());


                string outputFileName = "myfile.txt";// initialize to the output file
                var collection = database.GetCollection<BsonDocument>(myCollect.ToString()); // initialize to the collection to read from

                List<object> myList = collection.Find(new BsonDocument())
                    .ToList().ConvertAll(BsonTypeMapper.MapToDotNetValue);





                string contentInFile = JsonConvert.SerializeObject(myList);


                var savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".json" });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = myCollect.ToString();


                StorageFile file = null;

                file = await savePicker.PickSaveFileAsync();

                if (file != null)
                {
                    // Prevent updates to the remote version of the file until
                    // we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file


                    await FileIO.WriteTextAsync(file, contentInFile);
                    // Let Windows know that we're finished changing the file so
                    // the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);


                    if (status == FileUpdateStatus.Complete)
                    {


                        await new DialogService()._DialogService("file was saved", "press ok to continue");


                        //    this.textBlock.Text = "File " + file.Name + " was saved.";
                    }
                    else
                    {
                        await new DialogService()._DialogService("couldn't be saved ", "press ok to continue");
                        //      this.textBlock.Text = "File " + file.Name + " couldn't be saved.";
                    }
                }
                else
                {
                    await new DialogService()._DialogService("Operation cancelled ", "press ok to continue");
                    //     this.textBlock.Text = "Operation cancelled.";
                }



            }
            catch (Exception ex)
            {

                await new DialogService()._DialogService("Operation cancelled ", ex.Message);


            }

        }



        /// <summary>
        /// Get the hole list
        /// </summary>
        public Task<List<object>>  GetDocList_WRange(int rangeFROM, int rangeTO)
        {

            try
            {
                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

                ///database client
                var dbClient = new MongoClient(dbURI.ToString());
                /// get database
                var database = dbClient.GetDatabase(currentDB);
                ///   get collection
                var collection = database.GetCollection<BsonDocument>(myCollect);

                ///get all 
                List<object> myList = collection
                        .Find(new BsonDocument())
                        .Skip(rangeFROM)
                        .Limit(rangeTO)
                        .ToList()
                        .ConvertAll(BsonTypeMapper.MapToDotNetValue);

                ///return new list with range
                return Task.FromResult(myList) ;



            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error getting range", ex.Message);

                return null;

            }


        }


        public Task UpdateDocumentList(List<object> DocumentList = null)
        {
            try {


                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString(); 



                var dotNetObjList = new MongoDB_DatabaseService().GetDocumentList(

                    currentDB,  /// my database
                    myCollect,  /// collection
                    10 ///amount of documents

                    );


                if (DocumentList != null)
                {
                    dotNetObjList = DocumentList;
                }

                /// clear list
                Documents_Container
                     .Documents_ContainerContext
                     .DocumentContainer_StackPanel
                     .Children.Clear();



                foreach (var document in dotNetObjList)
                {
                    string json = JsonConvert.SerializeObject(document, Formatting.Indented);

                    Single_Document currentDocument = new Single_Document();
                    currentDocument.SingleTextBox.Text = json;





                    Documents_Container
                        .Documents_ContainerContext
                        .DocumentContainer_StackPanel
                        .Children
                        .Add(currentDocument);

                }

            }
            catch (Exception ex) {

                _ = new DialogService()._DialogService("Error updating documents", ex.Message);

            }

            return Task.CompletedTask;
        }

        public StackPanel GetDatabase_StackPanel()
        {


            try
            {



                StackPanel stackPanel = new StackPanel();



                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();



                MongoClient dbClient = new MongoClient(dbURI.ToString());

                var dbList = dbClient.ListDatabases().ToList();
                List<string> dbNames = dbList.Select(db => (string)db["name"]).ToList();




                var listCollections = dbNames.Select(ccollection => dbClient.GetDatabase(ccollection)
                   .ListCollectionsAsync()
                   .Result.ToListAsync()
                   .Result);





                Dictionary<string, List<string>> databaseN_collection = new Dictionary<string, List<string>>();


                foreach (var db in dbNames)
                {

                    var myDB = dbClient.GetDatabase(db);
                    string cname = string.Empty;
                    List<string> collections = new List<string>();
                    foreach (var item in myDB.ListCollectionsAsync().Result.ToListAsync().Result)
                    {

                        cname = item["name"].ToString();
                        collections.Add(cname);

                    }
                    databaseN_collection.Add(db, collections);

                }



                foreach (KeyValuePair<string, List<string>> entry in databaseN_collection)
                {
                    Database_Treeview current = new Database_Treeview(entry.Key, entry.Value);

                    current.DBTextHeader = entry.Key;

                    stackPanel.Children.Add(current);
                }
                return stackPanel;

            }
            catch (Exception ex)
            {



                _ = new DialogService()._DialogService("Error trying to connect", ex.Message, GoBackError);


                return null;
            }
            finally
            {

            }

        }

        private void GoBackError(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            MainPage.MainPageContext.Frame.GoBack();
        }

        public async void InsertDocument(BsonDocument newDocument)
        {


            try
            {


                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();


                if (myCollect == string.Empty)
                {

                    await new DialogService()._DialogService("Please select a collection", "Press ok to continue");
                    return;
                }

                MongoClient dbClient = new MongoClient(dbURI.ToString());
                var database = dbClient.GetDatabase(currentDB.ToString());
                var collection = database.GetCollection<BsonDocument>(myCollect.ToString());




                await collection.InsertOneAsync(newDocument);


                UpdateDocumentList();

            }
            catch (Exception ex)
            {

                await new DialogService()._DialogService("Error inserting", ex.Message);
            }


        }


        public async void DeleteDocumentW_ID(string DelDocument)
        {
            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
            var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

        


            ///database client
            MongoClient dbClient = new MongoClient(dbURI.ToString());
            var myDB = dbClient.GetDatabase(currentDB);
            var CurrentCollect = myDB.GetCollection<BsonDocument>(myCollect);

            BsonDocument currentBson = BsonDocument.Parse(DelDocument);
            string id = currentBson["_id"].ToString();
            ObjectId objectId = ObjectId.Parse(id);

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", objectId);

            await CurrentCollect.DeleteOneAsync(deleteFilter);

            ///    await new DialogService()._DialogService($"You succefully deleted {id}", "press ok to continue");

            UpdateDocumentList();


        }


        /// <summary>
        /// search in collection with text
        /// </summary>
        /// <param name="text"></param>
        internal void SearchInCollection(BsonDocument FilterBson)
        {

            try
            {

                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

                ///database client
                MongoClient dbClient = new MongoClient(dbURI);
                ///database 
                var myDB = dbClient.GetDatabase(currentDB);
                var CurrentCollect = myDB.GetCollection<BsonDocument>(myCollect);


         

                Debug.WriteLine("FilterBson:: " + FilterBson.ToJson());


                var myFilteredList = CurrentCollect.Find(FilterBson).Limit(10).ToList();


                UpdateDocumentList(myFilteredList.ConvertAll(BsonTypeMapper.MapToDotNetValue));


            }
            catch(Exception ex)
            {

           //   _= new DialogService()._DialogService("Error while searching",ex.Message);

            }


        }



        public async void UpdateDocument(string CurrentDocument, string newDocument)
        {

            try
            {

                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var myCollect = MongoDB_LocalSettings["CurrentCollection"].ToString();
                var currentDB = MongoDB_LocalSettings["CurrentDB"].ToString();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

                ///database client
                MongoClient dbClient = new MongoClient(dbURI.ToString());
                var myDB = dbClient.GetDatabase(currentDB.ToString());
                var CurrentCollect = myDB.GetCollection<BsonDocument>(myCollect.ToString());

                BsonDocument currentBson = BsonDocument.Parse(CurrentDocument);
                string id = currentBson["_id"].ToString();
                ObjectId objectId = ObjectId.Parse(id);




                var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
                var newBsonDocument = BsonDocument.Parse(newDocument);

                newBsonDocument.Remove("_id");
                newBsonDocument.Add("_id", objectId);

                // replace the first document in the collection
                var replaceOneResult = await CurrentCollect.FindOneAndReplaceAsync(filter, newBsonDocument);

                await new DialogService()._DialogService("Document was succefully updated", "press ok to continue");


            }

            catch (Exception ex)
            {

                await new DialogService()._DialogService("Error updating", ex.ToString());
            }

        }


        public void UpdateDBLeftMenu()
        {
            try {

                MongoDB_Manager.MongoDB_ManagerContext.DatabaseStackPanel.Children.Clear();
                MongoDB_Manager.MongoDB_ManagerContext.DatabaseStackPanel.Children.Add(GetDatabase_StackPanel());

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error getting range", ex.Message);



            }
        }


        public async void CreateDB_AND_Collection(string DBName, string CollectionName)
        {

            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

            ///database client
            ///


            MongoClient dbClient = new MongoClient(dbURI.ToString());


            var myDB = dbClient.GetDatabase(DBName);
            myDB.CreateCollection(CollectionName);

            await new DialogService()._DialogService("Dabase created", "created");

        }


        public List<object> GetDocumentList
            (
            string dabaseName,
            string collectionName,
            int amount,
            BsonDocument filterDefinition = null
            )
        {

            try
            {
                var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
                var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

   

                ///database client
                MongoClient dbClient = new MongoClient(dbURI.ToString());

                ///database list
                var dbList = dbClient.ListDatabases().ToList();
                List<string> dbNames = dbList.Select(db => (string)db["name"]).ToList();


                var database = dbClient.GetDatabase(dabaseName);
                var collection = database.GetCollection<BsonDocument>(collectionName);


                /// if filter is available
                if (filterDefinition != null)
                {


                    var filteredDocuments = collection.Find(filterDefinition).Limit(amount).ToList();



                    return filteredDocuments.ConvertAll(BsonTypeMapper.MapToDotNetValue);

                }

                ////first 10 documents
                var documents = collection.Find(x => true).Limit(amount).ToList();



                return documents.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            }
            catch
            {

                return null;
            }
        }


        public void DropCollectionWithName(string DcollectionName, string DdatabaseName)
        {


            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

            ///database client
            MongoClient dbClient = new MongoClient(dbURI);

            var database = dbClient.GetDatabase(DdatabaseName);
            database.DropCollection(DcollectionName);





        }


        public IAsyncCursor<string> GetCollections_WithDB(string GdbName)
        {

            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

            ///database client
            MongoClient dbClient = new MongoClient(dbURI.ToString());
            var database = dbClient.GetDatabase(GdbName);

            var listNames = database.ListCollectionNames();


            return listNames;
        }


        public void DeleteDatabase(string DatabaseToDelete)
        {

            var MongoDB_LocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var dbURI = MongoDB_LocalSettings["StringConnection URI"].ToString();

            ///database client
            MongoClient dbClient = new MongoClient(dbURI.ToString());

            dbClient.DropDatabase(DatabaseToDelete);

        }


        public void ViewCollectionAsGrid()
        {

            var MongoDBLocalSettings = new MongoDB_LocalSettings().GetLocalSettings_Bson();
            var CurrentColl = MongoDBLocalSettings["CurrentCollection"].ToString();
            var CurrentDB = MongoDBLocalSettings["CurrentDB"].ToString();



            Documents_Container.Documents_ContainerContext.DocumentContainer_StackPanel.Children.Clear();

            var dotNetObjList = new MongoDB_DatabaseService().GetDocumentList(CurrentDB, CurrentColl, 10);
            var stringJson = JsonConvert.SerializeObject(dotNetObjList);

            DataGrid dataGrid = new DataGrid();
            dataGrid.MaxColumnWidth = 200;
            dataGrid.CanUserResizeColumns = true;
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(stringJson);

            new Helper().ToSourceCollection(dataTable, dataGrid);


        }


        internal class Helper   {


            public void ToSourceCollection(DataTable dt, DataGrid dataGrid)
            {



                dataGrid.Columns.Clear();
                dataGrid.AutoGenerateColumns = false;

                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "#",
                    Binding = new Binding { Path = new PropertyPath("[" + 0.ToString() + "]") },


                });


                for (int i = 1; i < dt.Columns.Count + 1; i++)
                {


                    dataGrid.Columns.Add(new DataGridTextColumn()
                    {
                        Header = dt.Columns[i - 1].ColumnName,
                        Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") },
                        Tag = dt.Columns[i - 1].ColumnName,

                    });
                }



                var sourceCollection = new ObservableCollection<object>();



                ////add index to datagrid
                var index = 1;
                foreach (DataRow row in dt.Rows)
                {

                    var arr = row.ItemArray.ToList();
                    arr.Insert(0, index);

                    sourceCollection.Add(arr);
                    index++;
                }


                dataGrid.ItemsSource = sourceCollection;
            }


        }


    }
}
