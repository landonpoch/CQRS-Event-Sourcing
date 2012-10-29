using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pariveda.Application.DTOs
{
    public class OrderIndexDto
    {
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
    }
}