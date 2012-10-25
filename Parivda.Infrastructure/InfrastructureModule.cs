using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Pariveda.Domain.Services;
using Pariveda.Domain;
using MassTransit;
using Pariveda.Infrastructure.Contract;
using Pariveda.Infrastructure.ReadModel;
using Pariveda.Domain.Events;

namespace Parivda.EventStore
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBus>().To<ServiceBus.ServiceBus>();
            Bind<IEventSerializer>().To<EventSerializer>();
            Bind<IEventStore>().To<EventStore>();
            Bind<IOrderRepository>().To<OrderRepository>();
            Bind<IEventHandlers>().To<EventHandlers>();
            Bind<IReadModel>().To<ReadModel>();

            var events = Kernel.Get<IEventHandlers>();
            Bind<IServiceBus>().ToMethod(context =>
            {
                return ServiceBusFactory.New(sbc =>
                {
                    sbc.ReceiveFrom("loopback://localhost/test_queue");
                    sbc.SetConcurrentConsumerLimit(1); // Events should be handled one at a time
                    sbc.Subscribe(subs =>
                    {
                        subs.Handler<OrderCreated>(events.OrderCreatedHandler);
                        subs.Handler<OrderItemAdded>(events.OrderItemAddedHandler);
                    });
                });
            })
            .InSingletonScope();
        }
    }
}
