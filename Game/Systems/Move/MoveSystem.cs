using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Systems.Move
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec30Once)]
    [AttributeSystemPriority(1)]
    public class MovementSystem : SystemBase
    {
        public FilterMove Filter;

        public override void Initialization()
        {
            Filter = (FilterMove)ManagerFilters.GetFilter<FilterMove>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            return;
            for (int i = 0; i < Filter.Count; i++)
            {
                Filter.ComponentsT0[i].X = Filter.ComponentsT0[i].X + Filter.ComponentsT1[i].dX;
                Filter.ComponentsT0[i].Y = Filter.ComponentsT0[i].Y + Filter.ComponentsT1[i].dY;
                Filter.ComponentsT0[i].Z = Filter.ComponentsT0[i].Z + Filter.ComponentsT1[i].dZ;
            }
        }
    }
}