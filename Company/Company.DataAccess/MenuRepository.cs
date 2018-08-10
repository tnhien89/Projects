using System;
using System.Collections;
using System.Collections.Generic;
using Company.Interfaces;
using Company.Models;
using NLog;
using System.Data;
using System.Web;

namespace Company.DataAccess
{
    public class MenuRepository : IMenuRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            _log.Debug("ids: {0}", ids);
            string stored = "usp_Menu_Delete";
            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, new { Ids = ids });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<Menu> Get(string itemKey)
        {
            _log.Debug("itemKey: {0}", itemKey);
            string stored = "usp_Menu_Get";
            object obj = new
            {
                ItemKey = itemKey
            };

            ResultDTO<Menu> rs = DataProvider.ExcuteStoredReturnObject<Menu>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<Menu> Get(long id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<Menu> Get(int id)
        {
            _log.Debug("id: {0}", id);
            string stored = "usp_Menu_Get";
            object obj = new
            {
                Id = id
            };

            ResultDTO<Menu> rs = DataProvider.ExcuteStoredReturnObject<Menu>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<List<Menu>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            _log.Trace("parentId: {0} - records: {1} - condition: {2} - orderFields: {3} - orderType: {4}", parentId, records, condition, orderFields, orderType);
            string stored = "usp_Menu_Get";
            object obj = new
            {
                ParentId = parentId,
                Records = records,
                Condition = condition,
                OrderFields = orderFields,
                OrderType = orderType
            };

            ResultDTO<List<Menu>> rs = DataProvider.ExcuteStoredReturnCollection<Menu>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<ArrayList> GetDesignInfo()
        {
            ResultDTO<ArrayList> rs = DataProvider.ExcuteStoredReturnCollection("usp_Get_Menu_Design_Info", null, new Type[] { typeof(Menu), typeof(Option) });
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public Hashtable GetMenuFromXML(HttpContextBase context)
        {
            Hashtable hash = new Hashtable();
            DataSet ds = new DataSet();
            ds.ReadXml(context.Server.MapPath("~/Menu.xml"));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Menu menu = new Menu() {
                    Name = row["Name"].ToString(),
                    MenuType = row["Type"].ToString()
                };

                hash[row["Type"]] = row["Name"];
            }

            ds.Dispose();

            return hash;
        }

        public ResultDTO<int> InserOrUpdate(Menu obj)
        {
            _log.Debug("Id: {0} - Name: {1}", obj.Id, obj.Name);
            string stored = "usp_Menu_Insert_Update";

            ResultDTO<int> rs = DataProvider.ExecuteStored(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
