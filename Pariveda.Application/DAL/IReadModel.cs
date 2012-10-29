using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Application.DTOs;

namespace Pariveda.Application.DAL
{
    public interface IReadModel
    {
        List<OrderIndexDto> GetAllOrders();
        OrderDetailsDto GetOrderDetails(Guid orderId);
    }
}
