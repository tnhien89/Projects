using MvcFrontEnd.BusinessObjects;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class AboutDAL
    {
        private const string __type = "GioiThieu";
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static NewsBOL Get(int id = 0)
        { 
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            string query = "";
            if (id > 0)
            {
                query = string.Format("select * from vw_News where Type like '{0}' and Id = {1}", __type, id);
            }
            else
            { 
                query = string.Format("select top(1) * from vw_News where Type like '{0}'", __type);
            }

            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                return new NewsBOL(result.Data.Tables[0].Rows[0]);
            }

            return new NewsBOL();
        }
    }
}