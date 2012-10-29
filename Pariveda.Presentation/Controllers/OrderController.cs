using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pariveda.Domain;
using Pariveda.Domain.Commands;
using Pariveda.Presentation.WcfExtensions;
using MassTransit;
using Pariveda.Domain.Messages.Commands;

namespace Pariveda.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private const int ServiceTimeout = 30; // Could be configurable

        private IServiceBus _bus;
        private IReadServiceFactory _factory;

        public OrderController(IReadServiceFactory factory, IServiceBus bus)
        {
            _bus = bus;
            _factory = factory;
        }

        public ActionResult Index()
        {
            using (var client = _factory.CreateReadService())
            {
                var orders = client.GetAllOrders().ToList();
                return View(orders);
            }
        }

        public ActionResult Details(Guid id)
        {
            using (var client = _factory.CreateReadService())
            {
                var orderDetails = client.GetOrderDetails(id);
                return View(orderDetails);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string orderName)
        {
            // Presentation Logic
            Guid orderId = Guid.NewGuid();
            var createOrderCommand = new CreateNewOrder(orderId, orderName);

            CreateNewOrderResult result;
            Send(createOrderCommand, out result);

            ViewBag.Result = result.Message;
            return View();
        }

        public ActionResult AddOrderLine(Guid id, int version)
        {
            var model = new CreateOrderLineModel
            {
                Id = id,
                Version = version
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrderLine(CreateOrderLineModel model)
        {
            // Presentation Logic
            var command = new AddOrderItem(Guid.NewGuid(), model.ProductName, model.Id, model.Version);

            AddOrderItemResult result;
            Send(command, out result);

            ViewBag.Result = result.Message;
            return View();
        }
    
        private void Send<T, TResult>(T command, out TResult result)
            where T : Command
            where TResult : CommandResult, new()
        {
            var tempResult = new TResult();
            _bus.PublishRequest<T>(command, ctx =>
            {
                ctx.Handle<TResult>(msg => tempResult = msg);
                ctx.SetTimeout(new TimeSpan(0, 0, ServiceTimeout));
            });
            result = tempResult;
        }
    
    }

    public class CreateOrderLineModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Version { get; set; }
    }
}
