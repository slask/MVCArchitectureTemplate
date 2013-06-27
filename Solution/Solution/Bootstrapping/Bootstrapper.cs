using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace Solution.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }    
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();          
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerHttpRequest();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
            //builder.RegisterAssemblyTypes(typeof(CategoryRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerHttpRequest();          
           // var services = Assembly.Load("Domain");
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerHttpRequest();
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();
            //builder.RegisterType<DefaultFormsAuthentication>().As<IFormsAuthentication>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("Application"))
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerHttpRequest();
            builder.RegisterFilterProvider();
            IContainer container = builder.Build();                  
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));            
        }        
    }
}
