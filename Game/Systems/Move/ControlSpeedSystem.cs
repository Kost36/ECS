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
    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(1)]
    public class ControlSpeedSystem : SystemBase
    {
        public FilterControlSpeed Filter;

        public override void Initialization()
        {
            Filter = (FilterControlSpeed)ManagerFilters.GetFilter<FilterControlSpeed>();
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
                if (Filter.ComponentsT0[i].Len < Filter.ComponentsT1[i].FactSpeed * 300)
                {
                    if (Filter.ComponentsT1[i].FactSpeed < Filter.ComponentsT2[i].NeedSpeed)
                    {
                        Filter.ComponentsT2[i].NeedSpeed = Filter.ComponentsT1[i].FactSpeed;
                    }
                    Filter.ComponentsT2[i].NeedSpeed = Filter.ComponentsT2[i].NeedSpeed - (float)(Filter.ComponentsT1[i].MaxSpeed * 0.01); //Снижаем на 10% от максимальной скорости
                    if (ECS.GetComponent<Acceleration>(idEntity) == null)
                    {
                        ECS.AddComponent<Acceleration>(new Acceleration() { Id = idEntity });
                    } //Если нету ускорения
                    if (Filter.ComponentsT2[i].NeedSpeed < 0)
                    {
                        Filter.ComponentsT2[i].NeedSpeed = 0;
                    }
                } //Если время оставшегося пути меньше 5 минут
                if (Filter.ComponentsT0[i].Len > Filter.ComponentsT1[i].FactSpeed * 300)
                {
                    if (Filter.ComponentsT1[i].FactSpeed > Filter.ComponentsT2[i].NeedSpeed)
                    {
                        Filter.ComponentsT2[i].NeedSpeed = Filter.ComponentsT1[i].FactSpeed;
                    }
                    Filter.ComponentsT2[i].NeedSpeed = Filter.ComponentsT2[i].NeedSpeed + (float)(Filter.ComponentsT1[i].MaxSpeed * 0.01); //Снижаем на 1% от максимальной скорости
                    if (ECS.GetComponent<Acceleration>(idEntity) == null)
                    {
                        ECS.AddComponent<Acceleration>(new Acceleration() { Id = idEntity });
                    } //Если нету ускорения
                    if (Filter.ComponentsT2[i].NeedSpeed > Filter.ComponentsT1[i].MaxSpeed)
                    {
                        Filter.ComponentsT2[i].NeedSpeed = Filter.ComponentsT1[i].MaxSpeed;
                    }
                } //Если время оставшегося пути больше 5 минут
                //Рассчет необходимой скорости в направлении
                Filter.ComponentsT2[i].dXSV = Filter.ComponentsT0[i].NormX * Filter.ComponentsT2[i].NeedSpeed;
                Filter.ComponentsT2[i].dYSV = Filter.ComponentsT0[i].NormY * Filter.ComponentsT2[i].NeedSpeed;
                Filter.ComponentsT2[i].dZSV = Filter.ComponentsT0[i].NormZ * Filter.ComponentsT2[i].NeedSpeed;
                if (Filter.ComponentsT0[i].Len < 1)
                {
                    ECS.RemoveComponent<Speed>(Filter.ComponentsT0[i].Id); //Удалим скорость
                    ECS.RemoveComponent<SpeedSV>(Filter.ComponentsT0[i].Id); //Удалим заданную скорость
                    ECS.RemoveComponent<Acceleration>(Filter.ComponentsT0[i].Id); //Удалим ускорение
                    ECS.RemoveComponent<Way>(Filter.ComponentsT0[i].Id); //Удалим путь
                }
            }
        }
    }
}
