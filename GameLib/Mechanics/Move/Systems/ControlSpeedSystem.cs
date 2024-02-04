using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Move.Components;
using System;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(15)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlSpeedSystem : SystemExistComponents<Speed, SpeedSV, Way, WayToStop>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, Speed speed, SpeedSV speedSV, Way way, WayToStop wayToStop, float deltatime)
        {
            //Замедление
            if (wayToStop.EnargyHave == true && way.Len < wayToStop.Len * 1.1)
            {
                SpeedDown(speed, speedSV);
            } //Если энергии хватает на останов и оставшийся путь меньше пути останова*1.1
            else if (wayToStop.EnargyHave == false && way.Len < wayToStop.Len * 2)
            {
                SpeedDown(speed, speedSV);
            } //Если энергии нехватает на останов и оставшийся путь меньше пути останова*2

            //Ускорение
            else if (way.Len > wayToStop.Len * 3)
            {
                if (speedSV.SVSpeed == speed.Max)
                {
                    return;
                }
                SpeedUp(speed, speedSV);
            } //Если оставшийся путь в 3 раз больше пути останова
            else
            {
                return;
            }

            //Рассчет необходимой скорости в направлении
            speedSV.dXSV = way.NormX * speedSV.SVSpeed;
            speedSV.dYSV = way.NormY * speedSV.SVSpeed;
            speedSV.dZSV = way.NormZ * speedSV.SVSpeed;
        }

        private void SpeedUp(Speed speed, SpeedSV speedSV)
        {
            //if (speed.Fact > speedSV.SVSpeed)
            //{
            //    speedSV.SVSpeed = speed.Fact;
            //}
            if (speed.Fact >= speedSV.SVSpeed * 0.98)
            {
                speedSV.SVSpeed += (float)(speed.Max * 0.05); //Увеличиваем на 5% от максимальной скорости
                speedSV.Update = true;
                if (speedSV.SVSpeed > speed.Max)
                {
                    speedSV.SVSpeed = speed.Max;
                }
            }
        }

        private void SpeedDown(Speed speed, SpeedSV speedSV)
        {
            if (speedSV.SVSpeed > 0)
            {
                if (speed.Fact < speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.Fact;
                }
                speedSV.SVSpeed -= (float)(speed.Max * 0.1); //Снижаем на 10% от максимальной скорости
                speedSV.Update = true;
                if (speedSV.SVSpeed < 0)
                {
                    speedSV.SVSpeed = 0;
                }
            }
        }
    }
}
