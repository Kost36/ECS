using ECSCore;
using ECSCore.Interfaces.ECS;
using GameLib;
using GameLib.Entitys.StaticEntitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using GameLib.WorkFlow;
using GameLib.WorkFlow.NewProduct;

namespace GameLibTests
{
    [TestClass()]
    public class ProductionTests
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
        public void TestMemory()
        {
            Init();

            //var countObjects = 1000000; 
            //for (int i=0; i < countObjects; i++)
            //{
            //    var entity = IECS.AddEntity(new Stantion());
            //    entity.Add(new Enargy());
            //} //321 Mb

            var countObjects = 1000000;
            for (int i = 0; i < countObjects; i++)
            {
                var entity = IECS.AddEntity(new Stantion());
                entity.Add(new Iron());
            } //321 Mb

            var end = false;
            while (!end)
            {
                Thread.Sleep(1000);

                Console.WriteLine("breakpoint");
            }
        }

        [TestMethod()]
        public void Test()
        {
            Init();
            var entity = IECS.AddEntity(new Stantion());
            entity.Add(new Enargy() { Count = 10000 });
            entity.Add(new Ore() { Count = 10000 });
            entity.Add(new Iron() { Count = 0 }); //TODO Должно автоматом накинуться

            var entity1 = entity.AddChild(new GameLib.Entitys.StaticEntitys.ProductionModule());
            entity1.Add(new Production<Iron>());

            var end = false;
            while (!end)
            {
                Thread.Sleep(1000);

                Console.WriteLine("breakpoint");
            }
        }
    }
}
