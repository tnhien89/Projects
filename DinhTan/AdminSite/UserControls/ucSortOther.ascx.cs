using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucSortOther : System.Web.UI.UserControl
    {
        private const string __tag = "[ucSortOther]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartBindingOtherTypeItems();
            }
        }

        private void StartBindingOtherTypeItems()
        {
            string tag = __tag + "[StartBindingOtherTypeItems]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = OtherTypeDAL.GetAll();
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Attributes.Remove("hidden");
                    return;
                }

                lbxOther.DataSource = result.Data;
                lbxOther.DataTextField = "Name_VN";
                lbxOther.DataValueField = "Id";
                lbxOther.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Attributes.Remove("hidden");
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }
    }
}