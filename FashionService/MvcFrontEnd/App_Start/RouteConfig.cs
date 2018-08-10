using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcFrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ContactInfo",
                url: "Contact",
                defaults: new { 
                    controller = "Home",
                    action = "Contact",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "ServiceDetails",
                url: "{controller}/Details/{id}",
                defaults: new {
                    controller = "Service",
                    action = "Details",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}