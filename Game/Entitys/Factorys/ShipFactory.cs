using ECSCore;
using ECSCore.BaseObjects;
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
        /// Конструктор
        /// </summary>
        public static void AddShip()
        {
            ECS ecs = ECS.Instance;
            EntityBase entityBase = ecs.AddEntity(new Ship());
            entityBase.Add(new Components.ObjectStates.Pozition { X = 0, Y = 0, Z = 0 }); //Позиция
            entityBase.Add(new Components.ObjectStates.Direction { X = 1, Y = 0, Z = 0 }); //Направление

            entityBase.Add(new Components.ObjectStates.Health { Value = 1000, ValueMax = 10000 }); //Прочность
            entityBase.Add(new Components.ObjectStates.Shild { Value = 100, ValueMax = 10000 }); //Щиты
            entityBase.Add(new Components.ObjectStates.Energy { Value = 1000, ValueMax = 10000 }); //Энергия

            entityBase.Add(new Components.ObjectStates.Hold { ValueMax = 1000, ValueUse = 0 }); //Вместимость
            entityBase.Add(new Components.ObjectStates.Weight { Value = 1000 }); //Вес

            entityBase.Add(new Components.Propertys.EnargyReGeneration() { Value = 10 }); //Регенерация энергии
            entityBase.Add(new Components.Propertys.HealthReGeneration() { Value = 1 }); //Регенерация прочности
            entityBase.Add(new Components.Propertys.ShildReGeneration() { Value = 1 }); //Регенерация щитов
            entityBase.Add(new Components.Propertys.SpeedRotation()); //Скорость поворота

            entityBase.Add(new Components.ObjectStates.ShipState { StateShip = Enums.StateShip.TRADE }); //Торговать
        }
    }
}