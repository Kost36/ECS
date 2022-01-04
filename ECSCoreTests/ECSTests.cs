﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using ECSCore.Interfaces;
using ECSCore.BaseObjects;
using Assets.ECS.Entitys;
using GameLib.Components;
using Game.Entitys.Factorys;

namespace ECSCore.Tests
{
    [TestClass()]
    public class ECSTests
    {
        public static IECS IECS;
        public static IECSDebug IECSDebug;
        public static Entity _entity;
        [TestMethod()]
        public void Test_0InitializationIECS()
        {
            Ship ship = new Ship();
            ECS.Initialization(ship.GetType().Assembly);
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;
            Thread.Sleep(1000);
            Console.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void Test_1AddEntity()
        {
            _entity = IECS.AddEntity(new Ship());
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
            Assert.IsTrue(IECS.GetEntity(_entity.Id, out Entity ship));
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id==1);
        }

        [TestMethod()]
        public void Test_3RemoveEntity()
        {
            IECS.RemoveEntity(_entity.Id);
            IECS.RemoveEntity(3);
            Assert.IsFalse(IECS.GetEntity(_entity.Id, out Entity entity));
            Assert.IsNull(entity);
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 4);
        }

        [TestMethod()]
        public void Test_4AddComponent()
        {
            Assert.IsTrue(IECS.GetEntity(2, out Entity entity));
            entity.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
            Assert.IsTrue(entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            entity.Add(new Pozition() { X = 1, Y = 1, Z = 1 });
            Assert.IsTrue(entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.AddComponent(new Pozition() { X = 10, Y = 10, Z = 10, Id = 4 });
            Assert.IsTrue(IECS.GetEntity(4, out Entity entity1));
            Assert.IsTrue(entity1.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
            _entity = entity;
        }

        [TestMethod()]
        public void Test_5GetComponent()
        {
            Assert.IsTrue(_entity.Get(out Pozition pozition));
            Assert.IsNotNull(pozition);
            Assert.IsTrue(IECS.GetComponent(4, out Pozition pozition1));
            Assert.IsNotNull(pozition1);
            Assert.IsFalse(IECS.GetComponent(1, out Pozition pozition2));
            Assert.IsNull(pozition2);

        }

        [TestMethod()]
        public void Test_6RemoveComponent()
        {
            _entity.Remove<Pozition>();
            Assert.IsTrue(_entity.Components.Count == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.RemoveComponent<Pozition>(4); 
            Assert.IsTrue(IECS.GetEntity(4, out Entity Entity));
            Assert.IsTrue(Entity.Components.Count == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 0);
            IECS.GetComponent(Entity.Id, out Pozition pozition);
            IECS.GetComponent(4, out Pozition pozition1);
            Assert.IsNull(pozition);
            Assert.IsNull(pozition1);
        }

        [TestMethod()]
        public void Test_7AddComponentToFilter()
        {
            IECS.GetEntity(5, out Entity entity);
            entity.Add(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.IsTrue(entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            entity.Add(new Speed() { dX=1,dY=5, dZ=8 });
            Assert.IsTrue(entity.Components.Count == 2);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
            Assert.IsTrue(IECS.GetEntity(6, out Entity entity1));
            entity1.Add(new Pozition() { X = 10, Y = 10, Z = 10 });
            Assert.IsTrue(entity1.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            entity1.Add(new Speed());
            Assert.IsTrue(entity1.Components.Count == 2);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 4);
            Thread.Sleep(2500);
            Assert.IsTrue(IECSDebug.ManagerFilters.CountEntitys == 2);
            _entity = entity1;
        }

        [TestMethod()]
        public void Test_9RemoveComponentFromFilter()
        {
            _entity.Remove<Speed>();
            Assert.IsTrue(_entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            _entity.Remove<Way>();
            Assert.IsTrue(_entity.Components.Count == 1);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            Thread.Sleep(1000);
            Debug.WriteLine(IECSDebug.GetInfo(true));
        }

        [TestMethod()]
        public void Test_APerformance()
        {
            Thread.Sleep(10000);
            //Заполняем
            while (true)
            {
                for (int i=0; i<1000; i++)
                {
                    ShipFactory.AddShip();
                }
                Debug.WriteLine(IECSDebug.GetInfo(true));
                Thread.Sleep(100);
                if (IECSDebug.ManagerEntitys.CountEntitys > 50000)
                {
                    break;
                }
            }
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            //Ждем
            Thread.Sleep(50000);
            //Наблюдаем
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            int j = 0;
            while (j<2000)
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
                    if(IECS.GetEntity(entityNumb, out Entity Entity))
                    {
                        Entity.Death();
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
            Thread.Sleep(5000);
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 0);
            Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 0);
            Assert.IsTrue(IECSDebug.ManagerFilters.CountEntitys==0);
            Debug.WriteLine(IECSDebug.GetInfo(true));
        }


        [TestMethod()]
        public void Test_B()
        {
            //ECSCore.ECS.Initialization(this.GetType().Assembly); 

            //IECS iECS = ECSCore.ECS.InstanceIECS;

            //Ship ship = (Ship)iECS.AddEntity(new Ship());

            ////ship.Add(new Pozition() { X =10, Y =10, Z =10 });

            ////if (ship.Get(out Pozition pozition))
            ////{
            ////    pozition.X = 100;
            ////}

            ////ship.Remove<Pozition>();

            ////ship.Death();

            //iECS.AddComponent(new Pozition() { Id = 1, X = 10, Y = 10, Z = 10 });

            //if (iECS.GetComponent(1, out Pozition pozition))
            //{
            //    pozition.X = 100;
            //}

            //iECS.RemoveComponent<Pozition>(1);

            //iECS.RemoveEntity(1);

            //if (iECS.GetEntity(1, out Entity entity))
            //{
            //    Console.WriteLine("Сущьность есть");
            //}
            //else
            //{
            //    Console.WriteLine("Сущьности нет");
            //}

            //Console.WriteLine();

            //IECSDebug eCSDebug = ECSCore.ECS.InstanceDebug;


        }

        
    }

    [TestClass()]
    public class ECSTests1
    {
        public static IECS IECS;
        public static IECSDebug IECSDebug;
        public static Entity _entity;
        [TestMethod()]
        public void Test_01()
        {
            Ship ship = new Ship();
            ECS.Initialization(ship.GetType().Assembly);
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;
            Console.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        //[TestMethod()]
        //public void Test_02()
        //{
        //    Entity ship = IECS.AddEntity(new Ship());
        //    ship.Add(new Pozition() { X = 1, Y = 2, Z = 3 });
        //    ship.Add(new PozitionSV() { X = 10000, Y = 10000, Z = 10000 });
        //    ship.Add(new Enargy() { EnargyFact = 100, EnargyMax = 1000 });
        //    ship.Add(new EnargyReGeneration() { EnargyReGen = 4f});



        //    Thread.Sleep(500);
        //    while (true)
        //    {
        //        Thread.Sleep(1000);
        //        Debug.WriteLine("     ");
        //        if (ship.Get(out Pozition pozition))
        //        {
        //            Debug.WriteLine($"Позиция: {pozition.X} {pozition.Y} {pozition.Z}");
        //        }
        //        if (ship.Get(out PozitionSV pozitionSV))
        //        {
        //            Debug.WriteLine($"Заданныя позиция: {pozitionSV.X} {pozitionSV.Y} {pozitionSV.Z}");
        //        }
        //        if (ship.Get(out Way way))
        //        {
        //            Debug.WriteLine($"Путь: {way.LenX} {way.LenY} {way.LenZ} Расстояние: {way.Len} Направление: {way.NormX} {way.NormY} {way.NormZ}");
        //        }
        //        if (ship.Get(out Speed speed))
        //        {
        //            Debug.WriteLine($"Скорость: {speed.dX} {speed.dY} {speed.dZ} Скорость: {speed.SpeedFact} / {speed.SpeedMax}");
        //        }
        //        if (ship.Get(out SpeedSV speedSV))
        //        {
        //            Debug.WriteLine($"Заданная скорость: {speedSV.dXSV} {speedSV.dYSV} {speedSV.dZSV} Факт.: {speedSV.SVSpeed}");
        //        }
        //        if (ship.Get(out Enargy enargy))
        //        {
        //            Debug.WriteLine($"Энергия: {enargy.EnargyFact} / {enargy.EnargyMax}");
        //        }

        //    }
        //}


        [TestMethod()]
        public void Test_02()
        {
            int j = 0;
            while (j < 50000)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Entity ship = IECS.AddEntity(new Ship());
                    ship.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
                    ship.Add(new PozitionSV() { X = 1000, Y = 1000, Z = 1000 });
                    ship.Add(new Enargy() { EnargyFact = 100, EnargyMax = 1000 });
                    ship.Add(new EnargyReGeneration() { EnargyReGen = 5f });
                    j++;
                }
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));
            }

            Thread.Sleep(10000);
            IECSDebug.ManagerSystems.ClearStatisticSystems();
            while (true)
            {
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));
                if (IECSDebug.ManagerEntitys.CountEntitys == 0)
                {
                    break;
                }
            }
            Assert.IsTrue(true);
        }

    }
}