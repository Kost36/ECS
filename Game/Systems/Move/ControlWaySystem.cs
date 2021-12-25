using ECSCore.Attributes;
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
    public class ControlWaySystem : SystemBase
    {
        public FilterControlWay Filter;

        public override void Initialization()
        {
            Filter = (FilterControlWay)ManagerFilters.GetFilter<FilterControlWay>();
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
                //Рассчет направления
                Filter.ComponentsT2[i].LenX = Filter.ComponentsT0[i].X - Filter.ComponentsT1[i].X;
                Filter.ComponentsT2[i].LenY = Filter.ComponentsT0[i].Y - Filter.ComponentsT1[i].Y;
                Filter.ComponentsT2[i].LenZ = Filter.ComponentsT0[i].Z - Filter.ComponentsT1[i].Z;
                //Рассчет расстояния до заданной точки 
                Filter.ComponentsT2[i].Len = MathF.Sqrt(
                    MathF.Pow(Filter.ComponentsT2[i].LenX, 2) + 
                    MathF.Pow(Filter.ComponentsT2[i].LenY, 2) + 
                    MathF.Pow(Filter.ComponentsT2[i].LenZ, 2));
                //Нормализация вектора направления
                Filter.ComponentsT2[i].NormX = Filter.ComponentsT2[i].LenX / Filter.ComponentsT2[i].Len;
                Filter.ComponentsT2[i].NormY = Filter.ComponentsT2[i].LenY / Filter.ComponentsT2[i].Len;
                Filter.ComponentsT2[i].NormZ = Filter.ComponentsT2[i].LenZ / Filter.ComponentsT2[i].Len;

                if (ECS.GetComponent<Speed>(idEntity) == null)
                {
                    ECS.AddComponent<Speed>(new Speed() { SpeedMax = 10, Id = idEntity });
                } //Если скорости нету
                if (ECS.GetComponent<SpeedSV>(idEntity) == null)
                {
                    ECS.AddComponent<SpeedSV>(new SpeedSV() { Id = idEntity });
                } //Если задания скорости нету
            }
        }
    }
}