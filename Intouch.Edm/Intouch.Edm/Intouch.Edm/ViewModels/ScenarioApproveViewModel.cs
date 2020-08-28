using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.ApproveScenario;
using Intouch.Edm.Views;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ScenarioApproveViewModel(Scenario scenario)
        {
            scenario.Subject = scenario.SubjectType == 1 ? "Acil Durum" : "İş Sürekliliği";
            Scenario = scenario;
            _isVisible = Scenario.IsWaiting;
        }

        private ICommand _approveCommand;

        public ICommand ApproveButtonClicked => _approveCommand
                ?? (_approveCommand = new Command(async () => await ApproveClicked()));

        private async System.Threading.Tasks.Task ApproveClicked()
        {
            IsBusy = true;
            ApproveScenarioDto approveScenario = new ApproveScenarioDto { id = this.scenario.Id };

            var result = await DataService.ApproveScenario(approveScenario);
            string returnMessage = result.result.message;
            if (result.result.approveResult)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("ONAYLANDI", returnMessage, "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Başarısız", returnMessage, "TAMAM");
            }
        }

        private ICommand _rejectCommand;

        public ICommand RejectButtonClicked => _rejectCommand
                ?? (_rejectCommand = new Command(async () => await RejectClicked()));

        private async System.Threading.Tasks.Task RejectClicked()
        {
            IsBusy = true;
            ApproveScenarioDto approveScenario = new ApproveScenarioDto { id = this.Scenario.Id };

            var result = await DataService.RejectScenario(approveScenario);
            if (result)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("REDDEDİLDİ", "Kayıt Reddilmiştir.", "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Başarısız", "Kayıt Reddedilememiştir.", "TAMAM");
            }
        }
    }
}