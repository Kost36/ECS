using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.Propertys;
using Game.Components.Tasks;
using Game.Filters;
using System;

namespace Game.Systems.Move
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    public class AccselerateSystem : SystemBase
    {
        public FilterAccelerate Filter;

        public override void Initialization()
        {
            Filter = (FilterAccelerate)ManagerFilters.GetFilter<FilterAccelerate>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            for (int i = 0; i < Filter.Count; i++)
            {
                if( Filter.ComponentsT3[i].EnargyFact> Filter.ComponentsT1[i].EnargyUse)
                {
                    //Ускорение
                    if (Filter.ComponentsT0[i].dXSV < Filter.ComponentsT2[i].dX)
                    {
                        Filter.ComponentsT2[i].dX = Filter.ComponentsT2[i].dX - Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dXSV > Filter.ComponentsT2[i].dX)
                        {
                            Filter.ComponentsT2[i].dX = Filter.ComponentsT0[i].dXSV;
                        }
                    }
                    else if (Filter.ComponentsT0[i].dXSV > Filter.ComponentsT2[i].dX)
                    {
                        Filter.ComponentsT2[i].dX = Filter.ComponentsT2[i].dX + Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dXSV < Filter.ComponentsT2[i].dX)
                        {
                            Filter.ComponentsT2[i].dX = Filter.ComponentsT0[i].dXSV;
                        }
                    }
                    if (Filter.ComponentsT0[i].dYSV < Filter.ComponentsT2[i].dY)
                    {
                        Filter.ComponentsT2[i].dY = Filter.ComponentsT2[i].dY - Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dYSV > Filter.ComponentsT2[i].dY)
                        {
                            Filter.ComponentsT2[i].dY = Filter.ComponentsT0[i].dYSV;
                        }
                    }
                    else if (Filter.ComponentsT0[i].dYSV > Filter.ComponentsT2[i].dY)
                    {
                        Filter.ComponentsT2[i].dY = Filter.ComponentsT2[i].dY + Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dYSV < Filter.ComponentsT2[i].dY)
                        {
                            Filter.ComponentsT2[i].dY = Filter.ComponentsT0[i].dYSV;
                        }
                    }
                    if (Filter.ComponentsT0[i].dZSV < Filter.ComponentsT2[i].dZ)
                    {
                        Filter.ComponentsT2[i].dZ = Filter.ComponentsT2[i].dZ - Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dZSV > Filter.ComponentsT2[i].dZ)
                        {
                            Filter.ComponentsT2[i].dZ = Filter.ComponentsT0[i].dZSV;
                        }
                    }
                    else if (Filter.ComponentsT0[i].dZSV > Filter.ComponentsT2[i].dZ)
                    {
                        Filter.ComponentsT2[i].dZ = Filter.ComponentsT2[i].dZ + Filter.ComponentsT1[i].Acc;
                        Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
                        if (Filter.ComponentsT0[i].dZSV < Filter.ComponentsT2[i].dZ)
                        {
                            Filter.ComponentsT2[i].dZ = Filter.ComponentsT0[i].dZSV;
                        }
                    }
                }

                //Проверка достижения максимальной скорости
                if (Filter.ComponentsT2[i].dX == Filter.ComponentsT0[i].dXSV)
                {
                    if (Filter.ComponentsT2[i].dY == Filter.ComponentsT0[i].dXSV)
                    {
                        if (Filter.ComponentsT2[i].dZ == Filter.ComponentsT0[i].dXSV)
                        {
                            ECSCore.ECS.Instance.RemoveComponent<Acceleration>(Filter.ComponentsT0[i].Id); //Удалить компонент ускорения
                        }
                    }
                }
            }
        }
    }
}