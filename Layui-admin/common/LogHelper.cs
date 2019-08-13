using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Layui_admin.common
{
    public class LogHelper
    {
        private static readonly ILog logError = LogManager.GetLogger("logError");
        private static readonly ILog logInfo = LogManager.GetLogger("logInfo");
        private static readonly ILog logwarn = LogManager.GetLogger("logWarn");
        private LogHelper()
        {
        }
        public static void WriteInfo(object message)
        {
            logError.Info(message);
        }
        public static void WriteWarning(object message)
        {
            logwarn.Warn(message);
        }
        public static void WriteWarning(object message, System.Exception exception)
        {
            logwarn.Warn(message, exception);
        }
        public static void WriteError(object message)
        {
            logError.Error(message);
        }
        public static void WriteError(object message, System.Exception exception)
        {
            logError.Error(message, exception);
        }
        public static void DeleteLog()
        {
            string logDirPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Log");
            if (!Directory.Exists(logDirPath)) return;
            int days = 30;
            foreach (string filePath in Directory.GetFiles(logDirPath))
            {
                DateTime dt;
                DateTime.TryParse(Path.GetFileNameWithoutExtension(filePath).Replace(@"Log\", "").Replace(".", "-"), out dt);
                if (dt.AddDays(days).CompareTo(DateTime.Now) < 0)
                {
                    File.Delete(filePath);
                }
            }
        }

    }
}