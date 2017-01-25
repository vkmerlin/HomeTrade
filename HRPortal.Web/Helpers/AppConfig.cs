using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace HRPortal.Web.Helpers
{
    public static class AppConfig
    {
        public static string UploadFilesTo => WebConfigurationManager.AppSettings["UploadFilesTo"];

        public static string UploadItemImagesTo => WebConfigurationManager.AppSettings["UploadItemImagesTo"];

        public static string EmailSMTP => WebConfigurationManager.AppSettings["EmailSMTP"];

        public static string EmailUserName => WebConfigurationManager.AppSettings["EmailUserName"];
        public static string EmailPassword => WebConfigurationManager.AppSettings["EmailPassword"];

        public static bool IsAttachmentAcceptable(string contentType)
        {
            List<string> AcceptableAttachments = new List<string>
            {
                "image/jpeg", "image/png", "image/gif"
            };
            return AcceptableAttachments.FirstOrDefault(x => x == contentType) != null;
        }
    }
}