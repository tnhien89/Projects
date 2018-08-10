using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class OtherTypeDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from OtherType select * from Other where IsGroup = 1";
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAllSort()
        {
            string query = "select * from vw_OtherType where Name_VN <> 'Banner'";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select * from vw_OtherType where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(OtherTypeBOL BOL)
        {
            string stored = "sp_OtherType_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, BOL.GetParameters());

            return result;
        }

        public static ResultBOL<string> UpdateIndex(OtherTypeUpdateIndexBOL[] list)
        {
            string stored = "sp_OtherType_Insert_Update";

            ResultBOL<string> result = new ResultBOL<string>();

            foreach (OtherTypeUpdateIndexBOL obj in list)
            {
                var updateResult = DataAccessHelpers.ExecuteStored(stored, obj.GetParameters());
                if (updateResult.Code < 0)
                {
                    if (string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        result.ErrorMessage = updateResult.ErrorMessage;
                    }
                    else
                    {
                        result.ErrorMessage += "\n" + updateResult.ErrorMessage;
                    }
                }
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.Code = -1;
            }

            return result;
        }

        public static ResultBOL<DataSet> DeleteAll(string listId)
        {
            string stored = "sp_OtherType_Delete";
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
                stored = "sp_OtherType_Multiple_Delete";
            }

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
