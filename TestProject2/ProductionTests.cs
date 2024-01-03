using ECSCore.Interfaces.ECS;
using GameLib.Datas;
using GameLib.Entitys;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Production.Entites;
using GameLib.Mechanics.Products.Enums;
using GameLib.Mechanics.Stantion.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameLibTests
{
    [TestClass()]
    public sealed class ProductionTests : BaseTest
    {
        [TestMethod()]
        public void ProductionTest()
        {
            var entity = IECS.AddEntity(new Stantion());
            entity.AddComponent(new Warehouse()
            {
                VolumeMax = 100000,
                Products = new Dictionary<ProductType, Count>()
                {
                    { ProductType.Enargy, new Count() { Value = 1000, MaxValue = 5000 } },
                    { ProductType.Ice, new Count() { Value = 1000, MaxValue = 5000 } }
                }
            });

            var entity1 = entity.AddNestedEntity(new ModuleProduction());
            entity1.AddComponent(new Production() { ProductType = ProductType.Water });

            Thread.Sleep(1000);

            entity.TryGetComponent<Warehouse>(out var warehouseStantion);
            entity1.TryGetComponent<WarehouseProductionModule>(out var warehouseModule);
            entity1.TryGetComponent<ProductionModule>(out var productionModule);

            var end = false;
            var secCount = 0;
            while (!end)
            {
                Thread.Sleep(1000);

                if (warehouseStantion.Products.TryGetValue(ProductType.Water, out var water)) { }

                Debug.WriteLine($"{Environment.NewLine}" +
                    $"Stantion: {Environment.NewLine}" +
                    $"EntityId [{entity.Id}] " +
                    $"Enargy [{warehouseStantion.Products[ProductType.Enargy]?.Value}] " +
                    $"Ice [{warehouseStantion.Products[ProductType.Ice]?.Value}] " +
                    $"Water [{water?.Value}] ");

                Debug.WriteLine($"ProductionModule: {Environment.NewLine}" +
                    $"Module enable [{productionModule.Enable}] " +
                    $"Module work [{productionModule.Work}] " +
                    $"Module cycle [{productionModule.CycleCompletionPercentage}]% " +
                    $"EntityId [{entity1.Id}] " +
                    $"Enargy [{warehouseModule.Raws[ProductType.Enargy].Value}] " +
                    $"Ice [{warehouseModule.Raws[ProductType.Ice].Value}] " +
                    $"Water [{warehouseModule.Product.Value.Value}] ");

                secCount++;
                if (secCount > 185) //Прошло 3 минуты 5 сек
                {
                    Assert.IsTrue(warehouseStantion.Products[ProductType.Ice]?.Value > 0, "Not add irone to stantion");
                }

                if (water?.Value > 100)
                {
                    return;
                } //Ожидать результат по таймауту и бросать TestFail, если не дождались
            }
        }

        [TestMethod()]
        public void ProductionTwoProductsTest()
        {
            var entity = IECS.AddEntity(new Stantion());
            entity.AddComponent(new Warehouse()
            {
                VolumeMax = 100000,
                Products = new Dictionary<ProductType, Count>()
                {
                    { ProductType.Enargy, new Count() { Value = 2000, MaxValue = 5000 } },
                    { ProductType.Ice, new Count() { Value = 2000, MaxValue = 5000 } }
                }
            });

            var entity1 = entity.AddNestedEntity(new ModuleProduction());
            entity1.AddComponent(new Production() { ProductType = ProductType.Water });

            var entity2 = entity.AddNestedEntity(new ModuleProduction());
            entity2.AddComponent(new Production() { ProductType = ProductType.Grain });

            Thread.Sleep(1000);

            entity.TryGetComponent<Warehouse>(out var warehouseStantion);
            entity1.TryGetComponent<WarehouseProductionModule>(out var warehouseModule1);
            entity1.TryGetComponent<ProductionModule>(out var productionModule1);
            entity2.TryGetComponent<WarehouseProductionModule>(out var warehouseModule2);
            entity2.TryGetComponent<ProductionModule>(out var productionModule2);

            var end = false;
            var secCount = 0;
            while (!end)
            {
                Thread.Sleep(1000);

                if (warehouseStantion.Products.TryGetValue(ProductType.Water, out var water)) { }
                if (warehouseStantion.Products.TryGetValue(ProductType.Grain, out var grain)) { }

                Debug.WriteLine($"{Environment.NewLine}" +
                    $"Stantion: {Environment.NewLine}" +
                    $"EntityId [{entity.Id}] " +
                    $"Enargy [{warehouseStantion.Products[ProductType.Enargy]?.Value}] " +
                    $"Ice [{warehouseStantion.Products[ProductType.Ice]?.Value}] " +
                    $"Water [{water?.Value}] " +
                    $"Grain [{grain?.Value}] ");

                Debug.WriteLine($"ProductionModule1: {Environment.NewLine}" +
                    $"Module enable [{productionModule1.Enable}] " +
                    $"Module work [{productionModule1.Work}] " +
                    $"Module cycle [{productionModule1.CycleCompletionPercentage}]% " +
                    $"EntityId [{entity1.Id}] " +
                    $"Enargy [{warehouseModule1.Raws[ProductType.Enargy].Value}] " +
                    $"Ice [{warehouseModule1.Raws[ProductType.Ice].Value}] " +
                    $"Water [{warehouseModule1.Product.Value.Value}] ");

                Debug.WriteLine($"ProductionModule2: {Environment.NewLine}" +
                    $"Module enable [{productionModule2.Enable}] " +
                    $"Module work [{productionModule2.Work}] " +
                    $"Module cycle [{productionModule2.CycleCompletionPercentage}]% " +
                    $"EntityId [{entity2.Id}] " +
                    $"Enargy [{warehouseModule2.Raws[ProductType.Enargy].Value}] " +
                    $"Water [{warehouseModule2.Raws[ProductType.Water].Value}] " +
                    $"Grain [{warehouseModule2.Product.Value.Value}] ");

                secCount++;
                if (secCount > 185) //Прошло 3 минуты 5 сек
                {
                    Assert.IsTrue(warehouseStantion.Products[ProductType.Ice]?.Value > 0, "Not add irone to stantion");
                    Assert.IsTrue(warehouseStantion.Products[ProductType.Grain]?.Value > 0, "Not add irone to stantion");
                }

                if (grain?.Value > 20)
                {
                    return;
                } //Ожидать результат по таймауту и бросать TestFail, если не дождались
            }
        }
    }
}
