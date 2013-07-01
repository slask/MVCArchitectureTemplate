using System;

namespace Domain.Core
{
    public class DomainObject
    {
        public DomainObject()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        public Guid Id { get; /*private*/ set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
