using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain.Services;
using Pariveda.Domain.DTOs;

namespace Pariveda.Infrastructure.ReadModel
{
    public class ReadModel : IReadModel
    {
        #region IReadModel Members

        public List<OrderItemDto> GetAllOrders()
        {
            List<OrderItemDto> orders = new List<OrderItemDto>();
            using (var ctx = new TestEventSchemaEntities())
            {
                ctx.Orders.ToList().ForEach(o =>
                {
                    orders.Add(Convert(o));
                });
            }
            return orders;
        }

        public OrderDetailsDto GetOrderDetails(Guid id)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var orderDetails = ctx.Orders.Where(o => o.OrderId == id)
                    .Select(o => new OrderDetailsDto
                    {
                        OrderId = o.OrderId,
                        OrderName = o.OrderName,
                    })
                    .FirstOrDefault();
                orderDetails.OrderLines = new List<OrderLineDto>();
                ctx.OrderLines.Where(ol => ol.OrderId == id)
                    .ToList()
                    .ForEach(ol => orderDetails.OrderLines.Add(Convert(ol)));

                return orderDetails;
            }
        }

        #endregion

        #region Mapping Logic
        
        private OrderItemDto Convert(Order order)
        {
            return new OrderItemDto
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName
            };
        }

        private OrderLineDto Convert(OrderLine orderLine)
        {
            return new OrderLineDto
            {
                ProductName = orderLine.ProductName
            };
        }

        #endregion
    }
}
