using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.Entities
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }

        public OrderItem(Guid productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
}
