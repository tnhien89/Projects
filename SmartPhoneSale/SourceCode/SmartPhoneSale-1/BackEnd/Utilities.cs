using Newtonsoft.Json;
using BackEnd.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BackEnd.Models;
using HtmlAgilityPack;
using System.Xml.Serialization;
using System.Xml;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace BackEnd
{
    public class Utilities
    {
        public static bool IsLoggedUser()
        {
            if (HttpContext.Current.Session["LoggedUser"] == null)
            {
                return false;
            }

            return true;
        }

        public static UserProfile GetLoggedUser()
        {
            LogHelpers.LogHandler.Info("Start.");
            if (HttpContext.Current.Session["LoggedUser"] == null)
            {
                return null;
            }

            try
            {
                UserProfile user = (UserProfile)HttpContext.Current.Session["LoggedUser"];

                return user;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return null;
            }
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

        public static List<T> DataTableToList<T>(DataTable table) where T : new()
        {
            try
            {
                if (table == null || table.Rows.Count == 0)
                {
                    return null;
                }

                List<T> result = new List<T>();
                foreach (DataRow row in table.Rows)
                {
                    var item = CreateObjectWithDataRow<T>(row);
                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return null;
            }
        }

        private static T CreateObjectWithDataRow<T>(DataRow row) where T : new()
        {
            try
            {
                Type type = typeof(T);
                var result = new T();

                foreach (PropertyInfo prop in type.GetProperties())
                {
                    var attributes = prop.GetCustomAttributes(false);
                    string colName = prop.Name;

                    var colMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                    if (colMapping != null)
                    {
                        colName = (colMapping as DbColumnAttribute).Name;
                    }

                    if (!row.Table.Columns.Contains(colName))
                    {
                        continue;
                    }

                    if (row[colName] == DBNull.Value)
                    {
                        prop.SetValue(result, null);
                        continue;
                    }

                    try
                    {
                        prop.SetValue(result, row[colName]);
                    }
                    catch
                    {
                        prop.SetValue(result, Convert.ChangeType(row[colName], prop.PropertyType));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new T();
            }
        }

        public static bool CheckCountryHasStates(string country)
        {
            if (country == "US" ||
                country == "CA" ||
                country == "MX")
            {
                return true;
            }

            return false;
        }
        public static List<SelectListItem> ListCountries()
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/XMLData/countries.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                List<SelectListItem> result = new List<SelectListItem>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new SelectListItem()
                    {
                        Value = row["code"].ToString(),
                        Text = row["country_Text"].ToString()
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error("Exception: " + ex.ToString());
                return null;
            }
        }

        public static List<SelectListItem> ListStates(string type)
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                string fileName = "";
                switch (type)
                {
                    case "CA":
                        fileName = "StateCanada.xml";
                        break;
                    case "MX":
                        fileName = "StateMexico.xml";
                        break;
                    case "US":
                        fileName = "State.xml";
                        break;

                    default:
                        break;
                }

                DataSet ds = new DataSet();
                ds.ReadXml(HttpContext.Current.Server.MapPath("~/XMLData/" + fileName));
                List<SelectListItem> result = new List<SelectListItem>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new SelectListItem()
                    {
                        Value = row["value"].ToString(),
                        Text = row["text"].ToString()
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error("Exception: " + ex.ToString());
                return new List<SelectListItem>();
            }
        }

        public static List<SelectListItem> ListTimeZone()
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/XMLData/TimeZone.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                List<SelectListItem> result = new List<SelectListItem>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new SelectListItem()
                    {
                        Value = row["value"].ToString(),
                        Text = row["text"].ToString()
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return null;
            }
        }

        public static string GetTimeZoneISN(string timeZoneName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/XMLData/TimeZone.xml"));

                string result = string.Empty;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["text"].ToString().ToLower() == timeZoneName.ToLower())
                    {
                        result = ds.Tables[0].Rows[i]["ISN"].ToString();
                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return "-1";
            }
        }

        public static string ListToString(List<string> list, char c)
        {
            if (list == null)
            {
                return "";
            }

            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i].ToString();
                if (i < list.Count - 1)
                {
                    result += c;
                }
            }

            return result;
        }

        public static string FormatPhoneOrZipString(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "";
            }

            string result = phone.Trim();
            result = result.Replace(" ", "");
            result = result.Replace("-", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");

            return result;
        }

        public static DateTime ParseToDate(string str)
        {
            DateTime result;
            if (DateTime.TryParseExact(str, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            if (DateTime.TryParse(str, out result))
            {
                return result;
            }

            return DateTime.MinValue;
        }

        public static string ToMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static void SendCodeSMS_Nexmo(string phoneTo)
        {
            LogHelpers.LogHandler.Info("Start.");

            if (string.IsNullOrEmpty(phoneTo))
            {
                return;
            }

            if (phoneTo.Substring(0, 1) != "1")
                phoneTo = "1" + phoneTo;

            if (HttpContext.Current.Session["VerificationCode"] == null ||
                HttpContext.Current.Session["VerificationCode"].ToString() == "")
            {
                HttpContext.Current.Session["VerificationCode"] = CreateToken(6);
            }

            string sToken = HttpContext.Current.Session["VerificationCode"].ToString();

            string sReturn = "";

            try
            {
                //string content = "api_key=64e53b59&api_secret=33a8e965&to=17179838644&from=12036600275&text=" + "Your security code is " + sToken;
                string sURL = ConfigurationManager.AppSettings["PhoneAPI_URL"];
                string content = "api_key=" + ConfigurationManager.AppSettings["PhoneAPI_AppKey"] + "&api_secret=" + ConfigurationManager.AppSettings["PhoneAPI_AppPass"];
                content += "&to=" + Utilities.FormatPhoneOrZipString(phoneTo) + "&from=" + ConfigurationManager.AppSettings["PhoneAPI_From"];
                content += "&text=" + "Your security code is " + sToken;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";


                byte[] _byteVersion = Encoding.UTF8.GetBytes(content);
                request.ContentLength = _byteVersion.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(_byteVersion, 0, _byteVersion.Length);
                stream.Close();


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    sReturn = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        private static string CreateToken(int length)
        {
            Random random = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10);
            }

            return result;
        }

        public static string GetBrowser()
        {
            if (HttpContext.Current.Request.UserAgent != null)
            {
                if (HttpContext.Current.Request.UserAgent.IndexOf("OPR/") > -1)
                {
                    string sUserAgent = HttpContext.Current.Request.UserAgent;
                    int idx = HttpContext.Current.Request.UserAgent.IndexOf("OPR/");
                    string s = sUserAgent.Substring(idx + 4, 2);
                    return "Opera version " + s;
                }
            }

            HttpBrowserCapabilities caps = HttpContext.Current.Request.Browser;
            string browser = caps.Browser + " version " + caps.Version;
            if (caps.Browser.ToLower() == "applemac-safari" && caps.MajorVersion == 5)
                browser += " (Google Chrome)";

            return browser;
        }

        public static string GetCurrentIP()
        {
            string IP = string.Empty;

            try
            {
                IP = HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }

            return IP;
        }

        public static string GetPortal()
        {
            return "FE Website";
        }

        public static string GetOSName()
        {
            System.OperatingSystem os = System.Environment.OSVersion;
            string osName = "Unknown";


            switch (os.Platform)
            {
                case System.PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0:
                            osName = "Windows 95";
                            break;
                        case 10:
                            osName = "Windows 98";
                            break;
                        case 90:
                            osName = "Windows ME";
                            break;
                    }
                    break;
                case System.PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3:
                            osName = "Windws NT 3.51";
                            break;
                        case 4:
                            osName = "Windows NT 4";
                            break;
                        case 5:
                            if (os.Version.Minor == 0)
                                osName = "Windows 2000";
                            else if (os.Version.Minor == 1)
                                osName = "Windows XP";
                            else if (os.Version.Minor == 2)
                                osName = "Windows Server 2003";
                            break;
                        case 6:
                            //osName = "Windows Vista";
                            if (os.Version.Minor == 0)
                            {
                                if (os.Version.Build == 6002)
                                    osName = "Windows 7";
                                else
                                    osName = "Windows Vista";
                            }
                            else if (os.Version.Minor == 1)
                                osName = "Windows 7";
                            else if (os.Version.Minor == 2)
                                osName = "Windows 8";
                            else if (os.Version.Minor == 3)
                                osName = "Windows 8.1";
                            break;

                    }
                    break;
                case PlatformID.WinCE:
                    osName = "Windows CE";
                    break;
                case PlatformID.Unix:
                    osName = "Unix";
                    break;
            }

            //if (os.ServicePack != "")
            //{
            //    //Append it to the OS name.  i.e. "Windows XP Service Pack 3"
            //    osName += " " + os.ServicePack;
            //}

            return osName; //+ ", " + os.Version.ToString();
        }

        public static int RandomValue(int start, int end, List<int> oldValues)
        {
            Random rand = new Random();
            int val = rand.Next(start, end);

            if (oldValues != null)
            {
                while (oldValues.IndexOf(val) >= 0)
                {
                    val = rand.Next(start, end);
                }
            }

            return val;
        }

        public static void SetCookieExpiration(ref HttpCookie cookie)
        {
            // default 30D
            string expire = "30D";
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CookieExpiration"]))
            {
                expire = ConfigurationManager.AppSettings["CookieExpiration"];
            }

            try
            {
                int val = int.Parse(expire.Substring(0, expire.Length - 1));
                switch (expire[expire.Length - 1])
                {
                    case 'Y':
                        cookie.Expires = DateTime.Now.AddYears(val);
                        break;
                    case 'M':
                        cookie.Expires = DateTime.Now.AddMonths(val);
                        break;
                    case 'D':
                        cookie.Expires = DateTime.Now.AddDays(val);
                        break;
                    case 'H':
                        cookie.Expires = DateTime.Now.AddHours(val);
                        break;
                    case 'm':
                        cookie.Expires = DateTime.Now.AddMinutes(val);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        public static string GetBankISNWithFrontEndUrl()
        {
            LogHelpers.LogHandler.Info("Start");
            try
            {
                if (HttpContext.Current.Session["BankISN"] != null)
                {
                    return HttpContext.Current.Session["BankISN"].ToString();
                }

                string linkFE = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LinkFE"]) ? string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.Url.Authority,
                    VirtualPathUtility.ToAbsolute("~/")) : ConfigurationManager.AppSettings["LinkFE"];

                if (linkFE[linkFE.Length - 1] == '/')
                {
                    linkFE = linkFE.Remove(linkFE.Length - 1, 1);
                }

                string query = "select top 1 BankISN from BankConfig where LinkFE = @LinkFE";
                object obj = new
                {
                    LinkFE = linkFE
                };

                var result = DataAccessHelpers.ExecuteScalar(query, Utilities.GetParameters(obj));
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    return "";
                }

                if (result.Data != null)
                {
                    HttpContext.Current.Session["BankISN"] = result.Data;
                }

                return result.Data.ToString();
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return "";
            }
        }

        public static string ReplaceImageUri(int id, string html, string dir)
        {
            try
            {
                LogHelpers.LogHandler.Info(string.Format("id: {0}, dir: {1}",
                    id, dir));

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

                    src = SaveFileToServer(id, src, dir);

                    //var width = node.Attributes["width"].Value.Replace("px", "");
                    //var height = node.Attributes["height"].Value.Replace("px", "");


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
                LogHelpers.LogHandler.Error(ex.ToString());
                return html;
            }
        }

        private static string SaveFileToServer(int id, string url, string dir)
        {
            LogHelpers.LogHandler.Info(string.Format("id: {0}, Url: {1}, Dir: {2}",
                id, url, dir));
            //----
            try
            {
                string ext = Path.GetExtension(url);
                string fileName = DateTime.Now.Ticks.ToString() + ext;
                //---
                string dirServer = Path.Combine(dir, id.ToString());
                if (!Directory.Exists(dirServer))
                {
                    Directory.CreateDirectory(dirServer);
                }

                string fullPath = Path.Combine(dirServer, fileName);
                if (File.Exists(fullPath))
                {
                    return fileName;
                }

                WebClient wc = new WebClient();
                wc.DownloadFile(new Uri(url), fullPath);

                return fileName;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return string.Empty;
            }
        }

        public static ResultData<string> SaveFileToServer(HttpPostedFileBase file, string folder, string fileName)
        {
            LogHelpers.LogHandler.Info("Start");
            //----
            try
            {
                string path = Utilities.UploadFilesTempDir();
                path = Path.Combine(path, folder);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                }

                path = Path.Combine(path, fileName);

                file.SaveAs(path);
                return new ResultData<string>() {
                    Code = 0,
                    Data = fileName
                };
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new ResultData<string>() {
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static void MoveFile(string fileName, string fromDir, string toDir)
        {
            try
            {
                LogHelpers.LogHandler.Info(string.Format("fileName: {0}, fromDir: {1}, toDir: {2}", fileName, fromDir, toDir));
                if (!Directory.Exists(toDir))
                {
                    Directory.CreateDirectory(toDir);
                }

                string path = Path.Combine(fromDir, fileName);
                if (!File.Exists(path))
                {
                    LogHelpers.LogHandler.Error("File does not exists");
                    return;
                }

                string toPath = Path.Combine(toDir, fileName);
                if (File.Exists(toPath))
                {
                    File.Delete(toPath);
                }

                File.Move(path, toPath);
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
            }
        }

        public static string XmlSerializeObject(object obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, obj);

                    string result = writer.ToString();
                    writer.Close();
                    writer.Dispose();

                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return string.Empty;
            }
        }

        public static T XmlDeserializeObject<T>(string xmlString) where T : new()
        {
            try
            {
                if (string.IsNullOrEmpty(xmlString))
                {
                    return new T();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));

                T result = (T)serializer.Deserialize(stream);
                stream.Close();

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return new T();
            }
        }

        //----
        public static string UploadImagesDir()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/UploadImages"));
        }

        public static string UploadFilesTempDir()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/UploadImages/Temp"));
        }

        public static string TrimmingString(string s)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = s.Normalize(NormalizationForm.FormD);
                return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return "";
            }
        }  
    }
}