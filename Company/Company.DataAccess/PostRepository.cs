using System;
using System.Collections;
using System.Collections.Generic;
using Company.Interfaces;
using Company.Models;
using NLog;

namespace Company.DataAccess
{
    public class PostRepository : IPostRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            _log.Debug("ids: {0}", ids);
            string stored = "usp_Posts_Delete";
            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, new { Ids = ids });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<PostInfo> Get(string itemKey)
        {
            _log.Debug("itemKey: {0}", itemKey);
            string stored = "usp_Posts_Get";
            object obj = new
            {
                ItemKey = itemKey
            };

            ResultDTO<PostInfo> rs = DataProvider.ExcuteStoredReturnObject<PostInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<PostInfo> Get(long id)
        {
            _log.Debug("id: {0}", id);
            string stored = "usp_Posts_Get";
            object obj = new
            {
                Id = id
            };

            ResultDTO<PostInfo> rs = DataProvider.ExcuteStoredReturnObject<PostInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<PostInfo> Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<List<PostInfo>> Get(long parentId, int records, string condition, string orderFields, string orderType)
        {
            _log.Debug("parentId: {0} - records: {1} - condition: {2} - orderFields: {3} - orderType: {4}");
            string stored = "usp_Posts_Get";
            object obj = new
            {
                ParentId = parentId,
                Records = records,
                Condition = condition,
                OrderFields = orderFields,
                OrderType = orderType
            };

            ResultDTO<List<PostInfo>> rs = DataProvider.ExcuteStoredReturnCollection<PostInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<List<PostInfo>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<ArrayList> GetTopHotAndNew(int hotRecords, int newRecords)
        {
            _log.Trace("hotRecords: {0} - newRecords: {1}", hotRecords, newRecords);
            string stored = "usp_Posts_Get_Top";
            object obj = new {
                HotRecords = hotRecords,
                NewRecords = newRecords
            };

            ResultDTO<ArrayList> rs = DataProvider.ExcuteStoredReturnCollection(stored, obj, new Type[] { typeof(PostInfo), typeof(PostInfo) });
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<int> InserOrUpdate(PostInfo obj)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<PostInfo> InsertOrUpdateReturnObject(PostInfo obj)
        {
            _log.Debug("Id: {0} - Title: {1}", obj.Id, obj.Title);
            string stored = "usp_Posts_Insert_Update";

            ResultDTO<PostInfo> rs = DataProvider.ExcuteStoredReturnObject<PostInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<long> InsertWithBigId(PostInfo obj)
        {
            throw new NotImplementedException();
        }
    }
}
