using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucContacts : System.Web.UI.UserControl
    {
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

        public void StartBindingData()
        {
            var result = ContactsDAL.GetAll();
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;
            }

            lbError.Visible = false;
            //---
            _dataSource = result.Data.Tables[0];
            grvContactsItems.DataSource = result.Data;
            grvContactsItems.DataKeyNames = new string[] { "Id" };
            grvContactsItems.DataBind();
        }

        protected void grvContactsItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvContactsItems.PageIndex = e.NewPageIndex;
            grvContactsItems.DataBind();
        }

        protected void grvContactsItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                int colIndex = 0;
                foreach (DataControlField col in grvContactsItems.Columns)
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

                grvContactsItems.HeaderRow.Cells[colIndex].Controls.Add(sortImage);
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

            this.grvContactsItems.DataSource = _dataSource;
            grvContactsItems.DataBind();
        }

        protected void grvContactsItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}