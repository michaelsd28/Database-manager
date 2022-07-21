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


        public BsonDocument GetLocalSettings() 
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            try {

     

                string localValue = localSettings.Values["SQL Local Settings"] as string;




                return  BsonDocument.Parse(localValue);

            }
            catch (Exception ex) 
            {


                var localBson = new BsonDocument();
                localBson["StringConnection"] = "";
                localBson["CurrentTable"] = "";
                localBson["PortNumber"] = "";
                localBson["name"] = "";
                localSettings.Values["SQL Local Settings"] = localBson.ToJson().ToString();

      
                return localBson;
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
                var localBson = new BsonDocument();
                localBson["StringConnection"] = "";
                localBson["CurrentTable"] = "";
                localBson["PortNumber"] = "";
                localBson["name"] = "";
                localSettings.Values["SQL Local Settings"] = localBson.ToJson().ToString();


                return localBson;
            }
        }


        public BsonDocument UpdateCurrentTable(string newTable)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            try
            {


                var NewJsonBson = GetLocalSettings();
                NewJsonBson["CurrentTable"] = newTable;



                localSettings.Values["SQL Local Settings"] = NewJsonBson.ToJson().ToString();

                return NewJsonBson;




            }
            catch (Exception ex)
            {
                var localBson = new BsonDocument();
                localBson["StringConnection"] = "";
                localBson["CurrentTable"] = "";
                localBson["PortNumber"] = "";
                localBson["name"] = "";
                localSettings.Values["SQL Local Settings"] = localBson.ToJson().ToString();


                return localBson;
            }
        }


    }
}
