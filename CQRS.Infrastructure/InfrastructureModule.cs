using CQRS.Domain;
using CQRS.Domain.Services;
using CQRS.Infrastructure.Contract;
using CQRS.Infrastructure.Services;
using Ninject.Modules;

namespace CQRS.Infrastructure.EventStore
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
