using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Database_Manager.Services
{
    internal class DialogService
    {




        public async Task _DialogService(
            string title,
            string content = "Please press sure to continue",
             TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> PrimaryButton
            = null
            )
        {



            ContentDialog dialog = new ContentDialog();
            dialog.Title = title;
            dialog.Content = content;
            dialog.PrimaryButtonText = "Sure";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.PrimaryButtonClick += PrimaryButton;
            _ = await dialog.ShowAsync();


        }


    }
}
