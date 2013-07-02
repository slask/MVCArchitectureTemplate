using System.Collections.Generic;
using System.Linq;
using Domain.Core.Validation;

namespace Application
{
    public class OperationResult : IOperationResult
    {
        private bool _success;
        private IEnumerable<Notification> _validationFaults;

        public OperationResult(bool success)
            : this(success, null)
        {
        }

        public OperationResult(bool success, IEnumerable<Notification> validationResults)
        {
            _success = success;
            ValidationFaults = validationResults;
        }

        public bool Success
        {
            get
            {
                if (ValidationFaults != null && ValidationFaults.Any())
                    return false;

                return _success;
            }
            protected set { _success = value; }
        }

        public IEnumerable<Notification> ValidationFaults
        {
            get
            {
                if (_validationFaults == null)
                {
                    //avoid possible stupid exceptions in callers and simpify caller logic
                    return new List<Notification>();
                }
                return _validationFaults;
            }
            private set { _validationFaults = value; }
        }
    }
}