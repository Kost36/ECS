using ECSCore;
using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using ECSCore.Interfaces.ECS;
using ECSCoreTests.Components;
using ECSCoreTests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Tests.Entitys.Factorys
{
    /// <summary>
    /// Фабрика корабля
    /// </summary>
    public static class ShipFactory
    {
        /// <summary>
        /// Добавить корабль
        /// </summary>
        public static void AddShip()
        {
            IECS ecs = ECS.InstanceIECS;
            Entity Entity = ecs.AddEntity(new Ship());
            Entity.Add(new ShipAi { }); //Интелект

            Entity.Add(new Pozition { X = 0, Y = 0, Z = 0 }); //Позиция
            Entity.Add(new PozitionSV { X = 10000000, Y = 10000000, Z = 10000000 }); //Позиция
            Entity.Add(new Direction { XNorm = 1, YNorm = 0, ZNorm = 0 }); //Направление

            Entity.Add(new Health { HealthFact = 1000, HealthMax = 10000 }); //Прочность
            Entity.Add(new Shild { ShildFact = 100, ShildMax = 10000 }); //Щиты
            Entity.Add(new Enargy { EnargyFact = 1000, EnargyMax = 10000 }); //Энергия

            Entity.Add(new Hold { HoldMax = 1000, HoldUse = 0 }); //Вместимость
            Entity.Add(new Weight { WeightFact = 1000 }); //Вес

            Entity.Add(new EnargyReGeneration() { EnargyReGen = 10 }); //Регенерация энергии
            Entity.Add(new HealthReGeneration() { HealthReGen = 1 }); //Регенерация прочности
            Entity.Add(new ShildReGeneration() { ShildReGen = 1 }); //Регенерация щитов
            Entity.Add(new SpeedRotation()); //Скорость поворота

            Entity.Add(new ShipState { StateShip = StateShip.TRADE }); //Торговать
        }
    }
}