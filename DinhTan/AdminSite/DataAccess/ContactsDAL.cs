using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class ContactsDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Contacts";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select * from vw_Contacts where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(ContactBOL contact)
        {
            string stored = "sp_Contacts_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, contact.GetParameters());

            return result;
        }
    }
}
