using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Core;

namespace Utilities
{
    public class MvcDependencyResolverContainer : IDependecyContainer
    {
        public T GetService<T>()
        {
            return  DependencyResolver.Current.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return DependencyResolver.Current.GetServices<T>();
        }
    }
}