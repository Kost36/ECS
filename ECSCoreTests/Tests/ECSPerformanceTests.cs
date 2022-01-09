using ECSCore;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using ECSCoreTests.Components;
using ECSCoreTests.Entitys;
using ECSCoreTests.Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECSCoreTests
{
    [TestClass()]
    public class ECS_02Tests_Performance
    {
        private static IECS IECS;
        private static IECSDebug IECSDebug;
        [TestMethod()]
        public void Test_00_InitializationIECS()
        {
            Ship ship = new();
            ECS.Initialization(ship.GetType().Assembly);
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;
            Debug.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void Test_02_MechanicMove()
        {
            int entityCount = 180000;// 180000 - ПК //280000 - Рабочий ноут
            int j = 0;
            while (j < entityCount)
            {
                for (int i = 0; i < 10000; i++)
                {
                    Entity ship = IECS.AddEntity(new Ship());
                    ship.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
                    ship.Add(new PozitionSV() { X = 10000, Y = 10000, Z = 10000 });
                    ship.Add(new Enargy() { EnargyFact = 100, EnargyMax = 5000 });
                    j++;
                }


                while (true)
                {
                    Thread.Sleep(1000);
                    Debug.WriteLine(IECSDebug.GetInfo(true));
                    if (IECSDebug.ManagerSystems.GetSystem(out ControlSpeedSystemRemove controlSpeedSystemRemove))
                    {
                        if(controlSpeedSystemRemove.GetFilterCount() == IECSDebug.ManagerEntitys.CountEntitys)
                        {
                            break;
                        }
                    }
                }


                if (j % 10000 == 0)
                {
                    IECSDebug.ManagerSystems.ClearStatisticSystems();
                }
            }
            IECSDebug.ManagerSystems.ClearStatisticSystems();

            int k = 0;
            while (k<10)
            {
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));
                k++;
            }
            IECSDebug.ManagerSystems.ClearStatisticSystems();

            int l = 0;
            while (true)
            {
                l++;
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));

                int entityId = IECSDebug.ManagerEntitys.GetIdFirstEntity();


                if (l % 100 == 0)
                {
                    IECSDebug.ManagerSystems.ClearStatisticSystems();
                }

                if (IECS.GetEntity(entityId, out Entity entity))
                {
                    Debug.WriteLine($"Сущьность: {entityId}");
                    if (entity.Get(out Enargy enargy))
                    {
                        Debug.WriteLine($"Энергия: {enargy.EnargyFact}/{enargy.EnargyMax}");
                    }
                    if (entity.Get(out Pozition pozition))
                    {
                        Debug.WriteLine($"Позиция: {pozition.X}|{pozition.Y}|{pozition.Z}");
                    }
                    if (entity.Get(out Way way))
                    {
                        Debug.WriteLine($"Путь: {way.Len}");
                    }
                    if (entity.Get(out WayToStop wayToStop))
                    {
                        Debug.WriteLine($"Путь останова: {wayToStop.Len}; Энергии достаточно для полного останова: {wayToStop.EnargyHave}");
                    }
                    if (entity.Get(out Speed speed))
                    {
                        if (entity.Get(out Acceleration _))
                        {
                            Debug.WriteLine($"Скорость: {speed.dX}|{speed.dY}|{speed.dZ}; {speed.SpeedFact}/{speed.SpeedMax}; Ускорение/Замедление: True");
                        }
                        else
                        {
                            Debug.WriteLine($"Скорость: {speed.dX}|{speed.dY}|{speed.dZ}; {speed.SpeedFact}/{speed.SpeedMax}");
                        }
                    }
                    if (entity.Get(out SpeedSV speedSV))
                    {
                        Debug.WriteLine($"Скорость заданная: {speedSV.SVSpeed}; Изменение заданной скорости: {speedSV.Update}");
                    }
                    Debug.WriteLine("");
                }

                if (IECSDebug.ManagerEntitys.CountEntitys == 0)
                {
                    break;
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void Test_03_MechanicMove()
        {
            IECS.Despose();
            Thread.Sleep(500);
            Test_00_InitializationIECS();
            int i = 0;
            while (i<10)
            {
                Test_02_MechanicMove();
            }
        }
    }
}

//Редко - не добавилось ускорение на сущьность 