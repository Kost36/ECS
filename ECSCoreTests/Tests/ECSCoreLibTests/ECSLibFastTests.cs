using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Diagnostics;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using ECSCoreTests.Entitys;
using ECSCore;
using ECSCoreTests.Components;
using ECSCore.Exceptions;
using ECSCore.Interfaces;

namespace ECSCoreLibTests.Tests.ECSCoreLibTests
{
    [TestClass()]
    public class ECSLibFastTests
    {
        private static IECS IECS;
        private static IECSDebug IECSDebug;

        [TestMethod()]
        public void Test_00_InitializationIECS()
        {
            IECS?.Despose();

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
            Test_00_InitializationIECS();

            IECS.AddEntity(new Ship());
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
            Test_00_InitializationIECS();

            var entity = IECS.AddEntity(new Ship());
            Assert.IsTrue(IECS.GetEntity(entity.Id, out Entity ship));
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.Id == entity.Id);
        }

        [TestMethod()]
        public void Test_03_RemoveEntity()
        {
            Test_00_InitializationIECS();

            var entity = IECS.AddEntity(new Ship());
            var entity1 = IECS.AddEntity(new Ship());
            var entity2 = IECS.AddEntity(new Ship());

            IECS.RemoveEntity(entity.Id);
            IECS.RemoveEntity(entity2.Id);
            Assert.IsFalse(IECS.GetEntity(entity.Id, out Entity entity5));
            Assert.IsNull(entity5);
            Assert.AreEqual(1, IECSDebug.ManagerEntitys.CountEntitys);
        }

