using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.DTOs
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
    }
}
