using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain.Messages;

namespace Parivda.EventStore
{
    public abstract class Event : Message
    {
        public int Version { get; set; }
    }
}
