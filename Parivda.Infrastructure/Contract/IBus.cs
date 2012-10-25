using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;

namespace Pariveda.Infrastructure.Contract
{
    public interface IBus
    {
        void Publish(Event message);
    }
}
