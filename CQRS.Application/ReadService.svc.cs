using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CQRS.Application.DTOs;
using CQRS.Application.DAL;

namespace CQRS.Application
{
    public class ReadService : IReadService
    {
        private IReadModel _readModel;

        public ReadService(IReadModel readModel)
        {
            _readModel = readModel;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region IReadService Members

        public List<OrderIndexDto> GetAllOrders()
        {
            return _readModel.GetAllOrders();
        }

        public OrderDetailsDto GetOrderDetails(Guid id)
        {
            return _readModel.GetOrderDetails(id);
        }

        #endregion
    }
}
