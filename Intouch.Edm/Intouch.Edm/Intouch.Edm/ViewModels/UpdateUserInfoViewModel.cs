using Intouch.Edm.Services;
using Intouch.Edm.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class UpdateUserInfoViewModel : BaseViewModel
    {
        public UpdateUserInfoViewModel(int userId)
        {
            RetrieveTitles();
            RetrieveDepartments();
            var user = GetUser(userId);
            if (user?.Result.result.user != null)
            {
                var userInfo = user.Result.result.user;
                UserInfoName = userInfo.name;
                UserInfoSurname = userInfo.surname;
                UserInfoMobilePhone = userInfo.phoneNumber;
                UserInfoMailAddress = userInfo.emailAddress;
                TitleId = Convert.ToInt32(userInfo.jobTitleId);
                DepartmenId = Convert.ToInt32(userInfo.unitId);
            }
            else
            {
                UserInfoName = "SefaTest";
                UserInfoSurname = "SefaTest";
                UserInfoMobilePhone = "5548218081";
                UserInfoMailAddress = "m.sefaguller@gmail.com";
                TitleId = Convert.ToInt32(0);
                DepartmenId = Convert.ToInt32(0);
            }
        }

        public async Task<Models.Dtos.UserDto.Root> GetUser(int userId)
        {
            var user = await DataService.GetUser(userId);
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

            var user = GetUser(Convert.ToInt32(Helpers.Settings.UserId));
            if (user?.Result.result.user != null)
            {
                var userInfo = user.Result.result.user;
                userInfo.name = name;
                userInfo.surname = surname;
                userInfo.phoneNumber = phone;
                userInfo.emailAddress = email;
                userInfo.unitId = departmentId;
                userInfo.jobTitleId = titleId;
                try
                {
                    Models.Dtos.UserDto.UpdateUserInfo.Root updateUserInfo = new Models.Dtos.UserDto.UpdateUserInfo.Root();
                    updateUserInfo.user.name = userInfo.name;
                    updateUserInfo.user.surname = userInfo.surname;
                    updateUserInfo.user.phoneNumber = userInfo.phoneNumber;
                    updateUserInfo.user.emailAddress = userInfo.emailAddress;
                    updateUserInfo.user.jobTitleId = Convert.ToInt32(userInfo.jobTitleId);
                    updateUserInfo.user.unitId = Convert.ToInt32(userInfo.unitId);
                    updateUserInfo.user.password = Convert.ToString(userInfo.password);
                    updateUserInfo.user.isActive = userInfo.isActive;
                    updateUserInfo.user.shouldChangePasswordOnNextLogin = userInfo.shouldChangePasswordOnNextLogin;
                    updateUserInfo.user.isTwoFactorEnabled = userInfo.isTwoFactorEnabled;
                    updateUserInfo.user.isLockoutEnabled = userInfo.isLockoutEnabled;

                    await DataService.CreateOrUpdateUserAsync(updateUserInfo);
                }
                catch (Exception ex)
                {

                }
            }


            //await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            //Kullanıcı bilgi güncellemesi için PUT methodunun inputları burada hazırlanacak.
        }

        internal async Task Init()
        {
            // Method intentionally left empty.
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
                DepartmentText = "Department : " + _selectedDepartment.Name;
                DepartmenId = _selectedDepartment.Id;
            }
        }

        private string _departmentText;

        public string DepartmentText
        {
            get
            {
                return _departmentText;
            }
            set
            {
                SetProperty(ref _departmentText, value);
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
                TitleText = "Title : " + _selectedTitle.Name;
                TitleId = _selectedTitle.Id;
            }
        }

        private string _titleText;

        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                SetProperty(ref _titleText, value);
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

        public async void RetrieveDepartments()
        {
            var departments = await DataService.GetUnitsAsync();
            DepartmentCombobox = PickerService.GetDepartments(departments);
        }

        public async void RetrieveTitles()
        {
            var titles = await DataService.GetJobTitlesAsync();
            TitleCombobox = PickerService.GetTitles(titles);
        }
    }
}