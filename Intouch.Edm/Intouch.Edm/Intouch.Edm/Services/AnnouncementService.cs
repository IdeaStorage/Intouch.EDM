using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.AnnouncementListDto;

namespace Intouch.Edm.Services
{
    public class AnnouncementService
    {
        internal static Announcement GetAnnouncement(Item announcementItem)
        {
            Announcement announcement = new Announcement
            {
                Id = announcementItem.id,
                Description = announcementItem.text,
                Title = announcementItem.title,
                RecordDate = announcementItem.createTime.ToString("dd MMMM yyyy HH:mm")
            };
            return announcement;
        }
    }
}