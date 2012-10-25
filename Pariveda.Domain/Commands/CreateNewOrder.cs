using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain.Commands;

namespace Pariveda.Domain
{
    public class CreateNewOrder : Command
    {
        public Guid OrderId { get; private set; }
        public string OrderName { get; private set; }

        public CreateNewOrder(Guid orderId, string orderName)
        {
            OrderId = orderId;
            OrderName = orderName;
        }
    }
}
