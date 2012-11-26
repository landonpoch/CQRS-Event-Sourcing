using System;
using System.Collections.Generic;
using CQRS.Messages.Events;

namespace CQRS.Infrastructure.EventStore
{
    public interface IEventStore
    {
        void SaveChanges(Guid aggregateId, Type aggregateType, int originatingVersion, IEnumerable<Event> events);
        IEnumerable<Event> GetEvents(Guid aggregateId);
    }
}
