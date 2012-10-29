using MassTransit;
using Ninject.Modules;
using Parivda.EventStore;
using CQRS.Application.Services;
using CQRS.Presentation.Common;
using CQRS.WriteServiceConsole.Services;

namespace CQRS.WriteServiceConsole
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicationSettings>().To<ApplicationSettings>();
            Bind<ICommandHandlers>().To<CommandHandlers>();
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
