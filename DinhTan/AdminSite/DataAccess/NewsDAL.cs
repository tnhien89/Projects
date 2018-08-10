using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AdminSite.BusinessObject;
using HtmlAgilityPack;

namespace AdminSite.DataAccess
{
    public class NewsDAL
    {

        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_News";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAll(int menuId)
        {
            string query = string.Format("select * from vw_News where MenuId = {0}", menuId);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select Top 1 * from vw_News where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(NewsBOL BOL)
        {
            string imageDir = Utilities.GetDirectory("ImageNewsDir");
            //---
            //---
            BOL.Content_VN = Utilities.ReplaceImageUri(BOL.Content_VN, imageDir);
            BOL.Content_EN = Utilities.ReplaceImageUri(BOL.Content_EN, imageDir);

            string stored = "sp_News_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, BOL.GetParameters());

            return result;
        }

        public static ResultBOL<DataSet> DeleteAll(string listId)
        {
            string stored = "sp_News_Delete";
            object obj = new { 
                Id = listId
            };

            if (listId.Contains(","))
            {
                stored = "sp_News_Multiple_Delete";
                obj = new { 
                    ListId = listId
                };
            }

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
