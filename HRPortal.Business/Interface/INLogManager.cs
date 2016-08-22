using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public interface INLogManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
        void LogFatal(string message);

    }
}
