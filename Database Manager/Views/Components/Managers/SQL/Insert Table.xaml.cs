using Database_Manager.Services.SQL;
using Database_Manager.Views.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.SQL
{
    public sealed partial class Insert_Table : UserControl
    {
        public Insert_Table()
          =>  InitializeComponent();


        private void BCancelInsert(object sender, RoutedEventArgs e)
            => closeGRID();


        private async void BInsertTable(object sender, RoutedEventArgs e)
        {

            Value_TextBox.Document.GetText(TextGetOptions.None, out string query);


           await new SQL_Services().ExecQuery(query);

            await new SQL_Services().LoadDatabases();

            closeGRID();
        }


  


        


        private void closeGRID() 
          =>  SQL_Manager.sQL_ManagerContext.CreateTable_Popup.Visibility = Visibility.Collapsed;
        
    }
}
