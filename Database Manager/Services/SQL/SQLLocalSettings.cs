using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Services.SQL
{
    internal class SQLLocalSettings
    {



        public void UpdateURI(string URI) {

        var LocalSettings_Bson = GetLocalSettings_Bson();
            LocalSettings_Bson["StringConnection URI"] = URI;

            UpdateSettings(LocalSettings_Bson);


        }

        public void UpdateCurrentDB(string CurrentDB) 
        {
            var LocalSettings_Bson = GetLocalSettings_Bson();
            LocalSettings_Bson["CurrentDatabase"] = CurrentDB;

            UpdateSettings(LocalSettings_Bson);


        }

        public BsonDocument GetLocalSettings_Bson() 
        {
            var localSettings = new Helper().GetLocalSettings();
            try {


                string localValue = localSettings.Values["SQL Local Settings"] as string;


                return  BsonDocument.Parse(localValue);

            }
            catch (Exception ex) 
            {

                return new Helper().IfNotSettings();
            }
        }

        public BsonDocument UpdateSettings(BsonDocument newSettings)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            try
            {

                localSettings.Values["SQL Local Settings"] = newSettings.ToJson().ToString();

                string localValue = localSettings.Values["SQL Local Settings"] as string;


                return BsonDocument.Parse(localValue);

            }
            catch (Exception ex)
            {
                return new Helper().IfNotSettings();
            }
        }


        public BsonDocument UpdateCurrentTable(string newTable)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            try
            {


                var NewJsonBson = GetLocalSettings_Bson();
                NewJsonBson["CurrentTable"] = newTable;

                localSettings.Values["SQL Local Settings"] = NewJsonBson.ToJson().ToString();

                return NewJsonBson;




            }
            catch (Exception ex)
            {


                return new Helper().IfNotSettings();

            }
        }




        internal class Helper {


            public Windows.Storage.ApplicationDataContainer GetLocalSettings() 
            
            {
                return Windows.Storage.ApplicationData.Current.LocalSettings;
            }


            public BsonDocument IfNotSettings() {
           
                var localBson = new BsonDocument();
                localBson["StringConnection"] = "";
                localBson["StringConnection URI"] = "";
                localBson["CurrentDatabase"] = "";
                localBson["CurrentTable"] = "";
                localBson["PortNumber"] = "";
                localBson["name"] = "";
                GetLocalSettings().Values["SQL Local Settings"] = localBson.ToJson().ToString();

                return localBson;
            }
        
        
        }

    }
}
