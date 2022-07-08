using JsonFormatterPlus;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Database_Manager.Services.Redis
{
    internal class RedisLocalSettings
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public void UpdateDBNumber(string newNumber)
        {
        

            string localValue = localSettings.Values["RedisDBSettings"] as string;
            BsonDocument bson = BsonDocument.Parse(localValue);
            bson["DBNumber"] = newNumber;

            localSettings.Values["RedisDBSettings"] = bson.ToString();

        }


        public void UpdateCurrentKey(string newKey)
        {


            string localValue = localSettings.Values["RedisDBSettings"] as string;
            BsonDocument bson = BsonDocument.Parse(localValue);
            bson["CurrentKey"] = newKey;

            localSettings.Values["RedisDBSettings"] = bson.ToString();

        }



        public string RedisURI(string newURL = "") {


            try { 

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;



    


                // load a setting that is local to the device
                var localValue = localSettings.Values["RedisDBSettings"] as string;

            localValue =  JsonFormatter.Format(localValue);

      
            
            var bson = BsonDocument.Parse(localValue);

            if (newURL != "") {
                bson["RedisURL"] = newURL;
                localSettings.Values["RedisDBSettings"] = bson.ToString();
            }


                var uri = bson["RedisURL"].ToString();

            return uri;

            }
            catch (Exception ex) { 

                _ = new DialogService()._DialogService("Error redis URI", ex.Message);

           

                MainPage.MainPageContext.Frame.GoBack();
    
                return null;
            }
        }


        public void LocalSettings()
        {

       

            // Save a setting locally on the device
            localSettings.Values["RedisDBSettings"] = @"{""RedisURL"":""localhost:port"",
                                                                                                    ""DBNumber"":""0"",
                                                                                                    ""PortNumber"":""6379"",
                                                                                                    ""CurrentKey"":""empty"",
                                                                                                     ""name"":""michael"",
                                                                                                       ""password"":""cloud redis""
                                                                                                    }";

            string localValue = localSettings.Values["RedisDBSettings"] as string;

    




        }


        public string GetCurrentKey() {

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string localValue = localSettings.Values["RedisDBSettings"] as string;
            BsonDocument bson = BsonDocument.Parse(localValue);
            return bson["CurrentKey"].ToString();

        }


        public string AddValue_ButtonType(string NewType = null) {

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string localValue = localSettings.Values["RedisDB AddValue_ButtonType"] as string;

            if (NewType == null) 
            {
                return localValue;
            } 
            else {

                localSettings.Values["RedisDB AddValue_ButtonType"] = NewType;
                return localSettings.Values["RedisDB AddValue_ButtonType"] as string;

            } 




      


        }

    }
}
