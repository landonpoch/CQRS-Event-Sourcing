using System;
using System.Collections.Generic;
using CQRS.Domain.Services;
using CQRS.Infrastructure.EventStore;
using CQRS.Messages.Events;

namespace CQRS.Domain
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
            _store.SaveChanges(order.Id, order.GetType(), version, order.GetUncommitedChanges());
        }

        public Order GetById(Guid id)
        {
            IEnumerable<Event> events = _store.GetEvents(id);
            return new Order(events);
        }
    }
}
