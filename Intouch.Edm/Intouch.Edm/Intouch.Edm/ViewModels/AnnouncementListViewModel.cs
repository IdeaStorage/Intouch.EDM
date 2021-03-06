﻿using Intouch.Edm.Models;
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
        private readonly int _resultCount = 20;
        private int _totalCount = 0;

        public AnnouncementListViewModel()
        {
            AnnouncementItems = new ObservableRangeCollection<Announcement>();
            LoadAnnouncementsCommand = new Command(async () => await ExecuteLoadAnnouncementsCommand());
        }

        private async Task ExecuteLoadAnnouncementsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                AnnouncementItems.Clear();
                GetAnnouncements(_resultCount, 0);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Bağlantı sağlanırken hata oluştu.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadMoreItems(Announcement currentItem)
        {
            int itemIndex = AnnouncementItems.IndexOf(currentItem);

            if (AnnouncementItems.Count - 1 == itemIndex &&
                _totalCount != AnnouncementItems.Count &&
                AnnouncementItems.Count >= _resultCount)
            {
                IsBusy = true;
                GetAnnouncements((_totalCount - _resultCount), _resultCount);
                IsBusy = false;
            }
        }

        public async void GetAnnouncements(int maxCount, int skipCount)
        {
            try
            {
                var announcementItems = await DataService.GetAnnouncementsAsync(maxCount, skipCount);
                _totalCount = announcementItems.result.totalCount;
                int announcementColorModCount = 3;
                int index = 0;
                foreach (var announcementItem in announcementItems.result.items)
                {
                    Announcement announcement = AnnouncementService.GetAnnouncement(announcementItem);
                    announcement.Icon = string.Format("announcement_{0}.png", index % announcementColorModCount);
                    AnnouncementItems.Add(announcement);
                    index++;
                }
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                HandleException(ex, "Bağlantı sağlanırken hata oluştu.");
            }
        }
    }
}