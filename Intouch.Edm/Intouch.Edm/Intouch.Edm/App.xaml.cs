using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Intouch.Edm.Views;
using Intouch.Edm.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Intouch.Edm
{
    public partial class App : Application
    {
        public App(string value)
        {
            InitializeComponent();
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

            MainPage = new NavigationPage(new LoginPage());
           // MainPage = new MainPage();
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
