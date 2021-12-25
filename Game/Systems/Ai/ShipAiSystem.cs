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
    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(5)]
    public class ShipAiSystem : SystemBase
    {
        public FilterShipAi Filter;
        public override void Initialization()
        {
            Filter = (FilterShipAi)ManagerFilters.GetFilter<FilterShipAi>();
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
                //Если энергии мало, и нету регенерации. => Добавить компонент

                //Если жизни не полные и нету регенерации => добавить компонент

                //Если щиты не полные и нету регенерации => добавить компонент

                //Если есть задание на полет и нету way => добавить компонент

                //Если состояние торговли и нету компонента торговли => добавить компонент
            }
        }
    }
}