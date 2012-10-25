using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Pariveda.Infrastructure.Contract;
using Pariveda.Domain;
using Pariveda.Domain.Events;
using Pariveda.Infrastructure.ReadModel;

namespace Parivda.EventStore.ServiceBus
{
    public class ServiceBus : IBus
    {
        private IServiceBus _bus;

        public ServiceBus(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Publish(Event message)
        {
            Type type = message.GetType();
            _bus.Publish(message, type);
        }
    }
}
