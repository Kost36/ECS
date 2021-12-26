using ECSCore;
using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entitys.Factorys
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
            EntityBase entityBase = ecs.AddEntity(new Ship());
            entityBase.Add(new Components.Ai.ShipAi { }); //Интелект

            entityBase.Add(new Components.ObjectStates.Pozition { X = 0, Y = 0, Z = 0 }); //Позиция
            entityBase.Add(new Components.ObjectStates.Direction { X = 1, Y = 0, Z = 0 }); //Направление

            entityBase.Add(new Components.ObjectStates.Health { HealthFact = 1000, HealthMax = 10000 }); //Прочность
            entityBase.Add(new Components.ObjectStates.Shild { ShildFact = 100, ShildMax = 10000 }); //Щиты
            entityBase.Add(new Components.ObjectStates.Enargy { EnargyFact = 1000, EnargyMax = 10000 }); //Энергия

            entityBase.Add(new Components.ObjectStates.Hold { HoldMax = 1000, HoldUse = 0 }); //Вместимость
            entityBase.Add(new Components.ObjectStates.Weight { WeightFact = 1000 }); //Вес

            entityBase.Add(new Components.Propertys.EnargyReGeneration() { EnargyReGen = 10 }); //Регенерация энергии
            entityBase.Add(new Components.Propertys.HealthReGeneration() { HealthReGen = 1 }); //Регенерация прочности
            entityBase.Add(new Components.Propertys.ShildReGeneration() { ShildReGen = 1 }); //Регенерация щитов
            entityBase.Add(new Components.Propertys.SpeedRotation()); //Скорость поворота

            entityBase.Add(new Components.ObjectStates.ShipState { StateShip = Enums.StateShip.TRADE }); //Торговать
        }
    }
}