using Autofac;
using Autofac.Integration.Mvc;
using Personnel_ManagementIServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Personnel_ManagementApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务 var builder = new ContainerBuilder();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{name}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
