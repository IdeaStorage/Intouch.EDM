﻿using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.CreateScenario;
using Intouch.Edm.ViewModels;
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
        Task<Dtos.UserDto.Root> GetUser(int userId);
        Task<Dtos.TaskOptionDto.RootObject> GetTaskOptionsAsync(string taskId);

        Task<IList<TaskItem>> GetTaskAsync(string taskId);

        Task<ApiAuthenticationToken> RefreshTokenAsync(string refreshToken = null);

        Task<Scenario> AddNotificationAsync(Scenario notification);

        Task<Dtos.ViewScenario.RootObject> GetScenarioDetailAsync(string scenarioId);

        Task<Role> GetRoleAsync(int userId);

        Task<Dtos.SourceLookupDto.RootObject> GetSourcesAsync(int eventId);

        Task<Dtos.LookupDto.ImpactAreaLookup.RootObject> GetImpactAreaAsync(int locationId);

        Task<Dtos.LookupDto.RootObject> GetSubjectsAsync();

        Task<Dtos.LookupDto.EventTypeLookup.RootObject> GetEventsAsync(int subjectTypeId);

        Task<Dtos.LookupDto.LocationLookup.RootObject> GetLocationAsync(double latitude, double longitude);

        Task<Edm.Models.Dtos.Scenario.RootObject> GetEmergencyScenario(int? approveStatusId, int? maxResultCount, int skipCount);

        Task<Dtos.Scenario.RootObject> GetScenarioAsync(int? approveStatusId, int? maxResultCount, int skipCount);

        Task<Dtos.NewScenarioResultDto.Result> CreateEmergencyScenario(CreateEmergencyScenario.RootObject scenario);

        Task<Dtos.ApproveScenarioReturnDto.RootObject> ApproveScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario);

        Task<bool> RejectScenario(Dtos.ApproveScenario.ApproveScenarioDto scenario);

        Task<Dtos.AnnouncementListDto.RootObject> GetAnnouncementsAsync(int maxResultCount, int skipCount);

        Task<Dtos.AnnouncementCountDto.RootObject> GetAnnouncementsCountAsync();

        Task<bool> SaveFirebaseTokenAsync(UserMobileAppTokenInput userMobileAppTokenInput);

        Task<bool> CreateAnnouncementAsync(Dtos.CreateAnnouncementDto.CreateAnnouncementDto announcementDto);

        Task<bool> UpdateTaskOptions(Dtos.TaskOptionUpdateDto.RootObject updateTaskOption);

        Task<UploadResult> UploadImageAsync(Stream image, string fileName);

        Task<Dtos.PermissionDto.RootObject> GetPermissionsAsync(int userId);

        Task<bool> SetReadNotification(Dtos.AnnouncementSetReadDto.AnnouncementSetDto notificaon);

        Task<bool> SetReadNotifications();

        Task<Dtos.JobTiteDto.Root> GetJobTitlesAsync();
        Task<Dtos.UnitDto.Root> GetUnitsAsync();

        Task<Dtos.ScenarioTaskOptionsDto.Root> GetScenarioTaskOptions(string commiteApprovalId, int? userId = null);

        Task<bool> CreateOrUpdateUserAsync(Dtos.UserDto.UpdateUserInfo.Root userInfo);
    }
}