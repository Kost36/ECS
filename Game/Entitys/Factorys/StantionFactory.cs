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

            entityBase.Add(new Components.ObjectStates.Health { HealthFact = 1000, HealthMax = 100000 }); //Прочность
            entityBase.Add(new Components.ObjectStates.Shild { ShildFact = 1000, ShildMax = 100000 }); //Щиты
            entityBase.Add(new Components.ObjectStates.Enargy { EnargyFact = 1000, EnargyMax = 100000 }); //Энергия

            entityBase.Add(new Components.ObjectStates.Hold { HoldMax = 10000, HoldUse = 0 }); //Вместимость

            entityBase.Add(new Components.Propertys.EnargyReGeneration() { EnargyReGen = 10 }); //Регенерация энергии
            entityBase.Add(new Components.Propertys.HealthReGeneration() { HealthReGen = 10 }); //Регенерация прочности
            entityBase.Add(new Components.Propertys.ShildReGeneration() { ShildReGen = 10 }); //Регенерация щитов

            entityBase.Add(new Components.Products.Battery() { Count = 10000 });

            //entityBase.Add(new Components) //Производство
        }
    }
}