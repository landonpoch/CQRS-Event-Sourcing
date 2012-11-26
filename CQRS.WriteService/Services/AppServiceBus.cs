using System;
using CQRS.Domain.Messages;
using CQRS.Domain.Messages.Commands;
using MassTransit;

namespace CQRS.Application.Services
{
    public class AppServiceBus : IBus
    {
        private IServiceBus _bus;

        public AppServiceBus(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Publish(Message message)
        {
            Type type = message.GetType();
            _bus.Publish(message, type);
        }

        public void SubscribeHandler<T, TResult>(Func<T, TResult> function)
            where T : Message
            where TResult : CommandResult
        {
            _bus.SubscribeHandler<T>(msg =>
            {
                _bus.MessageContext<T>().Respond<TResult>(function.Invoke(msg));
            });
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}
