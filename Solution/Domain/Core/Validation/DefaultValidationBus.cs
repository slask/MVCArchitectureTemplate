using System.Collections.Generic;

namespace Domain.Core.Validation
{
    public class DefaultValidationBus : IValidationBus
    {
        private readonly IDependecyContainer _container;

        public DefaultValidationBus(IDependecyContainer container)
        {
            _container = container;
        }

        public IEnumerable<Notification> Validate<T>(T subject)
        {
            var handlers = _container.GetServices<IValidationHandler<T>>();

            var allNotifications = new List<Notification>();
            foreach (var validationHandler in handlers)
            {
                if (validationHandler == null)
                {
                    throw new ValidationHandlerNotFoundException(typeof (T));
                }
                allNotifications.AddRange(validationHandler.Validate(subject));
            }
            return allNotifications;
        }
    }
}

