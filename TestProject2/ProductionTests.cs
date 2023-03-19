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
            entity.Add(new Enargy() { Count = 1000 });
            entity.Add(new Ore() { Count = 1000 });
            entity.Add(new Iron() { Count = 0 }); //TODO Должно автоматом накинуться

            var entity1 = entity.AddChild(new GameLib.Entitys.StaticEntitys.ProductionModule());
            entity1.Add(new Production<Iron>());

            Thread.Sleep(1000);

            entity.Get<Enargy>(out var enargy);
            entity.Get<Ore>(out var ore);
            entity.Get<Iron>(out var iron);
            entity1.Get<WarehouseProductionModul>(out var warehouse);
            entity1.Get<GameLib.WorkFlow.ProductionModule>(out var productionModule);

            var end = false;
            var secCount = 0;
            while (!end)
            {
                Thread.Sleep(1000);

                Debug.WriteLine($"{Environment.NewLine}" +
                    $"Stantion: {Environment.NewLine}" +
                    $"EntityId [{entity.Id}] " +
                    $"Enargy [{enargy.Count}] " +
                    $"Ore [{ore.Count}] " +
                    $"Irone [{iron.Count}] ");
                Debug.WriteLine($"ProductionModule: {Environment.NewLine}" +
                    $"Module enable [{productionModule.Enable}] " +
                    $"Module work [{productionModule.Work}] " +
                    $"Module cycle [{productionModule.CycleCompletionPercentage}]% " +
                    $"EntityId [{entity1.Id}] " +
                    $"Enargy [{warehouse.Raws[ProductType.Enargy].Value}] " +
                    $"Ore [{warehouse.Raws[ProductType.Ore].Value}] " +
                    $"Irone [{warehouse.Product.Value.Value}] ");

                secCount++;
                if (secCount > 185) //Прошло 3 минуты 5 сек
                {
                    Assert.IsTrue(iron.Count > 0, "Not add irone to stantion");
                }

                if (iron.Count > 100)
                {
                    //return;
                } //Ожидать результат по таймауту и бросать TestFail, если не дождались
            }
        }
    }
}
