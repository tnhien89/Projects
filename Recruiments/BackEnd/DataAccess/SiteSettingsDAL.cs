using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace BackEnd.DataAccess
{
    public class SiteSettingsDAL
    {
        public static ResultData<SiteSetting> Get(int id, string type)
        { 
            try
            {
                LogHelpers.LogHandler.Info(string.Format("id: {0}, type: {1}", id, type));
                string stored = "sp_Settings_Get";
                object obj = new { 
                    Id = id,
                    Type = type
                };

                var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return new ResultData<SiteSetting>() { 
                        Code = result.Code,
                        ErrorMessage = result.ErrorMessage
                    };
                }

                ResultData<SiteSetting> rs = new ResultData<SiteSetting>();
                if (result.Data == null || result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    rs.Data = new SiteSetting();
                }
                else
                {
                    rs.Data = new SiteSetting(result.Data.Tables[0].Rows[0]);
                }

                return rs;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<SiteSetting>(){
                    Code = ex.HResult,
                    ErrorMessage = ex.Message,
                    Data = new SiteSetting()
                };
            }
        }

        public static ResultData<int> InsertOrUpdate(SiteSetting setting)
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                string stored = "sp_Settings_Insert_Update";
                var result = DataAccessHelpers.ExecuteStored(stored, setting.GetParameters());
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

        public static ResultData<SiteConfig> GetSiteConfig()
        {
            LogHelpers.LogHandler.Info("Start.");
            var result = Get(0, "SiteConfig");
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                return new ResultData<SiteConfig>() {
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Data = new SiteConfig()
                };
            }

            try
            {
                SiteConfig config = Utilities.XmlDeserializeObject<SiteConfig>(result.Data.XmlData);
                if (config == null)
                {
                    config = new SiteConfig();
                }

                return new ResultData<SiteConfig>() { 
                    Code = 0,
                    Data = config
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<SiteConfig>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static ResultData<int> SaveSiteConfig(SiteConfig config)
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                SiteSetting setting = new SiteSetting();
                setting.Type = "SiteConfig";
                setting.XmlData = Utilities.XmlSerializeObject(config);

                var result = InsertOrUpdate(setting);
                if (result.Code >= 0)
                {
                    Utilities.MoveFile(config.Logo,
                        Path.Combine(Utilities.UploadFilesTempDir(), "Logo"),
                        Path.Combine(Utilities.UploadImagesDir(), "Logo"));
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