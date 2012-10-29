using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pariveda.Application.DTOs;
using Pariveda.DataAccess;

namespace Pariveda.Application.DAL
{
    public class ReadModel : IReadModel
    {
        #region IReadModel Members

        public List<OrderIndexDto> GetAllOrders()
        {
            List<OrderIndexDto> orders = new List<OrderIndexDto>();
            using (var ctx = new TestEventSchemaEntities())
            {
                ctx.OrderIndexes.ToList().ForEach(oi =>
                {
                    orders.Add(Convert(oi));
                });
            }
            return orders;
        }

        public OrderDetailsDto GetOrderDetails(Guid id)
        {
            using (var ctx = new TestEventSchemaEntities())
            {
                var orderDetails = ctx.OrderDetails.Where(o => o.OrderId == id)
                    .Select(o => new OrderDetailsDto
                    {
                        OrderId = o.OrderId,
                        OrderName = o.Name,
                        Version = o.Version
                    })
                    .FirstOrDefault();
                orderDetails.OrderLines = new List<OrderItemDto>();
                ctx.OrderItemDetails.Where(ol => ol.OrderId == id)
                    .ToList()
                    .ForEach(ol => orderDetails.OrderLines.Add(Convert(ol)));

                return orderDetails;
            }
        }

        #endregion

        #region Mapping Logic

        private OrderIndexDto Convert(OrderIndex order)
        {
            return new OrderIndexDto
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName
            };
        }

        private OrderItemDto Convert(OrderItemDetail orderItem)
        {
            return new OrderItemDto
            {
                ProductName = orderItem.ProductName
            };
        }

        #endregion
    }
}