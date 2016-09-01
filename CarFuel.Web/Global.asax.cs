using Autofac;
using Autofac.Integration.Mvc;
using CarFuel.DataAccess;
using CarFuel.DataAccess.Bases;
using CarFuel.DataAccess.Contexts;
using CarFuel.Models;
using CarFuel.Services;
using CarFuel.Services.Bases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarFuel.Web {
  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {
      initAutoFac();
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    private void initAutoFac() {
      var builder = new ContainerBuilder();

      builder.RegisterControllers(typeof(MvcApplication).Assembly);
      
      // Repository
      builder.RegisterType<CarRepository>().AsSelf().As<IRepository<Car>>();
      builder.RegisterType<UserRepository>().AsSelf().As<IRepository<User>>();

      // Services
      builder.RegisterType<CarService>().AsSelf().As<IService<Car>>().As<ICarService>();
      builder.RegisterType<UserService>().AsSelf().As<IService<User>>().As<IUserService>().InstancePerLifetimeScope();

      // DbContexts
      builder.RegisterType<CarFuelDb>().As<DbContext>().InstancePerLifetimeScope();

      var container = builder.Build();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
  }
}
