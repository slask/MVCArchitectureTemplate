using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using DataAccess.Context;
using Domain.Core.Validation;

namespace Solution.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void Run(bool testMode = false)
        {
            SetAutofacContainer(!testMode ? "DefaultConnection" : "TestDB");
        }

        private static void SetAutofacContainer(string dbConnectionString)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DefaultValidationBus>().As<IValidationBus>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("Utilities")).AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterType<ScrabbleClubContext>().WithParameter("connectionStringName", dbConnectionString).InstancePerHttpRequest();

            var infrastructureDataAccessLayerAssembly = Assembly.Load("DataAccess");
            builder.RegisterAssemblyTypes(infrastructureDataAccessLayerAssembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces().InstancePerHttpRequest();

            var domainLayerAssembly = Assembly.Load("Domain");
            builder.RegisterAssemblyTypes(domainLayerAssembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();

            var applicationLayerAssembly = Assembly.Load("Application");
            builder.RegisterAssemblyTypes(applicationLayerAssembly)
                   .AsClosedTypesOf(typeof (IValidationHandler<>)).InstancePerHttpRequest();
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
