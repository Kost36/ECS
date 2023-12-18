using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec30Once)]
    [SystemPriority(5)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class MoveSystem : SystemExistComponents<Pozition, Speed>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Pozition pozition, Speed speed, float deltatime)
        {
            pozition.X += speed.dX * DeltaTime;
            pozition.Y += speed.dY * DeltaTime;
            pozition.Z += speed.dZ * DeltaTime;
        }
    }
}
