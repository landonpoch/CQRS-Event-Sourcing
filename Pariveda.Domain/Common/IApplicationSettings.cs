using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parivda.EventStore
{
    public interface IApplicationSettings
    {
        string EventStoreConnectionString { get; }
    }
}
