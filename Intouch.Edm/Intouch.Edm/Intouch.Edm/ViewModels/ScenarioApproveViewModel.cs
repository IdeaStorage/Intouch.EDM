using Intouch.Edm.Models;
using Intouch.Edm.Services;
using Intouch.Edm.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Intouch.Edm.Models.Dtos.ApproveScenario;

namespace Intouch.Edm.ViewModels
{
    public class ScenarioApproveViewModel : BaseViewModel
    {

        public Scenario scenario { get; set; }
        private bool _isVisible;
        public bool isVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                SetProperty(ref _isVisible, value);
            }
        }

        public Scenario Scenario
        {
            get { return scenario; }
            set
            {
                scenario = value;
                OnPropertyChanged();
            }
        }
        public string scenarioId { get; set; }

        //public override Task Init(Scenario scenario)
        //{
        //    this.Scenario = scenario;
        //}

        public ScenarioApproveViewModel(Scenario scenario)
        {
            this.Scenario = scenario;
            // RetrieveScenario(scenarioId);
            _isVisible = this.Scenario.IsWaiting;
        }

        ICommand _approveCommand;
        public ICommand ApproveButtonClicked => _approveCommand
                ?? (_approveCommand = new Command(async () => await ApproveClicked()));

        async System.Threading.Tasks.Task ApproveClicked()
        {
            ApproveScenarioDto approveScenario = new ApproveScenarioDto { id = this.scenario.Id };

            var result = await DataService.ApproveScenario(approveScenario);
            string returnMessage = result.result.message;
            if (result.result.approveResult)
            {
                
                await Application.Current.MainPage.DisplayAlert("ONAYLANDI", returnMessage, "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
                await Application.Current.MainPage.DisplayAlert("Başarısız", returnMessage, "TAMAM");

        }

        ICommand _rejectCommand;
        public ICommand RejectButtonClicked => _rejectCommand
                ?? (_rejectCommand = new Command(async () => await RejectClicked()));

        async System.Threading.Tasks.Task RejectClicked()
        {
            ApproveScenarioDto approveScenario = new ApproveScenarioDto { id = this.Scenario.Id };

            var result = await DataService.RejectScenario(approveScenario);
            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("REDDEDİLDİ", "Kayıt Reddilmiştir.", "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
                await Application.Current.MainPage.DisplayAlert("Başarısız", "Kayıt Reddedilememiştir.", "TAMAM");
        }
    }
}