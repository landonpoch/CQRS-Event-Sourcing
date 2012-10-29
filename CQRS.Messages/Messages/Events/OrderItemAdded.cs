using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;

namespace CQRS.Domain.Events
{
    public class OrderItemAdded : Event
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }

        public OrderItemAdded(Guid productId, string productName, Guid orderId)
        {
            ProductId = productId;
            ProductName = productName;
            OrderId = orderId;
        }
    }
}
