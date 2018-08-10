using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucOtherItems : System.Web.UI.UserControl
    {
        private const string __tag = "[ucOtherItems]";

        private DataTable _dataSource;

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void StartBidingOtherType()
        {
            string tag = __tag + "[StartBidingOtherType]";
            LogHelpers.WriteStatus(tag, "Start...");

            lbError.Visible = false;

            try
            {
                var result = OtherTypeDAL.GetAll();
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    return;
                }

                _dataSource = result.Data.Tables[0];
                grvOtherItems.DataSource = result.Data;
                grvOtherItems.DataKeyNames = new string[] { "Id" };
                grvOtherItems.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void grvOtherItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvOtherItems.PageIndex = e.NewPageIndex;
            grvOtherItems.DataBind();
        }

        protected void grvOtherItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string tag = __tag + "grvOtherItems_RowDataBound";
            LogHelpers.WriteStatus(tag, "Start...");

            if (sender == null ||
                e.Row.RowType != DataControlRowType.DataRow)
            {
                LogHelpers.WriteStatus(tag, "End.");
                return;
            }

            try
            {
                HyperLink hplName = (HyperLink)e.Row.FindControl("Name_VN");
                if (hplName.Text == "Banner")
                {
                    HtmlInputCheckBox chx = (HtmlInputCheckBox)e.Row.FindControl("chkRowItem");
                    chx.Disabled = true;
                    chx.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void grvOtherItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvOtherItems.Columns)
                {
                    if (col.SortExpression == e.SortExpression)
                    {
                        break;
                    }

                    colIndex++;
                }

                Image sortImage = new Image();
                sortImage.CssClass = "imgSortStyle";

                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                    //---
                    sortImage.ImageUrl = Utilities.GetSortImageUrl("ImageSortDesc");
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                    //---
                    sortImage.ImageUrl = Utilities.GetSortImageUrl("ImageSortAsc");
                }

                grvOtherItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvOtherItems_Sorting] Exception" + ex.ToString());
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance
            if (_dataSource == null)
            {
                return;
            }

            _dataSource.DefaultView.Sort = sortExpression + " " + direction;

            grvOtherItems.DataSource = _dataSource;
            grvOtherItems.DataBind();
        }

        public void StartDeleteItems()
        {
            if (grvOtherItems.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvOtherItems.Rows.Count; i++)
            {
                GridViewRow row = grvOtherItems.Rows[i];

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowItem") as HtmlInputCheckBox;

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvOtherItems.DataKeys[i].Value.ToString();
            }

            StartDeleteItemsProcessing(listId);
        }

        private void StartDeleteItemsProcessing(string listId)
        {
            try
            {
                var result = OtherTypeDAL.DeleteAll(listId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                if (result.Data != null
                    && result.Data.Tables.Count > 0
                    && result.Data.Tables[0].Rows.Count > 0)
                {
                    StartShowDeleteMessageErrors(result.Data.Tables[0]);
                }

                lbError.Visible = false;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException("[ucMenuItems][StartDeleteItemsProcessing]", ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartShowDeleteMessageErrors(DataTable dataTable)
        {
            
        }
    }
}