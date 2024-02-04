using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;
using System;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec30Once)]
    [SystemPriority(5)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class MoveSystem : SystemExistComponents<Position, Speed>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, Position position, Speed speed, float deltatime)
        {
            position.X += speed.dX * DeltaTime;
            position.Y += speed.dY * DeltaTime;
            position.Z += speed.dZ * DeltaTime;
        }
    }
}
