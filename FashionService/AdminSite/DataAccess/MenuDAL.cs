using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;
using NLog;

namespace AdminSite.DataAccess
{
    public class MenuDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public static int GetId(string type)
        { 
#if DEBUG
            _log.Debug("type: {0}", type);
#endif
            string query = string.Format("select Id from Menu where [Type] like '{0}' and (ParentId is null or ParentId < 1)", type);
            ResultBOL<object> result = DataAccessHelpers.ExecuteScalar(query);
            if (result.Code < 0)
            {
                _log.Error(result.ErrorMessage);
            }
            else if (result.Data != null)
            {
                return int.Parse(result.Data.ToString());
            }

            return int.MinValue;
        }

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

        public static ResultBOL<DataSet> GetAllSub(int id)
        {
            string query = string.Format("select * from vw_Menu where ParentId = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> GetAllSubMenuProjects()
        {
            object obj = new { 
                Type = "DuAn"
            };
            string query = "select * from vw_Menu where Type = @Type and ParentId > 0";

            var result = DataAccessHelpers.ExecuteQuery(query, obj);

            return result;
        }

        public static ResultBOL<DataSet> Get(int menuId)
        {
            string query = string.Format("select Top 1 * from vw_Menu where Id = {0}", menuId);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static string GetType(int menuId)
        {
            string query = string.Format("select Type from vw_Menu where Id = {0}", menuId);
            var result = DataAccessHelpers.ExecuteScalar(query);
            if (result.Code < 0)
            {
                LogHelpers.Log.Error(result.ErrorMessage);
            }

            return result.Data == null ? "" : result.Data.ToString();
        }

        public static ResultBOL<int> InsertOrUpdate(MenuBOL obj)
        {
            //string type = GetType(obj.ParentId);
            //if (!string.IsNullOrEmpty(type))
            //{
            //    switch (type)
            //    { 
            //        case "DichVu":
            //            return NewsDAL.InsertOrUpdate(new NewsBOL() { 
            //                Name_EN = obj.Name_EN,
            //                Name_VN = obj.Name_VN,
            //                ImageLink = obj.Image,
            //                Type = type,
            //                ParentId = obj.ParentId
            //            });

            //        default:
            //            break;
            //    }
            //}

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
            if (result.Code == -1)
            {
                result.ErrorMessage = Utilities.GetErrorMessage("TableUsed");
            }

            return result;
        }
    }
}
