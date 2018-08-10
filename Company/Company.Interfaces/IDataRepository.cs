using Company.Models;
using System.Collections.Generic;

namespace Company.Interfaces
{
    public interface IDataRepository<T>
    {
        ResultDTO<T> Get(int id);
        ResultDTO<T> Get(long id);
        ResultDTO<T> Get(string itemKey);
        ResultDTO<List<T>> Get(int parentId, int records, string condition, string orderFields, string orderType);
        ResultDTO<int> InserOrUpdate(T obj);
        ResultDTO<List<DeleteError>> Delete(string ids);
    }
}
