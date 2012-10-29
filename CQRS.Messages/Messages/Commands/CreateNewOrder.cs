using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain.Commands;

namespace CQRS.Domain
{
    public class CreateNewOrder : Command
    {
        public Guid OrderId { get; private set; }
        public string OrderName { get; private set; }

        public CreateNewOrder(Guid orderId, string orderName)
            : base(-1)
        {
            OrderId = orderId;
            OrderName = orderName;
        }
    }
}
