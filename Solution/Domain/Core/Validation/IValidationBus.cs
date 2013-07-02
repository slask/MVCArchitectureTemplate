using System.Collections.Generic;

namespace Domain.Core.Validation
{
    public interface IValidationBus
    {
        IEnumerable<Notification> Validate<T>(T subject);
    }
}

