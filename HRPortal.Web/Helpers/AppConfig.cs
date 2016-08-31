using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HRPortal.Web.Helpers
{
    public static class AppConfig
    {
        public static string UploadFilesTo => WebConfigurationManager.AppSettings["UploadFilesTo"];
        public static string UploadItemImagesTo => WebConfigurationManager.AppSettings["UploadItemImagesTo"];

        public static bool IsAttachmentAcceptable(string contentType)
        {
            List<string> AcceptableAttachments = new List<string>
            {
                "image/jpeg", "image/png", "image/gif"
            };
            return AcceptableAttachments.FirstOrDefault(x => x == contentType) != null
                ? true
                : false;
        }
    }
}