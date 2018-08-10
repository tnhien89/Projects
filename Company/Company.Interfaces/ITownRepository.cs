using Company.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Company.Interfaces
{
    public interface ITownRepository
    {
        Hashtable Get(HttpContextBase context);
        Hashtable GetPostsCount();
        ResultDTO<List<PostInfo>> GetPosts(string key);
    }
}
