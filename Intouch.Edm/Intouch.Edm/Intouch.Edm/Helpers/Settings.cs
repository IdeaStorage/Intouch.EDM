using Intouch.Edm.Models;
using Intouch.Edm.Services;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Intouch.Edm.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        private const string ApiAuthTokenKey = "apitoken_key";
        private static readonly string AuthTokenDefault = string.Empty;

        private const string ApiRefreshTokenKey = "refreshtoken_key";
        private static readonly string RefreshTokenDefault = string.Empty;

        private const string ExpireInSecondsKey = "expiretoken_key";
        private static readonly string ExpireInSecondsDefault = string.Empty;

        private const string UserIdKey = "userid_key";
        private static readonly string UserIdDefault = string.Empty;

        private const string UserRoleTokenKey = "user_role";
        private static readonly string UserRoleTokenDefault = string.Empty;

        private const string LoginDateKey = "login_key";
        private static readonly string LoginDateDefault = string.Empty;

        private const string UsernameKey = "username_key";
        private static readonly string UsernameDefault = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordDefault = string.Empty;

        private const string FirebaseNotificationKey = "firebasenotification_key";
        private static readonly string FirebaseNotificationDefault = string.Empty;

        private const string ContentIdKey = "contentid_key";
        private static readonly string ContentIdDefault = string.Empty;

      

        private const string FileNameKey = "filename_key";
        private static readonly string FileNameDefault = string.Empty;

        private const string ContentLengthKey ="contentlength_key";
        private static readonly string ContentLengthDefault = string.Empty;

        private const string ContentTypeKey = "contenttype_key";
        private static readonly string ContentTypeDefault = string.Empty;

        private const string DisplayFileNameKey = "displayfilename_key";
        private static readonly string DisplayFileNameDefault = string.Empty;
        public static string StatusIdKey = "statusid_key";
        public static string StatusIdDefault = string.Empty;

        public static string StatusId
        {
            get => AppSettings.GetValueOrDefault(StatusIdKey, StatusIdDefault);
            set => AppSettings.AddOrUpdateValue(StatusIdKey, value);
        }
        public static string ContentId
        {
            get => AppSettings.GetValueOrDefault(ContentIdKey, ContentIdDefault);
            set => AppSettings.AddOrUpdateValue(ContentIdKey, value);
        }
        public static string FileName
        {
            get => AppSettings.GetValueOrDefault(FileNameKey, FileNameDefault);
            set => AppSettings.AddOrUpdateValue(FileNameKey, value);
        }
        public static string ContentLength
        {
            get => AppSettings.GetValueOrDefault(ContentLengthKey, ContentLengthDefault);
            set => AppSettings.AddOrUpdateValue(ContentLengthKey, value);
        }
        public static string ContentType
        {
            get => AppSettings.GetValueOrDefault(ContentTypeKey, ContentTypeDefault);
            set => AppSettings.AddOrUpdateValue(ContentTypeKey, value);
        }
        public static string DisplayFileName
        {
            get => AppSettings.GetValueOrDefault(DisplayFileNameKey, DisplayFileNameDefault);
            set => AppSettings.AddOrUpdateValue(DisplayFileNameKey, value);
        }
        public static string AuthenticationToken
        {
            get => AppSettings.GetValueOrDefault(ApiAuthTokenKey, AuthTokenDefault);
            set => AppSettings.AddOrUpdateValue(ApiAuthTokenKey, value);
        }
        public static string UserRole
        {
            get => AppSettings.GetValueOrDefault(UserRoleTokenKey, UserRoleTokenDefault);
            set => AppSettings.AddOrUpdateValue(UserRoleTokenKey, value);
        }
        public static string RefreshToken
        {
            get => AppSettings.GetValueOrDefault(ApiRefreshTokenKey, RefreshTokenDefault);
            set => AppSettings.AddOrUpdateValue(ApiRefreshTokenKey, value);
        }
        public static int ExpireInSeconds
        {
            get => Convert.ToInt32(AppSettings.GetValueOrDefault(ExpireInSecondsKey, ExpireInSecondsDefault));
            set => AppSettings.AddOrUpdateValue(ExpireInSecondsKey, value);
        }
        public static DateTime LoginDate
        {
            get => Convert.ToDateTime(AppSettings.GetValueOrDefault(LoginDateKey, LoginDateDefault));
            set => AppSettings.AddOrUpdateValue(LoginDateKey, value);
        }

        internal static void SetPermission(Edm.Models.Dtos.PermissionDto.RootObject responsePermission)
        {
            var permissionList = responsePermission.result.permissions;
            isNotificationAuthorize = permissionList.Contains("Pages.Notifications");
            isADKAuthorize = permissionList.Contains("Pages.CommiteeApprovals");
            isBusinessContinuityScenariosAuthorize = permissionList.Contains("Pages.BusinessContinuityScenarios");
            isEmergencyScenariosAuthorize = permissionList.Contains("Pages.EmergencyScenarios");
            isTasksAuthorize = permissionList.Contains("Pages.UserTasks");
            isTaskDetailAuthorize = permissionList.Contains("Pages.CheckedOptions");
        }

        public static void SetTokenInformation(ApiAuthenticationToken response)
        {
            AuthenticationToken = response.Result.AccessToken;
            RefreshToken = response.Result.RefreshToken;
            ExpireInSeconds = response.Result.ExpireInSeconds;
            LoginDate = DateTime.Now;
            UserId = response.Result.UserId.ToString();
        }
        public static void SetRefreshTokenInformation(ApiAuthenticationToken response)
        {
            AuthenticationToken = response.Result.AccessToken;
            ExpireInSeconds = response.Result.ExpireInSeconds;
            LoginDate = DateTime.Now;
        }
        public static string UserId
        {
            get => (AppSettings.GetValueOrDefault(ExpireInSecondsKey, ExpireInSecondsDefault));
            set => AppSettings.AddOrUpdateValue(ExpireInSecondsKey, value);
        }
        public static string Username
        {
            get => AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault);
            set => AppSettings.AddOrUpdateValue(UsernameKey, value);
        }
        public static string Password
        {
            get => AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
            set => AppSettings.AddOrUpdateValue(PasswordKey, value);
        }
        public static string FirebaseNotification
        {
            get => AppSettings.GetValueOrDefault(FirebaseNotificationKey, FirebaseNotificationDefault);
            set => AppSettings.AddOrUpdateValue(FirebaseNotificationKey, value);
        }
        public static void SetPictureInformation(ApiAuthenticationToken response)
        {
            AuthenticationToken = response.Result.AccessToken;
            RefreshToken = response.Result.RefreshToken;
            ExpireInSeconds = response.Result.ExpireInSeconds;
            LoginDate = DateTime.Now;
            UserId = response.Result.UserId.ToString();
        }
        
        public static bool isADKAuthorize { get; set; }
        public static bool isNotificationAuthorize { get; set; }
        public static bool isBusinessContinuityScenariosAuthorize { get; set; }
        public static bool isEmergencyScenariosAuthorize { get; set; }
        public static bool isTasksAuthorize { get; set; }
        public static bool isTaskDetailAuthorize { get; set; }
      

        internal static void SetUploadResult(UploadResult resultPicture)
        {
            ContentId = resultPicture.ContentId;
            ContentLength = resultPicture.ContentLength.ToString();
            ContentType= resultPicture.ContentType;
            DisplayFileName= resultPicture.DisplayFileName;
            FileName = resultPicture.FileName;
        }
        internal static Edm.Models.Dtos.CreateScenario.CreateEmergencyScenario.Picture GetPictureDetail()
        {
            Edm.Models.Dtos.CreateScenario.CreateEmergencyScenario.Picture picture = new Edm.Models.Dtos.CreateScenario.CreateEmergencyScenario.Picture
            {
                contentId = ContentId,
                contentLength = Convert.ToInt32(ContentLength),
                contentType = ContentType,
                displayFileName = DisplayFileName,
                fileName = FileName
            };
            return picture;
        }
    }
}
