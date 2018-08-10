using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AdminSite.DataAccess;
using AdminSite.BusinessObject;

namespace AdminSite.UserControls
{
    public partial class ucNewsItems : System.Web.UI.UserControl
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
            if (IsPostBack)
            {
                
            }
        }

        public void StartDeleteItems()
        {
            if (this.grvIntroItems.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvIntroItems.Rows.Count; i++)
            {
                GridViewRow row = grvIntroItems.Rows[i];
                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowItem") as HtmlInputCheckBox;

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvIntroItems.DataKeys[i].Value.ToString();
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
                grvIntroItems.DataSource = result.Data;
                grvIntroItems.DataKeyNames = new string[] { "Id" };
                grvIntroItems.DataBind();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_navigationUrl))
            {
                return;
            }

            Response.Redirect(string.Format("~/{0}?MenuId={1}", _navigationUrl, _menuId), false);
        }

        protected void grvIntroItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvIntroItems.PageIndex = e.NewPageIndex;
            grvIntroItems.DataBind();
        }

        protected void grvIntroItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvIntroItems.Columns)
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

                grvIntroItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvIntroItems_Sorting] Exception" + ex.ToString());
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
             
            grvIntroItems.DataSource = _dataSource;
            grvIntroItems.DataBind();
        }

        protected void grvIntroItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            try
            {
                string id = grvIntroItems.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink newsLink = (HyperLink)e.Row.Cells[1].Controls[0];
                newsLink.NavigateUrl = string.Format("~/{0}?MenuId={1}&Id={2}", _navigationUrl, _menuId, id);
                //---
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                NewsBOL news = new NewsBOL(rowView.DataView.Table.Rows[e.Row.RowIndex]);
                HtmlInputCheckBox chx = e.Row.Cells[3].FindControl("chkDisable") as HtmlInputCheckBox;
                chx.Attributes.Add("data-id", news.Id.ToString());
                if (news.Disable)
                {
                    chx.Checked = true;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvIntroItems_RowDataBound]", ex.ToString());
            }
        }
    }
}