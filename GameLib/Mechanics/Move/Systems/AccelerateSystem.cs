using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Move.Components;
using GameLib.WorkFlow;
using MathLib;

namespace GameLib.Mechanics.Move.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(1)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class AccelerateSystem : SystemExistComponents<SpeedSV, Acceleration, Speed, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, SpeedSV speedSV, Acceleration acceleration, Speed speed, Enargy enargy, float deltatime)
        {
            float enargyUse = acceleration.EnargyUse * DeltaTime;
            float acc = acceleration.Acc * DeltaTime;
            if (enargy.Fact > enargyUse)
            {
                //Проверка достижения максимальной скорости
                if (speed.dX == speedSV.dXSV)
                {
                    if (speed.dY == speedSV.dYSV)
                    {
                        if (speed.dZ == speedSV.dZSV)
                        {
                            if (acceleration.SpeedOk)
                            {
                                IECS.RemoveComponent<Acceleration>(entityId);
                            }
                            else
                            {
                                acceleration.SpeedOk = true;
                            }
                            speedSV.Update = false;
                            return;
                        }
                    }
                }

                speedSV.Update = false;
                acceleration.SpeedOk = false;

                //Ускорение
                if (speedSV.dXSV < speed.dX)
                {
                    speed.dX -= acc;
                    if (speedSV.dXSV > speed.dX)
                    {
                        speed.dX = speedSV.dXSV;
                    }
                }
                else if (speedSV.dXSV > speed.dX)
                {
                    speed.dX += acc;
                    if (speedSV.dXSV < speed.dX)
                    {
                        speed.dX = speedSV.dXSV;
                    }
                }

                if (speedSV.dYSV < speed.dY)
                {
                    speed.dY -= acc;
                    if (speedSV.dYSV > speed.dY)
                    {
                        speed.dY = speedSV.dYSV;
                    }
                }
                else if (speedSV.dYSV > speed.dY)
                {
                    speed.dY += acc;
                    if (speedSV.dYSV < speed.dY)
                    {
                        speed.dY = speedSV.dYSV;
                    }
                }

                if (speedSV.dZSV < speed.dZ)
                {
                    speed.dZ -= acc;
                    if (speedSV.dZSV > speed.dZ)
                    {
                        speed.dZ = speedSV.dZSV;
                    }
                }
                else if (speedSV.dZSV > speed.dZ)
                {
                    speed.dZ += acc;
                    if (speedSV.dZSV < speed.dZ)
                    {
                        speed.dZ = speedSV.dZSV;
                    }
                }

                enargy.Fact -= enargyUse;
                speed.Fact = Vector.Len(speed.dX, speed.dY, speed.dZ);
            }
        }
    }
}
