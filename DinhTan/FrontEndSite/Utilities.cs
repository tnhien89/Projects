using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using FrontEndSite.BusinessObject;
using HtmlAgilityPack;

namespace FrontEndSite
{
    public class Utilities
    {
        public static string GetApplicationSettingsValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void GetMenuIdAndUrl(string contentType, out int id, out string url)
        {
            id = Utilities.GetRequestParameter("MenuId");
            //----
            var redirectBOL = Utilities.GetRedirectUrlObject(contentType);
            url = redirectBOL.Value;
            //----
            if (id < 0)
            {
                id = redirectBOL.Id;
            }
        }

        public static int GetRequestParameter(string key)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request[key]))
            {
                return -1;
            }

            int result;
            if (!int.TryParse(HttpContext.Current.Request[key], out result))
            {
                return -2;
            }

            return result;
        }

        public static int ParseId(object obj)
        {
            if (obj == null)
            {
                return -1;
            }

            int checkId;
            if (!int.TryParse(obj.ToString(), out checkId))
            {
                return -2;
            }

            return checkId;
        }

        public static int GetMenuId(string contentType)
        {
            int menuId = GetRequestParameter("MenuId");
            if (menuId > 0)
            {
                return menuId;
            }

            var BOL = GetRedirectUrlObject(contentType);
            if (BOL == null)
            {
                return -1;
            }

            return BOL.Id;
        }

        public static string GetParentUrl()
        {
            if (HttpContext.Current.Session["ParentUrl"] == null)
            {
                return "Default.aspx";
            }

            string url = HttpContext.Current.Session["ParentUrl"].ToString();
            if (!url.Contains(".aspx"))
            {
                return "Default.aspx";
            }

            return url;
        }

        public static Hashtable GetParameters(object obj)
        {
            Type type = obj.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());
            Hashtable result = new Hashtable();
            foreach (PropertyInfo prop in listProperty)
            {
                result.Add(prop.Name, prop.GetValue(obj));
            }

            return result;
        }

        public static RedirectUrlBOL GetRedirectUrlObject(string name)
        {
            string tag = "[Utilities][GetRedirectUrlObject]";
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/XML/MenuRedirectUrl.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(path);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Name"].ToString() == name)
                    {
                        return new RedirectUrlBOL(row);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
            }

            return new RedirectUrlBOL()
            {
                Id = 0,
                Value = "~/Default.aspx"
            };
        }

        public static string GetNavigationUrl(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key];
            }

            return string.Empty;
        }

        public static string GetNoImageUrl()
        {
            return "~/Images/noimage.png";
        }

        public static string GetSortImageUrl(string key)
        {
            string tag = "[Utilities][GetSortImageUrl]";
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/XML/GridView.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                var node = xmlDoc.SelectSingleNode(string.Format("Resources/{0}", key));
                if (node == null)
                {
                    return string.Empty;
                }

                return node.InnerText;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());

                return string.Empty;
            }
        }

        public static string GetDataUpdatedDate(DataRow row)
        {
            string tag = "[Utilities][GetDataUpdatedDate]";

            if (row == null)
            {
                return "-2";
            }

            try
            {
                DateTime lastItem = row.Field<DateTime>("UpdatedDate");

                return lastItem.ToString("MMddyyyy HH:mm:ss.fff");

            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                return "-3";
            }
        }

        public static string GetErrorMessage(string key)
        {
            string tag = "[Utilities][GetErrorMessage]";
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/XML/ErrorMessage.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(path);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Key"].ToString() == key)
                    {
                        return row["Value"].ToString();
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
                return ex.Message;
            }
        }

        public static void SetParentUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            if (!url.Contains(".aspx"))
            {
                return;
            }

            HttpContext.Current.Session["ParentUrl"] = url;
        }

        public static string GetDirectory(string key)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                return ConfigurationManager.AppSettings[key];
            }

            return string.Empty;
        }

        public static string CreateMapPath(string dir, string file)
        {
            string tag = "[Utilities][CreateMapPath]";
            try
            {
                string result = HttpContext.Current.Server.MapPath(Path.Combine(dir, file));

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(string.Format("{0}  -  Directory: {1} - File: {2}", tag, dir, file), ex.ToString());

                return string.Empty;
            }
        }

        private static string SaveFileToServer(string url, string dir)
        {
            string tag = "[Utilities][SaveFileToServer]";
            try
            {
                string ext = Path.GetExtension(url);
                string fileName = string.Format("{0}{1}", DateTime.Now.ToString("MMddyyyy-HHmmssfff"), ext);
                //---
                string dirServer = HttpContext.Current.Server.MapPath(dir);
                if (!Directory.Exists(dirServer))
                {
                    Directory.CreateDirectory(dirServer);
                }

                string fullPath = Path.Combine(dirServer, fileName);

                WebClient wc = new WebClient();
                wc.DownloadFile(new Uri(url), fullPath);

                return Path.Combine(dir, fileName);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
                return string.Empty;
            }
        }

        public static string ReplaceImageUri(string html, string imageDir)
        {
            string tag = "[Utilities][ReplaceImageUri]";

            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.OptionAutoCloseOnEnd = true;
                htmlDoc.LoadHtml(html);

                foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//img[@src]"))
                {
                    var src = node.Attributes["src"].Value.Split('?')[0];
                    if (!src.Contains("://"))
                    {
                        continue;
                    }

                    //var width = node.Attributes["width"].Value.Replace("px", "");
                    //var height = node.Attributes["height"].Value.Replace("px", "");

                    src = SaveFileToServer(src, imageDir);
                    node.SetAttributeValue("src", src);

                    //node.SetAttributeValue("src", string.Format(
                    //    "{0}?width={1}&height={2}",
                    //    src,
                    //    width == null ? "auto" : width,
                    //    height == null ? "auto" : height));
                }

                return htmlDoc.DocumentNode.OuterHtml;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
                return html;
            }
        }

        public static string SetFullLinkImage(string html, string rootImageDir)
        {
            string tag = "[Utilities][SetFullLinkImage]";

            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.OptionAutoCloseOnEnd = true;
                htmlDoc.LoadHtml(html);

                foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//img[@src]"))
                {
                    var src = node.Attributes["src"].Value.Split('?')[0];
                    if (src.Contains("://"))
                    {
                        continue;
                    }

                    string fullPath = Path.Combine(rootImageDir, src);
                    node.SetAttributeValue("src", fullPath.Replace("\\", "/"));

                    //node.SetAttributeValue("src", string.Format(
                    //    "{0}?width={1}&height={2}",
                    //    src,
                    //    width == null ? "auto" : width,
                    //    height == null ? "auto" : height));
                }

                return htmlDoc.DocumentNode.OuterHtml;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
                return html;
            }
        }

        public static string EncryptPassword(string input)
        {
            try
            {
                MD5 md5 = MD5.Create();

                byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(input));

                StringBuilder result = new StringBuilder();

                for (int i = 0; i < hashData.Length; i++)
                {
                    result.Append(hashData[i].ToString("X2"));
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[Utilities][EncryptPassword] Exception: " + ex.ToString());

                return string.Empty;
            }
        }

        public static void SaveLoggedInfo(UserBOL user)
        {
            try
            {
                var session = HttpContext.Current.Session;
                session["LoggedId"] = user.Id;
                session["LoggedUsername"] = user.Username;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[Utilities][SaveLoggedInfo] Exception: " + ex.ToString());
            }
        }

        public static int GetLoggedId()
        {
            var session = HttpContext.Current.Session;
            if (session["LoggedId"] == null)
            {
                return -1;
            }

            int result;
            if (!int.TryParse(session["LoggedId"].ToString(), out result))
            {
                return -2;
            }

            return result;
        }

        public static void LangueChanged()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["__EVENTARGUMENT"]))
            {
                string changed = HttpContext.Current.Request["__EVENTARGUMENT"];
                if (changed == "LangueEN_Changed")
                {
                    HttpContext.Current.Session["SelectedLangue"] = "EN";
                }
                else if (changed == "LangueVN_Changed")
                {
                    HttpContext.Current.Session["SelectedLangue"] = null;
                }
            }
        }

        public static bool IsLangueEN()
        {

            //if (HttpContext.Current.Session["SelectedLangue"] != null)
            //{
            //    string langue = HttpContext.Current.Session["SelectedLangue"].ToString();
            //    if (langue == "EN")
            //    {
            //        return true;
            //    }
            //}

            try
            {
                //var cookie = HttpContext.Current.Request.Cookies["SelectedLangue"];
                //if (cookie == null)
                //{
                //    return false;
                //}

                //if (cookie.Value != "EN")
                //{
                //    return false;
                //}

                //return true;
                if (HttpContext.Current.Request.RawUrl.Contains("/EN/"))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }

            number = number.Replace(" ", "");
            number = number.Replace(".", "");
            number = number.Replace(")", "");
            number = number.Replace("(", "");
            number = number.Replace("-", "");

            if (number.Length != 10 && number.Length != 11)
            {
                return false;
            }

            string pattern = "^[0-9]{10,11}$";

            return Regex.IsMatch(number, pattern);
        }
    }
}
