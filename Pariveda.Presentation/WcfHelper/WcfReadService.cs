using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pariveda.Presentation.ReadService;
using Pariveda.Presentation.WcfExtensions;
using System.ServiceModel;

namespace Pariveda.Presentation.WcfExtensions
{
    public interface IReadServiceProxy : IReadService, IDisposable
    {
    }

    public interface IReadServiceFactory
    {
        IReadServiceProxy CreateReadService();
    }

    public class ReadServiceFactory : IReadServiceFactory
    {
        #region IReadServiceFactory Members

        public IReadServiceProxy CreateReadService()
        {
            return new ReadServiceClient();
        }

        #endregion
    }
}

namespace Pariveda.Presentation.ReadService
{
    public partial class ReadServiceClient : IReadServiceProxy
    {
        void IDisposable.Dispose()
        {
            bool success = false;
            try
            {
                if (State != CommunicationState.Faulted)
                {
                    Close();
                    success = true;
                }
            }
            finally
            {
                if (!success)
                {
                    Abort();
                }
            }
        }
    }
}