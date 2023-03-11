using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Diagnostics;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using ECSCoreTests.Entitys;
using ECSCore;
using ECSCoreTests.Components;
using ECSCoreTests.Systems;
using ECSCore.Exceptions;
using ECSCore.Interfaces;

namespace ECSCoreLibTests.Tests.ECSCoreLibTests
{
    [TestClass()]
    public class ECSLibFastTests
    {
        private static IECS IECS;
        private static IECSDebug IECSDebug;
        private static Entity _entity;

        [TestMethod()]
        public void Test_00_InitializationIECS()
        {
            Ship ship = new();
            ECS.Initialization(ship.GetType().Assembly);
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;
            Thread.Sleep(1000);
            Console.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void Test_01_AddEntity()
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
        public void Test_02_GetEntity()
        {
            Assert.IsTrue(IECS.GetEntity(_entity.Id, out Entity ship));
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id == 1);
        }

        [TestMethod()]
        public void Test_03_RemoveEntity()
        {
            IECS.RemoveEntity(_entity.Id);
            IECS.RemoveEntity(3);
            Assert.IsFalse(IECS.GetEntity(_entity.Id, out Entity entity));
            Assert.IsNull(entity);
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == 4);
        }

        [TestMethod()]
        public void Test_04_AddComponent()
        {
            Assert.IsTrue(IECS.GetEntity(2, out Entity entity));
            entity.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
            Assert.IsTrue(entity.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            Assert.ThrowsException<ExceptionEntityHaveComponent>(() => entity.Add(new Pozition() { X = 1, Y = 1, Z = 1 }));
            Assert.IsTrue(entity.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.AddComponent(new Pozition() { X = 10, Y = 10, Z = 10, Id = 4 });
            Assert.IsTrue(IECS.GetEntity(4, out Entity entity1));
            Assert.IsTrue(entity1.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
            _entity = entity;
        }

        [TestMethod()]
        public void Test_05_GetComponent()
        {
            Assert.IsTrue(_entity.Get(out Pozition pozition));
            Assert.IsNotNull(pozition);
            Assert.IsTrue(IECS.GetComponent(4, out Pozition pozition1));
            Assert.IsNotNull(pozition1);
            Assert.IsFalse(IECS.GetComponent(1, out Pozition pozition2));
            Assert.IsNull(pozition2);

        }

        [TestMethod()]
        public void Test_06_RemoveComponent()
        {
            _entity.Remove<Pozition>();
            Assert.IsTrue(_entity.Components.Count == 0);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            IECS.RemoveComponent<Pozition>(4);
            Assert.IsTrue(IECS.GetEntity(4, out Entity Entity));
            Assert.IsTrue(Entity.Components.Count == 0);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 0);
            IECS.GetComponent(Entity.Id, out Pozition pozition);
            IECS.GetComponent(4, out Pozition pozition1);
            Assert.IsNull(pozition);
            Assert.IsNull(pozition1);
        }

        [TestMethod()]
        public void Test_07_AddComponentToFilter()
        {
            IECS.GetEntity(5, out Entity entity);
            entity.Add(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.IsTrue(entity.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 1);
            entity.Add(new Speed() { dX = 1, dY = 5, dZ = 8 });
            Assert.IsTrue(entity.Components.Count == 2);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 2);
            Assert.IsTrue(IECS.GetEntity(6, out Entity entity1));
            entity1.Add(new Pozition() { X = 10, Y = 10, Z = 10 });
            Assert.IsTrue(entity1.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            entity1.Add(new Speed());
            Assert.IsTrue(entity1.Components.Count == 2);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 4);
            Thread.Sleep(2500);
            Assert.IsTrue(IECSDebug.ManagerFilters.CountEntitys == 2);
            _entity = entity1;
        }

        [TestMethod()]
        public void Test_09_RemoveComponentFromFilter()
        {
            _entity.Remove<Speed>();
            Assert.IsTrue(_entity.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            _entity.Remove<Way>();
            Assert.IsTrue(_entity.Components.Count == 1);
            //Assert.IsTrue(IECSDebug.ManagerComponents.CountComponents == 3);
            Thread.Sleep(1000);
            Debug.WriteLine(IECSDebug.GetInfo(true));
        }

        [TestMethod()]
        public void Test_10_ChildEntity()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            int startCountEntitys = IECSDebug.ManagerEntitys.CountEntitys;

            Entity ship = IECS.AddEntity(new Ship());
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 1);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(ship.ParentEntity == null);
            Assert.IsTrue(ship.ChildEntitys.Count == 0);

            Entity shipChild = (Entity)ship.AddChild(new Ship());
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 2);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(ship.ParentEntity == null);
            Assert.IsTrue(ship.ChildEntitys.Count == 1);
            Assert.IsTrue(ship.GetChild(shipChild.Id, out IEntity _));
            Assert.IsTrue(shipChild.ParentEntity != null);
            Assert.IsTrue(shipChild.ParentEntity.Id == ship.Id);
            Assert.IsTrue(shipChild.ChildEntitys.Count == 0);

            Entity shipChild1 = (Entity)ship.AddChild(new Ship());
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 3);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsTrue(ship.ParentEntity == null);
            Assert.IsTrue(ship.ChildEntitys.Count == 2);
            Assert.IsTrue(ship.GetChild(shipChild.Id, out IEntity _));
            Assert.IsTrue(ship.GetChild(shipChild1.Id, out IEntity _));
            Assert.IsTrue(shipChild.ParentEntity != null);
            Assert.IsTrue(shipChild.ParentEntity.Id == ship.Id);
            Assert.IsTrue(shipChild.ChildEntitys.Count == 0);
            Assert.IsTrue(shipChild1.ParentEntity != null);
            Assert.IsTrue(shipChild1.ParentEntity.Id == ship.Id);
            Assert.IsTrue(shipChild1.ChildEntitys.Count == 0);

