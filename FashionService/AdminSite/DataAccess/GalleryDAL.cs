using AdminSite.BusinessObject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSite.DataAccess
{
    public class GalleryDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public static ResultBOL<int> InsertOrUpdate(GalleryBOL obj)
        {
            string stored = "sp_Galley_Insert_Update";
            ResultBOL<int> result = DataAccessHelpers.ExecuteStored(stored, obj);
            if (result.Code < 0)
            {
                _log.Error(result.ErrorMessage);
            }
            else
            {
                obj.Id = result.Data;
            }

            return result;
        }
    }
}