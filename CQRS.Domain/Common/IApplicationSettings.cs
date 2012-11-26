using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.Domain.Common
{
    public interface IApplicationSettings
    {
        string EventStoreConnectionString { get; }
    }
}
