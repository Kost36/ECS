﻿using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.Propertys;
using Game.Components.Tasks;
using Game.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Systems.Move
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    public class ControlAddWaySystem : SystemBase
    {
        public FilterAddWay Filter;

        public override void Initialization()
        {
            Filter = (FilterAddWay)ManagerFilters.GetFilter<FilterAddWay>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            for (int i = 0; i < Filter.Count; i++)
            {
                int idEntity = Filter.ComponentsT0[i].Id;
                if (ECS.GetComponent<Way>(idEntity) == null)
                {
                    ECS.AddComponent<Way>(new Way() { Id = idEntity });
                } //Если нету компонента путь
            }
        }
    }
}