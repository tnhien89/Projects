using Company.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Models;
using NLog;

namespace Company.DataAccess
{
    public class FileRepository : IFileRepository
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            _log.Trace("Ids: {0}", ids);
            string stored = "usp_Files_Delete";
            object obj = new {
                Ids = ids
            };

            ResultDTO<List<DeleteError>> rs = DataProvider.ExcuteStoredReturnCollection<DeleteError>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<UploadFileInfo> Get(string key)
        {
            _log.Trace("key: {0}", key);
            object obj = new {
                FileName = key
            };

            ResultDTO<UploadFileInfo> rs = DataProvider.ExcuteStoredReturnObject<UploadFileInfo>("usp_Files_Get", obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<UploadFileInfo> Get(long id)
        {
            _log.Trace("id: {0}", id);
            object obj = new
            {
                Id = id
            };

            ResultDTO<UploadFileInfo> rs = DataProvider.ExcuteStoredReturnObject<UploadFileInfo>("usp_Files_Get", obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }

        public ResultDTO<UploadFileInfo> InsertOrUpdate(UploadFileInfo obj)
        {
            _log.Trace("FileName: {0} - FileType: {1} - FileLength: {2}", obj.FileName, obj.FileType, string.IsNullOrEmpty(obj.FileData) ? 0 : obj.FileData.Length);
            string stored = "usp_Files_Insert_Update";

            ResultDTO<UploadFileInfo> rs = DataProvider.ExcuteStoredReturnObject<UploadFileInfo>(stored, obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return rs;
        }
    }
}
