﻿using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPasswordPage : ContentPage
    {
        private ForgetPasswordPageModel viewModel;

        public ForgetPasswordPage(ForgetPasswordPageModel _viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = _viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}