using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Components.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components.Products.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void WeightTest01_GetStaticFields()
        {
            Battery battery = new Battery();
            Assert.IsTrue(battery.Weight == 1);
            Assert.IsTrue(battery.Volume == 2);
            Ore ore = new Ore();
            Assert.IsTrue(ore.Weight == 3);
            Assert.IsTrue(ore.Volume == 4);
        }
    }
}