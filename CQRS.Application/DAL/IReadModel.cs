using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Application.DTOs;

namespace CQRS.Application.DAL
{
    public interface IReadModel
    {
        List<OrderIndexDto> GetAllOrders();
        OrderDetailsDto GetOrderDetails(Guid orderId);
    }
}
