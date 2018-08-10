using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.DataAccess
{
    public class MenuDAL
    {
        public static ResultData<List<Menu>> GetAll()
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                string query = "select * from vw_Menu";
                var result = DataAccessHelpers.ExecuteQuery(query);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<List<Menu>>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                if (result.Data == null ||
                    result.Data.Tables.Count == 0 ||
                    result.Data.Tables[0].Rows.Count == 0)
                {
                    return new ResultData<List<Menu>>() { 
                        Code = result.Code,
                        Data = new List<Menu>()
                    };
                }

                List<Menu> list = new List<Menu>();
                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    Menu menu = new Menu(row);
                    list.Add(menu);
                }

                return new ResultData<List<Menu>>() { 
                    Code = result.Code,
                    Data = list
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<List<Menu>>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static List<SelectListItem> GetSelectList()
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                var result = GetAll();
                if (result.Code < 0)
                {
                    return new List<SelectListItem>();
                }

                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Menu menu in result.Data)
                {
                    list.Add(new SelectListItem() { 
                        Value = menu.Id.ToString(),
                        Text = menu.Name_VN
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new List<SelectListItem>();
            }
        }

        public static ResultData<Menu> Get(int id)
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                string query = "select top 1 * from vw_Menu where Id = @Id";
                object obj = new { 
                    Id = id
                };

                var result = DataAccessHelpers.ExecuteQuery(query, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<Menu>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                Menu menu = new Menu(result.Data.Tables[0].Rows[0]);
                return new ResultData<Menu>() { 
                    Code = 0,
                    Data = menu
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<Menu>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<int> InsertOrUpdate(Menu menu)
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                string stored = "sp_Menu_Insert_Update";
                var result = DataAccessHelpers.ExecuteStored(stored, menu.GetParameters());

                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<int>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<int> DeleteAll(string listId)
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                string stored = "sp_Menu_Delete";
                object obj = new
                {
                    ListId = listId.Trim()
                };

                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                }

                return new ResultData<int>()
                {
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<int>()
                {
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}