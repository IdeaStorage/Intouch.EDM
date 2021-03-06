﻿using Intouch.Edm.Services;
using Intouch.Edm.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class UpdateUserInfoViewModel : BaseViewModel
    {
        public UpdateUserInfoViewModel()
        {
        }

        public async Task<Models.Dtos.UserDto.Root> GetUser(int userId, bool isSetTitleAndUnit = false)
        {
             var user = await DataService.GetUser(Convert.ToInt32(Helpers.Settings.UserId));

                if (user?.result.user != null)
                {
                    var userInfo = user.result.user;
                    UserInfoName = userInfo.name;
                    UserInfoSurname = userInfo.surname;
                    UserInfoMobilePhone = userInfo.phoneNumber;
                    UserInfoMailAddress = userInfo.emailAddress;
                    TitleId = Convert.ToInt32(userInfo.jobTitleId);
                    DepartmenId = Convert.ToInt32(userInfo.unitId);
                    if (isSetTitleAndUnit)
                    {
                        SelectedTitle = userInfo.jobTitleId != null && userInfo.jobTitleId != 0 ? TitleCombobox.FirstOrDefault(p => p.Id == Convert.ToInt32(userInfo.jobTitleId)) : null;
                        SelectedDepartment = userInfo.unitId != null && userInfo.unitId != 0 ? DepartmentCombobox.FirstOrDefault(p => p.Id == Convert.ToInt32(userInfo.unitId)) : null;
                    }
                    IsBusy = false;
                }
      
            return user;
        }

        private ICommand _saveCommand;

        public ICommand SaveClicked => _saveCommand
                ?? (_saveCommand = new Command(async () => await ExecuteSaveClicked()));

        private async Task ExecuteSaveClicked()
        {
            var result = await PopupSheet.ShowConfirmPopup(Application.Current.MainPage.Navigation, "Kaydetmek istediğinize emin misiniz?");

            if (result)
            {
                IsBusy = true;
                await UpdateUserInfo();
            }
        }

        private async Task UpdateUserInfo()
        {
            var departmentId = DepartmenId;
            var titleId = TitleId;
            var name = UserInfoName;
            var surname = UserInfoSurname;
            var phone = UserInfoMobilePhone;
            var email = UserInfoMailAddress;

            var user = await GetUser(Convert.ToInt32(Helpers.Settings.UserId));
            if (user?.result.user != null)
            {
                var userInfo = user.result.user;
                userInfo.name = name;
                userInfo.surname = surname;
                userInfo.phoneNumber = phone;
                userInfo.emailAddress = email;
                userInfo.unitId = departmentId;
                userInfo.jobTitleId = titleId;
                try
                {
                    Models.Dtos.UserDto.UpdateUserInfo.Root updateUserInfo = new Models.Dtos.UserDto.UpdateUserInfo.Root
                    {
                        name = userInfo.name,
                        surname = userInfo.surname,
                        phoneNumber = userInfo.phoneNumber,
                        emailAddress = userInfo.emailAddress,
                        jobTitleId = Convert.ToInt32(userInfo.jobTitleId),
                        unitId = Convert.ToInt32(userInfo.unitId),
                        id = userInfo.id
                    };
                    var response = await DataService.CreateOrUpdateUserAsync(updateUserInfo);
                    if (response)
                    {
                        IsBusy = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("User Update edilirken hata alındı:" + ex.Message);
                    HandleException(ex, "User Update edilirken hata alındı.");
                    IsBusy = false;
                }
            }

            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        internal async Task Init()
        {
            IsBusy = true;
            try
            {
                await RetrieveTitles();
                await RetrieveDepartments();
                await GetUser(Convert.ToInt32(Helpers.Settings.UserId), true);
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                HandleException(ex, "Bağlantı sağlanırken hata oluştu.");
            }
        }

        #region DepartmenSelection

        private ObservableCollection<ComboboxItem> _DepartmentCombobox;

        public ObservableCollection<ComboboxItem> DepartmentCombobox
        {
            get { return _DepartmentCombobox; }
            set
            {
                _DepartmentCombobox = value;
                OnPropertyChanged();
            }
        }

        private ComboboxItem _selectedDepartment;

        public ComboboxItem SelectedDepartment
        {
            get
            {
                return _selectedDepartment;
            }
            set
            {
                SetProperty(ref _selectedDepartment, value);
                if (_selectedDepartment != null)
                {
                    DepartmenId = _selectedDepartment.Id;
                }
            }
        }

        private int? _departmentId;

        public int? DepartmenId
        {
            get
            {
                return _departmentId;
            }
            set
            {
                SetProperty(ref _departmentId, value);
            }
        }

        #endregion DepartmenSelection

        #region TitleSelection

        private ObservableCollection<ComboboxItem> _TitleCombobox;

        public ObservableCollection<ComboboxItem> TitleCombobox
        {
            get { return _TitleCombobox; }
            set
            {
                _TitleCombobox = value;
                OnPropertyChanged();
            }
        }

        private ComboboxItem _selectedTitle;

        public ComboboxItem SelectedTitle
        {
            get
            {
                return _selectedTitle;
            }
            set
            {
                SetProperty(ref _selectedTitle, value);
                if (_selectedTitle != null)
                {
                    TitleId = _selectedTitle.Id;
                }
            }
        }

        private int? _titleId;

        public int? TitleId
        {
            get
            {
                return _titleId;
            }
            set
            {
                SetProperty(ref _titleId, value);
            }
        }

        #endregion TitleSelection

        private string _userInfoName;

        public string UserInfoName
        {
            get
            {
                return _userInfoName;
            }
            set
            {
                SetProperty(ref _userInfoName, value);
            }
        }

        private string _userInfoSurname;

        public string UserInfoSurname
        {
            get
            {
                return _userInfoSurname;
            }
            set
            {
                SetProperty(ref _userInfoSurname, value);
            }
        }

        private string _userInfoMobilePhone;

        public string UserInfoMobilePhone
        {
            get
            {
                return _userInfoMobilePhone;
            }
            set
            {
                SetProperty(ref _userInfoMobilePhone, value);
            }
        }

        private string _userInfoMailAddress;

        public string UserInfoMailAddress
        {
            get
            {
                return _userInfoMailAddress;
            }
            set
            {
                SetProperty(ref _userInfoMailAddress, value);
            }
        }

        public async Task RetrieveDepartments()
        {
            var departments = await DataService.GetUnitsAsync();
            DepartmentCombobox = PickerService.GetDepartments(departments);
        }

        public async Task RetrieveTitles()
        {
            var titles = await DataService.GetJobTitlesAsync();
            TitleCombobox = PickerService.GetTitles(titles);
        }
    }
}