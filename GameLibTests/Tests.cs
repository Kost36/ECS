using ECSCore;
using ECSCore.Interfaces.ECS;
using GameLib.Entitys.StaticEntitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace GameLib.Tests
{
    [TestClass()]
    public class Tests
    {
        private static IECS IECS;
        private static IECSDebug IECSDebug;

        [TestMethod()]
        public void Test01()
        {
            ECS.Initialization(GetAssembly.Get());
            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;

            Console.WriteLine("ОК");
            Assert.IsNotNull(IECS);
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestMethod()]
        public void Test02()
        {
            Star star = (Star)IECS.AddEntity(new Star());
            //star.Add()
        }
    }
}