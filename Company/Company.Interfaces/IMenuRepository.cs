using Company.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Company.Interfaces
{
    public interface IMenuRepository : IDataRepository<Menu>
    {
        ResultDTO<ArrayList> GetDesignInfo();
        Hashtable GetMenuFromXML(HttpContextBase context);
    }
}
