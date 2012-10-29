using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Pariveda.Domain.Services;
using Pariveda.Domain;
using MassTransit;
using Pariveda.Domain.Events;

namespace Parivda.EventStore
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventSerializer>().To<EventSerializer>();
            Bind<IEventStore>().To<EventStore>();
            Bind<IOrderRepository>().To<OrderRepository>();
        }
    }
}
