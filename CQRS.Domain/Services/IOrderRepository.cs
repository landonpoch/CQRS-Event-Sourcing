using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.Domain.Services
{
    public interface IOrderRepository
    {
        void Save(Order order, int version);
        Order GetById(Guid id);
    }
}
