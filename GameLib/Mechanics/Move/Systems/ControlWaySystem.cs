using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Mechanics.Move.Components;
using MathLib;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlWaySystem : SystemExistComponents<Pozition, PozitionSV, Way>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Pozition pozition, PozitionSV pozitionSV, Way way, float deltatime)
        {
            //Рассчет вектора направления
            way.LenX = pozitionSV.X - pozition.X;
            way.LenY = pozitionSV.Y - pozition.Y;
            way.LenZ = pozitionSV.Z - pozition.Z;

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
