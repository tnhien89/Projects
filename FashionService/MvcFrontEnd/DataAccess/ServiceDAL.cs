using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class ServiceDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private const string __type = "DichVu";
        public static AllCategoriesModel GetAllServices(int id = 0)
        {
#if DEBUG
            _log.Debug("=================== Start ==================");
#endif
            string stored = "sp_GetAllData_WithType";
            object obj = new { 
                Type = "DichVu",
                Id = id
            };

            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }

            AllCategoriesModel model = new AllCategoriesModel();

            if (result.Data != null && result.Data.Tables.Count > 1)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    MenuBOL menu = new MenuBOL(row);
                    if (menu.ParentId > 0)
                    {
                        model.Roots.Add(menu);
                    }
                }

                foreach (DataRow row in result.Data.Tables[1].Rows)
                {
                    NewsBOL news = new NewsBOL(row);
                    if (!news.Disable)
                        model.News.Add(news);
                }
            }

            return model;
        }

        public static List<NewsBOL> GetTop(int top = 9)
        { 
#if DEBUG
            _log.Debug("top: {0}", top);
#endif
            List<NewsBOL> lst = new List<NewsBOL>();
            string query = string.Format("select top({0}) * from vw_News where Type like '{1}'", top, __type);
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

        public static NewsBOL Get(int id)
        { 
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            string query = string.Format("select * from vw_News where Id = {0}", id);
            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                NewsBOL news = new NewsBOL(result.Data.Tables[0].Rows[0]);
                if (!news.Disable)
                {
                    return news;
                }
            }

            return null;
        }
    }
}