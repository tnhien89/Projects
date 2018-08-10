using MvcFrontEnd.BusinessObjects;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class NewsDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private const string __type = "TinTuc";

        public static NewsBOL Get(int id)
        { 
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            string query = "select * from vw_News where Id = " + id.ToString();
            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                NewsBOL news = new NewsBOL(result.Data.Tables[0].Rows[0]);
                if (!news.Disable)
                    return news;
            }

            return new NewsBOL();
        }

        public static List<NewsBOL> GetTop(int top = 10)
        { 
#if DEBUG
            _log.Debug("====================== Start ===============");
#endif
            string query = string.Format("select top({0}) * from vw_News where Type like '{1}'", top, __type);
            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            List<NewsBOL> lst = new List<NewsBOL>();

            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    NewsBOL news = new NewsBOL(row);
                    if (!news.Disable)
                    {
                        lst.Add(news);
                    }
                }
            }

            return lst;
        }

        public static List<NewsBOL> GetAll(int id  = 0)
        { 
#if DEBUG
            _log.Debug("==================== Start ====================");
#endif
            List<NewsBOL> lst = new List<NewsBOL>();
            string query = "";
            if (id > 0)
            {
                query = string.Format("select * from vw_News where Type like '{0}' and ParentId = {1}", __type, id);
            }
            else
            {
                query = string.Format("select * from vw_News where Type like '{0}'", __type);    
            }

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
                    if (!news.Disable)
                    {
                        lst.Add(news);
                    }
                }
            }

            return lst;
        }

        //
        public static NewsBOL GetWithType(string type, int id)
        {
#if DEBUG
            _log.Debug("type: {0} - id: {1}", type, id);
#endif
            string query = "";
            if (id > 0)
            {
                query = string.Format("select * from vw_News where Type like '{0}' and Id = {1}", type, id);
            }
            else
            {
                query = string.Format("select top(1) * from vw_News where Type like '{0}'", type);
            }

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

            return new NewsBOL();
        }

        public static List<NewsBOL> GetAllWithType(string type, int id)
        {
#if DEBUG
            _log.Debug("type: {0} - id: {1}", type, id);
#endif
            List<NewsBOL> lst = new List<NewsBOL>();
            string query = "";
            if (id > 0)
            {
                query = string.Format("select * from vw_News where Type like '{0}' and ParentId = {1}", type, id);
            }
            else
            {
                query = string.Format("select * from vw_News where Type like '{0}'", type);
            }

            ResultData<DataSet> result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    NewsBOL news = new NewsBOL(row);
                    if (!news.Disable)
                    {
                        lst.Add(news);
                    }
                }
            }

            return lst;
        }
    }
}