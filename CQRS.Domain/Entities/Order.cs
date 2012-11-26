using System;
using System.Collections.Generic;
using CQRS.Domain.Entities;
using CQRS.Domain.Events;
using CQRS.Messages.Events;

namespace CQRS.Domain
{
    public class Order : AggregateRoot
    {
        private Guid _orderId;
        private string _orderName;
        private List<OrderItem> orderItems = new List<OrderItem>();

        public override Guid Id
        {
            get { return _orderId; }
        }

        #region Constructors

        public Order(Guid orderId, string orderName)
        {
            // Behavior
            ApplyChange(new OrderCreated(orderId, orderName), Apply);
        }

        public Order(IEnumerable<Event> history)
        {
            foreach (Event @event in history)
            {
                // See if there is a more elegant way to call these methods
                if (@event is OrderCreated) ApplyChange(@event as OrderCreated, false, Apply);
                if (@event is OrderItemAdded) ApplyChange(@event as OrderItemAdded, false, Apply);
            }
        }

        #endregion
        
        public void AddOrderItem(Guid productId, string productName, Guid orderId)
        {
            // Behavior
            ApplyChange(new OrderItemAdded(productId, productName, orderId), Apply);
        }

        #region Private Methods

        private void Apply(OrderCreated @event)
        {
            // State
            _orderId = @event.OrderId;
            _orderName = @event.OrderName;
        }

        private void Apply(OrderItemAdded @event)
        {
            // State
            orderItems.Add(new OrderItem(@event.ProductId, @event.ProductName));
        }

        #endregion
    }
}
