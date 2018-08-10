using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class MenuDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static string GetTitle(int id)
        { 
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            string query = "select Name_VN from Menu where Id = " + id.ToString();
            ResultData<object> result = DataAccessHelpers.ExecuteScalar(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null)
            {
                return result.Data.ToString();
            }

            return "";
        }
    }
}