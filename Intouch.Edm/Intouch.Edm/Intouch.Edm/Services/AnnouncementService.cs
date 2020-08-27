using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.AnnouncementListDto;

namespace Intouch.Edm.Services
{
    public class AnnouncementService
    {
        internal static Announcement GetAnnouncement(Item announcementItem)
        {
            var userInfoJobTitle = !string.IsNullOrEmpty(announcementItem.creatorJobTitle) ? "(" + announcementItem.creatorJobTitle + ")" : string.Empty;
            Announcement announcement = new Announcement
            {
                Id = announcementItem.id,
                Description = announcementItem.text,
                Title = announcementItem.title,
                RecordDate = announcementItem.createTime.ToString("dd MMMM yyyy HH:mm"),
                AnnouncementUserInfo = $"{announcementItem.creatorUserName} {userInfoJobTitle}",
                ShortDescription = announcementItem.text?.Length <= 50 ? announcementItem.text : $"{announcementItem.text.Substring(0, 50)}..."
            };
            return announcement;
        }
    }
}