using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserLogin",
                url: "Login",
                defaults: new { 
                    controller = "Home",
                    action = "Login"
                });

            routes.MapRoute(
                name: "UserProfile",
                url: "UserProfile",
                defaults: new { 
                    controller = "Home",
                    action = "UserProfile"
                });

            routes.MapRoute(
                name: "UsersDefault",
                url: "Users/{action}",
                defaults: new { 
                    controller = "Users",
                    action = "Index"
                });


            routes.MapRoute(
                name: "UploadLogo",
                url: "UploadLogo",
                defaults: new { 
                    controller = "Home",
                    action = "UploadLogo"
                });

            routes.MapRoute(
                name: "AddMenu",
                url: "Menu/Add",
                defaults: new { 
                    controller = "Menu",
                    action = "Add"
                });

            routes.MapRoute(
                name: "EditMenu",
                url: "Menu/Edit/{id}",
                defaults: new
                {
                    controller = "Menu",
                    action = "Edit",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "DeleteMenu",
                url: "Menu/Delete",
                defaults: new {
                    controller = "Menu",
                    action = "Delete"
                });

            routes.MapRoute(
                name: "DefaultMenu",
                url: "Menu/{action}",
                defaults: new
                {
                    controller = "Menu",
                    action = "Index"
                });

            routes.MapRoute(
                name: "AddNews",
                url: "News/Add",
                defaults: new { 
                    controller = "News",
                    action = "Add"
                });

            routes.MapRoute(
                name: "EditNews",
                url: "News/Edit/{id}",
                defaults: new { 
                    controller = "News",
                    action = "Edit",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "DeleteNews",
                url: "News/Delete",
                defaults: new { 
                    controller = "News",
                    action = "Delete"
                });

            routes.MapRoute(
                name: "DefaultNews",
                url: "News/{action}",
                defaults: new
                {
                    controller = "News",
                    action = "Index"
                });

            routes.MapRoute(
                name: "SetupDefault",
                url: "Setup/{action}",
                defaults: new {
                    controller = "Setup",
                    action = "Index"
                });

            routes.MapRoute(
                name: "Default",
                url: "{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}