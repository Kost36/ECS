using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(EnargyReGeneration))]
    [AttributeSystemParallelCountThreads(8)]
    public class EnargyOnRegenerationSystem : SystemExistComponents<Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Enargy enargy, float deltatime)
        {
            if (enargy.Fact < enargy.Max * 0.95)
            {
                if (IECS.GetComponent(entityId, out EnargyReGeneration enargyReGeneration) == false)
                {
                    IECS.AddComponent(new EnargyReGeneration() { EnargyRegen = 5f, Id = entityId });
                }
            }
        }
    }
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class EnargyRegenerationSystem : SystemExistComponents<Enargy, EnargyReGeneration>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Enargy enargy, EnargyReGeneration enargyReGeneration, float deltatime)
        {
            if (enargy.Fact < enargy.Max)
            {
                enargy.Fact += enargyReGeneration.Regen * DeltaTime;
                if (enargy.Fact > enargy.Max)
                {
                    enargy.Fact = enargy.Max;
                    IECS.RemoveComponent<EnargyReGeneration>(entityId);
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(HealthReGeneration))]
    [AttributeSystemParallelCountThreads(8)]
    public class HealthOnRegenerationSystem : SystemExistComponents<Health>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Health health, float deltatime)
        {
            if (health.Fact < health.Max * 0.95)
            {
                if (IECS.GetComponent(entityId, out HealthReGeneration healthReGeneration) == false)
                {
                    IECS.AddComponent(new HealthReGeneration() { Regen = 1f, EnargyUse = 3, Id = entityId });
                }
            }
        }
    }
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class HealthRegenerationSystem : SystemExistComponents<Health, HealthReGeneration, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Health health, HealthReGeneration healthReGeneration, Enargy enargy, float deltatime)
        {
            if (health.Fact < health.Max)
            {
                float enargyUse = healthReGeneration.EnargyUse * DeltaTime;
                if (enargy.Fact > enargyUse)
                {
                    health.Fact += healthReGeneration.Regen * DeltaTime;
                    enargy.Fact -= enargyUse;
                    if (health.Fact > health.Max)
                    {
                        health.Fact = health.Max;
                        IECS.RemoveComponent<HealthReGeneration>(entityId);
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(ShildReGeneration))]
    [AttributeSystemParallelCountThreads(8)]
    public class ShildOnRegenerationSystem : SystemExistComponents<Shild>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Shild shild, float deltatime)
        {
            if (shild.Fact < shild.Max * 0.95)
            {
                if (IECS.GetComponent(entityId, out ShildReGeneration shildReGeneration) == false)
                {
                    IECS.AddComponent(new ShildReGeneration() { Regen = 1f, EnargyUse = 3, Id = entityId });
                }
            }
        }
    }
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class ShildRegenerationSystem : SystemExistComponents<Shild, ShildReGeneration, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Shild shild, ShildReGeneration shildReGeneration, Enargy enargy, float deltatime)
        {
            if (shild.Fact < shild.Max)
            {
                float enargyUse = shildReGeneration.EnargyUse * DeltaTime;
                if (enargy.Fact > enargyUse)
                {
                    shild.Fact += shildReGeneration.Regen * DeltaTime;
                    enargy.Fact -= enargyUse;
                    if (shild.Fact > shild.Max)
                    {
                        shild.Fact = shild.Max;
                        IECS.RemoveComponent<ShildReGeneration>(entityId);
                    }
                }
            }
        }
    }
}