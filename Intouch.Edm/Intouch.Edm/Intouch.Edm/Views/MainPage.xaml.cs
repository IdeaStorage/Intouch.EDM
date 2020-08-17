﻿using Intouch.Edm.Models;
using Intouch.Edm.Services;
using Intouch.Edm.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        private MainPageViewModel viewModel;
        private DataService dataService;

        public MainPage(int currentTab = 0)
        {
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();
            BindingContext = viewModel = new MainPageViewModel();
            dataService = new DataService(new Uri("http://edm.intouch.istanbul"), Helpers.Settings.AuthenticationToken);
            CurrentPageChanged += async (object sender, EventArgs e) =>
            {
                BindingContext = viewModel = new MainPageViewModel();
                int numPage = Children.IndexOf(CurrentPage);

                if (numPage == (int)TabPageEnums.AnnouncementListPage)
                {
                    await dataService.SetReadNotifications();
                    (BindingContext as MainPageViewModel).UnreadCount = 0;
                }
            };
            if (currentTab == (int)TabPageEnums.AnnouncementListPage)
            {
                CurrentPage = this.FindByName<NavigationPage>("AnnouncementList");
            }
            else if (currentTab == (int)TabPageEnums.TableViewPage)
            {
                CurrentPage = this.FindByName<NavigationPage>("TableViewMenu");
            }
            if (currentTab == (int)TabPageEnums.ScenarioListPage)
            {
                CurrentPage = this.FindByName<NavigationPage>("ScenarioList");
            }
            if (currentTab == (int)TabPageEnums.TaskListPage)
            {
                CurrentPage = this.FindByName<NavigationPage>("TaskList");
            }

            if (Helpers.Settings.isADKAuthorize == "0")
            {
                this.Children.Remove(ScenarioList);
            }
            if (Helpers.Settings.isTasksAuthorize == "0")
            {
                this.Children.Remove(TaskList);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}