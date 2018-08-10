using Company.Interfaces;
using Company.Models;
using NLog;
using System.Collections.Generic;
using System;

namespace Company.DataAccess
{
    public class CategoryRepository : IDataRepository<Category>
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<Category> Get(int id)
        {
            _log.Debug("id: {0}", id);
            string stored = "usp_Categories_Get";
            object obj = new
            {
                Id = id
            };

            ResultDTO<Category> rs = DataProvider.ExcuteStoredReturnObject<Category>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<Category> Get(long id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<Category> Get(string itemKey)
        {
            _log.Debug("itemKey: {0}", itemKey);
            string stored = "usp_Categories_Get";
            object obj = new
            {
                ItemKey = itemKey
            };

            ResultDTO<Category> rs = DataProvider.ExcuteStoredReturnObject<Category>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<List<Category>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            _log.Debug("parentId: {0} - records: {1} - condition: {2} - orderFields: {3} - orderType: {4}");
            string stored = "usp_Categories_Get";
            object obj = new
            {
                ParentId = parentId,
                Records = records,
                Condition = condition,
                OrderFields = orderFields,
                OrderType = orderType
            };

            ResultDTO<List<Category>> rs = DataProvider.ExcuteStoredReturnCollection<Category>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<int> InserOrUpdate(Category obj)
        {
            _log.Debug("Id: {0} - Name: {1} - ItemKey: {1}", obj.Id, obj.Name, obj.ItemKey);
            string stored = "usp_Categories_Insert_Update";

            ResultDTO<int> rs = DataProvider.ExecuteStored(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            _log.Debug("ids: {0}", ids);
            string stored = "usp_Categories_Delete";
            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, new { Ids = ids });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
