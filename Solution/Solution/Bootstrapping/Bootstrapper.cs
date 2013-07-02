using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Domain.Core.Validation;

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
            builder.RegisterType<DefaultValidationBus>().As<IValidationBus>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("Utilities")).AsImplementedInterfaces().InstancePerHttpRequest();
           
          //  builder.RegisterAssemblyTypes(typeof(CategoryRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerHttpRequest();          
            var applicationLayerAssembly = Assembly.Load("Application");
            builder.RegisterAssemblyTypes(applicationLayerAssembly).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(applicationLayerAssembly)
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();                  
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));            
        }        
    }
}
