using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using MassTransit;
using Pariveda.Infrastructure.ReadModel;
using Pariveda.Domain;
using Pariveda.Domain.Events;

namespace Pariveda.ReadModel
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var events = new EventHandlers();
            ServiceBusFactory.New(sbc =>
            {
                sbc.ReceiveFrom("loopback://localhost/test_queue");
                sbc.Subscribe(subs =>
                {
                    subs.Handler<OrderCreated>(events.OrderCreatedHandler);
                    subs.Handler<OrderItemAdded>(events.OrderItemAddedHandler);
                });
            });
        }

        protected override void OnStop()
        {
        }
    }
}
