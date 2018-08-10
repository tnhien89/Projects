using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.DataAccess
{
    public class SiteVisitorsDAL
    {
        public static ResultBOL<DataSet> Get()
        {
            string query = "select top(1) * from SiteVisitors";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> UpdateSiteVisitors()
        {
            string stored = "sp_UpdateSiteVisitors";
            var result = DataAccessHelpers.ExecuteStored(stored, null);

            return result;
        }
    }
}
