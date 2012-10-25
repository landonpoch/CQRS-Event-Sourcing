using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.Commands
{
    public class AddOrderItem : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }

        public AddOrderItem(Guid productId, string productName, Guid orderId)
        {
            ProductId = productId;
            ProductName = productName;
            OrderId = orderId;
        }
    }
}
