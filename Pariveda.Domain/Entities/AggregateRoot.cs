using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;

namespace Pariveda.Domain
{
    public abstract class AggregateRoot
    {
        public abstract Guid Id { get; }

        private readonly List<Event> _changes = new List<Event>();

        protected void ApplyChange<T>(T @event, Action<T> apply)
            where T : Event
        {
            ApplyChange(@event, true, apply);
        }

        protected void ApplyChange<T>(T @event, bool isNew, Action<T> apply)
            where T : Event
        {
            apply.Invoke(@event);
            if (isNew) _changes.Add(@event);
        }

        public List<Event> GetUncommitedChanges()
        {
            return _changes;
        }
    }
}
