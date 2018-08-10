using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.DataAccess
{
    public class MenuDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Menu";
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAll(int id)
        {
            string query = string.Format("select * from vw_Menu where ParentId = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int menuId)
        {
            string query = string.Format("select Top 1 * from vw_Menu where Id = {0}", menuId);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<object> GetMenuName(int menuId)
        {
            string query = string.Format("select Name_VN from vw_Menu where Id = {0}", menuId);
            if (Utilities.IsLangueEN())
            {
                query = string.Format("select Name_EN from vw_Menu where Id = {0}", menuId);
            }

            var result = DataAccessHelpers.ExecuteScalar(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(MenuBOL obj)
        {
            string stored = "sp_Menu_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, obj.GetParameters());

            return result;
        }

        public static ResultBOL<DataSet> DeleteAll(string listId)
        {
            string stored = "sp_Menu_Delete";
            object obj = new { 
                Id = listId
            };

            if (listId.Contains(","))
            {
                stored = "sp_Menu_Multiple_Delete";
                obj = new { 
                    ListId = listId
                };
            }

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
