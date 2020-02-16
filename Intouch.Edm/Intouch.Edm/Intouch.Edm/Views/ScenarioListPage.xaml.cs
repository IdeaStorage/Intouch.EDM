using Intouch.Edm.Models;
using Intouch.Edm.Models.Enums;
using Intouch.Edm.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenarioListPage : TabbedPage
    {
        ScenarioListViewModel viewModel;
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
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Scenario scenario))
            {
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new ScenarioApprovePage(new ScenarioApproveViewModel(scenario)));
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
    }
}