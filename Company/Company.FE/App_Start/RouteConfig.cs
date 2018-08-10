using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company.FE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ReturnImage",
                url: "Files/{key}",
                defaults: new
                {
                    controller = "Home",
                    action = "Files",
                    key = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Recruitment",
                url: "tuyen-dung/{key}",
                defaults: new {
                    controller = "Recruitment",
                    action = "Index",
                    key = UrlParameter.Optional
                }    
            );

            routes.MapRoute(
                name: "Town",
                url: "viec-lam-theo-quan/{key}",
                defaults: new {
                    controller = "Town",
                    action = "Index",
                    key = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc/{key}",
                defaults: new {
                    controller = "News",
                    action = "Index",
                    key = UrlParameter
                    .Optional
                }   
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new {
                    controller = "Contact",
                    action = "Index"
                }    
            );

            routes.MapRoute(
                name: "Default",
                url: "{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
