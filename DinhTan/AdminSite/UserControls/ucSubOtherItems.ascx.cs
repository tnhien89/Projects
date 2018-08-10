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
    public partial class ucSubOtherItems : System.Web.UI.UserControl
    {
        private const string __tag = "[ucOtherItems]";

        private int _catId;
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

        public void StartBidingOther(int catId)
        {
            _catId = catId;

            string tag = __tag + "[StartBidingOther]";
            LogHelpers.WriteStatus(tag, "CatId: " + _catId.ToString() ,"Start...");

            lbError.Visible = false;

            try
            {
                if (_catId <= 0)
                {
                    return;
                }

                var result = OtherDAL.GetAll(_catId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    return;
                }

                _dataSource = result.Data.Tables[0];
                grvSubOtherItems.DataSource = result.Data;
                grvSubOtherItems.DataKeyNames = new string[] { "Id" };
                grvSubOtherItems.DataBind();
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

        protected void grvSubOtherItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSubOtherItems.PageIndex = e.NewPageIndex;
            grvSubOtherItems.DataBind();
        }

        protected void grvSubOtherItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            string tag = __tag + "[grvSubOtherItems_Sorting]";

            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvSubOtherItems.Columns)
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

                grvSubOtherItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }

        protected void grvSubOtherItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (_catId <= 0)
            {
                _catId = Utilities.GetRequestParameter("CatId");
            }

            string tag = __tag + "grvSubOtherItems_RowDataBound";
            LogHelpers.WriteStatus(tag, "Start...");

            if (sender == null ||
                e.Row.RowType != DataControlRowType.DataRow)
            {
                LogHelpers.WriteStatus(tag, "End.");
                return;
            }

            try
            {
                Image imgLink = (Image)e.Row.FindControl("ImageLink");
                imgLink.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImageOtherDir"),
                    imgLink.ImageUrl);

                HyperLink hplLink = (HyperLink)e.Row.FindControl("Name_VN");
                hplLink.NavigateUrl = string.Format("~/OtherDetail.aspx?CatId={0}&Id={1}",
                    _catId,
                    hplLink.NavigateUrl);
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

        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance
            if (_dataSource == null)
            {
                return;
            }

            _dataSource.DefaultView.Sort = sortExpression + " " + direction;

            grvSubOtherItems.DataSource = _dataSource;
            grvSubOtherItems.DataBind();
        }

        public void StartDeleteItems()
        {
            if (grvSubOtherItems.Rows.Count == 0)
            {
                return;
            }

            string listId = string.Empty;

            for (int i = 0; i < grvSubOtherItems.Rows.Count; i++)
            {
                GridViewRow row = grvSubOtherItems.Rows[i];

                HtmlInputCheckBox chx = row.Cells[0].FindControl("chkRowItem") as HtmlInputCheckBox;

                if (chx.Disabled)
                {
                    continue;
                }

                if (!chx.Checked)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(listId))
                {
                    listId += ",";
                }

                listId += grvSubOtherItems.DataKeys[i].Value.ToString();
            }

            StartDeleteItemsProcessing(listId);
        }

        private void StartDeleteItemsProcessing(string listId)
        {
            try
            {
                var result = OtherDAL.DeleteAll(listId);
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