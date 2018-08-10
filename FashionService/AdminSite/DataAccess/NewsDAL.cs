using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AdminSite.BusinessObject;
using HtmlAgilityPack;
using NLog;

namespace AdminSite.DataAccess
{
    public class NewsDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

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

        public static ResultBOL<DataSet> GetAll(string type, int menuId = 0)
        {
#if DEBUG
            _log.Debug("type: {0} - menuId: {1}", type, menuId);
#endif
            string query = "";
            if (menuId > 0)
            {
                query = string.Format("select * from News where ParentId = '{0}' and Type like '{1}'",
                    menuId,
                    type);
            }
            else
            {
                query = string.Format("select * from News where Type like '{0}'", type);
            }

            var result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.ErrorMessage);
            }

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
            string imageDir = Utilities.GetDirectory("ImagesDir");
            //---
            //---
            BOL.Content_VN = Utilities.ReplaceImageUri(BOL.Content_VN, imageDir);
            BOL.Content_EN = Utilities.ReplaceImageUri(BOL.Content_EN, imageDir);

            string stored = "sp_News_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, BOL.GetParameters());

            return result;
        }

        public static ResultBOL<int> UpdatePriorityOrDisable(object obj)
        {
            string stored = "sp_News_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, obj);

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
