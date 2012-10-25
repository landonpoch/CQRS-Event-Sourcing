using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parivda.EventStore
{
    public interface IEventSerializer
    {
        byte[] Serialize(Event @event);
        Event Deserialize(byte[] @event);
    }
}
