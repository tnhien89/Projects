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

namespace AdminSite.UserControls
{
    public partial class ucProjectItems : System.Web.UI.UserControl
    {
        private string _navigationUrl;
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

        }

        

        public void StartDeleteProjects()
        {
            if (this.grvProjectItems.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvProjectItems.Rows.Count; i++)
            {
                GridViewRow row = grvProjectItems.Rows[i];

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowItem") as HtmlInputCheckBox;

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvProjectItems.DataKeys[i].Value.ToString();
            }

            if (!string.IsNullOrEmpty(listId))
            {
                var result = ProjectsDAL.DeleteAll(listId);
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
                    StartShowDeleteErrors(result);
                }
            }
        }

        private void StartShowDeleteErrors(BusinessObject.ResultBOL<System.Data.DataSet> result)
        {
            
        }

        public void StartBindingData(int menuId)
        {
            _navigationUrl = "ProjectDetail.aspx";
            _menuId = menuId;

            try
            {
                var result = ProjectsDAL.GetAll(menuId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                lbError.Visible = false;
                _dataSource = result.Data.Tables[0];
                grvProjectItems.DataSource = result.Data;
                grvProjectItems.DataKeyNames = new string[] { "Id" };
                grvProjectItems.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucProjectItems][StartBindingData] Exception: " + ex.ToString());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("~/{0}?MenuId={1}", Utilities.GetNavigationUrl("ProjectDetail"),
                    _menuId), false);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucProjectItems][btnAdd_Click] Exception: "  + ex.ToString());
            }
        }

        protected void grvProjectItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvProjectItems.PageIndex = e.NewPageIndex;
            grvProjectItems.DataBind();
        }

        protected void grvProjectItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            int colIndex = 0;
            foreach (DataControlField col in grvProjectItems.Columns)
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

                sortImage.ImageUrl = Utilities.GetSortImageUrl("ImageSortDesc");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);

                sortImage.ImageUrl = Utilities.GetSortImageUrl("ImageSortAsc");
            }

            grvProjectItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
        }

        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance
            if (_dataSource == null)
            {
                return;
            }

            _dataSource.DefaultView.Sort = sortExpression + direction;

            grvProjectItems.DataSource = _dataSource;
            grvProjectItems.DataBind();
        }

        protected void grvProjectItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            try
            {
                string id = this.grvProjectItems.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink newsLink = (HyperLink)e.Row.Cells[1].Controls[0];
                newsLink.NavigateUrl = string.Format("~/{0}?MenuId={1}&Id={2}", _navigationUrl, _menuId, id);
                //---
                Image imgLink = (Image)e.Row.FindControl("ImageLink");
                imgLink.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), imgLink.ImageUrl.Split('|')[0]);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][grvIntroItems_RowDataBound]", ex.ToString());
            }
        }
    }
}