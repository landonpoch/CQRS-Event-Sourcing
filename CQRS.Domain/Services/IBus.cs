﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain.Messages;
using CQRS.Domain.Messages.Commands;

namespace CQRS.Domain.Services
{
    public interface IBus : IDisposable
    {
        void Publish(Message message);
        void SubscribeHandler<T, TResult>(Func<T, TResult> function)
            where T : Message
            where TResult : CommandResult;
    }
}
