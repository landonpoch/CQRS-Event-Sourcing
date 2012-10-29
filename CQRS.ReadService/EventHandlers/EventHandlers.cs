using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CQRS.DataAccess;
using CQRS.Domain.Events;
using CQRS.Domain;

namespace CQRS.ReadModel.EventHandlers
{
    public class EventHandlers : IEventHandlers
    {
        public void OrderCreatedHandler(OrderCreated @event)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var orderIndex = new OrderIndex
                {
                    OrderId = @event.OrderId,
                    OrderName = @event.OrderName,
                };
                ctx.OrderIndexes.AddObject(orderIndex);

                var orderDetail = new OrderDetail
                {
                    OrderId = @event.OrderId,
                    Name = @event.OrderName,
                    Version = @event.Version
                    
                };
                ctx.OrderDetails.AddObject(orderDetail);

                ctx.SaveChanges();
                Console.WriteLine("Order Created.");
            }
        }

        public void OrderItemAddedHandler(OrderItemAdded @event)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var orderDetails = ctx.OrderDetails
                    .Where(od => od.OrderId == @event.OrderId)
                    .FirstOrDefault();
                orderDetails.Version = @event.Version;

                var orderItemDetail = new OrderItemDetail
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = @event.OrderId,
                    ProductId = @event.ProductId,
                    ProductName = @event.ProductName,
                };
                ctx.OrderItemDetails.AddObject(orderItemDetail);

                ctx.SaveChanges();
                Console.WriteLine("Order Line Added");
            }
        }
    }
}
