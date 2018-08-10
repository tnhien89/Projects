using Company.DataAccess;
using Company.Models;

namespace Company.BusinessLogic
{
    public class CategoryService
    {
        public static ResultDTO<Category> Get(int id)
        {
            DataService<Category> service = new DataService<Category>(new CategoryRepository());
            ResultDTO<Category> rs = service.Get(id);

            return rs;
        }
    }
}
