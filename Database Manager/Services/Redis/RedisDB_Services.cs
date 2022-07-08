using Database_Manager.Services.Redis;
using Database_Manager.Views.Components.Managers.Redis;
using Database_Manager.Views.Components.Managers.Redis.tree;
using FreeRedis;
using JsonFormatterPlus;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Database_Manager.Services
{
    internal class RedisDB_Services
    {

        public ConnectionMultiplexer redisConn() {

  
            try {


            //    var name = "michael";
            //    var password = "ZvXL6R0OCTdOuvl3BPuLzeKugnVFCvRY";


                var RedisURI = new RedisLocalSettings().RedisURI();
                //    var testURl = "redis-19535.c89.us-east-1-3.ec2.cloud.redislabs.com:19535";

            
                var redis = ConnectionMultiplexer.Connect($"{RedisURI},abortConnect=true,connectRetry=1,connectTimeout=500,connectTimeout=5000");

                return redis;
            }
            catch (Exception ex) { 
                _ = new DialogService()._DialogService("Connection error", ex.Message);
                return null;
            }
        }

        private void getDialog(object sender, ConnectionFailedEventArgs e)
        {
            _ = new DialogService()._DialogService("Connection error");
        }

        public void DelKey(string keyToDel) {

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
            int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());


            var RedisURI = new RedisLocalSettings().RedisURI();
            var redis = ConnectionMultiplexer.Connect(RedisURI);
            var db = redis.GetDatabase(DBNumber);

            db.KeyDelete(keyToDel);


            UpdateLeftMenu();

        }


        public void UpdateLeftMenu(bool isCloud = false) {

            Redis_Manager.Redis_ManagerContext.DatabaseStackPanel.Children.Clear();

            if (isCloud) 
            {
                Redis_Manager.Redis_ManagerContext.DatabaseStackPanel.Children.Add(CloudLeftMenu_StackPanel());
            }
             else
            {

                Redis_Manager.Redis_ManagerContext.DatabaseStackPanel.Children.Add(LocalLeftMenu_StackPanel());

            }
          

           

            

        }



        public StackPanel CloudLeftMenu_StackPanel()
        {

            StackPanel stackPanel = new StackPanel();

        

            Database_Cloud_Tree database_Cloud_Tree = new Database_Cloud_Tree
                (

                 HeadText: "My database"

                );

            stackPanel.Children.Add(database_Cloud_Tree);



            return stackPanel;
        }


            public StackPanel LocalLeftMenu_StackPanel() {

   

            StackPanel stackPanel = new StackPanel();



            for (int dbNumber = 0; dbNumber < 16; dbNumber++)
            {

                Database_tree currentTree = new Database_tree
                      (
                    HeadText: $"My database {dbNumber}"
                    );



                stackPanel.Children.Add(currentTree);

            }

            return stackPanel;
        }



        public void CloudMenu() { 
        
        
        }


        public  void DisplayKeyValue(string KeyText, int dBNumber = 0) {



        
            var db = redisConn().GetDatabase(dBNumber);

            DisplayValue displayValue = new DisplayValue();



            string KeyType = db.KeyType(KeyText).ToString();

        


            switch (KeyType)
            {
                case "String":
                        displayValue.DisplayString(KeyText, dBNumber);
                    break;

                case "Hash":
                        displayValue.DisplayHash(KeyText, dBNumber);
                    break;

                case "List":
                    displayValue.DisplayList(KeyText, dBNumber);
                    break;

                case "Set":
                    displayValue.DisplaySet(KeyText, dBNumber);
                    break;

                case "SortedSet":
                    displayValue.DisplaySortedSet(KeyText, dBNumber);
                    break;

                default:
                //    _ = new DialogService()._DialogService("Not a valid type", "Please press sure to continue");
                    break;


            }




        }


        public bool ValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;

            }
            catch (JsonReaderException ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }


        public void SaveKeyValue(string key, string value,string typeToAdd = null) {

            SaveValue saveValue = new SaveValue();


            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
            int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());

            var db = redisConn().GetDatabase(DBNumber);

            string KeyType = db.KeyType(key).ToString();

            saveValue.checkTypeToSave (  key,  value, typeToAdd, KeyType );



        }

    }//


    internal class SaveValue {



        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(new RedisLocalSettings().RedisURI());
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        DisplayValue displayValue = new DisplayValue(); 



        public void checkTypeToSave(string key, string value, string typeToAdd = null,string KeyType = null)
        {
           

            switch (KeyType)
            {
                case "String":
                    SaveStringValue(key, value);
                    break;

                case "Hash":
                    SaveHash(key, value);
                    break;

                case "List":
                    SaveList(key,value);
                    break;

                case "Set":
                    SaveSet(key, value);
                    break;

                case "SortedSet":
                    SaveSortedSet(key, value);
                    break;

                default:
                 //   _ = new DialogService()._DialogService("Not a valid type");
                    break;

            }



            switch (typeToAdd)
            {
                case "String":
                    SaveStringValue(key, value);
                    break;

                case "Hash":
                    SaveHash(key, value);
                    break;

                case "List":
                    SaveList(key, value);
                    break;

                case "Set":
                    SaveSet(key, value);
                    break;

                case "Sorted set":
                    SaveSortedSet(key, value);
                    break;

                default:
              //      _ = new DialogService()._DialogService("Not a valid type");
                    break;

            }


        }

        private void SaveSortedSet(string key, string value)
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
                int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());


                //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

                var RedisURI = new RedisLocalSettings().RedisURI();

                RedisClient redis = new RedisClient(RedisURI);
                var db = redis.GetDatabase(DBNumber);



                if (key == "" || value == "")
                {

                    _ = new DialogService()._DialogService("Not empty values allowed", "Press 'sure' to continue");

                    new Add_ValuePair().ClearTextBoxes();

                    return;

                }



                Dictionary<string, object> dicValue = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);

                db.Del(key);
                foreach (KeyValuePair<string, object> entry in dicValue)
                {
                    db.ZAdd(key, Convert.ToDecimal(entry.Value) ,entry.Key );
                }

                new RedisDB_Services().UpdateLeftMenu();

                displayValue.DisplaySortedSet(key, DBNumber);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error while saving hash", ex.Message);

            }
        }

        private void SaveSet(string key, string value)
        {
            try
            {


                string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
                int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());


                var db = redis.GetDatabase(DBNumber);

                if (key == "" || value == "")
                {

                    _ = new DialogService()._DialogService("Not empty values allowed", "Press 'sure' to continue");

                    new Add_ValuePair().ClearTextBoxes();

                    return;

                }


                var stringList = JsonConvert.DeserializeObject<List<string>>(value);




                db.KeyDelete(key);


                foreach (var newItem in stringList)
                {
                    db.SetAdd(key, newItem);
                }



                new RedisDB_Services().UpdateLeftMenu();
                displayValue.DisplaySet(key, DBNumber);

            }
            catch (Exception ex)
            {
                _ = new DialogService()._DialogService("Error saving list", ex.Message);
            }
        }

        private async void SaveList(string key, string value)
        {

            try { 


              string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
              int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());

     
                var db = redis.GetDatabase(DBNumber);

            if (key == "" || value == "")
            {

                _ = new DialogService()._DialogService("Not empty values allowed", "Press 'sure' to continue");

                new Add_ValuePair().ClearTextBoxes();

                return;

            }


                var stringList = JsonConvert.DeserializeObject<List<string>>(value);

              


                db.KeyDelete(key );
         

                foreach ( var newItem in stringList) {
                    db.ListRightPush(key,newItem );
                }

             




            new RedisDB_Services().UpdateLeftMenu();
                displayValue.DisplayList(key, DBNumber);

            }
            catch (Exception ex) 
            {
                _ = new DialogService()._DialogService("Error saving list", ex.Message);
            }
        }

        public async void SaveStringValue(string key, string value)
        {


              ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
              string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
             int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());

            var db = redis.GetDatabase(DBNumber);

            if (key == "" || value == "")
            {

                _ = new DialogService()._DialogService("Not empty values allowed", "Press 'sure' to continue");

                new Add_ValuePair().ClearTextBoxes();

                return;

            }


            await db.StringSetAsync(key, value);

            new RedisDB_Services().UpdateLeftMenu();

          displayValue.DisplayString(key, DBNumber);

        }

        internal void SaveHash(string key, string value)
        {

            try { 
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
            int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());

                Debug.WriteLine($"/**/DBNumber:: {DBNumber}");

                //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

                var RedisURI = new RedisLocalSettings().RedisURI();

                RedisClient redis = new RedisClient(RedisURI);
                var db = redis.GetDatabase(DBNumber);



            if (key == "" || value == "")
            {

                _ = new DialogService()._DialogService("Not empty values allowed", "Press 'sure' to continue");

                new Add_ValuePair().ClearTextBoxes();

                return;

            }



                Dictionary<string,object> dicValue = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);

                db.HSet(key, dicValue);

            new RedisDB_Services().UpdateLeftMenu();

                displayValue.DisplayHash(key, DBNumber);

            }
            catch (Exception ex) {

                _ = new DialogService()._DialogService("Error while saving hash", ex.Message);

            }
        }
    }


    internal class DisplayValue {


        public Single_Document SingleDocument_TextBox(string KeyText,string keyType , string TValue) {


            Single_Document currentTextBox = new Single_Document(keyType.ToString());
            currentTextBox.Margin = new Thickness(20, 0, 0, 0);
            currentTextBox.Name = "documents_Container";
            currentTextBox.SingleTextBox.Text = TValue;
            currentTextBox.KeyType = keyType.ToString();
            currentTextBox.KeyText = KeyText;

            return currentTextBox;

        }
    
    
        public async void DisplayString(string KeyText, int dBNumber)
        {

            try
            {


                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                // load a setting that is local to the device
                var localValue = localSettings.Values["RedisDBSettings"] as string;


                var RedisURI = new RedisLocalSettings().RedisURI();

                var redis = ConnectionMultiplexer.Connect($"{RedisURI},abortConnect=true,connectRetry=1,connectTimeout=500");


                var db = redis.GetDatabase(dBNumber);

                object keyType = db.KeyType(KeyText);


          

                string value = await db.StringGetAsync(KeyText);





                if (value == null)
                {

                    
                    Single_Document nullTextBox = SingleDocument_TextBox("*Null*","No type", "*Empty*");
         
                    Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                    Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(nullTextBox);

                    return;
                }



                if (new RedisDB_Services().ValidateJSON(value)) value = JsonFormatter.Format(value);


                Single_Document currentTextBox = SingleDocument_TextBox(KeyText,keyType.ToString(),value); 

      

                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(currentTextBox);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error trying to display string", ex.Message);


            }



        }

        internal async void DisplayHash(string keyText, int dBNumber)
        {

            try
            {


                var RedisURI = new RedisLocalSettings().RedisURI();
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisURI);

                var db = redis.GetDatabase(dBNumber);



                string  value =   db.HashGetAll(keyText).ToStringDictionary().ToJson();



                db.KeyType(keyText);

                if (value == null)
                {
                    _ = new DialogService()._DialogService("Emty document");

                    return;
                }



                if (new RedisDB_Services().ValidateJSON(value)) value = JsonFormatter.Format(value);


                string KeyType = db.KeyType(keyText).ToString();

                Single_Document currentTextBox = new Single_Document(KeyType);
                currentTextBox.Margin = new Thickness(20, 0, 0, 0);
                currentTextBox.Name = "documents_Container";
                currentTextBox.KeyText = keyText;
                currentTextBox.KeyType = KeyType;
                currentTextBox.SingleTextBox.Text = value;
 




                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(currentTextBox);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);


            }
        }

        internal void DisplayList(string keyText, int dBNumber)
        {
            try
            {

                Debug.WriteLine("called display list");

       
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
     





                var RedisURI = new RedisLocalSettings().RedisURI();
                ConnectionMultiplexer redis02 = ConnectionMultiplexer.Connect(RedisURI);
                var db02 = redis02.GetDatabase(dBNumber);

                RedisClient redis = new RedisClient(RedisURI);
                var db = redis.GetDatabase(dBNumber);


                var keyExist = db.Exists(keyText);



                var value = db.LRange(keyText,0,1).ToJson().ToString();



       

                if (value == null)
                {
                    _ = new DialogService()._DialogService("Emty document");

                    return;
                }



                if (new RedisDB_Services().ValidateJSON(value)) value = JsonFormatter.Format(value);


                string KeyType = db02.KeyType(keyText).ToString();

                Single_Document currentTextBox = SingleDocument_TextBox(keyText, KeyType, value);
     


                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(currentTextBox);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);


            }
        }

        internal void DisplaySet(string keyText, int dBNumber)
        {
            try
            {

                //  ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                //    string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
                // int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());


                var RedisURI = new RedisLocalSettings().RedisURI();
                RedisClient redisC = new RedisClient(RedisURI);
             var db = redisC.GetDatabase(dBNumber);

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisURI);
                var db01 = redis.GetDatabase(dBNumber);


                string value = db.SMembers(keyText).ToJson().ToString();



           

                if (value == null)
                {
                    _ = new DialogService()._DialogService("Emty document");

                    return;
                }



                if (new RedisDB_Services().ValidateJSON(value)) value = JsonFormatter.Format(value);


                string KeyType = db01.KeyType(keyText).ToString();


                Single_Document currentTextBox = SingleDocument_TextBox(keyText, KeyType.ToString(), value);


                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(currentTextBox);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);


            }
        }

        internal void DisplaySortedSet(string keyText, int dBNumber)
        {
            try
            {

                //  ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                //    string RedisDBSettings = localSettings.Values["RedisDBSettings"] as string;
                // int DBNumber = int.Parse(BsonDocument.Parse(RedisDBSettings)["DBNumber"].ToString());


                var RedisURI = new RedisLocalSettings().RedisURI();
                RedisClient redisC = new RedisClient(RedisURI);
                var db = redisC.GetDatabase(dBNumber);

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisURI);
                var db01 = redis.GetDatabase(dBNumber);


                Dictionary<string, object> dict = new Dictionary<string, object>();
           //     dict.Add("keyDic", "value");
              //  dict.Add("keyDic02", "value02");

  

                var mySetsKey = db.ZRange(keyText, 0, -1);

               

                foreach (var kv in mySetsKey)
                {
                    dict.Add(kv, db.ZScore(keyText,kv).Value.ToString()); ;
                }

                string value = dict.ToJson().ToString();





                if (value == null)
                {
                    _ = new DialogService()._DialogService("Emty document");

                    return;
                }



                if (new RedisDB_Services().ValidateJSON(value)) value = JsonFormatter.Format(value);


                string KeyType = db01.KeyType(keyText).ToString();

                Single_Document currentTextBox = SingleDocument_TextBox(keyText, KeyType.ToString(), value);

                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Clear();
                Redis_Manager.Redis_ManagerContext.Redis_documentContainer.Children.Add(currentTextBox);

            }
            catch (Exception ex)
            {

                _ = new DialogService()._DialogService("Error", ex.Message);


            }
        }
    }



}//
