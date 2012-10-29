using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRS.Application.DTOs
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
        public List<OrderItemDto> OrderLines { get; set; }
        public int Version { get; set; }
    }
}