using System.Collections.Generic;

namespace Intouch.Edm.Services
{
    public class UploadResult
    {
        public string FileName { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public string DisplayFileName { get; set; }
        public string ContentId { get; set; }
    }
    public class UploadFilesAjaxResponse
    {

        public List<UploadResult> Result { get; set; }
    }
}