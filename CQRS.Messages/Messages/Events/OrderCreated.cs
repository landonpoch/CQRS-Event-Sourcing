using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;

namespace CQRS.Domain
{
    public class OrderCreated : Event
    {
        public Guid OrderId { get; private set; }
        public string OrderName { get; private set; }

        public OrderCreated(Guid orderId, string orderName)
        {
            OrderId = orderId;
            OrderName = orderName;
        }
    }
}
