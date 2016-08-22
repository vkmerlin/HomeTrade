using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public class NLogManager: INLogManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogFatal(string message)
        {
            logger.Fatal(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        void INLogManager.LogInfo(string message)
        {
            logger.Info(message);
        }
    }
}
