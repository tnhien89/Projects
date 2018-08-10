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
    public class MenuService
    {
        private IMenuRepository _repo;

        public MenuService()
        {
            _repo = new MenuRepository();
        }

        public static ResultDTO<Menu> Get(int id)
        {
            DataService<Menu> service = new DataService<Menu>(new MenuRepository());
            return service.Get(id);
        }

        public static ResultDTO<Menu> Get(string itemKey)
        {
            DataService<Menu> service = new DataService<Menu>(new MenuRepository());
            return service.Get(itemKey);
        }

        public static ResultDTO<List<Menu>> Get(int parentId, int records, string condition, string orderFields, string orderType)
        {
            DataService<Menu> service = new DataService<Menu>(new MenuRepository());
            return service.Get(parentId, records, condition, orderFields, orderType);
        }

        public Hashtable GetMenuFromXML(HttpContextBase context)
        {
            return _repo.GetMenuFromXML(context);
        }

        public ResultDTO<ArrayList> GetDesignInfo()
        {
            return _repo.GetDesignInfo();
        }

        public static ResultDTO<int> InserOrUpdate(Menu obj)
        {
            DataService<Menu> service = new DataService<Menu>(new MenuRepository());
            return service.InserOrUpdate(obj);
        }

        public static ResultDTO<List<DeleteError>> Delete(string ids)
        {
            DataService<Menu> service = new DataService<Menu>(new MenuRepository());
            return service.Delete(ids);
        }
    }
}
