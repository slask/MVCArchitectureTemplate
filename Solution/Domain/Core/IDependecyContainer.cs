using System.Collections.Generic;

namespace Domain.Core
{
    public interface IDependecyContainer
    {
        T GetService<T>();
        IEnumerable<T> GetServices<T>();
    }
}
