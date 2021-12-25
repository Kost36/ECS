using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Entitys.Factorys;
using Game.Entitys;
using System.Threading;
using ECSCore.BaseObjects;
using ECSCore.Interface;
using Game.Components.ObjectStates;
using Game.Components.Tasks;
using System.Diagnostics;

namespace ECSCore.Tests
{
    [TestClass()]
    public class ECSTests
    {
        public static ECS ECS;
        public static EntityBase Entity;
        [TestMethod()]
        public void Test_0InitializationECS()
        {
            Ship ship = new Ship();
            ECS.Initialization(ship.GetType().Assembly);
            ECS = ECS.Instance;
            for (int i = 0; i<10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"{i} - ОК");
            }
            Assert.IsNotNull(ECS);
            Debug.WriteLine(ECS.GetInfo());
        }

        [TestMethod()]
        public void Test_1AddEntity()
        {
            Entity = ECS.AddEntity(new Ship());
            ECS.AddEntity(new Ship());
            ECS.AddEntity(new Ship());
            ECS.AddEntity(new Ship());
            ECS.AddEntity(new Ship());
            ECS.AddEntity(new Ship());
            Console.WriteLine("");
        }

        [TestMethod()]
        public void Test_2GetEntity()
        {
            Ship ship = (Ship)ECS.GetEntity(Entity.Id);
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id==1);
        }

        [TestMethod()]
        public void Test_3RemoveEntity()
        {
            ECS.RemoveEntity(Entity.Id);
            ECS.RemoveEntity(3);
            IEntity entity = ECS.GetEntity(Entity.Id);
            Assert.IsNull(entity);
        }

        [TestMethod()]
        public void Test_4AddComponent()
        {
            Entity = (Ship)ECS.GetEntity(2);
            Entity.Add<Pozition>(new Pozition() { X = 0, Y = 0, Z = 0 });
            Entity.Add<Pozition>(new Pozition() { X = 1, Y = 1, Z = 1 });
            ECS.AddComponent<Pozition>(new Pozition() { X = 10, Y = 10, Z = 10, Id = 4 });
            Console.WriteLine("");
        }

        [TestMethod()]
        public void Test_5GetComponent()
        {
            Pozition pozition = (Pozition)Entity.Get<Pozition>();
            Pozition pozition1 = (Pozition)ECS.GetComponent<Pozition>(4);
            ComponentBase componentBase = ECS.GetComponent<Pozition>(1);
            Assert.IsNotNull(pozition);
            Assert.IsNotNull(pozition1);
            Assert.IsNull(componentBase);
        }

        [TestMethod()]
        public void Test_6RemoveComponent()
        {
            Entity.Remove<Pozition>();
            ECS.RemoveComponent<Pozition>(4);
            ComponentBase componentBase = ECS.GetComponent<Pozition>(Entity.Id);
            ComponentBase componentBase1 = ECS.GetComponent<Pozition>(4);
            Assert.IsNull(componentBase);
            Assert.IsNull(componentBase1);
        }

        [TestMethod()]
        public void Test_7AddComponentToFilter()
        {
            Entity = (Ship)ECS.GetEntity(5);
            Entity.Add<Pozition>(new Pozition() { X = 0, Y = 0, Z = 0 });
            Entity.Add<PozitionSV>(new PozitionSV() { X = 10000, Y = 10000, Z = 10000 });
            Entity = (Ship)ECS.GetEntity(6);
            Entity.Add<Pozition>(new Pozition() { X = 10, Y = 10, Z = 10 });
            Entity.Add<PozitionSV>(new PozitionSV() { X = 100, Y = 100, Z = 100 });
            Thread.Sleep(2500);
            Console.WriteLine("");
        }

        [TestMethod()]
        public void Test_9RemoveComponentFromFilter()
        {
            Entity.Remove<PozitionSV>();
            Entity.Remove<Way>();
            //while (true)
            //{
                Thread.Sleep(5000);
                Entity = (Ship)ECS.GetEntity(5);
                Debug.WriteLine(ECS.GetInfo(true));
            //}
        }

        [TestMethod()]
        public void Test_APerformance()
        {
            while (true)
            {
                for (int i=0; i<100; i++)
                {
                    ShipFactory.AddShip();
                }
                Debug.WriteLine(ECS.GetInfo(true));
                Thread.Sleep(1000);
                if (ECS.ManagerEntitys.CountEntitys > 10000)
                {
                    break;
                }
            }
            while (true)
            {
                Debug.WriteLine(ECS.GetInfo(true));
                Thread.Sleep(1000);
            }
        }
    }
}