using System;
using System.ServiceModel;
using CQRS.Presentation.ReadService;
using CQRS.Presentation.WcfExtensions;

namespace CQRS.Presentation.WcfExtensions
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

namespace CQRS.Presentation.ReadService
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