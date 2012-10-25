using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.DTOs
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
    }
}
