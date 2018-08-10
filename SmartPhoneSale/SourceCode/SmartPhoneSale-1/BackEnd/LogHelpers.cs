using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class LogHelpers
    {
        public static Logger LogHandler {
            get {
                return LogManager.GetCurrentClassLogger();
            }
        }

        public static void WriteLog(string text)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Error(text);
        }

        public static void WriteInfo(string text)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info(text);
        }
    }
}
