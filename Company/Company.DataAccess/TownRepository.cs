using Company.Interfaces;
using System.Collections;
using System.Data;
using System.Web;
using System;
using Company.Models;
using NLog;
using System.Collections.Generic;

namespace Company.DataAccess
{
    public class TownRepository : ITownRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public Hashtable Get(HttpContextBase context)
        {
            Hashtable hash = new Hashtable();
            DataSet ds = new DataSet();
            ds.ReadXml(context.Server.MapPath("~/Town.xml"));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                hash[row["Key"]] = row["Name"];
            }

            ds.Dispose();

            return hash;
        }

        public Hashtable GetPostsCount()
        {
            Hashtable hash = new Hashtable();
            string stored = "usp_Posts_Town_Count";
            ResultDTO<DataSet> rs = DataProvider.ExecuteStoredReturnDataSet(stored, null);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            if (rs.Data != null && rs.Data.Tables.Count > 0)
            {
                foreach (DataRow row in rs.Data.Tables[0].Rows)
                {
                    hash[row["Town"]] = row["NumItems"];
                }
            }

            return hash;
        }

        public ResultDTO<List<PostInfo>> GetPosts(string key)
        {
            _log.Trace("key: {0}", key);
            string condition = string.Format("Town='{0}'", key.Replace("'", ""));
            PostRepository repo = new PostRepository();

            return repo.Get((long)0, 0, condition, "", "");
        }
    }
}
