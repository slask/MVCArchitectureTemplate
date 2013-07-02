using System;

namespace Domain.Core
{
    //TODO: modify this layer super type
    public class DomainObject
    {
        public DomainObject()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }
        //TODO: make private
        public Guid Id { get; /*private*/ set; }

        //TO do: remove these
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
