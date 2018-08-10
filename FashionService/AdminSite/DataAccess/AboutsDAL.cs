using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class AboutsDAL
    {

        public static ResultBOL<DataSet> Get()
        {
            string query = "select top 1 * from vw_About";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }

        public static ResultBOL<int> InsertOrUpdate(AboutBOL about)
        {
            if (about == null)
            {
                return new ResultBOL<int>() { 
                    Code = -20,
                    ErrorMessage = Utilities.GetErrorMessage("NullObject")
                };
            }

            string stored = "sp_About_Insert_Update";
            about.InsertDate = DateTime.Now;
            about.UpdatedDate = DateTime.Now;

            var result = DataAccessHelpers.ExecuteStored(stored, about.GetParameters());

            return result;
        }
    }
}
