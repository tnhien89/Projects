
using FrontEndSite.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FrontEndSite.DataAccess
{
    public class OtherDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Other";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAll(int otherTypeId)
        {
            string query = string.Format("select * from vw_Other where OtherType = {0}", otherTypeId);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAllBanners()
        {
            string query = "select * from vw_Banners_Get_All";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }
    }
}