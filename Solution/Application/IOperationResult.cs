using System.Collections.Generic;
using Domain.Core.Validation;

namespace Application
{
    public interface IOperationResult
    {
        bool Success { get; }
        IEnumerable<Notification> ValidationFaults { get; }
    }
}
