using MvcFrontEnd.BusinessObjects;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcFrontEnd.DataAccess
{
    public class ContactsDAL
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public static ContactBOL GetInfo()
        {
#if DEBUG
            _log.Debug("=================== Start =================");
#endif
            
            string query = "select Top(1) * from vw_About";
            var result = DataAccessHelpers.ExecuteQuery(query);
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                return new ContactBOL(result.Data.Tables[0].Rows[0]);
            }

            return new ContactBOL();
        }

        public static ResultData<int> InsertOrUpdate(ContactBOL contact)
        {
            string stored = "sp_Contacts_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, contact.GetParameters());
            if (result.Code < 0)
            {
                _log.Error(result.Message);
            }

            return result;
        }
    }
}
