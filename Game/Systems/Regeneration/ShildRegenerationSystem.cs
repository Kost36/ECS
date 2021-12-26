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
            foreach (Shild shild in Filter.ComponentsT0.Values)
            {
                AсtionUser(shild, Filter.ComponentsT1[shild.Id], Filter.ComponentsT2[shild.Id]);
            }
        }

        public void AсtionUser(Shild shild, ShildReGeneration shildReGeneration, Enargy enargy)
        {
            if (shild.ShildFact < shild.ShildMax)
            {
                if (enargy.EnargyFact > shildReGeneration.EnargyUse)
                {
                    shild.ShildFact = shild.ShildFact + shildReGeneration.ShildReGen;
                    enargy.EnargyFact = enargy.EnargyFact - shildReGeneration.EnargyUse;
                    if (shild.ShildFact > shild.ShildMax)
                    {
                        shild.ShildFact = shild.ShildMax;
                        ECS.RemoveComponent<ShildReGeneration>(shild.Id, null);
                    }
                }
            }
        }
    }
}