using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components.Energy;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(9)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlWayToStopSystem : SystemExistComponents<WayToStop, Speed, Acceleration, Energy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, WayToStop wayToStop, Speed speed, Acceleration acceleration, Energy enargy, float deltatime)
        {
            float timeDeAcc = speed.Fact / acceleration.Acc; //Сколько времени нужно замедляться
            wayToStop.Len = (speed.Fact * timeDeAcc) / 2f; //Путь останова
            float enargyNeed = timeDeAcc * acceleration.EnargyUse; //Необходимое колличество энергии для останова
            
            if (enargyNeed > enargy.Fact)
            {
                wayToStop.EnargyHave = false;
                return;
            }

            wayToStop.EnargyHave = true;
        }
    }
}
