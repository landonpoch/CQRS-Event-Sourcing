using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain;
using CQRS.Domain.Events;

namespace CQRS.ReadModel.EventHandlers
{
    public interface IEventHandlers
    {
        void OrderCreatedHandler(OrderCreated @event);
        void OrderItemAddedHandler(OrderItemAdded @event);
    }
}
