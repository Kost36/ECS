using ECSCore;
using ECSCore.Interfaces.ECS;
using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace GameLibTests
{
    public abstract class BaseTest
    {
        public static IECS IECS { get; set; }
        public static IECSDebug IECSDebug { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ECS.Initialization(GetAssembly.Get());

            IECS = ECS.InstanceIECS;
            IECSDebug = ECS.InstanceDebug;

            Assert.IsNotNull(IECS);

            Debug.WriteLine("Initialize OK");
            Debug.WriteLine(IECSDebug.GetInfo());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            IECS.Despose();
            Debug.WriteLine("Cleanup OK");
        }
    }
}
