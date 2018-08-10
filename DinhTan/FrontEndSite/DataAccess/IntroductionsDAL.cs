using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.DataAccess
{
    public class IntroductionsDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string query = "select * from vw_News";
            ResultBOL<DataSet> result = DataAccessHelpers.ExecuteQuery(query);

            return result;
        }
    }
}
