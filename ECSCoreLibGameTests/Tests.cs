using ECSCore;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using GameLib.Components.WorkFlow;
using GameLib.Entitys.StaticEntitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

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
            Stantion stantion = (Stantion)IECS.AddEntity(new Stantion());
            stantion.Add(new ProdactionMetal() {CountEnargyRaw = 100, CountOreRaw = 100, CountMetalProd = 50, Performance = 1 });
            stantion.Add(new Enargy() { Count = 1000 });
            stantion.Add(new Ore() { Count = 1000 });
            stantion.Add(new Metal() { Count = 0 });

            while (true)
            {
                Thread.Sleep(1000);
                Debug.WriteLine(IECSDebug.GetInfo(true));

                int entityId = IECSDebug.ManagerEntitys.GetIdFirstEntity();

                if (IECS.GetEntity(entityId, out Entity entity))
                {
                    Debug.WriteLine($"Сущьность: {entityId}");
                    if (entity.Get(out ProdactionMetal prodactionMetal))
                    {
                        Debug.WriteLine($"Производство металла:");
                        Debug.WriteLine($"Производительность: { prodactionMetal.Performance}");
                        Debug.WriteLine($"Процент: { prodactionMetal.Percent}");
                    }
                    if (entity.Get(out Enargy enargy))
                    {
                        Debug.WriteLine($"Энергия: {enargy.Count}");
                    }
                    if (entity.Get(out Ore ore))
                    {
                        Debug.WriteLine($"Руда: {ore.Count}");
                    }
                    if (entity.Get(out Metal metal))
                    {
                        Debug.WriteLine($"Метал: {metal.Count}");
                    }
                    Debug.WriteLine("");
                }
            }
        }
    }
}