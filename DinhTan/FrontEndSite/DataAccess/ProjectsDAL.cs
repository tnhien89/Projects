using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.DataAccess
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

        public static ResultBOL<DataSet> GetAllFromParent(int projectId)
        {
            string stored = "sp_Projects_GetAll_In_Menu";
            object obj = new { 
                Id = projectId
            };

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }

        public static ResultBOL<DataSet> GetPageItems(int menuId, DateTime scanTime, string type)
        {
            string tag = "[ProjectsDAL][GetPageItems]";

            if (scanTime == null)
            {
                return new ResultBOL<DataSet> { 
                    Code = -1,
                    ErrorMessage = Utilities.GetErrorMessage("NullObject")
                };
            }

            try
            {
                int itemsCount;
                if (type == "Home")
                {
                    itemsCount = int.Parse(Utilities.GetApplicationSettingsValue("HomeProject-Items"));
                }
                else
                {
                    itemsCount = int.Parse(Utilities.GetApplicationSettingsValue("Project-Items"));
                }

                object obj = new { 
                    MenuId = menuId,
                    ScanTime = scanTime,
                    ItemsCount = itemsCount
                };

                string stored = "sp_Projects_GetAll_PageItems";
                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                return new ResultBOL<DataSet>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultBOL<DataSet> GetTopProjects(int top, int menuId)
        {
            string query = string.Format("select top({0}) * from vw_Menu_Projects", top);

            if (menuId > 0)
            {
                query = string.Format("select top({0}) * from vw_Menu_Projects where menuId = {1}",
                    top,
                    menuId);
            }

            var result = DataAccessHelpers.ExecuteQuery(query);

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
