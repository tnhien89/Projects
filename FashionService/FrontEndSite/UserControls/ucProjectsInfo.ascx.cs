using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucProjectsInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucProjectsInfo]";
        //---
        private int _selectedId;
        private int _itemsCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartLoadProjectInfo(int id)
        {
            string tag = __tag + "[StartLoadProjectInfo]";
            //---
            LogHelpers.WriteStatus(tag, "Id = " + id, "Start...");
            //---
            if (id <= 0)
            {
                return;
            }
            _selectedId = id;

            try
            {
                var result = ProjectsDAL.GetAllFromParent(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    //---
                    return;
                }

                lbError.Visible = false;
                //---
                StartLoadProjectDetail(result.Data.Tables[0].Rows[0]);
                //---
                _itemsCount = result.Data.Tables[1].Rows.Count;
                //---
                lvProjectsCarousel.DataSource = result.Data.Tables[1];
                lvProjectsCarousel.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }

            LogHelpers.WriteStatus(tag, "End.");
        }

        private void StartLoadProjectDetail(DataRow row)
        {
            string tag = __tag + "StartLoadProjectDetail";
            LogHelpers.WriteStatus(tag, "Start....");

            if (row == null)
            {
                lbError.InnerText = Utilities.GetErrorMessage("NullObject");
                lbError.Visible = true;

                return;
            }

            try
            {
                ProjectBOL project = new ProjectBOL(row);
                lbProjectNameInfo.InnerText = Utilities.IsLangueEN() ? project.Name_EN : project.Name_VN;
                //imgCurrentProject.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageRootDir"), project.ImageLink);
                lbContent.Text = Utilities.IsLangueEN() ?
                    Utilities.SetFullLinkImage(project.Content_EN, Utilities.GetDirectory("ImageProjectsDir")) :
                    Utilities.SetFullLinkImage(project.Content_VN, Utilities.GetDirectory("ImageProjectsDir"));
                //---
                StartLoadProjectImages(project.ImageLink);
                //---
                lbProjectAddressInfo.InnerText = Utilities.IsLangueEN() ? project.Address_EN : project.Address_VN;
                if (project.StartDate != null &&
                    project.StartDate != DateTime.MinValue)
                {
                    this.lbStartDateInfo.InnerText = project.StartDate.ToString("MM/dd/yyyy");
                }

                if (project.EndDate != null &&
                    project.EndDate != DateTime.MinValue)
                {
                    this.lbEndDateInfo.InnerText = project.EndDate.ToString("MM/dd/yyyy");
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartLoadProjectImages(string imageLinks)
        {
            if (string.IsNullOrEmpty(imageLinks))
            {
                return;
            }

            string tag = __tag + "[StartLoadProjectImages]";
            LogHelpers.WriteStatus(tag, "ImageLinks: " + imageLinks, "Start....");
            //---
            try
            {
                string[] images = imageLinks.Split('|');
                for (int i = 0; i < images.Length; i++)
                {
                    //---
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.Attributes.Add("data-target", "#ProjectImagesCarousel");
                    li.Attributes.Add("data-slide-to", i.ToString());
                    if (i == 0)
                    {
                        li.Attributes.Add("class", "active");
                    }

                    olProjectIndicators.Controls.Add(li);
                    //---
                    string imgUrl = Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), images[i]);
                    //--
                    HtmlGenericControl divImage = new HtmlGenericControl("div");
                    if (i == 0)
                    {
                        divImage.Attributes.Add("class", "item active");
                    }
                    else
                    {
                        divImage.Attributes.Add("class", "item");
                    }
                    //---
                    HtmlGenericControl img = new HtmlGenericControl("img");
                    img.Attributes.Add("src", imgUrl);
                    //---
                    divImage.Controls.Add(img);
                    ProjectImagesCarouselInner.Controls.Add(divImage);
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }

        protected void lvProjectsCarousel_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvProjectsCarousel_ItemDataBound]";

            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }



            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                //---
                HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");
                HiddenField hfName = (HiddenField)e.Item.FindControl("hfName");
                hfName.Value = Utilities.IsLangueEN() ? rowView["Name_EN"].ToString() : rowView["Name_VN"].ToString();
                //---
                if (hfId.Value == _selectedId.ToString())
                {
                    HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("divItem");
                    div.Attributes.Add("class", "item active");
                    //----
                    //DataRowView rowView = (DataRowView)e.Item.DataItem;
                    //imgCurrentProject.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageRootDir"), rowView["ImageLink"].ToString());
                    //---
                }

                Label lbItemsCount = (Label)e.Item.FindControl("lbItemsCount");
                lbItemsCount.Text = string.Format("{0}/{1}", e.Item.DataItemIndex + 1, _itemsCount);

                if (e.Item.DataItemIndex > 0)
                {
                    ListViewItem prevItem = (ListViewItem)lvProjectsCarousel.Controls[e.Item.DataItemIndex - 1];
                    //----
                    HiddenField hfPrevName = (HiddenField)prevItem.FindControl("hfName");
                    //----
                    Label lbPrevTitle = (Label)e.Item.FindControl("lbPrevTitle");
                    Label lbNextTitle = (Label)prevItem.FindControl("lbNextTitle");
                    //-----
                    lbPrevTitle.Text = hfPrevName.Value;
                    lbNextTitle.Text = hfName.Value;
                    //----
                    // set prevTitle first item
                    if (e.Item.DataItemIndex == _itemsCount - 1)
                    {
                        Label lbFirstPrevTitle = (Label)lvProjectsCarousel.Controls[0].FindControl("lbPrevTitle");
                        lbFirstPrevTitle.Text = hfName.Value;
                        //----
                        Label lbLastNextTitle = (Label)e.Item.FindControl("lbNextTitle");
                        lbLastNextTitle.Text = ((HiddenField)lvProjectsCarousel.Controls[0].FindControl("hfName")).Value;
                    }
                    //----
                    // set nextTitle last item
                    
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }
    }
}