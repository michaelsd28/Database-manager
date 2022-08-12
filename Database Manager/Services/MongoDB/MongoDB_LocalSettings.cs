using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Services.MongoDB
{
    internal class MongoDB_LocalSettings
    {


        public BsonDocument GetLocalSettings_Bson()
        {

            try
            {
                string localValue = new Helper().GetLocalSettings().Values["Mongodb Local Settings"] as string;

                return BsonDocument.Parse(localValue);

            }
            catch (Exception ex)
            {
                return new Helper().IfNoBsonExists();
            }


        }



        public void UpdateURI(string URI) {

            var localSettings = GetLocalSettings_Bson();
            localSettings["StringConnection URI"] = URI;


            UpdateSettings(localSettings);

        }

        public BsonDocument UpdateSettings(BsonDocument newSettings)
        {
     
            try
            {

                //update settings
                new Helper().GetLocalSettings().Values["Mongodb Local Settings"] = newSettings.ToJson().ToString();

                ///get string
                string localValue = new Helper().GetLocalSettings().Values["Mongodb Local Settings"] as string;

                Debug.WriteLine("localValue:: " + localValue);

                ///return as BsonDocument
                return BsonDocument.Parse(localValue);

            }
            catch (Exception ex)
            {

                return new Helper().IfNoBsonExists();

            }
        }


        public BsonDocument UpdateCurrentCollection(string NewCollection)
        {
   
            try
            {


                var LocalSettings_Bson = GetLocalSettings_Bson();
                LocalSettings_Bson["CurrentTable"] = NewCollection;

                ///update settings
                UpdateSettings(LocalSettings_Bson);

                /// return BsonDocument
                return LocalSettings_Bson;




            }
            catch (Exception ex)
            {
                return new Helper().IfNoBsonExists(); 
            }
        }






        internal class Helper
        {

            public BsonDocument IfNoBsonExists()

            {
                var localBson = new BsonDocument();
                localBson["StringConnection"] = "";
                localBson["CurrentTable"] = "";
                localBson["PortNumber"] = "";
                localBson["name"] = "";
                new Helper().GetLocalSettings().Values["Mongodb Local Settings"] = localBson.ToJson().ToString();


                return localBson;

            }

            public Windows.Storage.ApplicationDataContainer GetLocalSettings() {

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


                return localSettings;

            }
        }

    }
}
