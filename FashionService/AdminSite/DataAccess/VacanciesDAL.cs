using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminSite.BusinessObject;

namespace AdminSite.DataAccess
{
    public class VacanciesDAL
    {
        public static ResultBOL<DataSet> GetAll()
        {
            string tag = "[VacanciesDAL]" + "[GetAll]";

            string query = "select * from vw_Vacancies";
            var result = DataAccessHelpers.ExecuteQuery(query);

            try
            {
                DataRow row = result.Data.Tables[0].NewRow();
                row["Name_VN"] = "--- Chọn vị trí ---";
                row["Name_EN"] = "--- Select One ---";

                if (result.Data.Tables.Count == 0)
                {
                    DataTable table = new DataTable();
                    result.Data.Tables.Add(table);
                }
                
                result.Data.Tables[0].Rows.InsertAt(row, 0);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
            }

            return result;
        }
    }
}
