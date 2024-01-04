using ECSCore.Interfaces.ECS;
using GameLib.Components;
using GameLib.Components.Energy;
using GameLib.Entitys;
using GameLib.Mechanics.MineralExtraction.AI.Components;
using GameLib.Mechanics.MineralExtraction.AI.Enums;
using GameLib.Mechanics.MineralExtraction.Components;
using GameLib.Mechanics.MineralExtraction.Components.Commands;
using GameLib.Mechanics.MineralExtraction.Entites;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Production.Entites;
using GameLib.Mechanics.Products.Enums;
using GameLib.Mechanics.Stantion.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace GameLibTests
{
    [TestClass()]
    public sealed class MiningTests : BaseTest
    {
        /// <summary>
        /// 1) Станция потребляет несколько видов минерала через производство.
        /// 2) Для производства у станции не хватает энергии.
        /// 3) Рядом есть астеройды с нужными станции минералами.
        /// 4) Есть кораблю шахтер.
        /// Expected: Корабль должен обеспечить станцию минералами.
        /// </summary>
        [TestMethod()]
        public void Mining_StationSupplyTest()
        {
            //OwnerCompany
            //KnownInformation

            var asteroidIce = IECS.AddEntity(new Asteroid());
            asteroidIce.AddComponent(new Position() { X = 1000, Y = 1000, Z = 0 });
            asteroidIce.AddComponent(new AsteroidMineral() { Type = MineralType.Ice, InitialCapacity = 10000, Capacity = 10000 });

            var asteroidSilicon = IECS.AddEntity(new Asteroid());
            asteroidSilicon.AddComponent(new Position() { X = 0, Y = 1000, Z = 0 });
            asteroidSilicon.AddComponent(new AsteroidMineral() { Type = MineralType.Silicon, InitialCapacity = 5000, Capacity = 5000 });

            var stantion = IECS.AddEntity(new Stantion());
            stantion.AddComponent(new Position() { X = 0, Y = 0, Z = 0 });
            stantion.AddComponent(new Warehouse()
            {
                VolumeMax = 100000
            });

            var productionModuleEnargy = stantion.AddNestedEntity(new ModuleProduction());
            productionModuleEnargy.AddComponent(new Production() { ProductType = ProductType.Enargy });

            var productionModuleWater = stantion.AddNestedEntity(new ModuleProduction());
            productionModuleWater.AddComponent(new Production() { ProductType = ProductType.Water });

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new Position() { X = 0, Y = 0, Z = 0 });
            ship.AddComponent(new EnergyCapacity() { Max = 1000 });
            ship.AddComponent(new EnergyRegeneration() { Regeneration = 2 });
            ship.AddComponent(new Energy() { Fact = 100 });
            ship.AddComponent(new MiningAI() { MiningAIState = MiningAIState.Wait });
            ship.AddComponent(new ShipModuleMining() { CompletionPercentagePerSec = 0.5f, EnergyConsumptionPerSec = 5 });
            ship.AddComponent(new ShipCommandSupplyStantion() { StantionId = stantion.Id });
            ship.AddComponent(new ShipMiningSetting());

            Thread.Sleep(1000);

            stantion.TryGetComponent<Warehouse>(out var warehouseStantion);
            productionModuleEnargy.TryGetComponent<WarehouseProductionModule>(out var warehouseEnargyModule);
            productionModuleEnargy.TryGetComponent<ProductionModule>(out var productionEnargyModule);

            productionModuleWater.TryGetComponent<WarehouseProductionModule>(out var warehouseWaterModule);
            productionModuleWater.TryGetComponent<ProductionModule>(out var productionWaterModule);

            asteroidIce.TryGetComponent<Position>(out var asteroidIcePosition);
            asteroidIce.TryGetComponent<AsteroidMineral>(out var asteroidIceMineral);

            asteroidSilicon.TryGetComponent<Position>(out var asteroidSiliconPosition);
            asteroidSilicon.TryGetComponent<AsteroidMineral>(out var asteroidSiliconMineral);

            ship.TryGetComponent<Position>(out var shipPosition);
            ship.TryGetComponent<Energy>(out var shipEnergy);
            ship.TryGetComponent<MiningAI>(out var shipMiningAI);

            var end = false;
            var secCount = 0;
            while (!end)
            {
                Thread.Sleep(1000);

                Debug.WriteLine($"{Environment.NewLine}" +
                    $"Stantion: {Environment.NewLine}" +
                    $"EntityId [{stantion.Id}] " +
                    $"Enargy [{warehouseStantion.Products[ProductType.Enargy]?.Count}] " +
                    $"Ice [{warehouseStantion.Products[ProductType.Ice]?.Count}] " +
                    $"Water [{warehouseStantion.Products[ProductType.Water]?.Count}] ");

                Debug.WriteLine($"ProductionModuleEnargy: {Environment.NewLine}" +
                    $"Module enable [{productionEnargyModule.Enable}] " +
                    $"Module work [{productionEnargyModule.Work}] " +
                    $"Module cycle [{productionEnargyModule.CycleCompletionPercentage}]% " +
                    $"EntityId [{productionModuleEnargy.Id}] " +
                    $"Enargy [{warehouseEnargyModule.Product.Value.Value}] ");

                Debug.WriteLine($"ProductionModuleWater: {Environment.NewLine}" +
                    $"Module enable [{productionWaterModule.Enable}] " +
                    $"Module work [{productionWaterModule.Work}] " +
                    $"Module cycle [{productionWaterModule.CycleCompletionPercentage}]% " +
                    $"EntityId [{productionModuleWater.Id}] " +
                    $"Enargy [{warehouseWaterModule.Raws[ProductType.Enargy].Value}] " +
                    $"Ice [{warehouseWaterModule.Raws[ProductType.Ice].Value}] " +
                    $"Water [{warehouseWaterModule.Product.Value.Value}] ");

                Debug.WriteLine($"AsteroidIce: {Environment.NewLine}" +
                    $"Position X:[{asteroidIcePosition.X}] Y:[{asteroidIcePosition.Y}] Z:[{asteroidIcePosition.Z}] " +
                    $"Mineral Type:[{asteroidIceMineral.Type}] Capacity:[{asteroidIceMineral.Capacity}]");

                Debug.WriteLine($"AsteroidSilicon: {Environment.NewLine}" +
                    $"Position X:[{asteroidSiliconPosition.X}] Y:[{asteroidSiliconPosition.Y}] Z:[{asteroidSiliconPosition.Z}] " +
                    $"Mineral Type:[{asteroidSiliconMineral.Type}] Capacity:[{asteroidSiliconMineral.Capacity}]");

                Debug.WriteLine($"Ship: {Environment.NewLine}" +
                    $"Position X:[{shipPosition.X}] Y:[{shipPosition.Y}] Z:[{shipPosition.Z}] " +
                    $"Enargy [{shipEnergy.Fact}] " +
                    $"MiningAI [{shipMiningAI.MiningAIState}] ");

                //secCount++;
                ////if (secCount > 185) //Прошло 3 минуты 5 сек
                ////{
                ////    Assert.IsTrue(warehouseStantion.Products[ProductType.Ice]?.Value > 0, "Not add irone to stantion");
                ////}

                //if (water?.Value > 100)
                //{
                //    return;
                //} //Ожидать результат по таймауту и бросать TestFail, если не дождались
            }





            //Настроить AI так, что бы он снабжал станцию минералами.
            //AI должен анализировать потребляемые станцией минералы и добывать недостающие на станции минералы
            //AI должен выбрать минерал, потребляемый станцией, которого меньше всего на станции, далее найти астеройд с данным минералом. Добраться до астеройда. Добыть минерал с астеройда. Привести на станцию. Повторить действие по снабжению станции.
        }
    }
}
