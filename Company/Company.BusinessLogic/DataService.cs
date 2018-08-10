using Company.Interfaces;
using Company.Models;
using System.Collections.Generic;

namespace Company.BusinessLogic
{
    public class DataService<T>
    {
        private IDataRepository<T> _repo;

        public DataService(IDataRepository<T> repo)
        {
            this._repo = repo;
        }

        public ResultDTO<T> Get(int id)
        {
            return this._repo.Get(id);
        }

        public ResultDTO<T> Get(long id)
        {
            return this._repo.Get(id);
        }

        public ResultDTO<T> Get(string itemKey)
        {
            return _repo.Get(itemKey);
        }

        public ResultDTO<List<T>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            return _repo.Get(parentId, records, condition, orderFields, orderType);
        }

        public ResultDTO<int> InserOrUpdate(T obj)
        {
            return _repo.InserOrUpdate(obj);
        }

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            return _repo.Delete(ids);
        }
    }
}
