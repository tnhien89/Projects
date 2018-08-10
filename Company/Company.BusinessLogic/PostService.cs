using Company.DataAccess;
using Company.Interfaces;
using Company.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BusinessLogic
{
    public class PostService
    {
        private IPostRepository _repo;

        public PostService()
        {
            _repo = new PostRepository();
        }

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            return _repo.Delete(ids);
        }

        public ResultDTO<PostInfo> Get(string itemKey)
        {
            return _repo.Get(itemKey);
        }

        public ResultDTO<PostInfo> Get(long id)
        {
            return _repo.Get(id);
        }

        public ResultDTO<PostInfo> Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<ArrayList> GetTopHotAndNew(int hotRecords, int newRecords)
        {
            return _repo.GetTopHotAndNew(hotRecords, newRecords);
        }

        public ResultDTO<List<PostInfo>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            throw new NotImplementedException();
        }

        public ResultDTO<List<PostInfo>> Get(long parentId, int records, string condition, string orderFields, string orderType)
        {
            return _repo.Get(parentId, records, condition, orderFields, orderType);
        }

        public ResultDTO<PostInfo> InserOrUpdate(PostInfo obj)
        {
            return _repo.InsertOrUpdateReturnObject(obj);
        }
    }
}
