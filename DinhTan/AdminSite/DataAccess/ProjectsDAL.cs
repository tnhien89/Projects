using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class ProjectsDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Projects";
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAll(int menuId)
        {
            string query = string.Format("select * from vw_Projects where MenuId = {0}", menuId);
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select * from vw_Projects where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(ProjectBOL BOL)
        {
            string imageDir = Utilities.GetDirectory("ImageProjectsDir");
            BOL.Content_VN = Utilities.ReplaceImageUri(BOL.Content_VN, imageDir);
            BOL.Content_EN = Utilities.ReplaceImageUri(BOL.Content_VN, imageDir);

            string stored = "sp_Peojects_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, BOL.GetParameters());

            return result;
        }

        public static ResultBOL<DataSet> DeleteAll(string listId)
        {
            string stored = "sp_Projects_Delete";
            Object obj = new { 
                Id = listId
            };
            if (listId.Contains(","))
            {
                obj = new { 
                    listId = listId
                };
                stored = "sp_Projects_Multiple_Delete";
            }

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
