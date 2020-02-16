namespace Intouch.Edm.Models.Dtos.CreateScenario
{
    public class CreateEmergencyScenario
    {
        public class Picture
        {
            public string fileName { get; set; }
            public int contentLength { get; set; }
            public string contentType { get; set; }
            public string displayFileName { get; set; }
            public string contentId { get; set; }
        }

        public class RootObject
        {
            public int userId { get; set; }
            public int subjectType { get; set; }
            public int siteId { get; set; }
            public int impactAreaId { get; set; }
            public int eventTypeId { get; set; }
            public int sourceId { get; set; }
            public Picture picture { get; set; }
        }
    }
}
