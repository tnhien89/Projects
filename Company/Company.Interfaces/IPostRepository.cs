using Company.Models;
using System.Collections;
using System.Collections.Generic;

namespace Company.Interfaces
{
    public interface IPostRepository : IDataRepository<PostInfo>
    {
        ResultDTO<List<PostInfo>> Get(long parentId, int records, string condition, string orderFields, string orderType);
        ResultDTO<ArrayList> GetTopHotAndNew(int hotRecords, int newRecords);
        ResultDTO<PostInfo> InsertOrUpdateReturnObject(PostInfo obj);
    }
}
