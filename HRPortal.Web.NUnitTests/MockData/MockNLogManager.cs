using HRPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Web.NUnitTests.MockData
{
    public class MockNLogManager : INLogManager
    {
        public void LogError(string message)
        {
            var logMessage = message;
        }

        public void LogFatal(string message)
        {
            var logMessage = message;
        }

        public void LogInfo(string message)
        {
            var logMessage = message;
        }

        public void LogWarn(string message)
        {
            var logMessage = message;
        }
    }
}
