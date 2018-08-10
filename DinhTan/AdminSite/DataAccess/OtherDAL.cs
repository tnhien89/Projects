using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class OtherDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Other";
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAll(int menuId)
        {
            string query = string.Format("select * from vw_Other where OtherType = {0}", menuId);
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select * from vw_Other where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(OtherBOL BOL)
        {
            string stored = "sp_Other_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, BOL.GetParameters());

            return result;
        }

        public static ResultBOL<DataSet> DeleteAll(string listId)
        {
            string stored = "sp_Other_Delete";
            object obj = new
            {
                Id = listId
            };
            if (listId.Contains(","))
            {
                obj = new
                {
                    listId = listId
                };
                stored = "sp_Other_Multiple_Delete";
            }

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
