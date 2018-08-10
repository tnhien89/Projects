using FrontEndSite.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FrontEndSite.DataAccess
{
    public class OtherTypeDAL
    {
        public static ResultBOL<DataSet> GetAll(bool hasBanner)
        {
            string query = "select * from vw_OtherType";
            if (!hasBanner)
            {
                query += " where Name_EN <> 'Banner'";
            }
            
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }
    }
}