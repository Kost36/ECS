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
    /// Фабрика станции
    /// </summary>
    public static class StantionFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public static void AddStantion(Components.ObjectStates.Pozition pozition)
        {
            ECS ecs = ECS.Instance;
            EntityBase entityBase = ecs.AddEntity(new Stantion());
            entityBase.Add(pozition); //Позиция

            entityBase.Add(new Components.ObjectStates.Health { Value = 1000, ValueMax = 100000 }); //Прочность
            entityBase.Add(new Components.ObjectStates.Shild { Value = 1000, ValueMax = 100000 }); //Щиты
            entityBase.Add(new Components.ObjectStates.Energy { Value = 1000, ValueMax = 100000 }); //Энергия

            entityBase.Add(new Components.ObjectStates.Hold { ValueMax = 10000, ValueUse = 0 }); //Вместимость

            entityBase.Add(new Components.Propertys.EnargyReGeneration() { Value = 10 }); //Регенерация энергии
            entityBase.Add(new Components.Propertys.HealthReGeneration() { Value = 10 }); //Регенерация прочности
            entityBase.Add(new Components.Propertys.ShildReGeneration() { Value = 10 }); //Регенерация щитов

            entityBase.Add(new Components.Products.Battery() { Count = 10000 });

            //entityBase.Add(new Components) //Производство
        }
    }
}