using Intouch.Edm.Models;
using Intouch.Edm.Views;
using Plugin.FirebasePushNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Intouch.Edm
{
    public partial class App : Application
    {
        public App(string value)
        {
            InitializeComponent();

            Redirect(value);
        }

        public bool IsSignedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Helpers.Settings.AuthenticationToken);
            }
        }

        private void Redirect(string value)
        {
            if (value == "1")
            {
                MainPage = new NavigationPage(new MainPage((int)TabPageEnums.TaskListPage));
            }
            else if (value == "2")
            {
                MainPage = new NavigationPage(new MainPage((int)TabPageEnums.AnnouncementListPage));
            }
            else if (value == "3")
            {
                MainPage = new NavigationPage(new MainPage((int)TabPageEnums.ScenarioListPage));
            }
        }

        public App()
        {
            InitializeComponent();
            if (IsSignedIn)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

            try
            {
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                        Helpers.Settings.AuthenticationToken = p.Token;
                    };

                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                };

                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Opened");
                    foreach (var data in p.Data)
                    {
                        if (data.Key == "NotificationTypeId")
                        {
                            Redirect(data.Value.ToString());
                        }
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    }
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"error occured: {ex.ToString()}");
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}