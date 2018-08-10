using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace FrontEnd.DataAccess
{
    public class NewsDAL
    {
        public static ResultData<List<News>> GetAll(int menuId)
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                string stored = "sp_News_GetAll";
                object obj = new { 
                    MenuId = menuId
                };
                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<List<News>>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                ResultData<List<News>> rs = new ResultData<List<News>>() { 
                    Code = 0,
                    Data = new List<News>()
                };
                if (result.Data == null || result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    return rs;
                }

                foreach (DataRow row in result.Data.Tables[0].Rows)
                {
                    News news = new News(row);
                    news.Content_VN = Utilities.CreateImagesUrl(news.Id, news.Content_VN, ConfigurationManager.AppSettings["UploadImagesUrl"]);
                    news.Content_EN = Utilities.CreateImagesUrl(news.Id, news.Content_EN, ConfigurationManager.AppSettings["UploadImagesUrl"]);

                    rs.Data.Add(news);
                }

                return rs;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<List<News>>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<News> Get(int id)
        {
            LogHelpers.LogHandler.Info("id: " + id.ToString());
            try
            {
                string query = "select top 1 * from vw_News where Id = @Id";
                object obj = new { 
                    Id = id
                };

                var result = DataAccessHelpers.ExecuteQuery(query, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<News>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                return new ResultData<News>() { 
                    Code = 0,
                    Data = new News(result.Data.Tables[0].Rows[0])
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<News>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<int> InsertOrUpdate(News news)
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                if (news.Id > 0)
                {
                    news.Content_EN = Utilities.ReplaceImageUri(news.Id, news.Content_EN, ConfigurationManager.AppSettings["UploadFilesDir"]);
                    news.Content_VN = Utilities.ReplaceImageUri(news.Id, news.Content_VN, ConfigurationManager.AppSettings["UploadImagesDir"]);
                }

                string stored = "sp_News_Insert_Update";
                var result = DataAccessHelpers.ExecuteStored(stored, news.GetParameters());
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                }
                else if (news.Id <= 0)
                {
                    news.Id = result.Code;
                    return UpdateContent(news);
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

        private static ResultData<int> UpdateContent(News news)
        {
            if (news == null)
            {
                return new ResultData<int>() { 
                    Code = -104,
                    ErrorMessage = "Data not found"
                };
            }

            LogHelpers.LogHandler.Info("NewsId: " + news.Id.ToString());
            try
            {
                news.Content_EN = Utilities.ReplaceImageUri(news.Id, news.Content_EN, ConfigurationManager.AppSettings["UploadImagesDir"]);
                news.Content_VN = Utilities.ReplaceImageUri(news.Id, news.Content_VN, ConfigurationManager.AppSettings["UploadImagesDir"]);

                string stored = "sp_News_Insert_Update";
                var result = DataAccessHelpers.ExecuteStored(stored, news.GetParameters());
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
    }
}