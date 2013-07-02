using System.Collections.Generic;

namespace Domain.Core.Validation
{
    public interface IValidationHandler<in T>
    {
        IEnumerable<Notification>  Validate(T subject);
    }
}
