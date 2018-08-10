using AdminSite.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AdminSite.UserControls
{
    public partial class ucServiveItems : System.Web.UI.UserControl
    {
        private string _navigationUrl;
        private int _menuId;

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

        public void StartDeleteItems()
        {
            if (this.grvServiceItems.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvServiceItems.Rows.Count; i++)
            {
                GridViewRow row = grvServiceItems.Rows[i];

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowMenu") as HtmlInputCheckBox;

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvServiceItems.DataKeys[i].Value.ToString();
            }

            StartDeleteItemsProcessing(listId);
        }

        private void StartDeleteItemsProcessing(string listId)
        {
            try
            {
                var result = NewsDAL.DeleteAll(listId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                lbError.Visible = false;

                if (result.Data != null &&
                    result.Data.Tables.Count > 0 &&
                    result.Data.Tables[0].Rows.Count > 0)
                {
                    ShowDeleteMessageErrors(result.Data.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException("[ucNewsItems][StartDeleteItemsProcessing]", ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void ShowDeleteMessageErrors(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public void StartBindingData(int menuId, string navigationUrl)
        {
            try
            {
                var result = NewsDAL.GetAll(menuId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = false;
                    return;
                }

                _menuId = menuId;
                _navigationUrl = navigationUrl;

                _dataSource = result.Data.Tables[0];
                this.grvServiceItems.DataSource = result.Data;
                grvServiceItems.DataKeyNames = new string[] { "Id" };
                grvServiceItems.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][StartBindingData] Exception: " + ex.ToString());
            }
        }

        public void ShowErrorMessage(string msg)
        {
            lbError.InnerText = msg;
            lbError.Visible = true;
        }

        public void HideErrorMessage()
        {
            lbError.Visible = false;
        }

        public void SetHeader(string header)
        {
            this.lbHeader.InnerText = header;
        }

        protected void grvServiceItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvServiceItems.PageIndex = e.NewPageIndex;
            grvServiceItems.DataBind();
        }

        protected void grvServiceItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvServiceItems.Columns)
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

                grvServiceItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvServiceItems_Sorting] Exception" + ex.ToString());
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

            grvServiceItems.DataSource = _dataSource;
            grvServiceItems.DataBind();
        }

        protected void grvServiceItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            try
            {
                string id = grvServiceItems.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink newsLink = (HyperLink)e.Row.Cells[1].Controls[0];
                newsLink.NavigateUrl = string.Format("~/{0}?MenuId={1}&Id={2}", _navigationUrl, _menuId, id);
                //---
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvServiceItems_RowDataBound]", ex.ToString());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_navigationUrl))
            {
                return;
            }

            Response.Redirect(string.Format("~/{0}?MenuId={1}", _navigationUrl, _menuId), false);
        }
    }
}