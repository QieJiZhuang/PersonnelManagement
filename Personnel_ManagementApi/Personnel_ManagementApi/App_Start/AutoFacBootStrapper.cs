using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Personnel_ManagementIServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Personnel_ManagementApi.App_Start
{
    public class AutoFacBootStrapper
    {
        public static void CoreAutoFacInit()
        {
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            //注册所有的Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            #region 可正常 用
            var baseType = typeof(IServiceSupport);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var allServices = assemblys.SelectMany(s => s.GetTypes()).Where(p => baseType.IsAssignableFrom(p) && p != baseType);
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            //注册所有的ApiControllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //autofac 注册依赖
            IContainer container = builder.Build();
            // webApi部分注册
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion 可正常用

        }

    }
}