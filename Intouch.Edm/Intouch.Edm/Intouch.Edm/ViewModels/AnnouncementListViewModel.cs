using Intouch.Edm.Models;
using Intouch.Edm.Services;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class AnnouncementListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Announcement> AnnouncementItems { get; set; }
        public Command LoadAnnouncementsCommand { get; set; }

        public AnnouncementListViewModel()
        {
            AnnouncementItems = new ObservableRangeCollection<Announcement>();
            LoadAnnouncementsCommand = new Command(async () => await ExecuteLoadAnnouncementsCommand());
        }

        async Task ExecuteLoadAnnouncementsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                AnnouncementItems.Clear();
                var announcementItems = await DataService.GetAnnouncementsAsync();
                foreach (var announcementItem in announcementItems.result.items)
                {
                    Announcement announcement = AnnouncementService.GetAnnouncement(announcementItem);
                    AnnouncementItems.Add(announcement);
                }
                //Serden için okunmamış duyurular
                var announcementCount = await DataService.GetAnnouncementsCountAsync();
                int unReadAnnouncementCount = 0;
                if (announcementCount?.result != null)
                    unReadAnnouncementCount = announcementCount.result;
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