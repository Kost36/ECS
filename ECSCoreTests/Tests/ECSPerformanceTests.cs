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
        public static IECS IECS;
        public static IECSDebug IECSDebug;
        public static Entity _entity;
        [TestMethod()]
        public void Test_00_InitializationIECS()
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
        public void Test_02_MechanicMove()
        {
            int entityCount = 200000;//50000;
            int j = 0;
            while (j < entityCount)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Entity ship = IECS.AddEntity(new Ship());
                    ship.Add(new Pozition() { X = 0, Y = 0, Z = 0 });
                    ship.Add(new PozitionSV() { X = 10000, Y = 10000, Z = 10000 });
                    ship.Add(new Enargy() { EnargyFact = 100, EnargyMax = 1000 });
                    ship.Add(new EnargyReGeneration() { EnargyReGen = 5f });
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

            while (true)
            {
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));

                int entityId = IECSDebug.ManagerEntitys.GetIdFirstEntity();

                if (IECS.GetEntity(entityId, out Entity entity))
                {
                    Debug.WriteLine($"Сущьность: {entityId}");
                    if (entity.Get(out Pozition pozition))
                    {
                        Debug.WriteLine($"Позиция: {pozition.X},{pozition.Y},{pozition.Z}");
                    }
                    if (entity.Get(out Speed speed))
                    {
                        Debug.WriteLine($"Скорость: {speed.dX},{speed.dY},{speed.dZ}");
                    }
                    if (entity.Get(out Enargy enargy))
                    {
                        Debug.WriteLine($"Энергия: {enargy.EnargyFact}/{enargy.EnargyMax}");
                    }
                    if (entity.Get(out Way way))
                    {
                        Debug.WriteLine($"Путь: {way.Len}");
                    }
                    if (entity.Get(out WayToStop wayToStop))
                    {
                        Debug.WriteLine($"Путь останова: {wayToStop.Len}");
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

    }
}