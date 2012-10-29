using MassTransit;
using Ninject.Modules;
using CQRS.Domain;
using CQRS.Domain.Events;
using CQRS.ReadModel.EventHandlers;

namespace CQRS.ReadModel
{
    public class ReadModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventHandlers>().To<EventHandlers.EventHandlers>();
            
            // TODO: Fix this concrete reference when MassTransit gets updated
            // Resoving events causes subscriptions to break
            // Ninject support should be available soon.
            // var events = Kernel.Get<IEventHandlers>();
            var events = new EventHandlers.EventHandlers();

            Bind<IServiceBus>().ToMethod(context =>
            {
                return ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://localhost/read");
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
