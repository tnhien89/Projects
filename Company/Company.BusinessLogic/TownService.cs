using Company.DataAccess;
using Company.Interfaces;
using Company.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Company.BusinessLogic
{
    public class TownService
    {
        private ITownRepository _repo;

        public TownService()
        {
            _repo = new TownRepository();
        }

        public Hashtable Get(HttpContextBase context)
        {
            return _repo.Get(context);
        }

        public Hashtable GetPostsCount()
        {
            return _repo.GetPostsCount();
        }

        public ResultDTO<List<PostInfo>> GetPosts(string key)
        {
            return _repo.GetPosts(key);
        }
    }
}
