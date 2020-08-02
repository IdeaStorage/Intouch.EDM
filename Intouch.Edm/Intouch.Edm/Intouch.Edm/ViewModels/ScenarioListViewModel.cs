using Intouch.Edm.Models;
using Intouch.Edm.Services;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class ScenarioListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Scenario> AllScenarioItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }
        public ObservableRangeCollection<Scenario> ScenarioItems { get; set; }
        public Command LoadScenariosCommand { get; set; }
        private readonly int _resultCount = 20;
        private int _totalCount = 0;

        public ScenarioListViewModel(int? statusId = null)
        {
            if (statusId != null)
            {
                Helpers.Settings.StatusId = statusId.ToString();
            }
            else
            {
                statusId = Convert.ToInt32(Helpers.Settings.StatusId);
            }
            ScenarioItems = new ObservableRangeCollection<Scenario>();
            AllScenarioItems = new ObservableRangeCollection<Scenario>();
            ScenarioItems = new ObservableRangeCollection<Scenario>();
            LoadScenariosCommand = new Command(async () => await ExecuteLoadScenariosCommand(statusId));
        }

        private async Task ExecuteLoadScenariosCommand(int? statusId)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                AllScenarioItems.Clear();
                ScenarioItems.Clear();

                GetScenarios(statusId, _resultCount, 0);
                AllScenarioItems.ReplaceRange(ScenarioItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadMoreItems(Scenario currentItem, int? statusId = null)
        {
            int itemIndex = ScenarioItems.IndexOf(currentItem);

            if (ScenarioItems.Count - 1 == itemIndex &&
                _totalCount != ScenarioItems.Count &&
                ScenarioItems.Count >= _resultCount)
            {
                IsBusy = true;
                GetScenarios(statusId, null, _resultCount);
                AllScenarioItems.ReplaceRange(ScenarioItems);
                _totalCount = ScenarioItems.Count;
                IsBusy = false;
            }
        }

        public async void GetScenarios(int? statusId, int? maxCount, int skipCount)
        {
            var scenarios = await DataService.GetScenarioAsync(statusId, maxCount, skipCount);
            if (scenarios.result != null)
            {
                var records = scenarios.result.items.Where(a => a.type == 1).OrderByDescending(x => x.creationTime).ToList();
                foreach (var x in records)
                {
                    // Serden: Bu geçici olarak konuldu servisten bize eventID gelirse buna gerek yok iconu direkt Taskservice.GetEvent ile ayarlayabiliriz.
                    // İsim ile çalışması riskli...
                    var eventValue = TaskService.GetEventByName(x.eventTypeName);
                    var scenario = new Scenario()
                    {
                        Id = x.id,
                        Event = x.eventTypeName,
                        ImpactArea = x.impactAreaName,
                        Site = x.siteName,
                        Subject = x.subjectType == 1 ? $"Acil Durum\n{x.eventTypeName}" : $"İş Sürekliliği\n{x.eventTypeName}",
                        RecordDate = x.creationTime.ToString("dd MMMM yyyy \n HH:mm"),
                        Icon = eventValue.eventIcon,
                        PictureUrl = x.pictureUrl,
                        ScenarioId = x.scenarioId,
                        IsWaiting = x.approveStatus == 0
                    };
                    ScenarioItems.Add(scenario);
                }
            }
        }
    }
}