using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    public class StartMoveSystem : SystemExistComponents<Pozition, PozitionSV>, ISystemActionAdd
    {
        public override void ActionAdd(Pozition pozition, PozitionSV pozitionSV, Entity entity)
        {
            IECS.AddComponent(new Way() { Id = entity.Id });
            IECS.AddComponent(new WayToStop() { Id = entity.Id });
            IECS.AddComponent(new Speed() { Max = 10, Id = entity.Id });
            IECS.AddComponent(new SpeedSV() { Id = entity.Id });
        }
    }
}
