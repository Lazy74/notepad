using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Notepad
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new
                {
                    controller = "Start",
                    action = "NotepadPage"
                }
            );

            routes.MapRoute(
                name: "CreateNotepad",
                url: "notepad/Create/",
                defaults: new
                {
                    controller = "Start",
                    action = "CreateNotepadPage",
                    text = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Notepad",
                url: "notepad/{name}/",
                defaults: new
                {
                    controller = "Start",
                    action = "NotepadPage",
                    name = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Image",
                url: "Image/{text}/",
                defaults: new
                {
                    controller = "Start",
                    action = "Image",
                    text = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Log",
                url: "Log",
                defaults: new
                {
                    controller = "Start",
                    action = "LogPage"
                }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new
            //    {
            //        controller = "Start",
            //        action = "notepad",
            //        id = UrlParameter.Optional
            //    }
            //);
        }
    }
}
