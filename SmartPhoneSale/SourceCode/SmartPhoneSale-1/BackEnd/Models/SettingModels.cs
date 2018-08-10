using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Models
{
    public class SiteSetting : BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string XmlData { get; set; }
        public string Description { get; set; }
        public int InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public SiteSetting()
        { }

        public SiteSetting(DataRow row)
            : base(row)
        { }
    }

    public class SiteConfig : BaseObject
    {
        public string HeaderBackground { get; set; }
        public string Logo { get; set; }
        public string LogoFullUrl { 
            get {
                if (string.IsNullOrEmpty(Logo))
                {
                    return "";
                }

                return Path.Combine(ConfigurationManager.AppSettings["UploadImagesUrl"], 
                    "Logo", 
                    Logo) + "?t=" + DateTime.Now.Ticks.ToString();
            } 
        }
        public string HeaderText { get; set; }
        //---
        public string Information { get; set; }
        public string GoogleMapsLocation { get; set; } // address
        public double Latitude { get; set; }
        public double longitude { get; set; }

        public SiteConfig()
        { }
    }
}