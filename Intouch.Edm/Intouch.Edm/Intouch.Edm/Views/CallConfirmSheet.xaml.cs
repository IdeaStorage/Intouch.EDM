using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallConfirmSheet : PopupPage
    {
        private readonly Action<bool> setResultAction;

        public void CancelClicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(false);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        public void ConfirmClicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(true);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        public CallConfirmSheet(Action<bool> setResultAction, string phoneNumber)
        {
            InitializeComponent();
            this.setResultAction = setResultAction;
            PhoneNumberButton.Text = phoneNumber;
        }
        public CallConfirmSheet()
        {
            InitializeComponent();
        }

        public static async Task<bool> ShowConfirmPopup(INavigation navigation, string message)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            void callback(bool didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new CallConfirmSheet(callback, message);
            await navigation.PushPopupAsync(popup);
            return await completionSource.Task;
        }
    }
}