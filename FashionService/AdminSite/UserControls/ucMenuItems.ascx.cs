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
    public partial class ucMenuItems : System.Web.UI.UserControl
    {
        private DataTable _dataSource;
        private int _menuId;

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
                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "DeleteMenu")
                {
                    StartDeleteMenuItems();
                }
            }
        }

        private void StartDeleteMenuItems()
        {
            if (grvIntroMenu.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvIntroMenu.Rows.Count; i++)
            {
                GridViewRow row = grvIntroMenu.Rows[i];

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowMenu") as HtmlInputCheckBox;

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvIntroMenu.DataKeys[i].Value.ToString();
            }

            StartDeleteMenuItemsProcessing(listId);
        }

        private void StartDeleteMenuItemsProcessing(string listId)
        {
            try
            {
                var result = MenuDAL.DeleteAll(listId);
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
                LogHelpers.WriteException("[ucMenuItems][StartDeleteMenuItemsProcessing]", ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartShowDeleteMessageErrors(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public void StartBindingData(int id)
        {
            _menuId = id;
            try
            {
                var result = MenuDAL.GetAll(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                lbError.Visible = false;
                _dataSource = result.Data.Tables[0];
                this.grvIntroMenu.DataSource = result.Data;
                grvIntroMenu.DataKeyNames = new string[] { "Id" };
                this.grvIntroMenu.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucMenuItems][StartBindingData] Exception: " + ex.ToString());
            }
        }

        public void ShowErrorMessage(string msg)
        {
            this.lbError.InnerText = msg;
            this.lbError.Visible = true;
        }

        public void HideErrorMessagee()
        {
            this.lbError.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            string parentId = _menuId.ToString();

            for (int i = 0; i < grvIntroMenu.Rows.Count; i++)
            {
                GridViewRow row = grvIntroMenu.Rows[i];

                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowMenu") as HtmlInputCheckBox;
                if (chx.Checked)
                {
                    parentId = grvIntroMenu.DataKeys[i].Value.ToString();
                    break;
                }
            }

            if (!string.IsNullOrEmpty(parentId))
            {
                HttpContext.Current.Response.Redirect(string.Format("~/MenuDetails.aspx?ParentId={0}", parentId));
            }
            else if (Session["SelectedMenuId"] != null)
            {
                HttpContext.Current.Response.Redirect(string.Format("~/MenuDetails.aspx?ParentId={0}", Session["SelectedMenuId"]));
            }
        }

        protected void grvIntroMenu_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvIntroMenu.Columns)
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

                grvIntroMenu.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
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

            grvIntroMenu.DataSource = _dataSource;
            grvIntroMenu.DataBind();
        }

        protected void grvIntroMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvIntroMenu.PageIndex = e.NewPageIndex;
            grvIntroMenu.DataBind();

            //if (pageChanging != null)
            //{
            //    pageChanging();
            //}
        }
    }
}