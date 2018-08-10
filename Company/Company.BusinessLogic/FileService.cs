using Company.DataAccess;
using Company.Interfaces;
using Company.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Company.BusinessLogic
{
    public class FileService
    {
        private IFileRepository _repo;
        private Logger _log = LogManager.GetCurrentClassLogger();

        public FileService()
        {
            _repo = new FileRepository();
        }
       
        public ResultDTO<List<DeleteError>> Delete(string ids)
        {
            return _repo.Delete(ids);
        }

        public ResultDTO<UploadFileInfo> Get(string key)
        {
            return _repo.Get(key);
        }

        public ResultDTO<UploadFileInfo> Get(long id)
        {
            return _repo.Get(id);
        }

        public void CheckAndSaveImage(PostInfo obj, HttpFileCollectionBase files)
        {
            if (files == null || files.Count == 0)
            {
                return;
            }

            HttpPostedFileBase file = files[0];
            if (file.ContentLength == 0)
            {
                return;
            }

            string fileName = string.IsNullOrEmpty(obj.Images) ? obj.ItemKey : obj.Images;

            byte[] bytes = new byte[file.ContentLength];
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                bytes = reader.ReadBytes(file.ContentLength);
            }

            UploadFileInfo info = new UploadFileInfo();
            info.FileName = fileName;
            info.FileData = Convert.ToBase64String(bytes);
            info.SaveMethod = string.IsNullOrEmpty(obj.Images) ? "Insert" : "Update";

            ResultDTO<UploadFileInfo> rs = this.InsertOrUpdate(info);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }
            else
            {
                obj.Images = rs.Data.FileName;
            }
        }

        public ResultDTO<UploadFileInfo> Insert(HttpPostedFileBase data)
        {
            UploadFileInfo obj = new UploadFileInfo();
            obj.FileName = data.FileName;
            obj.SaveMethod = "Insert";

            using (BinaryReader reader = new BinaryReader(data.InputStream))
            {
                byte[] bytes = new byte[data.ContentLength];
                bytes = reader.ReadBytes(data.ContentLength);
                obj.FileData = Convert.ToBase64String(bytes);
            }

            return this.InsertOrUpdate(obj);
        }

        public ResultDTO<UploadFileInfo> InsertOrUpdate(UploadFileInfo obj)
        {
            return _repo.InsertOrUpdate(obj);
        }
    }
}
