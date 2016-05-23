using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using Notepad.Models;

namespace Notepad
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Services.Insert(typeof(ModelBinderProvider), 0, new SimpleModelBinderProvider(typeof(NewNotepadModel), new NewModel()));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{name}",
                defaults: new { name = RouteParameter.Optional }
            );
        }
    }
}
