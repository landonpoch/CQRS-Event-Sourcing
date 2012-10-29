using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain;
using Pariveda.Domain.Events;

namespace Pariveda.ReadModel.EventHandlers
{
    public interface IEventHandlers
    {
        void OrderCreatedHandler(OrderCreated @event);
        void OrderItemAddedHandler(OrderItemAdded @event);
    }
}
