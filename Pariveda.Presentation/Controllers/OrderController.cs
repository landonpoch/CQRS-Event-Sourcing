using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pariveda.Domain.Services;
using Pariveda.Domain;
using Pariveda.Domain.Commands;

namespace Pariveda.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private IReadModel _readModel;

        public OrderController(IOrderRepository repo, IReadModel readModel)
        {
            _repo = repo;
            _readModel = readModel;
        }

        public ActionResult Index()
        {
            var orders = _readModel.GetAllOrders();
            return View(orders);
        }

        public ActionResult Details(Guid id)
        {
            var orderDetails = _readModel.GetOrderDetails(id);
            return View(orderDetails);
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
            
            // Application Logic
            var order = new Order(createOrderCommand.OrderId, createOrderCommand.OrderName);
            _repo.Save(order, 0);
            
            // Presentation Logic
            ViewBag.Result = "Order Successfully Saved";
            return View();
        }

        public ActionResult AddOrderLine(Guid id)
        {
            var model = new CreateOrderLineModel
            {
                Id = id,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrderLine(CreateOrderLineModel model)
        {
            // Presentation Logic
            var command = new AddOrderItem(Guid.NewGuid(), model.ProductName, model.Id);
            
            // Application Logic
            var order = _repo.GetById(model.Id);
            order.AddOrderItem(command.ProductId, command.ProductName, command.OrderId);
            _repo.Save(order, 1); // TODO: Figure out versioning

            // Presentation Logic
            ViewBag.Result = "Order Line Successfully Added";
            return View();
        }
    }

    public class CreateOrderLineModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
    }
}
