using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain;
using Pariveda.Domain.Commands;
using Pariveda.Domain.Messages.Commands;

namespace Pariveda.WriteServiceConsole.Services
{
    public interface ICommandHandlers
    {
        CreateNewOrderResult Handle(CreateNewOrder command);
        AddOrderItemResult Handle(AddOrderItem command);
    }
}
