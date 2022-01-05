using ECSCore;
using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using ECSCore.Interfaces.ECS;
using Game.Components;
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
        public static void AddStantion(Pozition pozition)
        {
            IECS ecs = ECS.InstanceIECS;
            Entity Entity = ecs.AddEntity(new Stantion());
            Entity.Add(pozition); //Позиция

            Entity.Add(new Health { HealthFact = 1000, HealthMax = 100000 }); //Прочность
            Entity.Add(new Shild { ShildFact = 1000, ShildMax = 100000 }); //Щиты
            Entity.Add(new Enargy { EnargyFact = 1000, EnargyMax = 100000 }); //Энергия

            Entity.Add(new Hold { HoldMax = 10000, HoldUse = 0 }); //Вместимость

            Entity.Add(new EnargyReGeneration() { EnargyReGen = 10 }); //Регенерация энергии
            Entity.Add(new HealthReGeneration() { HealthReGen = 10 }); //Регенерация прочности
            Entity.Add(new ShildReGeneration() { ShildReGen = 10 }); //Регенерация щитов

            //Entity.Add(new Battery() { Count = 10000 });

            //Entity.Add(new Components) //Производство
        }
    }
}