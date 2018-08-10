using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.Models;
using NLog;
using System.Collections.Generic;
using System.Data;

namespace MvcFrontEnd.DataAccess
{
    public class OtherDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static List<OtherBOL> GetAllAds()
        {
            List<OtherBOL> lst = new List<OtherBOL>();
            string stored = "sp_GetAllAds";
            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, null);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    lst.Add(new OtherBOL(row));
                }
            }

            return lst;
        }

        public static AdsModel GetModel()
        {
            AdsModel model = new AdsModel();
            string stored = "sp_GetAllAds";
            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, null);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    model.Parents.Add(new OtherBOL(row));
                }

                if (result.Data.Tables.Count > 1)
                {
                    foreach (DataRow row in result.Data.Tables[1].Rows)
                    {
                        model.Childrens.Add(new OtherBOL(row));
                    }
                }
            }

            return model;
        }

        public static List<OtherBOL> GetAllBanners()
        {
            List<OtherBOL> lst = new List<OtherBOL>();
            string query = "select * from vw_Banners_Get_All";
            var result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    lst.Add(new OtherBOL(row));
                }
            }

            return lst;
        }
    }
}
