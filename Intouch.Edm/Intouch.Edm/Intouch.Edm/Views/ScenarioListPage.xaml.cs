﻿using Intouch.Edm.Helpers;
using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.TaskOptionDto;
using Intouch.Edm.Models.Enums;
using Intouch.Edm.Services;
using Intouch.Edm.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenarioListPage : TabbedPage
    {
        private ScenarioListViewModel viewModel;
        private DataService dataService;

        public ScenarioListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ScenarioListViewModel((int)StatusEnums.Waiting);
            CurrentPageChanged += async (object sender, EventArgs e) =>
            {
                int numPage = Children.IndexOf(CurrentPage);
                BindingContext = viewModel = new ScenarioListViewModel(numPage);
                viewModel.LoadScenariosCommand.Execute(null);
            };
            dataService = new DataService(new Uri(Constants.ApplicationServiceUrl), Helpers.Settings.AuthenticationToken);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Scenario scenario))
            {
                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new ScenarioApprovePage(new ScenarioApproveViewModel(scenario)));
        }

        private async void OnItemSelectedToCheckDetailOptions(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Scenario scenario))
            {
                return;
            }
            TaskItem taskItem = new TaskItem();
            taskItem.Options = new RootObject();
            taskItem.Options.result = new Models.Dtos.TaskOptionDto.Result();
            taskItem.Options.result.items = new System.Collections.Generic.List<Item>();

            if (scenario.commiteeApprovalId != null)
            {
                try
                {
                    var options = await dataService.GetScenarioTaskOptions(scenario.commiteeApprovalId);
                    foreach (var option in options.result)
                    {
                        Item item = new Item();
                        item.checkedOption.id = option.id;
                        item.checkedOption.text = option.text;
                        item.checkedOption.userId = option.userId;
                        item.checkedOption.userFullName = option.userFullName;
                        item.checkedOption.completed = option.completed;
                        item.checkedOption.taskUserPhone = option.taskUserPhone;
                        taskItem.Options.result.items.Add(item);
                    }
                    taskItem.Options.result.totalCount = options.result.Count;

                    await Application.Current.MainPage.Navigation.PushAsync(new ScenarioTaskOptionsPage(new ScenarioTaskOptionsViewModel(taskItem)));
                }
                catch (System.Exception ex)
                {
                    await DisplayAlert("Uyarı!", "Bağlantı sağlanırken hata oluştu.", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Alert", "CommiteeApprovalId null geldiği için Detaya gidilemiyor.", "Tamam");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ScenarioItems.Count == 0)
            {
                viewModel.LoadScenariosCommand.Execute(null);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void ApprovedListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (BindingContext as ScenarioListViewModel).LoadMoreItems(e.Item as Scenario, Children.IndexOf(CurrentPage));
        }

        private void RejectedListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (BindingContext as ScenarioListViewModel).LoadMoreItems(e.Item as Scenario, Children.IndexOf(CurrentPage));
        }
    }
}