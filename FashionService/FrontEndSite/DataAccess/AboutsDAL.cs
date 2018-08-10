using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.DataAccess
{
    public class AboutsDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select Top(1) * from vw_About";
            var result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }
    }
}
