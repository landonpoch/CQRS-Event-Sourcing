using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parivda.EventStore
{
    public interface IEventStore
    {
        void SaveChanges(Guid aggregateId, Type aggregateType, int originatingVersion, IEnumerable<Event> events);
        IEnumerable<Event> GetEvents(Guid aggregateId);
    }
}
