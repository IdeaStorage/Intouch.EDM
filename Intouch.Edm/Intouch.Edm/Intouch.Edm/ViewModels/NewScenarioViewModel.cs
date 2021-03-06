﻿using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.CreateScenario;
using Intouch.Edm.Services;
using Intouch.Edm.Views;
using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class NewScenarioViewModel : BaseViewModel
    {
        private ICommand _saveCommand;
        private CreateEmergencyScenario.Picture picture = new CreateEmergencyScenario.Picture();

        public GeoCoords gpsCoords = new GeoCoords();
        private bool isVisibleLocation = true;
        public string EventName;

        public bool IsVisibleLocation
        {
            get { return isVisibleLocation; }
            set { SetProperty(ref isVisibleLocation, value); }
        }

        public ICommand SaveClicked => _saveCommand
                ?? (_saveCommand = new Command(async () => await ExecuteSaveClicked()));

        private async Task ExecuteSaveClicked()
        {
            var result = await PopupSheet.ShowConfirmPopup(Application.Current.MainPage.Navigation, $"{EventName} Bildirimi Yapmak İstediğinize Emin Misiniz?");
            if (result)
            {
                await CreateNotification();
            }
        }

        internal async Task Init()
        {
            // Method intentionally left empty.
        }

        private async Task CreateNotification()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                GetLocationInfoBySelectedEventId(Convert.ToInt32(EventId));

                if (!gpsCoords.IsGpsClose)
                {
                    if (!string.IsNullOrEmpty(Helpers.Settings.ContentId))
                    {
                        picture = Helpers.Settings.GetPictureDetail();
                    }
                    else
                    {
                        picture = null;
                    }
                    var newItem = new CreateEmergencyScenario.RootObject()
                    {
                        userId = Convert.ToInt32(Helpers.Settings.UserId),
                        subjectType = SubjectId != 0 ? SubjectId : null,
                        eventTypeId = EventId != 0 ? EventId : null,
                        siteId = LocationId != 0 ? LocationId : null,
                        sourceId = SourceId != 0 ? SourceId : null,
                        impactAreaId = ImpactAreaId != 0 ? ImpactAreaId : null,
                        picture = picture,
                        latitude = gpsCoords.Latitude,
                        longitude = gpsCoords.Longitude
                    };

                    var resultCreateScenario = await DataService.CreateEmergencyScenario(newItem);
                    if (resultCreateScenario != null && resultCreateScenario.result)
                    {
                        Helpers.Settings.ContentId = null;
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("BAŞARILI", resultCreateScenario.message, "TAMAM");
                        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                    else if (resultCreateScenario != null && !resultCreateScenario.result)
                    {
                        await Application.Current.MainPage.DisplayAlert("BAŞARISIZ", resultCreateScenario.message, "TAMAM");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("BAŞARISIZ", "İşlem Sırasında Hata Alındı.", "TAMAM");
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public NewScenarioViewModel()
        {
            Initialize();
        }

        public NewScenarioViewModel(int selectedId)
        {
            IsBusy = true;
            Initialize(selectedId);
        }

        public async void RetrieveImpactArea(int? id)
        {
            var locationId = id ?? 0;
            var impactAreatList = await DataService.GetImpactAreaAsync(locationId);
            ImpactAreaCombobox = PickerService.GetImpactArea(impactAreatList);
        }

        public async Task RetrieveSource(int? id)
        {
            var eventId = id ?? 0;
            Models.Dtos.SourceLookupDto.RootObject sourceList = await DataService.GetSourcesAsync(eventId);
            SourceCombobox = PickerService.GetSource(sourceList);
        }

        private async void Initialize(int selectedEventId = 0)
        {
            int selectedSubjectId = selectedEventId != Convert.ToInt32(Events.BusinessContuniuty) ? Convert.ToInt32(Subjects.Emergency) : Convert.ToInt32(Subjects.BusinessContuniuty);
            IsVisibleLocation = true;

            GetLocationInfoBySelectedEventId(selectedEventId);

            Models.Dtos.SourceLookupDto.RootObject sourceList = await DataService.GetSourcesAsync(selectedEventId);
            Models.Dtos.LookupDto.EventTypeLookup.RootObject eventList = await DataService.GetEventsAsync(selectedSubjectId);
            Models.Dtos.LookupDto.LocationLookup.RootObject locationList = await DataService.GetLocationAsync(gpsCoords.Latitude, gpsCoords.Longitude);

            LocationCombobox = PickerService.GetLocation(locationList);
            SelectedLocation = LocationCombobox.FirstOrDefault(x => x.IsSelected);
            SubjectCombobox = PickerService.GetSubject();
            SourceCombobox = PickerService.GetSource(sourceList);
            EventCombobox = PickerService.GetEvent(eventList);

            ControlFormComponentsBySelectedEventId(selectedEventId);
            var eventDetail = TaskService.GetEvent(selectedEventId);
            EventIcon = eventDetail.eventIcon;
            EventName = eventDetail.eventName;
            IsBusy = false;
        }

        public async void GetLocationInfoBySelectedEventId(int selectedEventId = 0)
        {
            if (selectedEventId == Convert.ToInt32(Events.Earthquake))
            {
                IsVisibleLocation = IsImpactAreaEnabled = false;
            }
            var locationService = DependencyService.Get<ILocationService>();
            var position = await locationService.GetGeoCoordinatesAsync();
            gpsCoords = position;

            if (position == null || position.IsGpsClose)
            {
                await Application.Current.MainPage.DisplayAlert("GPS", "Lokasyon bilgisini almak için GPS i açınız!", "TAMAM");
            }
        }

        public void ControlFormComponentsBySelectedEventId(int selectedEventId)
        {
            SelectedSubject = new ComboboxItem();
            SelectedEvent = new ComboboxItem();

            if (selectedEventId == Convert.ToInt32(Events.WaterFlood) ||
                selectedEventId == Convert.ToInt32(Events.Fire) ||
                selectedEventId == Convert.ToInt32(Events.Earthquake) ||
                selectedEventId == Convert.ToInt32(Events.Pandemic))
            {
                IsEnabledEvent = IsEventEnabledEvent = false;

                if (selectedEventId == Convert.ToInt32(Events.Earthquake) && SourceCombobox.Count == 1)
                {
                    SelectedSource = SourceCombobox.First();
                }
                SelectedSubject = SubjectCombobox.First(p => p.Id == 1);
                SelectedEvent = EventCombobox.First(p => p.Id == selectedEventId);
            }
            else if (selectedEventId == Convert.ToInt32(Events.BusinessContuniuty))
            {
                SelectedSubject = SubjectCombobox.First(p => p.Id == 2);
                IsEventEnabledEvent = true;
                IsEnabledEvent = false;
            }
            else if (selectedEventId == Convert.ToInt32(Events.Other))
            {
                SelectedSubject = SubjectCombobox.First(p => p.Id == 1);
            }
        }

        private bool _isEnabledEvent = true;

        public bool IsEnabledEvent
        {
            get
            {
                return _isEnabledEvent;
            }
            set
            {
                SetProperty(ref _isEnabledEvent, value);
            }
        }

        private bool _isEventEnabledEvent = true;

        public bool IsEventEnabledEvent
        {
            get
            {
                return _isEventEnabledEvent;
            }
            set
            {
                SetProperty(ref _isEventEnabledEvent, value);
            }
        }

        private bool _isImpactAreaEnabled = true;
        public bool IsImpactAreaEnabled
        {
            get
            {
                return _isImpactAreaEnabled;
            }
            set
            {
                SetProperty(ref _isImpactAreaEnabled, value);
            }
        }

        private ComboboxItem _selectedEvent;

        public ComboboxItem SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                SetProperty(ref _selectedEvent, value);
                EventId = SelectedEvent.Id;
                if (SelectedEvent.Id != 0 && SelectedEvent.Id != Convert.ToInt32(Events.Earthquake))
                {
                    RetrieveSource(EventId);
                }
            }
        }

        private ComboboxItem _selectedSource;

        public ComboboxItem SelectedSource
        {
            get
            {
                return _selectedSource;
            }
            set
            {
                SetProperty(ref _selectedSource, value);
                SourceId = SelectedSource.Id;
            }
        }

        private ComboboxItem _selectedImpactArea;

        public ComboboxItem SelectedImpactArea
        {
            get
            {
                return _selectedImpactArea;
            }
            set
            {
                SetProperty(ref _selectedImpactArea, value);
                ImpactAreaId = SelectedImpactArea.Id;
            }
        }

        private ComboboxSelectedVersion _selectedLocation;

        public ComboboxSelectedVersion SelectedLocation
        {
            get
            {
                return _selectedLocation;
            }
            set
            {
                SetProperty(ref _selectedLocation, value);
                LocationId = SelectedLocation.Id;
                RetrieveImpactArea(LocationId);
            }
        }

        private ComboboxItem _selectedSubject;

        public ComboboxItem SelectedSubject
        {
            get
            {
                return _selectedSubject;
            }
            set
            {
                SetProperty(ref _selectedSubject, value);
                SubjectText = "Subject : " + _selectedSubject.Name;
                SubjectId = _selectedSubject.Id;
            }
        }

        private int? _subjectId;

        public int? SubjectId
        {
            get
            {
                return _subjectId;
            }
            set
            {
                SetProperty(ref _subjectId, value);
            }
        }

        private int? _eventId;

        public int? EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                SetProperty(ref _eventId, value);
            }
        }

        private string _eventIcon;

        public string EventIcon
        {
            get
            {
                return _eventIcon;
            }
            set
            {
                SetProperty(ref _eventIcon, value);
            }
        }

        private int? _siteId;

        public int? SiteId
        {
            get
            {
                return _siteId;
            }
            set
            {
                SetProperty(ref _siteId, value);
            }
        }

        private int? _sourceId;

        public int? SourceId
        {
            get
            {
                return _sourceId;
            }
            set
            {
                SetProperty(ref _sourceId, value);
            }
        }

        private int? _locationId;

        public int? LocationId
        {
            get
            {
                return _locationId;
            }
            set
            {
                SetProperty(ref _locationId, value);
            }
        }

        private int? _impactAreaId;

        public int? ImpactAreaId
        {
            get
            {
                return _impactAreaId;
            }
            set
            {
                SetProperty(ref _impactAreaId, value);
            }
        }

        private string _subjectText;

        public string SubjectText
        {
            get
            {
                return _subjectText;
            }
            set
            {
                SetProperty(ref _subjectText, value);
            }
        }

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        private ICommand _takePhotoClicked;

        public ICommand TakePhotoClicked => _takePhotoClicked
                ?? (_takePhotoClicked = new Command(async () => await TakePhotoCommand()));

        private async Task TakePhotoCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("UYARI", "Cihazınızın kamerası aktif değil!", "OK");
                return;
            }
            IsBusy = true;
            UploadResult resultPicture = null;
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(
                       new Plugin.Media.Abstractions.StoreCameraMediaOptions
                       {
                           Directory = "temp",
                           Name = DateTime.Now + ".jpg",
                           DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                           CompressionQuality = 50
                       });
                if (file == null)
                {
                    IsBusy = false;
                    return;
                }

                var imageFile = file;
                resultPicture = await DataService.UploadImageAsync(imageFile.GetStream(), $"Test_Photo - {DateTime.Now}" + ".jpg");

                Helpers.Settings.SetUploadResult(resultPicture);

                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("HATA", "Fotoğraf çekilirken hata oluştu", "OK");
            }
            IsBusy = false;

            if (resultPicture?.ContentId != null)
            {
                await Application.Current.MainPage.DisplayAlert("BAŞARILI", "Fotoğraf başarıyla kaydedildi.", "OK");
            }
        }

        private ICommand _uploadPhotoClicked;

        public ICommand UploadPhotoClicked => _uploadPhotoClicked
                ?? (_uploadPhotoClicked = new Command(async () => await UploadPhotoCommand()));

        private async Task UploadPhotoCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("UYARI", "Galeriye erişme yetkiniz yok!", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new
                                  Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 50
            });

            IsBusy = true;

            UploadResult resultPicture = null;

            try
            {
                if (file == null)
                {
                    IsBusy = false;
                    return;
                }

                var imageFile = file;
                resultPicture = await DataService.UploadImageAsync(imageFile.GetStream(), $"Test_Photo - {DateTime.Now}" + ".jpg");
                Helpers.Settings.SetUploadResult(resultPicture);

                ImageSource = ImageSource.FromStream(() =>
                  {
                      var stream = file.GetStream();
                      file.Dispose();
                      return stream;
                  });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("HATA", "Fotoğraf gönderilirken hata oluştu", "OK");
            }

            IsBusy = false;

            if (resultPicture?.ContentId != null)
            {
                await Application.Current.MainPage.DisplayAlert("BAŞARILI", "Fotoğraf başarıyla kaydedildi.", "OK");
            }
        }

        private ObservableCollection<ComboboxItem> _SubjectCombobox;

        public ObservableCollection<ComboboxItem> SubjectCombobox
        {
            get { return _SubjectCombobox; }
            set
            {
                _SubjectCombobox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComboboxItem> _EventCombobox;

        public ObservableCollection<ComboboxItem> EventCombobox
        {
            get { return _EventCombobox; }
            set
            {
                _EventCombobox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComboboxItem> _SourceCombobox;

        public ObservableCollection<ComboboxItem> SourceCombobox
        {
            get { return _SourceCombobox; }
            set
            {
                _SourceCombobox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComboboxItem> _ImpactAreaCombobox;

        public ObservableCollection<ComboboxItem> ImpactAreaCombobox
        {
            get { return _ImpactAreaCombobox; }
            set
            {
                _ImpactAreaCombobox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComboboxSelectedVersion> _LocationCombobox;

        public ObservableCollection<ComboboxSelectedVersion> LocationCombobox
        {
            get { return _LocationCombobox; }
            set
            {
                _LocationCombobox = value;
                OnPropertyChanged();
            }
        }
    }
}