        [TestMethod()]
        public void Test_04_AddComponent()
        {
            Test_00_InitializationIECS();

            var entity1 = IECS.AddEntity(new Ship());
            var entity2 = IECS.AddEntity(new Ship());
            entity1.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });
            Assert.AreEqual(1, entity1.Components.Count);
            Assert.ThrowsException<EntityAlreadyHaveComponentException>(() => entity1.AddComponent(new Pozition() { X = 1, Y = 1, Z = 1 }));
            Assert.AreEqual(1, entity1.Components.Count);
            IECS.AddComponent(new Pozition() { X = 10, Y = 10, Z = 10, Id = entity2.Id });
            Assert.IsTrue(IECS.GetEntity(entity2.Id, out Entity entity));
            Assert.AreEqual(1, entity.Components.Count);
        }

        [TestMethod()]
        public void Test_05_GetComponent()
        {
            Test_00_InitializationIECS();

            var entity1 = IECS.AddEntity(new Ship());
            entity1.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });

            var entity2 = IECS.AddEntity(new Ship());

            var entity3 = IECS.AddEntity(new Ship());
            entity3.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });

            Assert.IsTrue(entity1.TryGetComponent(out Pozition pozition1));
            Assert.IsNotNull(pozition1);

            Assert.IsFalse(entity2.TryGetComponent(out Pozition pozition2));
            Assert.IsNull(pozition2);

            Assert.IsTrue(entity3.TryGetComponent(out Pozition pozition3));
            Assert.IsNotNull(pozition3);

            Assert.IsTrue(IECS.GetComponent(entity1.Id, out pozition1));
            Assert.IsNotNull(pozition1);

            Assert.IsFalse(IECS.GetComponent(entity2.Id, out pozition2));
            Assert.IsNull(pozition2);

            Assert.IsTrue(IECS.GetComponent(entity3.Id, out pozition3));
            Assert.IsNotNull(pozition3);
        }

        [TestMethod()]
        public void Test_06_RemoveComponent()
        {
            Test_00_InitializationIECS();

            var entity1 = IECS.AddEntity(new Ship());
            entity1.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });

            var entity2 = IECS.AddEntity(new Ship());
            entity2.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });

            var entity3 = IECS.AddEntity(new Ship());
            entity3.AddComponent(new Pozition() { X = 0, Y = 0, Z = 0 });

            entity1.RemoveComponent<Pozition>();
            Assert.AreEqual(0, entity1.Components.Count);

            IECS.RemoveComponent<Pozition>(entity3.Id);
            Assert.AreEqual(0, entity3.Components.Count);

            Assert.IsFalse(IECS.GetComponent(entity1.Id, out Pozition pozition1));
            Assert.IsTrue(IECS.GetComponent(entity2.Id, out Pozition pozition2));
            Assert.IsFalse(IECS.GetComponent(entity3.Id, out Pozition pozition3));
            Assert.IsNull(pozition1);
            Assert.IsNotNull(pozition2);
            Assert.IsNull(pozition3);
        }

        [TestMethod()]
        public void Test_07_AddComponentToFilter()
        {
            Test_00_InitializationIECS();

            var entity1 = IECS.AddEntity(new Ship());
            entity1.AddComponent(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.AreEqual(1, entity1.Components.Count);

            entity1.AddComponent(new Speed() { dX = 1, dY = 5, dZ = 8 });
            Assert.AreEqual(2, entity1.Components.Count);

            var entity2 = IECS.AddEntity(new Ship());
            entity2.AddComponent(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.AreEqual(1, entity2.Components.Count);

            entity2.AddComponent(new Speed() { dX = 1, dY = 5, dZ = 8 });
            Assert.AreEqual(2, entity2.Components.Count);

            Thread.Sleep(2500);
            Assert.AreEqual(2, IECSDebug.ManagerFilters.CountEntitys);
        }

        [TestMethod()]
        public void Test_09_RemoveComponentFromFilter()
        {
            Test_00_InitializationIECS();

            var entity1 = IECS.AddEntity(new Ship());
            entity1.AddComponent(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.AreEqual(1, entity1.Components.Count);

            entity1.AddComponent(new Speed() { dX = 1, dY = 5, dZ = 8 });
            Assert.AreEqual(2, entity1.Components.Count);

            var entity2 = IECS.AddEntity(new Ship());
            entity2.AddComponent(new Pozition() { X = 1, Y = 2, Z = 3 });
            Assert.AreEqual(1, entity2.Components.Count);

            entity2.AddComponent(new Speed() { dX = 1, dY = 5, dZ = 8 });
            Assert.AreEqual(2, entity2.Components.Count);

            entity1.RemoveComponent<Speed>();
            Assert.AreEqual(1, entity1.Components.Count);

            Thread.Sleep(2500);
            Assert.AreEqual(1, IECSDebug.ManagerFilters.CountEntitys);
        }

        [TestMethod()]
        public void Test_10_ChildEntity()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            int startCountEntitys = IECSDebug.ManagerEntitys.CountEntitys;

            Entity ship = IECS.AddEntity(new Ship());
            Assert.AreEqual(startCountEntitys + 1, IECSDebug.ManagerEntitys.CountEntitys);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.AreEqual(null, ship.ExternalEntity);
            Assert.AreEqual(0, ship.NestedEntities.Count);

            Entity shipChild = (Entity)ship.AddNestedEntity(new Ship());
            Assert.AreEqual(startCountEntitys + 2, IECSDebug.ManagerEntitys.CountEntitys);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsNull(ship.ExternalEntity);
            Assert.AreEqual(1, ship.NestedEntities.Count);
            Assert.IsTrue(ship.TryGetNestedEntity(shipChild.Id, out IEntity _));
            Assert.IsNotNull(shipChild.ExternalEntity);
            Assert.AreEqual(ship.Id, shipChild.ExternalEntity.Id);
            Assert.AreEqual(0, shipChild.NestedEntities.Count);

            Entity shipChild1 = (Entity)ship.AddNestedEntity(new Ship());
            Assert.AreEqual(startCountEntitys + 3, IECSDebug.ManagerEntitys.CountEntitys);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsNull(ship.ExternalEntity);
            Assert.AreEqual(2, ship.NestedEntities.Count);
            Assert.IsTrue(ship.TryGetNestedEntity(shipChild.Id, out IEntity _));
            Assert.IsTrue(ship.TryGetNestedEntity(shipChild1.Id, out IEntity _));
            Assert.IsNotNull(shipChild.ExternalEntity);
            Assert.AreEqual(ship.Id, shipChild.ExternalEntity.Id);
            Assert.AreEqual(0, shipChild.NestedEntities.Count);
            Assert.IsNotNull(shipChild1.ExternalEntity);
            Assert.AreEqual(ship.Id, shipChild1.ExternalEntity.Id);
            Assert.AreEqual(0, shipChild1.NestedEntities.Count);

            Assert.IsTrue(ship.RemoveNestedEntity(shipChild1.Id, out IEntity entity));
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 3);
            Assert.IsTrue(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsTrue(ship.ExternalEntity == null);
            Assert.IsTrue(ship.NestedEntities.Count == 1);
            Assert.IsTrue(ship.TryGetNestedEntity(shipChild.Id, out IEntity _));
            Assert.IsFalse(ship.TryGetNestedEntity(shipChild1.Id, out IEntity _));
            Assert.IsTrue(shipChild.ExternalEntity != null);
            Assert.IsTrue(shipChild.ExternalEntity.Id == ship.Id);
            Assert.IsTrue(shipChild.NestedEntities.Count == 0);
            Assert.IsTrue(shipChild1.ExternalEntity == null);
            Assert.IsTrue(shipChild1.NestedEntities.Count == 0);

            ship.Death();
            Assert.IsTrue(IECSDebug.ManagerEntitys.CountEntitys == startCountEntitys + 2);
            Assert.IsFalse(IECS.GetEntity(ship.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild.Id, out _));
            Assert.IsTrue(IECS.GetEntity(shipChild1.Id, out _));
            Assert.IsTrue(ship.ExternalEntity == null);
            Assert.IsTrue(ship.NestedEntities.Count == 0);
            Assert.IsFalse(ship.TryGetNestedEntity(shipChild.Id, out IEntity _));
            Assert.IsFalse(ship.TryGetNestedEntity(shipChild1.Id, out IEntity _));
            Assert.IsTrue(shipChild.ExternalEntity == null);
            Assert.IsTrue(shipChild.NestedEntities.Count == 0);
            Assert.IsTrue(shipChild1.ExternalEntity == null);
            Assert.IsTrue(shipChild1.NestedEntities.Count == 0);
        }

        [TestMethod()]
        public void Test_11_ExcludeFilterCallForAddInclude()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            Entity entity = IECS.AddEntity(new Ship());
            entity.AddComponent(new Include());

            Thread.Sleep(50);
            if(entity.TryGetComponent(out Include include))
            {
                if (!include.CallOfAdd)
                {
                    Assert.Fail("Can not call ActionAdd of system");
                }
            }

            Thread.Sleep(100);
            Assert.AreEqual(1, include.CallOfAddCount, "ActionAdd of system not actual count call");
          
            entity.RemoveComponent<Include>();

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
            entity.AddComponent(new Exclude());
            entity.AddComponent(new Include());

            Thread.Sleep(50);
            if (entity.TryGetComponent(out Include include))
            {
                if (include.CallOfAdd)
                {
                    Assert.Fail("ActionAdd of system is call");
                }
            }

            entity.RemoveComponent<Exclude>();

            Thread.Sleep(50);
            if (entity.TryGetComponent(out Include include1))
            {
                if (!include1.CallOfAdd)
                {
                    Assert.Fail("Can not call ActionAdd of system");
                }
            }

            Thread.Sleep(100);
            if (entity.TryGetComponent(out Include include2))
            {
                Assert.AreEqual(1, include2.CallOfAddCount, "ActionAdd of system not actual count call");
            }

            entity.AddComponent(new Exclude());
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
        public void Test_13_Pause()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            IECS.Pause();

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new Pozition());
            ship.AddComponent(new Speed() { dX =1});

            ship.TryGetComponent<Pozition>(out var poz);

            var sec = 0;
            var timeSec = 10;
            while (true)
            {
                if (sec > timeSec)
                {
                    break;
                }
                Debug.WriteLine(poz.X);
                Thread.Sleep(1000);
                sec++;
            }

            Assert.AreEqual(0, poz.X);
        }

        [TestMethod()]
        public void Test_14_Run()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            IECS.Pause();

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new Pozition());
            ship.AddComponent(new Speed() { dX = 1 });

            ship.TryGetComponent<Pozition>(out var poz);

            var sec = 0;
            var timeSec = 60;

            IECS.Run();

            while (true)
            {
                if (sec > timeSec)
                {
                    IECS.Pause();
                    break;
                }
                Debug.WriteLine(poz.X);
                Thread.Sleep(1000);
                sec++;
            }

            Assert.IsTrue(poz.X < 62);
            Assert.IsTrue(poz.X > 58);
        }

        [TestMethod()]
        public void Test_15_Speed_X0_5()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            IECS.Pause();

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new Pozition());
            ship.AddComponent(new Speed() { dX = 1 });

            ship.TryGetComponent<Pozition>(out var poz);

            var sec = 0;
            var timeSec = 60;

            IECS.Run();
            IECS.SetSpeed(ECSCore.Enums.ECSSpeed.X_0_5);

            while (true)
            {
                if (sec > timeSec)
                {
                    IECS.Pause();
                    break;
                }
                Debug.WriteLine(poz.X);
                Thread.Sleep(1000);
                sec++;
            }

            Assert.IsTrue(poz.X > 28);
            Assert.IsTrue(poz.X < 32);
        }

        [TestMethod()]
        public void Test_15_Speed_X2()
        {
            IECS?.Despose();
            Test_00_InitializationIECS();

            IECS.Pause();

            var ship = IECS.AddEntity(new Ship());
            ship.AddComponent(new Pozition());
            ship.AddComponent(new Speed() { dX = 1 });

            ship.TryGetComponent<Pozition>(out var poz);

            var sec = 0;
            var timeSec = 60;

            IECS.Run();
            IECS.SetSpeed(ECSCore.Enums.ECSSpeed.X_2_0);

            while (true)
            {
                if (sec > timeSec)
                {
                    IECS.Pause();
                    break;
                }
                Debug.WriteLine(poz.X);
                Thread.Sleep(1000);
                sec++;
            }

            Assert.IsTrue(poz.X > 118);
            Assert.IsTrue(poz.X < 124);
        }
    }
}