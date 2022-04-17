using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(9)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class ControlWayToStopSystem : SystemExistComponents<WayToStop, Speed, Acceleration, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, WayToStop wayToStop, Speed speed, Acceleration acceleration, Enargy enargy, float deltatime)
        {
            float timeDeAcc = speed.Fact / acceleration.Acc; //Сколько времени нужно замедляться
            wayToStop.Len = (speed.Fact * timeDeAcc) / 2f; //Путь останова
            float enargyNeed = timeDeAcc * acceleration.EnargyUse; //Необходимое колличество энергии для останова
            if (enargyNeed > enargy.Fact)
            {
                wayToStop.EnargyHave = false;
                return;
            } //Если энергии меньше, чем нужно
            wayToStop.EnargyHave = true;
        }
    }
}
