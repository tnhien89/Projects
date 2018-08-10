using MvcFrontEnd.BusinessObjects;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class TrainDAL
    {
        private const string __type = "DaoTao";
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static List<NewsBOL> GetTop(int top = 0)
        { 
#if DEBUG
            _log.Debug("top: {0}", top);
#endif
            string query = "";
            if (top > 0)
            {
                query = string.Format("select top({0}) * from vw_News where [Type] like '{1}'", top, __type);
            }
            else
            {
                query = string.Format("select * from vw_News where [Type] like '{0}'", __type);
            }

            List<NewsBOL> lst = new List<NewsBOL>();
            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    NewsBOL news = new NewsBOL(row);
                    if (news.Disable)
                    {
                        continue;
                    }

                    lst.Add(news);
                }
            }

            return lst;
        }
    }
}