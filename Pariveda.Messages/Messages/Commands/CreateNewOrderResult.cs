using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.Messages.Commands
{
    public class CreateNewOrderResult : CommandResult
    {
        public bool WasSuccessful { get; set; }
        public string Message { get; set; }
    }
}
