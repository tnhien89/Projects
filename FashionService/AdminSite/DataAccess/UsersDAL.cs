using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class UsersDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_Users";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<DataSet> Get(int id)
        {
            string query = string.Format("select top(1) * from vw_Users where Id = {0}", id);
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(UserBOL user)
        {
            string stored = "sp_Users_Insert_Update";
            var result = DataAccessHelpers.ExecuteStored(stored, user.GetParameters());

            return result;
        }

        public static ResultBOL<DataSet> Login(string username, string password)
        {
            string stored = "sp_Users_Login";
            Object obj = new { 
                Username = username,
                Password = Utilities.EncryptPassword(password)
            };

            var result = DataAccessHelpers.ExecuteStoredReturnDataSet(stored, obj);

            return result;
        }
    }
}
