using Intouch.Edm.Models;
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
                var scenarios = await DataService.GetScenarioAsync(statusId);
                var records = scenarios.result.items.Where(a => a.type == 1).OrderByDescending(x => x.creationTime).ToList();
                foreach (var x in records)
                {
                    var scenario = new Scenario()
                    {
                        Id = x.id,
                        Event = x.eventTypeName,
                        ImpactArea = x.impactAreaName,
                        Site = x.siteName,
                        Source = x.sourceName != null ? x.sourceName.ToString() : "",
                        Subject = x.subjectType == 1 ? "Acil Durum" : "İş Sürekliliği",
                        RecordDate = x.creationTime.ToString("dd MMMM yyyy HH:mm"),
                        // Buraya servisten statuId dönmek zorunda, ona göre ikon belirleyeceğiz....
                        StatusIcon = x.approveStatus == 0 ? "waitingIcon.png" : "approveIcon.png",
                        Icon = x.subjectType == 1 ? "emergencyIcon.png" : "businessIcon.png",
                        PictureUrl = x.pictureUrl,
                        ScenarioId = x.scenarioId,
                        IsWaiting = x.approveStatus == 0
                    };
                    ScenarioItems.Add(scenario);
                }
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
    }
}