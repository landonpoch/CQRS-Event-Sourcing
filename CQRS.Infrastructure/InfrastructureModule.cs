using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using CQRS.Domain.Services;
using CQRS.Domain;
using MassTransit;
using CQRS.Domain.Events;

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
