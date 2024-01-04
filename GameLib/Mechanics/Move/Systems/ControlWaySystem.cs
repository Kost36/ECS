using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;
using MathLib.Structures;
using System;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlWaySystem : SystemExistComponents<Position, PositionSV, Way>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, Position position, PositionSV positionSV, Way way, float deltatime)
        {
            //Рассчет вектора направления
            way.LenX = positionSV.X - position.X;
            way.LenY = positionSV.Y - position.Y;
            way.LenZ = positionSV.Z - position.Z;

            //Рассчет расстояния до заданной точки 
            way.Len = Vector.Len(way.LenX, way.LenY, way.LenZ);

            //Нормализация вектора направления
            Vector vector = Vector.Norm(way.LenX, way.LenY, way.LenZ, way.Len);
            way.NormX = vector.X;
            way.NormY = vector.Y;
            way.NormZ = vector.Z;

            //Инициализация пройдена
            if (!way.InitOk)
            {
                way.InitOk = true;
            }
        }
    }
}
