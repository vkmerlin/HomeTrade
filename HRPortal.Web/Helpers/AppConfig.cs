using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HRPortal.Web.Helpers
{
    public static class AppConfig
    {
        public static string UploadFilesTo
        {
            get
            {
                return WebConfigurationManager.AppSettings["UploadFilesTo"];
            }
            private set
            {
                WebConfigurationManager.AppSettings["UploadFilesTo"] = value;
            }
        }

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