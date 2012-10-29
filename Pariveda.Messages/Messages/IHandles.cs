using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pariveda.Domain.Messages
{
    public interface IHandles<T> where T : Message
    {
        void Handle(T message);
    }
}
