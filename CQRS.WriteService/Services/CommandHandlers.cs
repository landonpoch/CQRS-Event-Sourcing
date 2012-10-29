using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain;
using CQRS.Domain.Commands;
using CQRS.Domain.Messages.Commands;
using CQRS.Domain.Services;
using CQRS.Domain;

namespace CQRS.WriteServiceConsole.Services
{
    public class CommandHandlers : ICommandHandlers
    {
        private IOrderRepository _repo;

        public CommandHandlers(IOrderRepository repo)
        {
            _repo = repo;
        }

        #region ICommandHandlers Members

        public CreateNewOrderResult Handle(CreateNewOrder command)
        {
            var successMessage = "Order Successfully Created";
            var result = new CreateNewOrderResult();
            try
            {
                var order = new Order(command.OrderId, command.OrderName);
                _repo.Save(order, command.Version);
                result.WasSuccessful = true;
                result.Message = successMessage;
                Console.WriteLine(successMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Message = "An error ocured while creating your order";
            }
            return result;
        }

        public AddOrderItemResult Handle(AddOrderItem command)
        {
            var successMessage = "Order Item Successfully Added";
            var result = new AddOrderItemResult();
            try
            {
                var order = _repo.GetById(command.OrderId);
                order.AddOrderItem(command.ProductId, command.ProductName, command.OrderId);
                _repo.Save(order, command.Version);
                result.WasSuccessful = true;
                result.Message = successMessage;
                Console.WriteLine(successMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Message = "An error occured while adding the item to your order";
            }
            return result;
        }

        #endregion
    }
}
