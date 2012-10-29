using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;
using Pariveda.Domain.Messages;
using Pariveda.Domain.Messages.Commands;

namespace Pariveda.Application.Services
{
    public interface IBus
    {
        void Publish(Message message);
        void SubscribeHandler<T, TResult>(Func<T, TResult> function)
            where T : Message
            where TResult : CommandResult;
        void Dispose();
    }
}
