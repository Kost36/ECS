using ECSCore.Interfaces.ECS;
using GameLib.Components.Energy;
using GameLib.Entitys;
using GameLib.Mechanics.Mining.AI.Components;
using GameLib.Mechanics.Mining.AI.Enums;
using GameLib.Mechanics.Mining.Components;
using GameLib.Mechanics.Mining.Entites;
using GameLib.Mechanics.Products.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var asteroid = IECS.AddEntity(new Asteroid());
            asteroid.AddComponent(new Mineral() { Type = MineralType.Ice, InitialCapacity = 10000, Capacity = 10000 });

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new EnergyCapacity() { Max = 1000 });
            ship.AddComponent(new EnergyRegeneration() { Regeneration = 10 });
            ship.AddComponent(new Energy() { Fact = 1000 });
            ship.AddComponent(new MiningAI() { MiningAIState = MiningAIState.Wait });
            ship.AddComponent(new ShipModuleMining() { CompletionPercentagePerSec = 0.5f, EnergyConsumptionPerSec = 5 });
            ship.AddComponent(new ShipCommandMining() { TargetAsteroidId = asteroid.Id });

            //Настроить AI так, что бы он снабжал станцию минералами.
            //AI должен анализировать потребляемые станцией минералы и добывать недостающие на станции минералы
            //AI должен выбрать минерал, потребляемый станцией, которого меньше всего на станции, далее найти астеройд с данным минералом. Добраться до астеройда. Добыть минерал с астеройда. Привести на станцию. Повторить действие по снабжению станции.
        }
    }
}
