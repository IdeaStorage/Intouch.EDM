using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.CreateScenario;
using Intouch.Edm.Models.Dtos.LookupDto;
using Intouch.Edm.Models.Enums;
using Intouch.Edm.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private struct IdProviderToken
        {
            [JsonProperty("sessionToken")]
            public string AccessToken { get; set; }
        }

        public DataService(Uri baseUri, string authToken)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();

            _headers.Add("authorization", PrepareBearerString(authToken));
        }

        private string PrepareBearerString(string authToken)
        {
            return "Bearer " + authToken;
        }

        public async Task<Scenario> AddNotificationAsync(Scenario scenario)
        {
            var url = new Uri(_baseUri, "/api/services/app/UserTasks/GetAllByUser");
            var response = await SendRequestAsync<Scenario>(url, HttpMethod.Post, _headers, scenario);
            return response;
            // var response = await SaveEntityToJsonAsync<Scenario>();
            // return response;
        }

        public async Task<Dtos.ViewScenario.RootObject> GetScenarioAsync(string scenarioId)
        {
            var url = new Uri(_baseUri, string.Format("/api/services/app/Scenarios/GetScenarioForView?id={0}", scenarioId));
            var response = await SendRequestAsync<Dtos.ViewScenario.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.TaskListDto.RootObject> GetTasksAsync(int userId)
        {
            var url = new Uri(_baseUri, $"/api/services/app/UserTasks/GetAllByUser?UserId={userId}");
            var response = await SendRequestAsync<Dtos.TaskListDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.TaskOptionDto.RootObject> GetTaskOptionsAsync(string taskId)
        {
            var url = new Uri(_baseUri, $"/api/services/app/CheckedOptions/GetAll?UserTaskId={taskId}");
            var response = await SendRequestAsync<Dtos.TaskOptionDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<IList<TaskItem>> GetTaskAsync(string taskId)
        {
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
                refreshToken != null ? refreshToken : Edm.Helpers.Settings.RefreshToken));
            var response = await SendRequestAsync<ApiAuthenticationToken>(url, HttpMethod.Post, _headers);
            _headers.Remove("authorization");
            _headers.Add("authorization", PrepareBearerString(response.Result.AccessToken));
            if (refreshToken == null)
                Edm.Helpers.Settings.SetRefreshTokenInformation(response);

            return response;
        }

        public Task<Role> GetRoleAsync(int userId)
        {
            throw new NotImplementedException();
        }

        async public Task<IList<ComboboxItem>> GetImpactAreasAsync()
        {
            var url = new Uri(_baseUri, string.Format("/api/services/app/ImpactAreas/GetAll?SiteId=1&DisablePaging=true"));
            var response = await SendRequestAsync<ImpactAreaDto[]>(url, HttpMethod.Get, _headers);
            var query = response.FirstOrDefault().Items.Select(x => new ComboboxItem()
            { Name = x.ImpactArea.Name, Id = x.ImpactArea.Id });
            return query.ToList();
        }

        async public Task<Dtos.LookupDto.LocationLookup.RootObject> GetLocationAsync()
        {
            var url = new Uri(_baseUri, "/api/services/app/Sites/GetAll?DisablePaging=true");
            var response = await SendRequestAsync<Dtos.LookupDto.LocationLookup.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.EventTypeLookup.RootObject> GetEventsAsync(int SubjectTypeId)
        {
            Uri url = null;
            if (SubjectTypeId != Convert.ToInt32(Subjects.BusinessContuniuty))
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

        async public Task<Dtos.SourceLookupDto.RootObject> GetSourcesAsync(string eventId, int SubjectTypeId)
        {
            Uri url = null;
            if (SubjectTypeId != Convert.ToInt32(Subjects.BusinessContuniuty))
            {
                url = new Uri(_baseUri, $"/api/services/app/Sources/GetAllByEventId?EventId={eventId}&DisablePaging=true");
            }
            else
            {
                url = new Uri(_baseUri, $"/api/services/app/Sources/GetAllByEventId?EventId={eventId}&DisablePaging=true");
            }
            var response = await SendRequestAsync<Dtos.SourceLookupDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.ImpactAreaLookup.RootObject> GetImpactAreaAsync(int locationId)
        {
            Uri url = null;
            url = new Uri(_baseUri, $"/api/services/app/ImpactAreas/GetAll?siteId={locationId}&DisablePaging=true");
            //if (SubjectTypeId != Convert.ToInt32(Subjects.BusinessContuniuty))
            //{
            //    url = new Uri(_baseUri, $"/api/services/app/ImpactAreas/GetAll?");
            //}
            //else
            //{
            //    url = new Uri(_baseUri, $"/api/services/app/ImpactAreas/GetAll");
            //}
            var response = await SendRequestAsync<Dtos.LookupDto.ImpactAreaLookup.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        async public Task<Dtos.LookupDto.RootObject> GetSubjectsAsync()
        {
            var url = new Uri(_baseUri, "/api/services/app/Scenarios/GetAllSourceForLookupTable");
            var response = await SendRequestAsync<Dtos.LookupDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.Scenario.RootObject> GetEmergencyScenario(int? approveStatusId)
        {
            var url = new Uri(_baseUri, $"/api/services/app/CommiteeApprovals/GetAllForTree?ApproveStatus={approveStatusId}&DisablePaging=true");
            var response = await SendRequestAsync<Dtos.Scenario.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Dtos.Scenario.RootObject> GetScenarioAsync(int? approveStatusId)
        {
            Task<Dtos.Scenario.RootObject> EmergencyScenarios = GetEmergencyScenario(approveStatusId);
            //Task<Dtos.Scenario.RootObject> businessContuniutyScenarios = GetBusinessContinuityScenario();
            //EmergencyScenarios.Result.result.items.AddRange(businessContuniutyScenarios.Result.result.items);
            return await EmergencyScenarios;
        }

        public async Task<Dtos.Scenario.RootObject> GetBusinessContinuityScenario()
        {
            var url = new Uri(_baseUri, string.Format("/api/services/app/BusinessContinuityScenarios/GetAll"));
            var response2 = await SendRequestAsync<TaskDto[]>(url, HttpMethod.Get, _headers);
            return null;
        }

        public async Task<bool> CreateEmergencyScenario(CreateEmergencyScenario.RootObject scenario)
        {
            var url = new Uri(_baseUri, string.Format("/api/services/app/Scenarios/Notify"));
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, scenario);
            return response.success;
        }

        public async Task<Dtos.ApproveScenarioReturnDto.RootObject> ApproveScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario)
        {
            var url = new Uri(_baseUri, "/api/services/app/CommiteeApprovals/ApproveScenario");
            var response = await SendRequestAsync<Dtos.ApproveScenarioReturnDto.RootObject>(url, HttpMethod.Post, _headers, scenario);
            return response;
        }

        public async Task<bool> RejectScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario)
        {
            var url = new Uri(_baseUri, "/api/services/app/CommiteeApprovals/RejectScenario");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, scenario);
            return response?.success == null ? false : response.success;
        }

        public async Task<Dtos.AnnouncementListDto.RootObject> GetAnnouncementsAsync()
        {
            try
            {
                var url = new Uri(_baseUri, "/api/services/app/Announcements/GetUserAnnouncements?DisablePaging=true");
                var response = await SendRequestAsync<Dtos.AnnouncementListDto.RootObject>(url, HttpMethod.Get, _headers);

                return response;
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public async Task<Dtos.AnnouncementCountDto.RootObject> GetAnnouncementsCountAsync()
        {
            var url = new Uri(_baseUri, "/api/services/app/Announcements/GetUserAnnouncementsUnreadCount");
            var response = await SendRequestAsync<Dtos.AnnouncementCountDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<bool> CreateAnnouncementAsync(Dtos.CreateAnnouncementDto.CreateAnnouncementDto createAnnouncement)
        {
            var url = new Uri(_baseUri, "/api/services/app/Announcements/SendAnnouncement");
            var response = await SendRequestAsync<Dtos.NewAnnouncementReturnDto.RootObject>(url, HttpMethod.Post, _headers, createAnnouncement);
            return response.success;
        }

        public async Task<bool> UpdateTaskOptions(Dtos.TaskOptionUpdateDto.RootObject updateTaskOption)
        {
            var url = new Uri(_baseUri, "/api/services/app/UserTasks/Update");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Put, _headers, updateTaskOption);
            return response.success;
        }

        public async Task<bool> CreatePictureAsync(Byte[] byte12)
        {
            return true;
        }

        public async Task<UploadResult> UploadImageAsync(Stream image, string fileName)
        {
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
                    throw new Exception("Hata oluştu");
                }

                return files.Result.FirstOrDefault();
            }
        }

        private async System.Threading.Tasks.Task ControlAccessTokenAsync()
        {
            int secondDifference = 1800;//  30 dk eklendiginde verilen sure doluyorsa  token yenilenir.
            if ((Helpers.Settings.LoginDate.AddSeconds(Edm.Helpers.Settings.ExpireInSeconds) - DateTime.Now).Hours < 1)
            {
                await RefreshTokenAsync();
            }
        }

        public async Task<bool> SaveFirebaseTokenAsync(UserMobileAppTokenInput userMobileAppTokenInput)
        {
            var url = new Uri(_baseUri, "/api/services/app/UserMobileAppTokens/CreateOrEdit");
            var response = await SendRequestAsync<Edm.Models.Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, userMobileAppTokenInput);
            return response.success;
        }

        public async Task<Dtos.PermissionDto.RootObject> GetPermissionsAsync(int userId)
        {
            var url = new Uri(_baseUri, $"/api/services/app/User/GetUserPermissions?Id={userId}");
            var response = await SendRequestAsync<Dtos.PermissionDto.RootObject>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<bool> SetReadNotification(Dtos.AnnouncementSetReadDto.AnnouncementSetDto notificaon)
        {
            var url = new Uri(_baseUri, "/api/services/app/Notification/SetNotificationAsRead");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers, notificaon);
            return response.success;
        }

        public async Task<bool> SetReadNotifications()
        {
            var url = new Uri(_baseUri, "/api/services/app/Announcements/SetAllAnnouncementsAsRead");
            var response = await SendRequestAsync<Dtos.CreateScenario.RootObject>(url, HttpMethod.Post, _headers);
            return response.success;
        }

        /*
public async Task<Role> GetRoleAsync(int userId)
{
await ControlAccessTokenAsync();

var url = new Uri(_baseUri, "/api/Role");
var response = await SendRequestAsync<Role>(url,
HttpMethod.Get, _headers);
return response;
}

private async System.Threading.Tasks.Task ControlAccessTokenAsync()
{
int secondDifference = -86360;
if (Helpers.Settings.LoginDate.AddSeconds(Edm.Helpers.Settings.ExpireInSeconds).AddSeconds(secondDifference) > DateTime.Now)
{
await RefreshTokenAsync();
}
}

async public Task<IList<ComboboxItem>> GetSitesAsync()
{
var response = await GetEntityFromJsonAsync<Site[]>();
var query = from res in response
select new ComboboxItem() { Name = res.Name, Id = res.Id};

return query.ToList();
}

*/
    }
}