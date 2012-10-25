using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain;
using Pariveda.Domain.Events;
using Pariveda.Domain.Services;

namespace Pariveda.Infrastructure.ReadModel
{
    public class EventHandlers : IEventHandlers
    {

        public void OrderCreatedHandler(OrderCreated @event)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var order = new Order
                {
                    OrderId = @event.OrderId,
                    OrderName = @event.OrderName
                };
                ctx.Orders.AddObject(order);
                ctx.SaveChanges();
                Console.WriteLine("Order Created.");
            }
        }

        public void OrderItemAddedHandler(OrderItemAdded @event)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var orderLine = new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    OrderId = @event.OrderId,
                    ProductId = @event.ProductId,
                    ProductName = @event.ProductName
                };
                ctx.OrderLines.AddObject(orderLine);
                ctx.SaveChanges();
                Console.WriteLine("Order Line Added");
            }
        }
    }
}
