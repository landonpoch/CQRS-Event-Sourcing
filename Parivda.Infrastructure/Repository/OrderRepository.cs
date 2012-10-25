using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parivda.EventStore;
using Pariveda.Domain.Services;

namespace Pariveda.Domain
{
    public class OrderRepository : IOrderRepository
    {
        IEventStore _store;

        public OrderRepository(IEventStore store)
        {
            _store = store;
        }

        public void Save(Order order, int version)
        {
            _store.SaveChanges(order.Id, version, order.GetUncommitedChanges());
        }

        public Order GetById(Guid id)
        {
            IEnumerable<Event> events = _store.GetEvents(id);
            return new Order(events);
        }
    }
}
