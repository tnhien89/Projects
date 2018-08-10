using Company.Interfaces;
using Company.Models;
using NLog;
using System;
using System.Collections.Generic;

namespace Company.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            _log.Debug("ids: {0}", ids);
            string stored = "usp_Users_Delete";
            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, new { Ids = ids });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<UserInfo> Get(string itemKey)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<UserInfo> Get(long id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<UserInfo> Get(int id)
        {
            _log.Debug("id: {0}", id);
            string stored = "usp_Users_Get";
            object obj = new
            {
                Id = id
            };

            ResultDTO<UserInfo> rs = DataProvider.ExcuteStoredReturnObject<UserInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<UserInfo> Get(string username, string password)
        {
            _log.Trace("username: {0}", username);
            string stored = "usp_Users_Login";
            object obj = new {
                Username = username,
                Password = password
            };

            ResultDTO<UserInfo> rs = DataProvider.ExcuteStoredReturnObject<UserInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<List<UserInfo>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            _log.Debug("parentId: {0} - records: {1} - condition: {2} - orderFields: {3} - orderType: {4}");
            string stored = "usp_Users_Get";
            object obj = new
            {
                Records = records,
                Condition = condition,
                OrderFields = orderFields,
                OrderType = orderType
            };

            ResultDTO<List<UserInfo>> rs = DataProvider.ExcuteStoredReturnCollection<UserInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<int> InserOrUpdate(UserInfo obj)
        {
            _log.Debug("Id: {0} - Username: {1}", obj.Id, obj.Username);
            string stored = "usp_Users_Insert_Update";

            ResultDTO<int> rs = DataProvider.ExecuteStored(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
