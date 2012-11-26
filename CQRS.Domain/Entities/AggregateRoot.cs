using System;
using System.Collections.Generic;
using CQRS.Messages.Events;

namespace CQRS.Domain
{
    public abstract class AggregateRoot
    {
        public abstract Guid Id { get; }

        private readonly List<Event> _changes = new List<Event>();

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        protected void ApplyChange(Event @event, bool isNew)
        {
            Apply(@event);
            if (isNew) _changes.Add(@event);
        }

        public List<Event> GetUncommitedChanges()
        {
            return _changes;
        }

        protected abstract void Apply(Event @event);
    }
}
