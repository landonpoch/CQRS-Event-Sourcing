using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain;
using Pariveda.Domain.Events;

namespace Pariveda.Infrastructure.ReadModel
{
    public class EventHandlers
    {
        public void OrderCreatedHandler(OrderCreated @event)
        {
            Console.WriteLine(@event.OrderName);
        }

        public void OrderItemAddedHandler(OrderItemAdded @event)
        {
            Console.WriteLine(@event.ProductName);
        }
    }
}
