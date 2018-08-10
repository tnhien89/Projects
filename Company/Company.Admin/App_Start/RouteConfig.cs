using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new {
                    controller = "Home",
                    action = "Login"
                });

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new {
                    controller = "Home",
                    action = "Logout"
                }    
            );

            routes.MapRoute(
                name: "AddMenu",
                url: "Menu/Add",
                defaults: new { controller = "Menu", action = "Add" }
            );

            routes.MapRoute(
                name: "DeleteMenu",
                url: "Menu/Delete",
                defaults: new {
                    controller = "Menu",
                    action = "Delete",
                    ids = UrlParameter.Optional
                }     
            );

            routes.MapRoute(
                name: "DesignMenu",
                url: "Menu/Design",
                defaults: new {
                    controller = "Menu",
                    action = "Design"
                }    
            );

            routes.MapRoute(
                name: "DesignInfo",
                url: "Menu/GetDesignInfo",
                defaults: new {
                    controller = "Menu",
                    action = "GetDesignInfo"
                }
            );

            routes.MapRoute(
                name: "Menu",
                url: "Menu/{key}",
                defaults: new { controller = "Menu", action = "Index", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewPost",
                url: "Posts/Add",
                defaults: new {
                    controller = "Posts",
                    action = "Add"
                }
            );

            routes.MapRoute(
                name: "DeletePost",
                url: "Posts/Delete",
                defaults: new {
                    controller = "Posts",
                    action = "Delete",
                    ids = UrlParameter.Optional
                }    
            );

            routes.MapRoute(
                name: "Posts",
                url: "Posts/{key}",
                defaults: new {
                    controller = "Posts",
                    action = "Index",
                    key = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "CKUpload",
                url: "Home/CKUploadFiles",
                defaults: new {
                    controller = "Home",
                    action = "CKUploadFiles"
                }
            );

            routes.MapRoute(
                name: "ReturnImage",
                url: "Files/{key}",
                defaults: new {
                    controller = "Home",
                    action = "Files",
                    key = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{key}",
                defaults: new { controller = "Home", action = "Index", key = UrlParameter.Optional }
            );
        }
    }
}
