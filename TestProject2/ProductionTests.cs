using ECSCore;
using ECSCore.Interfaces.ECS;
using GameLib;
using GameLib.Components;
using GameLib.Datas;
using GameLib.Entitys.StaticEntitys;
using GameLib.Mechanics.Production.Components;
using GameLib.Products;
using GameLib.Products.Lvl1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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
        public void Test()
        {
            Init();
            var entity = IECS.AddEntity(new Stantion());
            entity.Add(new Warehouse()
            {
                VolumeMax = 100000,
                Products = new Dictionary<ProductType, Count>() 
                {
                    { ProductType.Battery, new Count() { Value = 1000, MaxValue = 5000 } },
                    { ProductType.Ore, new Count() { Value = 1000, MaxValue = 5000 } }
                }
            });

            var entity1 = entity.AddChild(new GameLib.Entitys.StaticEntitys.ProductionModule());
            entity1.Add(new Production<Iron>());

            Thread.Sleep(1000);

            entity.Get<Warehouse>(out var warehouseStantion);
            entity1.Get<WarehouseProductionModul>(out var warehouseModule);
            entity1.Get<GameLib.Mechanics.Production.Components.ProductionModule>(out var productionModule);

            var end = false;
            var secCount = 0;
            while (!end)
            {
                Thread.Sleep(1000);
                
                if( warehouseStantion.Products.TryGetValue(ProductType.Iron, out var irone)) { }

                Debug.WriteLine($"{Environment.NewLine}" +
                    $"Stantion: {Environment.NewLine}" +
                    $"EntityId [{entity.Id}] " +
                    $"Enargy [{warehouseStantion.Products[ProductType.Battery]?.Value}] " +
                    $"Ore [{warehouseStantion.Products[ProductType.Ore]?.Value}] " +
                    $"Irone [{irone?.Value}] ");

                Debug.WriteLine($"ProductionModule: {Environment.NewLine}" +
                    $"Module enable [{productionModule.Enable}] " +
                    $"Module work [{productionModule.Work}] " +
                    $"Module cycle [{productionModule.CycleCompletionPercentage}]% " +
                    $"EntityId [{entity1.Id}] " +
                    $"Enargy [{warehouseModule.Raws[ProductType.Battery].Value}] " +
                    $"Ore [{warehouseModule.Raws[ProductType.Ore].Value}] " +
                    $"Irone [{warehouseModule.Product.Value.Value}] ");

                secCount++;
                if (secCount > 185) //Прошло 3 минуты 5 сек
                {
                    Assert.IsTrue(warehouseStantion.Products[ProductType.Iron]?.Value > 0, "Not add irone to stantion");
                }

                if (irone?.Value > 100)
                {
                    return;
                } //Ожидать результат по таймауту и бросать TestFail, если не дождались
            }
        }
    }
}
