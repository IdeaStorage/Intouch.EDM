using Intouch.Edm.Helpers;
using Intouch.Edm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataService DataService => DependencyService.Get<IDataService>() ?? new DataService(new Uri(Constants.ApplicationServiceUrl), Helpers.Settings.AuthenticationToken);

        private bool isBusy = false;
      
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
      
        
        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        public async void HandleException(Exception exc, string message)
        {
            await Application.Current.MainPage.DisplayAlert("Uyarı!", $"{message}", "TAMAM");
            Debug.WriteLine("{0} {1}", message, exc);
        }
     
    }
}