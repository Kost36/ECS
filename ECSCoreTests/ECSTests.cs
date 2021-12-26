﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Entitys.Factorys;
using Game.Entitys;
using System.Threading;
using Game.Components.ObjectStates;
using Game.Components.Tasks;
using System.Diagnostics;
using ECSCore.Interfaces;
using ECSCore.BaseObjects;

namespace ECSCore.Tests
{
    [TestClass()]
    public class ECSTests
    {
        public static IECS IECS;
        public static IECSDebug IECSDebug;
        public static EntityBase Entity;
        [TestMethod()]
        public void Test_0InitializationIECS()
        {
            Ship ship = new Ship();
            ECS.Initialization(ship.GetType().Assembly, 110000);
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;
            for (int i = 0; i<10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"{i} - ОК");
            }
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void Test_1AddEntity()
        {
            Entity = IECS.AddEntity(new Ship());
            IECS.AddEntity(new Ship());
            IECS.AddEntity(new Ship());
            IECS.AddEntity(new Ship());
            IECS.AddEntity(new Ship());
            IECS.AddEntity(new Ship());
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 6);
        }

        [TestMethod()]
        public void Test_2GetEntity()
        {
            Assert.IsTrue(IECS.GetEntity(Entity.Id, out EntityBase ship));
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id==1);
        }

        [TestMethod()]
        public void Test_3RemoveEntity()
        {
            IECS.RemoveEntity(Entity.Id);
            IECS.RemoveEntity(3);
            Assert.IsFalse(IECS.GetEntity(Entity.Id, out EntityBase entityBase));
            Assert.IsNull(entityBase);
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 4);
        }

        [TestMethod()]
        public void Test_4AddComponent()
        {
            Assert.IsTrue(IECS.GetEntity(2, out Entity));
            Entity.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
            Assert.IsTrue(Entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            Entity.Add(new Pozition() { X = 1, Y = 1, Z = 1 });
            Assert.IsTrue(Entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.AddComponent(new Pozition() { X = 10, Y = 10, Z = 10, Id = 4 });
            Assert.IsTrue(IECS.GetEntity(4, out EntityBase entityBase));
            Assert.IsTrue(entityBase.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
        }

        [TestMethod()]
        public void Test_5GetComponent()
        {
            Assert.IsTrue(Entity.Get(out Pozition pozition));
            Assert.IsNotNull(pozition);
            Assert.IsTrue(IECS.GetComponent(4, out Pozition pozition1));
            Assert.IsNotNull(pozition1);
            Assert.IsFalse(IECS.GetComponent(1, out Pozition pozition2));
            Assert.IsNull(pozition2);
        }

        [TestMethod()]
        public void Test_6RemoveComponent()
        {
            Entity.Remove<Pozition>();
            Assert.IsTrue(Entity.Components.Count == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.RemoveComponent<Pozition>(4); 
            Assert.IsTrue(IECS.GetEntity(4, out EntityBase entityBase));
            Assert.IsTrue(entityBase.Components.Count == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 0);
            IECS.GetComponent(Entity.Id, out Pozition pozition);
            IECS.GetComponent(4, out Pozition pozition1);
            Assert.IsNull(pozition);
            Assert.IsNull(pozition1);
        }

        [TestMethod()]
        public void Test_7AddComponentToFilter()
        {
            IECS.GetEntity(5, out Entity);
            Entity.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
            Assert.IsTrue(Entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            Entity.Add(new PozitionSV() { X = 10000, Y = 10000, Z = 10000 });
            Assert.IsTrue(Entity.Components.Count == 2);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
            Assert.IsTrue(IECS.GetEntity(6, out Entity));
            Entity.Add(new Pozition() { X = 10, Y = 10, Z = 10 });
            Assert.IsTrue(Entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            Entity.Add(new PozitionSV() { X = 100, Y = 100, Z = 100 });
            Assert.IsTrue(Entity.Components.Count == 2);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 4);
            Thread.Sleep(2500);
            //Assert.IsTrue(IECS.ManagerFilters.GetFilter<FilterMove>().Count == 2);
        }

        [TestMethod()]
        public void Test_9RemoveComponentFromFilter()
        {
            Entity.Remove<PozitionSV>();
            Assert.IsTrue(Entity.Components.Count == 4);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 9);
            Entity.Remove<Way>();
            Assert.IsTrue(Entity.Components.Count == 3);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 8);
            Thread.Sleep(1000);
            Debug.WriteLine(IECSDebug.GetInfo(true));
        }

        [TestMethod()]
        public void Test_APerformance()
        {
            //Заполняем
            while (true)
            {
                for (int i=0; i<1000; i++)
                {
                    ShipFactory.AddShip();
                }
                Debug.WriteLine(IECSDebug.GetInfo(true));
                Thread.Sleep(100);
                if (IECSDebug.ManagerEntitys.CountEntitys > 100000)
                {
                    break;
                }
            }
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            //Ждем
            Thread.Sleep(1000);
            //Наблюдаем
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            int j = 0;
            while (j<100)
            {
                Debug.WriteLine(IECSDebug.GetInfo(true));
                Thread.Sleep(1000);
                j++;
            }
            //Удаляем
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            int entityNumb = 0;
            while (true)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if(IECS.GetEntity(entityNumb, out EntityBase entityBase))
                    {
                        entityBase.Death();
                    }
                    entityNumb++;
                }
                Debug.WriteLine(IECSDebug.GetInfo(true));
                Thread.Sleep(100);
                if (IECSDebug.ManagerEntitys.CountEntitys == 0)
                {
                    break;
                }
            }
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            //Ждем
            Thread.Sleep(30000);
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 0);
            Assert.IsTrue(IECSDebug.ManagerFilters.CountEntitys==0);
            Debug.WriteLine(IECSDebug.GetInfo(true));
        }


        public void A()
        {
        }
    }
}