using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.Propertys;
using Game.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Systems.Regeneration
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    public class HealthRegenerationSystem : SystemBase
    {
        public FilterHealthRegeneration Filter;

        public override void Initialization()
        {
            Filter = (FilterHealthRegeneration)ManagerFilters.GetFilter<FilterHealthRegeneration>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            for (int i = 0; i < Filter.Count; i++)
            {
                int entityId = Filter.ComponentsT0[i].Id;
                if (Filter.ComponentsT0[i].HealthFact < Filter.ComponentsT0[i].HealthMax)
                {
                    if (Filter.ComponentsT2[i].EnargyFact > Filter.ComponentsT1[i].EnargyUse)
                    {
                        Filter.ComponentsT0[i].HealthFact = Filter.ComponentsT0[i].HealthFact + Filter.ComponentsT1[i].HealthReGen;
                        Filter.ComponentsT2[i].EnargyFact = Filter.ComponentsT2[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].HealthFact > Filter.ComponentsT0[i].HealthMax)
                        {
                            Filter.ComponentsT0[i].HealthFact = Filter.ComponentsT0[i].HealthMax;
                            ECS.RemoveComponent<HealthReGeneration>(entityId);
                        }
                    }
                }
            }
        }
    }
}