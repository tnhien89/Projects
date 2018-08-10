using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace FrontEndSite
{
    public class LogHelpers
    {
        public static void WriteError(string msg)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Error(msg);
        }

        public static void WriteError(string tag, string msg)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Error(string.Format("{0} Error: {1}", tag, msg));
        }

        public static void WriteException(string tag, string msg)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Error(string.Format("{0} Exception: {1}", tag, msg));
        }

        public static void WriteStatus(string tag, string status)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info(string.Format("{0} Status: {1}", tag, status));
        }

        public static void WriteStatus(string tag, string info, string status)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info(string.Format("{0}-[{1}] Status: {2}", tag, info, status));
        }
    }
}
