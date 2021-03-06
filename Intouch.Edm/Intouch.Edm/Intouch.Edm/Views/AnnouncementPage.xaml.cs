﻿using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementPage : ContentPage
    {
        private AnnouncementViewModel viewModel;

        public AnnouncementPage(AnnouncementViewModel _viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = _viewModel;
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