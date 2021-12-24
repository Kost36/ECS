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

namespace ECSCore.Tests
{
    [TestClass()]
    public class ECSTests
    {
        public static ECS ECS;
        public static EntityBase Entity;
        [TestMethod()]
        public void Test0InitializationECS()
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
        }

        [TestMethod()]
        public void Test1AddEntity()
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
        public void Test2GetEntity()
        {
            Ship ship = (Ship)ECS.GetEntity(Entity.Id);
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id==1);
        }

        [TestMethod()]
        public void Test3RemoveEntity()
        {
            ECS.RemoveEntity(Entity.Id);
            ECS.RemoveEntity(3);
            IEntity entity = ECS.GetEntity(Entity.Id);
            Assert.IsNull(entity);
        }

        [TestMethod()]
        public void Test4AddComponent()
        {
            Entity = (Ship)ECS.GetEntity(2);
            Entity.Add<Pozition>(new Pozition() { X = 0, Y = 0, Z = 0 });
            Entity.Add<Pozition>(new Pozition() { X = 1, Y = 1, Z = 1 });
            ECS.AddComponent<Pozition>(new Pozition() { X = 10, Y = 10, Z = 10, Id = 4 });
            Console.WriteLine("");
        }

        [TestMethod()]
        public void Test5GetComponent()
        {
            Pozition pozition = (Pozition)Entity.Get<Pozition>();
            Pozition pozition1 = (Pozition)ECS.GetComponent<Pozition>(4);
            ComponentBase componentBase = ECS.GetComponent<Pozition>(1);
            Assert.IsNotNull(pozition);
            Assert.IsNotNull(pozition1);
            Assert.IsNull(componentBase);
        }

        [TestMethod()]
        public void Test6RemoveComponent()
        {
            Entity.Remove<Pozition>();
            ECS.RemoveComponent<Pozition>(4);
            ComponentBase componentBase = ECS.GetComponent<Pozition>(Entity.Id);
            ComponentBase componentBase1 = ECS.GetComponent<Pozition>(4);
            Assert.IsNull(componentBase);
            Assert.IsNull(componentBase1);
        }

        [TestMethod()]
        public void Test7AddComponentToFilter()
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
        public void Test9RemoveComponentFromFilter()
        {
            Entity.Remove<PozitionSV>();
            Entity.Remove<Way>();
            while (true)
            {
                Thread.Sleep(15000);
                Entity = (Ship)ECS.GetEntity(5);
                Console.WriteLine("");
            }
        }
    }
}