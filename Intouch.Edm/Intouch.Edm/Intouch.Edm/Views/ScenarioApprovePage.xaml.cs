﻿using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenarioApprovePage : ContentPage
    {
        private ScenarioApproveViewModel viewModel;

        public ScenarioApprovePage(ScenarioApproveViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            viewModel.Logout();
        }
    }
}