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
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    public class EnargyRegenerationSystem : SystemBase
    {
        public FilterEnargyRegeneration Filter;

        public override void Initialization()
        {
            Filter = (FilterEnargyRegeneration)ManagerFilters.GetFilter<FilterEnargyRegeneration>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            foreach (Enargy enargy in Filter.ComponentsT0.Values)
            {
                AсtionUser(enargy, Filter.ComponentsT1[enargy.Id]);
            }
        }

        public void AсtionUser(Enargy enargy, EnargyReGeneration enargyReGeneration)
        {
            if (enargy.EnargyFact < enargy.EnargyMax)
            {
                enargy.EnargyFact = enargy.EnargyFact + enargyReGeneration.EnargyReGen;
                if (enargy.EnargyFact > enargy.EnargyMax)
                {
                    enargy.EnargyFact = enargy.EnargyMax;
                    ECS.RemoveComponent<EnargyReGeneration>(enargy.Id, null);
                }
            }
        }
    }
}