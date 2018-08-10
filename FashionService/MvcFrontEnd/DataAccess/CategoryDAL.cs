using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.DataAccess
{
    public class CategoryDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public static AllCategoriesModel GetAllWithTypeGetAll(bool removeRoot = false)
        {
            string controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string type = "";
            switch (controller)
            { 
                case "About":
                    type = "GioiThieu";
                    break;

                case "News":
                    type = "TinTuc";
                    break;
                    
                case "Train":
                    type = "DaoTao";
                    break;
                case "Gallery":
                    type = "ThuVienHinh";
                    break;

                default:
                    type = "DichVu";
                    break;
            }
#if DEBUG
            _log.Debug("type: {0}", type);
#endif
            string stored = "sp_GetAllData_WithType";
            object obj = new
            {
                Type = type
            };

            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }

            AllCategoriesModel model = new AllCategoriesModel();
            model.PageType = (controller == "Home" || string.IsNullOrEmpty(controller)) ? "Service" : controller;

            if (result.Data != null && result.Data.Tables.Count > 1)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    MenuBOL menu = new MenuBOL(row);
                    if (removeRoot && menu.ParentId <= 0)
                    {
                        continue;
                    }

                    model.Roots.Add(menu);
                }

                if (controller.ToLower() != "gallery")
                {
                    foreach (DataRow row in result.Data.Tables[1].Rows)
                    {
                        model.News.Add(new NewsBOL(row));
                    }
                }
            }

            return model;
        }

        public static AllCategoriesModel GetAll(bool removeRoot = false)
        {
            string stored = "sp_GetAllData_WithType";
            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, new { });
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }

            AllCategoriesModel model = new AllCategoriesModel();

            if (result.Data != null && result.Data.Tables.Count > 1)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    MenuBOL menu = new MenuBOL(row);
                    if (removeRoot && menu.ParentId <= 0)
                    {
                        continue;
                    }

                    model.Roots.Add(menu);
                }

                foreach (DataRow row in result.Data.Tables[1].Rows)
                {
                    model.News.Add(new NewsBOL(row));
                }
            }

            return model;
        }

        public static AllCategoriesModel GetAllWithType(int id, bool removeRoot = false)
        {
            string controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string type = "";
            switch (controller)
            {
                case "About":
                    type = "GioiThieu";
                    break;
                case "News":
                    type = "TinTuc";
                    break;
                case "Train":
                    type = "DaoTao";
                    break;
                case "Gallery":
                    type = "ThuVienHinh";
                    break;

                default:
                    type = "DichVu";
                    break;
            }
#if DEBUG
            _log.Debug("type: {0}", type);
#endif
            string stored = "sp_GetAllData_WithType";
            object obj = new
            {
                Type = type,
                Id = id
            };

            ResultData<DataSet> result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }

            AllCategoriesModel model = new AllCategoriesModel();
            model.PageType = (controller == "Home" || string.IsNullOrEmpty(controller)) ? "Service" : controller;

            if (result.Data != null && result.Data.Tables.Count > 1)
            {
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    MenuBOL menu = new MenuBOL(row);
                    if (removeRoot && menu.ParentId <= 0)
                    {
                        continue;
                    }
                    
                    model.Roots.Add(menu);
                }

                if (controller.ToLower() != "gallery")
                {
                    foreach (DataRow row in result.Data.Tables[1].Rows)
                    {
                        model.News.Add(new NewsBOL(row));
                    }
                }
            }

            return model;
        }
    }
}