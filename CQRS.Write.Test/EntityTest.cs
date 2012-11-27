using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CQRS.Domain;

namespace CQRS.Write.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class EntityTest
    {
        [TestMethod]
        public void DynamicApplyMethodsCalled()
        {
            var order = new Order(Guid.NewGuid(), "Some name");
            order.AddOrderItem(Guid.NewGuid(), "Some Product", order.Id);
        }
    }
}
