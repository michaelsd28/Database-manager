using Database_Manager.Services;
using Database_Manager.Services.SQL;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Database_Manager.Views.Components.Managers.SQL.tree
{
    public sealed partial class Table_button : UserControl
    {



        public string TableName
        {
            get { return (string)GetValue(TableNameProperty); }
            set { SetValue(TableNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TableName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TableNameProperty =
            DependencyProperty.Register("TableName", typeof(string), typeof(Table_button), new PropertyMetadata(string.Empty));


        public Table_button()
        {
            InitializeComponent();

  


        }

        private async void BTable_Click(object sender, RoutedEventArgs e)
       
           => await new SQL_Services().UpdateTableGrid(TableName);

        private void BDeleteTable(object sender, RoutedEventArgs e)
        {

            _ = new DialogService()._DialogService(
                title:  $"Do you want to delete *{TableName}*?", 
                PrimaryButton:  DeleteTableService 
                );
           
        }

        private async void DeleteTableService(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
         
           await new SQL_Services().ExecQuery($"DROP TABLE {TableName};");
            _ = new SQL_Services().LoadDatabases();
        }
    }
}
