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
            return;
            for (int i = 0; i < Filter.Count; i++)
            {
                int idEntity = Filter.ComponentsT0[i].Id;
                if (Filter.ComponentsT0[i].Len < Filter.ComponentsT1[i].SpeedFact * 300)
                {
                    if (Filter.ComponentsT1[i].SpeedFact < Filter.ComponentsT2[i].SVSpeed)
                    {
                        Filter.ComponentsT2[i].SVSpeed = Filter.ComponentsT1[i].SpeedFact;
                    }
                    Filter.ComponentsT2[i].SVSpeed = Filter.ComponentsT2[i].SVSpeed - (float)(Filter.ComponentsT1[i].SpeedMax * 0.01); //Снижаем на 10% от максимальной скорости
                    if (ECS.GetComponent(idEntity, out Acceleration acceleration) == false)
                    {
                        ECS.AddComponent(new Acceleration() { Id = idEntity }, null);
                    } //Если нету ускорения
                    if (Filter.ComponentsT2[i].SVSpeed < 0)
                    {
                        Filter.ComponentsT2[i].SVSpeed = 0;
                    }
                } //Если время оставшегося пути меньше 5 минут
                if (Filter.ComponentsT0[i].Len > Filter.ComponentsT1[i].SpeedFact * 300)
                {
                    if (Filter.ComponentsT1[i].SpeedFact > Filter.ComponentsT2[i].SVSpeed)
                    {
                        Filter.ComponentsT2[i].SVSpeed = Filter.ComponentsT1[i].SpeedFact;
                    }
                    Filter.ComponentsT2[i].SVSpeed = Filter.ComponentsT2[i].SVSpeed + (float)(Filter.ComponentsT1[i].SpeedMax * 0.01); //Снижаем на 1% от максимальной скорости
                    if (ECS.GetComponent(idEntity, out Acceleration acceleration) == false)
                    {
                        ECS.AddComponent(new Acceleration() { Id = idEntity }, null);
                    } //Если нету ускорения
                    if (Filter.ComponentsT2[i].SVSpeed > Filter.ComponentsT1[i].SpeedMax)
                    {
                        Filter.ComponentsT2[i].SVSpeed = Filter.ComponentsT1[i].SpeedMax;
                    }
                } //Если время оставшегося пути больше 5 минут
                //Рассчет необходимой скорости в направлении
                Filter.ComponentsT2[i].dXSV = Filter.ComponentsT0[i].NormX * Filter.ComponentsT2[i].SVSpeed;
                Filter.ComponentsT2[i].dYSV = Filter.ComponentsT0[i].NormY * Filter.ComponentsT2[i].SVSpeed;
                Filter.ComponentsT2[i].dZSV = Filter.ComponentsT0[i].NormZ * Filter.ComponentsT2[i].SVSpeed;
                if (Filter.ComponentsT0[i].Len < 1)
                {
                    ECS.RemoveComponent<Speed>(Filter.ComponentsT0[i].Id, null); //Удалим скорость
                    ECS.RemoveComponent<SpeedSV>(Filter.ComponentsT0[i].Id, null); //Удалим заданную скорость
                    ECS.RemoveComponent<Acceleration>(Filter.ComponentsT0[i].Id, null); //Удалим ускорение
                    ECS.RemoveComponent<Way>(Filter.ComponentsT0[i].Id, null); //Удалим путь
                }
            }
        }
    }
}
