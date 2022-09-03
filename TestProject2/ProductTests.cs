using ECSCore;
using ECSCore.Interfaces.ECS;
using GameLib.Entitys.DynamicEntitys;
using GameLib.WorkFlow.NewProduct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace GameLib.Tests
{
    [TestClass()]
    public class ProductTests
    {
        private static IECS IECS;
        private static IECSDebug IECSDebug;

        public void Init()
        {
            ECS.Initialization(GetAssembly.Get());
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;

            Console.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void TestInitialize()
        {
            Init();
            var initializer = new Initializer();
            initializer.Init();

            Assert.AreEqual(2, ProductVolumes.Volumes.Count);
            Assert.AreEqual(2, ProductWeights.Weights.Count);
            Assert.AreEqual(2, ProductProductionСycles.Сycles.Count);

            var oreInstance = new Ore();
            Assert.AreEqual(ProductType.Ore, oreInstance.ProductType);
            Assert.AreEqual(2, oreInstance.Volume);
            Assert.AreEqual(1, oreInstance.Weight);
            Assert.AreEqual(10, oreInstance.Cycle);
        }

        [TestMethod()]
        public void TestProductOnEntity()
        {
            Init();
            var initializer = new Initializer();
            initializer.Init();

            var ship = IECS.AddEntity(new Ship());
            ship.Add(new Products());
            ship.Get<Products>(out var product);
            product.Collection.TryGetValue((int)ProductType.Ore, out var ore);
            Assert.AreEqual(0, ore.Count); //Будет null
        }
    }
}
