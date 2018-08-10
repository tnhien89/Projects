using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Interfaces
{
    public interface IFileRepository
    {
        ResultDTO<UploadFileInfo> Get(long id);
        ResultDTO<UploadFileInfo> Get(string key);
        ResultDTO<UploadFileInfo> InsertOrUpdate(UploadFileInfo obj);
        ResultDTO<List<DeleteError>> Delete(string ids);
    }
}
