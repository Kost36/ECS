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
    [ExcludeComponentSystem(typeof(Acceleration))]
    public class ControlAccelerateSystem : SystemExistComponents<SpeedSV>, ISystemAction
    {
        public override void Action(Guid entityId, SpeedSV speedSV, float deltatime)
        {
            if (speedSV.Update)
            {
                if (IECS.GetComponent(entityId, out Acceleration _) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = entityId });
                } //Если нету ускорения
                speedSV.Update = false;
            }
        }
    }
}
