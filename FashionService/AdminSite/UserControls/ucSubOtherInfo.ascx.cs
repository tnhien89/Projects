using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using NLog;

namespace AdminSite.UserControls
{
    public partial class ucSubOtherInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucSubOtherInfo]";
        private Logger _log = LogManager.GetCurrentClassLogger();

        private int _catId = 0;
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _catId = Utilities.GetRequestParameter("CatId");
            _id = Utilities.GetRequestParameter("Id");

            if (_catId <= 0)
            {
                Response.Redirect("~/Other.aspx", false);
            }

            btnSubmit.Text = "Add";

            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    StartLoadSubOtherInfo(_id);
                }
            }
        }

        private void StartLoadSubOtherInfo(int id)
        {
            string tag = __tag + "[StartLoadSubOtherInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            lbError.Visible = false;

            try
            {
                var result = OtherDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                OtherBOL other = new OtherBOL(result.Data.Tables[0].Rows[0]);
                tbxNameVN.Text = other.Name_VN;
                tbxNameEN.Text = other.Name_EN;
                tbxLink.Text = other.Link;
                if (!string.IsNullOrEmpty(other.ImageLink))
                {
                    //imgImage.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), other.ImageLink.Split('|')[0]);
                    //imgImage.Visible = true;
                    //---
                    groupImages.Visible = true;
                    StartLoadImages(other.ImageLink);
                }
                else
                {
                    //imgImage.Visible = false;
                    groupImages.Visible = false;
                }
                tbxDesVN.Text = other.Description_VN;
                tbxDesEN.Text = other.Description_EN;

                btnSubmit.Text = "Update";
                btnCancel.Visible = true;
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

        private void StartLoadImages(string imageLinks)
        {
            if (string.IsNullOrEmpty(imageLinks))
            {
                return;
            }

#if DEBUG
            _log.Debug("imageLinks: {0}", imageLinks);
#endif

            try
            {
                string[] images = imageLinks.Split('|');
                if (images != null && images.Length > 0)
                {
                    lvProjectImages.DataSource = images.ToList();
                    lvProjectImages.DataBind();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            InsertOrUpdateBannerProcess();
        }

        private void InsertOrUpdateBannerProcess()
        {
            string tag = __tag + "[InsertOrUpdateBannerProcess]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                OtherBOL banner = new OtherBOL()
                {
                    Id = _id > 0 ? _id : 0,
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Description_VN = tbxDesVN.Text,
                    Description_EN = tbxDesEN.Text,
                    Link = tbxLink.Text,
                    ImageLink = UploadImageProcess(),
                    ParentId = _catId,
                    OtherType = _catId,
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = OtherDAL.InsertOrUpdate(banner);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                StartClearData();

                Response.Redirect(Request.RawUrl, false);
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

        private string UploadImageProcess()
        {
            string tag = __tag + "[UploadImageProcess]";
            LogHelpers.WriteStatus(tag, "Start...");


            try
            {
                if (!fileUploadImage.HasFile)
                {
                    return string.Empty;
                }

                List<string> fileNames = new List<string>();
                string dir = Server.MapPath("~/" + Utilities.GetDirectory("ImagesDir"));

                foreach (HttpPostedFile file in fileUploadImage.PostedFiles)
                {
                    string fileName = file.FileName;
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    try
                    {
                        file.SaveAs(Path.Combine(dir, fileName));
                        fileNames.Add(fileName);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.ToString());
                    }
                }

                return String.Join("|", fileNames);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());

                lbError.InnerText = ex.Message;
                lbError.Visible = true;

                return string.Empty;
            }
        }

        private void StartClearData()
        {
            tbxNameVN.Text = "";
            tbxNameEN.Text = "";
            tbxLink.Text = "";
            tbxDesVN.Text = "";
            tbxDesEN.Text = "";
            groupImages.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Other.aspx?Id=" + _catId.ToString(), false);
        }

        protected void lvProjectImages_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            try
            {
                string imageLink = e.Item.DataItem.ToString();
                Image img = (Image)e.Item.FindControl("itemImage");
                //---
                img.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), imageLink);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}