            Assert.IsTrue(ship.RemoveChild(shipChild1.Id, out IEntity entity));
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 3);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsTrue(ship.ParentEntity == null);
            Assert.IsTrue(ship.ChildEntitys.Count == 1);
            Assert.IsTrue(ship.GetChild(shipChild.Id, out IEntity _));
            Assert.IsFalse(ship.GetChild(shipChild1.Id, out IEntity _));
            Assert.IsTrue(shipChild.ParentEntity != null);
            Assert.IsTrue(shipChild.ParentEntity.Id == ship.Id);
            Assert.IsTrue(shipChild.ChildEntitys.Count == 0);
            Assert.IsTrue(shipChild1.ParentEntity == null);
            Assert.IsTrue(shipChild1.ChildEntitys.Count == 0);

            ship.Death();
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 2);
            Assert.IsFalse(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsTrue(ship.ParentEntity == null);
            Assert.IsTrue(ship.ChildEntitys.Count == 0);
            Assert.IsFalse(ship.GetChild(shipChild.Id, out IEntity _));
            Assert.IsFalse(ship.GetChild(shipChild1.Id, out IEntity _));
            Assert.IsTrue(shipChild.ParentEntity == null);
            Assert.IsTrue(shipChild.ChildEntitys.Count == 0);
            Assert.IsTrue(shipChild1.ParentEntity == null);
            Assert.IsTrue(shipChild1.ChildEntitys.Count == 0);
        }

        [TestMethod()]
        public void Test_11_ExcludeFilterCallForAddInclude()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            Entity entity = IECS.AddEntity(new Ship());
            entity.Add(new Include());

            Thread.Sleep(50);
            if(entity.Get(out Include include))
            {
                if (!include.CallOfAdd)
                {
                    Assert.Fail("Can not call ActionAdd of system");
                }
            }

            Thread.Sleep(100);
            Assert.AreEqual(1, include.CallOfAddCount, "ActionAdd of system not actual count call");
          
            entity.Remove<Include>();

            Thread.Sleep(50);
            if (!include.CallOfRemove)
            {
                Assert.Fail("Can not call ActionRemove of system");
            }

            Thread.Sleep(100);
            Assert.AreEqual(1, include.CallOfRemoveCount, "ActionRemove of system not actual count call");
            Assert.IsTrue(include.CallActionCount > 5, "Action of system not more 5 count call");
        }

        [TestMethod()]
        public void Test_12_ExcludeFilterCallForRemoveExclude()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            Entity entity = IECS.AddEntity(new Ship());
            entity.Add(new Exclude());
            entity.Add(new Include());

            Thread.Sleep(50);
            if (entity.Get(out Include include))
            {
                if (include.CallOfAdd)
                {
                    Assert.Fail("ActionAdd of system is call");
                }
            }

            entity.Remove<Exclude>();

            Thread.Sleep(50);
            if (entity.Get(out Include include1))
            {
                if (!include1.CallOfAdd)
                {
                    Assert.Fail("Can not call ActionAdd of system");
                }
            }

            Thread.Sleep(100);
            if (entity.Get(out Include include2))
            {
                Assert.AreEqual(1, include2.CallOfAddCount, "ActionAdd of system not actual count call");
            }

            entity.Add(new Exclude());
            Thread.Sleep(50);

            if (!include.CallOfRemove)
            {
                Assert.Fail("Can not call ActionRemove of system");
            }

            Thread.Sleep(100);
            Assert.AreEqual(1, include.CallOfRemoveCount, "ActionRemove of system not actual count call");
            Assert.IsTrue(include.CallActionCount > 5, "Action of system not more 5 count call");
        }
    }
}