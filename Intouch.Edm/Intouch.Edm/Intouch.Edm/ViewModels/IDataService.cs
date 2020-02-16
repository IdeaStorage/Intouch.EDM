
using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.CreateScenario;
using Intouch.Edm.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dtos = Intouch.Edm.Models.Dtos;

namespace Intouch.Edm.Services
{
    public interface IDataService
    {
        Task<ApiAuthenticationToken> GetAuthTokenAsync(User user);
        Task<Dtos.TaskListDto.RootObject> GetTasksAsync(int userId);

        Task<Dtos.TaskOptionDto.RootObject> GetTaskOptionsAsync(string taskId);

        Task<IList<TaskItem>> GetTaskAsync(string taskId);
        Task<ApiAuthenticationToken> RefreshTokenAsync(string refreshToken = null);
        //Task<TaskDetail> GetTaskDetailAsync(string taskDetailId);
        Task<Scenario> AddNotificationAsync(Scenario notification);
        Task<Dtos.ViewScenario.RootObject> GetScenarioAsync(string scenarioId);
        Task<Role> GetRoleAsync(int userId);
        //Task<NotificationMessage> UpdateEntryAsync(NotificationMessage entry);
        //Task RemoveEntryAsync(NotificationMessage entry);

        //Task<IList<ComboboxItem>> GetSitesAsync();
        Task<Dtos.SourceLookupDto.RootObject> GetSourcesAsync(string eventId, int SubjectTypeId);

        Task<Dtos.LookupDto.ImpactAreaLookup.RootObject> GetImpactAreaAsync(int SubjectTypeId);
        Task<Dtos.LookupDto.RootObject> GetSubjectsAsync();
        Task<Dtos.LookupDto.EventTypeLookup.RootObject> GetEventsAsync(int subjectTypeId);
        Task<Dtos.LookupDto.LocationLookup.RootObject> GetLocationAsync();

        Task<Edm.Models.Dtos.Scenario.RootObject> GetEmergencyScenario(int? approveStatusId);

        Task<Dtos.Scenario.RootObject> GetScenarioAsync(int? approveStatusId);

        Task<Dtos.Scenario.RootObject> GetBusinessContinuityScenario();

        Task<bool> CreateEmergencyScenario(CreateEmergencyScenario.RootObject scenario);

        Task<Dtos.ApproveScenarioReturnDto.RootObject> ApproveScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario);

        Task<bool> RejectScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario);

        Task<Dtos.AnnouncementListDto.RootObject> GetAnnouncementsAsync();

        Task<Dtos.AnnouncementCountDto.RootObject> GetAnnouncementsCountAsync();
        Task<bool> SaveFirebaseTokenAsync(UserMobileAppTokenInput userMobileAppTokenInput);
        Task<bool> CreateAnnouncementAsync(Dtos.CreateAnnouncementDto.CreateAnnouncementDto scenario);
        Task<bool> UpdateTaskOptions(Dtos.TaskOptionUpdateDto.RootObject updateTaskOption);

        Task<bool> CreatePictureAsync(Byte[] byte12);
        Task<UploadResult> UploadImageAsync(Stream image, string fileName);

        Task<Dtos.PermissionDto.RootObject> GetPermissionsAsync(int userId);

        Task<bool> SetReadNotification(Dtos.AnnouncementSetReadDto.AnnouncementSetDto notificaon);

        Task<bool> SetReadNotifications();
    }
}
