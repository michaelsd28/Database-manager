
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Database_Manager.Services
{
    internal class localSettings_Service
    {

        public string NewCardType(string newValue = "") {


            var localSettings = ApplicationData.Current.LocalSettings;

            // Save a setting locally on the device
       
            if (newValue != "") {

                localSettings.Values["Connection with uri Button"] = newValue;
            }
  
            // load a setting that is local to the device
            string localValue = localSettings.Values["Connection with uri Button"] as string;

            return localValue;

        }



        public SolidColorBrush ConvertColorFromHexString(string colorStr)
        {
            //Target hex string
           
            colorStr = colorStr.Replace("#", string.Empty);
            // from #RRGGBB string
            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);
            //get the color
            Color color = Color.FromArgb(255, r, g, b);
            // create the solidColorbrush
            var myBrush = new SolidColorBrush(color);

            return myBrush;
        }




    }
}
