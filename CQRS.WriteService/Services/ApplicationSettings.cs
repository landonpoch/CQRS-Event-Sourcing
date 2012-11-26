using System.Configuration;
using CQRS.Domain.Common;

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