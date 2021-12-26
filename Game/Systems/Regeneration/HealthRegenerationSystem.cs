using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.ObjectStates;
using Game.Components.Propertys;
using Game.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Systems.Regeneration
{
    //[AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    //[AttributeSystemPriority(1)]
    //public class HealthRegenerationSystem : SystemBase
    //{
    //    public FilterHealthRegeneration Filter;

    //    public override void Initialization()
    //    {
    //        Filter = (FilterHealthRegeneration)ManagerFilters.GetFilter<FilterHealthRegeneration>();
    //    }
    //    public override void PreAсtion()
    //    {
    //        Filter.Сalculate();
    //    }
    //    public override void Aсtion()
    //    {
    //        foreach (Health health in Filter.ComponentsT0.Values)
    //        {
    //            AсtionUser(health, Filter.ComponentsT1[health.Id], Filter.ComponentsT2[health.Id]);
    //        }
    //    }

    //    public void AсtionUser(Health health, HealthReGeneration healthReGeneration, Enargy enargy)
    //    {
    //        if (health.HealthFact < health.HealthMax)
    //        {
    //            if (enargy.EnargyFact > healthReGeneration.EnargyUse)
    //            {
    //                health.HealthFact = health.HealthFact + healthReGeneration.HealthReGen;
    //                enargy.EnargyFact = enargy.EnargyFact - healthReGeneration.EnargyUse;
    //                if (health.HealthFact > health.HealthMax)
    //                {
    //                    health.HealthFact = health.HealthMax;
    //                    ECS.RemoveComponent<HealthReGeneration>(health.Id, null);
    //                }
    //            }
    //        }
    //    }
    //}
}