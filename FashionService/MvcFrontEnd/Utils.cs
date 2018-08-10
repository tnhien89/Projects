using HtmlAgilityPack;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcFrontEnd
{
    public class Utils
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public static Hashtable GetParameters(object obj)
        {
            Type type = obj.GetType();
            List<PropertyInfo> listProperty = new List<PropertyInfo>(type.GetProperties());
            Hashtable result = new Hashtable();
            foreach (PropertyInfo prop in listProperty)
            {
                result.Add(prop.Name, prop.GetValue(obj, null));
            }

            return result;
        }

        public static string GetImagesUrl(string image)
        { 
#if DEBUG
            _log.Debug("image: {0}", image);
#endif
            if (!string.IsNullOrEmpty(image) && image.Contains("|"))
            {
                image = image.Split('|')[0];
            }

            string url = Path.Combine(ConfigurationManager.AppSettings["ImagesUrl"],
                string.IsNullOrEmpty(image) ? ConfigurationManager.AppSettings["ImageDefault"] : image);
            return url;
        }

        public static string SetFullLinkImage(string html)
        {
            string tag = "[Utilities][SetFullLinkImage]";
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }

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

                    string fullPath = Path.Combine(ConfigurationManager.AppSettings["ImagesUrl"], src);
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
                _log.Error(ex.ToString());
                return html;
            }
        }
    }
}