using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain.DTOs;

namespace Pariveda.Domain.Services
{
    public interface IReadModel
    {
        List<OrderItemDto> GetAllOrders();
        OrderDetailsDto GetOrderDetails(Guid id);
    }
}
