using CQRS.Domain;
using CQRS.Domain.Services;
using CQRS.Infrastructure.Contract;
using CQRS.Infrastructure.Services;
using Ninject.Modules;
using MassTransit;

namespace CQRS.Infrastructure.EventStore
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventSerializer>().To<EventSerializer>();
            Bind<IEventStore>().To<EventStore>();
            Bind<IOrderRepository>().To<OrderRepository>();

            Bind<IBus>().To<AppServiceBus>();

            Bind<IServiceBus>().ToMethod(context =>
            {
                return ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://localhost/write");
                    sbc.SetConcurrentConsumerLimit(1); // Events should be handled one at a time
                });
            })
            .InSingletonScope();
        }
    }
}
