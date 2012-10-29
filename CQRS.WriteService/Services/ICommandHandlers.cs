using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRS.Domain;
using CQRS.Domain.Commands;
using CQRS.Domain.Messages.Commands;
using CQRS.Domain;

namespace CQRS.WriteServiceConsole.Services
{
    public interface ICommandHandlers
    {
        CreateNewOrderResult Handle(CreateNewOrder command);
        AddOrderItemResult Handle(AddOrderItem command);
    }
}
