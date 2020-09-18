using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.CreateScenario;
using Intouch.Edm.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dtos = Intouch.Edm.Models.Dtos;

namespace Intouch.Edm.Services
{
    public class DataService : BaseHttpService, IDataService
    {
        private readonly Uri _baseUri;
        private readonly IDictionary<string, string> _headers;

        public DataService(Uri baseUri, string authToken)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>
            {
                { "authorization", PrepareBearerString(authToken) }
            };
        }

        private string PrepareBearerString(string authToken)
        {
            return "Bearer " + authToken;
        }

        public async Task<Scenario> AddNotificationAsync(Scenario notification)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/UserTasks/GetAllByUser");
            var response = await SendRequestAsync<Scenario>(url, HttpMethod.Post, _headers, notification);
            return response;
        }

        public async Task<Dtos.ViewScenario.RootObject> GetScenarioDetailAsync(string scenarioId)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, string.Format("/api/services/app/Scenarios/GetScenarioForView?id={0}", scenarioId));
            var response = await SendRequestAsync<Dtos.ViewScenario.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.TaskListDto.RootObject> GetTasksAsync(int userId)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, $"/api/services/app/UserTasks/GetAllByUser?UserId={userId}");
            var response = await SendRequestAsync<Dtos.TaskListDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.TaskOptionDto.RootObject> GetTaskOptionsAsync(string taskId)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, $"/api/services/app/CheckedOptions/GetAll?UserTaskId={taskId}");
            var response = await SendRequestAsync<Dtos.TaskOptionDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<IList<TaskItem>> GetTaskAsync(string taskId)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, string.Format("/api/services/app/UserTasks/GetUserTaskForEdit?id={0}", taskId));
            var response = await SendRequestAsync<TaskItem[]>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<ApiAuthenticationToken> GetAuthTokenAsync(User user)
        {
            _headers.Remove("authorization");
            var url = new Uri(_baseUri, "/api/TokenAuth/Authenticate");
            var response = await SendRequestAsync<ApiAuthenticationToken>(url, HttpMethod.Post, _headers, user);
            if (response.Success)
            {
                _headers.Add("authorization", PrepareBearerString(response.Result.AccessToken));
            }

            return response;
        }

        public async Task<ApiAuthenticationToken> RefreshTokenAsync(string refreshToken = null)
        {
            var url = new Uri(_baseUri, string.Format("/api/TokenAuth/RefreshToken?refreshToken={0}",
                refreshToken ?? Helpers.Settings.RefreshToken));
            var response = await SendRequestAsync<ApiAuthenticationToken>(url, HttpMethod.Post, _headers, null, false);
            _headers.Remove("authorization");
            _headers.Add("authorization", PrepareBearerString(response.Result.AccessToken));
            if (refreshToken == null)
            {
                Helpers.Settings.SetRefreshTokenInformation(response);
            }

            return response;
        }

        public Task<Role> GetRoleAsync(int userId)
        {
            throw new NotImplementedException();
        }

        async public Task<IList<ComboboxItem>> GetImpactAreasAsync()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/ImpactAreas/GetAll?SiteId=1&DisablePaging=true");
            var response = await SendRequestAsync<ImpactAreaDto[]>(url, HttpMethod.Get, _headers);
            var query = response.FirstOrDefault().Items.Select(x => new ComboboxItem()
            { Name = x.ImpactArea.Name, Id = x.ImpactArea.Id });
            return query.ToList();
        }

        async public Task<Dtos.LookupDto.LocationLookup.RootObject> GetLocationAsync(double latitude, double longitude)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, $"/api/services/app/Sites/GetAll?DisablePaging=true&Latitude={latitude}&Longitude={longitude}");
            var response = await SendRequestAsync<Dtos.LookupDto.LocationLookup.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.EventTypeLookup.RootObject> GetEventsAsync(int subjectTypeId)
        {
            await ControlAccessTokenAsync();

            Uri url = null;
            if (subjectTypeId != Convert.ToInt32(Subjects.BusinessContuniuty))
            {
                url = new Uri(_baseUri, "/api/services/app/EventTypes/GetAll?SubjectTypeId=1&DisablePaging=true");
            }
            else
            {
                url = new Uri(_baseUri, "/api/services/app/EventTypes/GetAll?SubjectTypeId=2&DisablePaging=true");
            }
            var response = await SendRequestAsync<Dtos.LookupDto.EventTypeLookup.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.SourceLookupDto.RootObject> GetSourcesAsync(int eventId)
        {
            await ControlAccessTokenAsync();

            Uri url = null;
            if (eventId != Convert.ToInt32(Events.Other))
            {
                url = new Uri(_baseUri, $"/api/services/app/Sources/GetAllByEventId?EventId={eventId}&DisablePaging=true");
            }
            else
            {
                url = new Uri(_baseUri, $"/api/services/app/Sources/GetAllByEventId?DisablePaging=true");
            }
            var response = await SendRequestAsync<Dtos.SourceLookupDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.ImpactAreaLookup.RootObject> GetImpactAreaAsync(int locationId)
        {
            await ControlAccessTokenAsync();

            Uri url = null;
            url = new Uri(_baseUri, $"/api/services/app/ImpactAreas/GetAll?siteId={locationId}&DisablePaging=true");
            var response = await SendRequestAsync<Dtos.LookupDto.ImpactAreaLookup.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.RootObject> GetSubjectsAsync()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Scenarios/GetAllSourceForLookupTable");
            var response = await SendRequestAsync<Dtos.LookupDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.Scenario.RootObject> GetEmergencyScenario(int? approveStatusId, int? maxResultCount, int skipCount)
        {
            await ControlAccessTokenAsync();
            var url = $"/api/services/app/CommiteeApprovals/GetAllForTree?ApproveStatusFilter={approveStatusId}&skipCount={skipCount}";

            if (maxResultCount != null)
            {
                url = $"{url}&MaxResultCount={maxResultCount}";
            }
            var response = await SendRequestAsync<Dtos.Scenario.RootObject>(new Uri(_baseUri, url), HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.Scenario.RootObject> GetScenarioAsync(int? approveStatusId, int? maxResultCount, int skipCount)
        {
            await ControlAccessTokenAsync();

            Task<Dtos.Scenario.RootObject> EmergencyScenarios = GetEmergencyScenario(approveStatusId, maxResultCount, skipCount);
            return await EmergencyScenarios;
        }

        public async Task<Dtos.NewScenarioResultDto.Result> CreateEmergencyScenario(CreateEmergencyScenario.RootObject scenario)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Scenarios/Notify");
            var response = await SendRequestAsync<Dtos.NewScenarioResultDto.Root>(url, HttpMethod.Post, _headers, scenario);
            return response.result;
        }

        public async Task<Dtos.ApproveScenarioReturnDto.RootObject> ApproveScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/CommiteeApprovals/ApproveScenario");
            var response = await SendRequestAsync<Dtos.ApproveScenarioReturnDto.RootObject>(url, HttpMethod.Post, _headers, scenario);
            return response;
        }

        public async Task<bool> RejectScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/CommiteeApprovals/RejectScenario");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, scenario);
            return response?.success != null && response.success;
        }

        public async Task<Dtos.AnnouncementListDto.RootObject> GetAnnouncementsAsync(int maxResultCount, int skipCount)
        {
            await ControlAccessTokenAsync();

            var url = $"/api/services/app/Announcements/GetUserAnnouncements?MaxResultCount={maxResultCount}&SkipCount={skipCount}";
            var response = await SendRequestAsync<Dtos.AnnouncementListDto.RootObject>(new Uri(_baseUri, url), HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.AnnouncementCountDto.RootObject> GetAnnouncementsCountAsync()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Announcements/GetUserAnnouncementsUnreadCount");
            var response = await SendRequestAsync<Dtos.AnnouncementCountDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<bool> CreateAnnouncementAsync(Dtos.CreateAnnouncementDto.CreateAnnouncementDto announcementDto)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Announcements/SendAnnouncement");
            var response = await SendRequestAsync<Dtos.NewAnnouncementReturnDto.RootObject>(url, HttpMethod.Post, _headers, announcementDto);
            return response.success;
        }

        public async Task<bool> UpdateTaskOptions(Dtos.TaskOptionUpdateDto.RootObject updateTaskOption)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/UserTasks/Update");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Put, _headers, updateTaskOption);
            return response.success;
        }

        public async Task<UploadResult> UploadImageAsync(Stream image, string fileName)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/App/Upload/Save");
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = fileName };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(url, formData);
                var responseContent = await response.Content.ReadAsStringAsync();
                var files = JsonConvert.DeserializeObject<UploadFilesAjaxResponse>(responseContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Fotoğraf servise gönderilirken hata oluştu!");
                }

                return files.Result.FirstOrDefault();
            }
        }

        private async Task ControlAccessTokenAsync()
        {
            string loginTicks = Helpers.Settings.LoginDate;
            long loginDateTicks = long.Parse(loginTicks);
            DateTime loginDate = new DateTime(loginDateTicks);
            if ((DateTime.Now - loginDate).Seconds > 0)
            {
                await RefreshTokenAsync();
            }
        }

        public async Task<bool> SaveFirebaseTokenAsync(UserMobileAppTokenInput userMobileAppTokenInput)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/UserMobileAppTokens/CreateOrEdit");
            var response = await SendRequestAsync<Edm.Models.Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, userMobileAppTokenInput);
            return response.success;
        }

        public async Task<Dtos.PermissionDto.RootObject> GetPermissionsAsync(int userId)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, $"/api/services/app/User/GetUserPermissions?Id={userId}");
            var response = await SendRequestAsync<Dtos.PermissionDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<bool> SetReadNotification(Dtos.AnnouncementSetReadDto.AnnouncementSetDto notificaon)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Notification/SetNotificationAsRead");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, notificaon);
            return response.success;
        }

        public async Task<bool> SetReadNotifications()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Announcements/SetAllAnnouncementsAsRead");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers);
            return response.success;
        }

        public async Task<Dtos.UserDto.Root> GetUser(int userId)
        {
            await ControlAccessTokenAsync();
            var url = new Uri(_baseUri, $"/api/services/app/User/GetUserForEdit?Id={userId}");
            var response = await SendRequestAsync<Dtos.UserDto.Root>(url, HttpMethod.Get, _headers);
            return response;
        }

        async public Task<Dtos.JobTiteDto.Root> GetJobTitlesAsync()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/JobTitles/GetAll?DisablePaging=true");
            var response = await SendRequestAsync<Dtos.JobTiteDto.Root>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.UnitDto.Root> GetUnitsAsync()
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/Units/GetAll?DisablePaging=true");
            var response = await SendRequestAsync<Dtos.UnitDto.Root>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.ScenarioTaskOptionsDto.Root> GetScenarioTaskOptions(string commiteApprovalId, int? userId = null)
        {
            await ControlAccessTokenAsync();
            var url = userId == null ?
                new Uri(_baseUri, $"/api/services/app/CheckedOptions/GetScenarioTasksByCommiteeApprovalId?CommiteeApprovalId={commiteApprovalId}") :
                new Uri(_baseUri, $"/api/services/app/CheckedOptions/GetScenarioTasksByCommiteeApprovalId?CommiteeApprovalId={commiteApprovalId}&UserId={userId}");

            var response = await SendRequestAsync<Dtos.ScenarioTaskOptionsDto.Root>(url, HttpMethod.Get, _headers);
            return response;
        }

        public async Task<bool> CreateOrUpdateUserAsync(Dtos.UserDto.UpdateUserInfo.Root userInfo)
        {
            await ControlAccessTokenAsync();

            var url = new Uri(_baseUri, "/api/services/app/User/UpdateUserInfo");
            var response = await SendRequestAsync<Dtos.NewAnnouncementReturnDto.RootObject>(url, HttpMethod.Put, _headers, userInfo);
            return response.success;
        }
    }
}