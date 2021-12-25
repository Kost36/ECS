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
    public class ShildRegenerationSystem : SystemBase
    {
        public FilterShildRegeneration Filter;

        public override void Initialization()
        {
            Filter = (FilterShildRegeneration)ManagerFilters.GetFilter<FilterShildRegeneration>();
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
                if (Filter.ComponentsT0[i].ShildFact < Filter.ComponentsT0[i].ShildMax)
                {
                    if (Filter.ComponentsT2[i].EnargyFact > Filter.ComponentsT1[i].EnargyUse)
                    {
                        Filter.ComponentsT0[i].ShildFact = Filter.ComponentsT0[i].ShildFact + Filter.ComponentsT1[i].ShildReGen;
                        Filter.ComponentsT2[i].EnargyFact = Filter.ComponentsT2[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse; 
                        if (Filter.ComponentsT0[i].ShildFact > Filter.ComponentsT0[i].ShildMax)
                        {
                            Filter.ComponentsT0[i].ShildFact = Filter.ComponentsT0[i].ShildMax;
                            ECS.RemoveComponent<ShildReGeneration>(entityId);
                        }
                    }
                }
            }
        }
    }
}