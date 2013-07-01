using System.Collections.Generic;

namespace Domain.Core.Validation
{
    public interface IValidationHandler<in T> where T : DomainObject
    {
        IEnumerable<ValidationResult>  Validate(T command);
    }
}
