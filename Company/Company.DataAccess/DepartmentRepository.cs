using Company.Interfaces;
using Company.Models;
using NLog;
using System.Collections.Generic;
using System;

namespace Company.DataAccess
{
    public class DepartmentRepository : IDataRepository<Department>
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<Department> Get(int id)
        {
            _log.Debug("id: {0}", id);
            string stored = "usp_Department_Get";
            object obj = new
            {
                Id = id
            };

            ResultDTO<Department> rs = DataProvider.ExcuteStoredReturnObject<Department>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<Department> Get(long id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<Department> Get(string itemKey)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<List<Department>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            _log.Debug("parentId: {0} - records: {1} - condition: {2} - orderFields: {3} - orderType: {4}");
            string stored = "usp_Department_Get";
            object obj = new
            {
                ParentId = parentId,
                Records = records,
                Condition = condition,
                OrderFields = orderFields,
                OrderType = orderType
            };

            ResultDTO<List<Department>> rs = DataProvider.ExcuteStoredReturnCollection<Department>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<int> InserOrUpdate(Department obj)
        {
            _log.Debug("Id: {0} - Name: {1}", obj.Id, obj.Name);
            string stored = "usp_Department_Insert_Update";

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
            string stored = "usp_Department_Delete";
            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, new { Ids = ids });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
