using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parivda.EventStore;
using System.Configuration;

namespace CQRS.Presentation.Common
{
    public class ApplicationSettings : IApplicationSettings
    {
        #region IApplicationSettings Members

        public string EventStoreConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["TestEventStore"].ConnectionString; }
        }

        #endregion
    }
}