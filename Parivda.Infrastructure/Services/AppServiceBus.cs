using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Pariveda.Domain;
using Pariveda.Domain.Events;
using Parivda.EventStore;
using Pariveda.Application.Services;
using Pariveda.Domain.Messages;
using Pariveda.Domain.Messages.Commands;

namespace Pariveda.Application.Services
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
