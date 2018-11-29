using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            SetCreatedAt();
        }

        protected Entity(Guid id)
        {
            Id = id;
            SetCreatedAt();
        }

        protected void SetCreatedAt()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
