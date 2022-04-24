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

namespace ECSCoreLibTests.Tests.ECSCoreLibTests
{
    [TestClass()]
    public class ECSLibLongTests
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
        public void Test_01_FillingFilter()
        {
            IECS?.Despose();
            Thread.Sleep(500);
            Test_00_InitializationIECS();

            int entityCount = 100000;
            int j = 0;
            while (j < entityCount)
            {
                for (int i = 0; i < 100; i++)
                {
                    Entity ship = IECS.AddEntity(new Ship());
                    ship.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
                    //Thread.Sleep(1);
                    ship.Add(new PozitionSV() { X = 1000, Y = 1000, Z = 1000 });
                    //Thread.Sleep(1);
                    ship.Add(new Enargy() { EnargyFact = 100, EnargyMax = 1000 });
                    j++;
                }

                int count = 0;
                int countWait = 100;
                while (true)
                {
                    Thread.Sleep(10);
                    Debug.WriteLine(IECSDebug.GetInfo(true));
                    if (IECSDebug.ManagerSystems.GetSystem(out StartMoveSystem startMoveSystem))
                    {
                        if (startMoveSystem.GetFilterCount() == IECSDebug.ManagerEntitys.CountEntitys)
                        {

                        }
                        else
                        {
                            //continue;
                        }
                    }
                    if (IECSDebug.ManagerSystems.GetSystem(out ControlSpeedSystemRemove controlSpeedSystemRemove))
                    {
                        if (controlSpeedSystemRemove.GetFilterCount() == IECSDebug.ManagerEntitys.CountEntitys)
                        {
                            break;
                        }
                    }
                    count++;
                    if (count >= countWait)
                    {
                        Assert.Fail();
                    }
                }

                if (j % 10000 == 0)
                {
                    IECSDebug.ManagerSystems.ClearStatisticSystems();
                }
            }

            if (IECSDebug.ManagerSystems.GetSystem(out ControlSpeedSystemRemove controlSpeedSystemRemove1))
            {
                if (controlSpeedSystemRemove1.GetFilterCount() == IECSDebug.ManagerEntitys.CountEntitys)
                {
                    return;
                }
            }
            Assert.Fail();
        }

        //[TestMethod()]
        public void Test_02_SearchBug()
        {
            int i = 0;
            while (true)
            {
                Test_01_FillingFilter();
                i++;
                if (i > 100)
                {
                    return;
                }
            }
        }

        [TestMethod()]
        public void Test_03_ECSDespose()
        {
            IECS.Despose();
            Thread.Sleep(500);
            Assert.IsNull(ECS.Instance);
        }
    }
}