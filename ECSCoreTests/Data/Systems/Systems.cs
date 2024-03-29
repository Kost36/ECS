﻿using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using ECSCoreTests.Components;
using ECSCoreTests.Lib;
using System;

namespace ECSCoreTests.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(5)]
    [SystemEnable]
    public class StartMoveSystem : SystemExistComponents<Pozition, PozitionSV>, ISystemActionAdd
    {
        public override void ActionAdd(Pozition pozition, PozitionSV pozitionSV, Entity entity)
        {
            IECS.AddComponent(new Way() { Id = entity.Id });
            IECS.AddComponent(new WayToStop() { Id = entity.Id });
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(9)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlWaySystem : SystemExistComponents<Pozition, PozitionSV, Way>, ISystemAction, ISystemActionAdd, ISystemParallel
    {
        public override void ActionAdd(Pozition existComponentT1, PozitionSV existComponentT2, Way existComponentT3, Entity entity)
        {
            if (IECS.GetComponent(entity.Id, out Speed _) == false)
            {
                IECS.AddComponent(new Speed() { SpeedMax = 10, Id = entity.Id });
            } //Если скорости нету
            if (IECS.GetComponent(entity.Id, out SpeedSV _) == false)
            {
                IECS.AddComponent(new SpeedSV() { Id = entity.Id });
            } //Если задания скорости нету
        }
        public override void Action(Guid entityId, Pozition pozition, PozitionSV pozitionSV, Way way, float deltatime)
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
            if (way.InitOk == false)
            {
                way.InitOk = true;
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(9)]
    [SystemEnable]
    public class ControlWayToStopSystem : SystemExistComponents<WayToStop, Speed, Acceleration, Enargy>, ISystemAction
    {
        public override void Action(Guid entityId, WayToStop wayToStop, Speed speed, Acceleration acceleration, Enargy enargy, float deltatime)
        {
            //Сколько времени нужно замедляться
            float timeDeAcc = speed.SpeedFact / acceleration.Acc;
            wayToStop.Len = (speed.SpeedFact * timeDeAcc) / 2f;
            float enargyNeed = timeDeAcc * acceleration.EnargyUse;
            if (enargyNeed > enargy.EnargyFact)
            {
                wayToStop.EnargyHave = false;
                return;
            }
            wayToStop.EnargyHave = true;
        }
    }

    [SystemCalculate(SystemCalculateInterval.Min30Once)]
    [SystemPriority(15)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class ControlSpeedSystem : SystemExistComponents<Speed, SpeedSV, Way, WayToStop>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, Speed speed, SpeedSV speedSV, Way way, WayToStop wayToStop, float deltatime)
        {
            //Замедление
            if (wayToStop.EnargyHave && way.Len < wayToStop.Len*1.1)
            {
                DownSpeed(speed, speedSV);
            } //Если энергии хватает на останов и оставшийся путь меньше пути останова*1.1
            else if (wayToStop.EnargyHave == false && way.Len < wayToStop.Len * 2)
            {
                DownSpeed(speed, speedSV);
            } //Если энергии нехватает на останов и оставшийся путь меньше пути останова*2
            //Ускорение
            else if (way.Len > wayToStop.Len*3)
            {
                if (speedSV.SVSpeed == speed.SpeedMax)
                {
                    return;
                } //Есди заданная скорость равна максимальной
                UpSpeed(speed, speedSV);
            } //Если оставшийся путь в 1.5 раз больше пути останова
            else
            {
                return;
            }

            //Рассчет необходимой скорости в направлении
            speedSV.dXSV = way.NormX * speedSV.SVSpeed;
            speedSV.dYSV = way.NormY * speedSV.SVSpeed;
            speedSV.dZSV = way.NormZ * speedSV.SVSpeed;


            void UpSpeed(Speed speed, SpeedSV speedSV)
            {
                if (speed.SpeedFact > speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.SpeedFact;
                }
                if (speed.SpeedFact >= speedSV.SVSpeed * 0.95)
                {
                    speedSV.SVSpeed += (float)(speed.SpeedMax * 0.05); //Увеличиваем на 5% от максимальной скорости
                    speedSV.Update = true;
                    if (speedSV.SVSpeed > speed.SpeedMax)
                    {
                        speedSV.SVSpeed = speed.SpeedMax;
                    }
                }
            }
            void DownSpeed(Speed speed, SpeedSV speedSV)
            {
                if (speedSV.SVSpeed > 0)
                {
                    if (speed.SpeedFact < speedSV.SVSpeed)
                    {
                        speedSV.SVSpeed = speed.SpeedFact;
                    }
                    speedSV.SVSpeed -= (float)(speed.SpeedMax * 0.1); //Снижаем на 10% от максимальной скорости
                    speedSV.Update = true;
                    if (speedSV.SVSpeed < 0)
                    {
                        speedSV.SVSpeed = 0;
                    }
                }
            }
        }
        
    }

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
            //if (speedSV.SVSpeed > speed.SpeedFact * 0.95)
            //{
            //    if (IECS.GetComponent(entityId, out Acceleration acceleration) == false)
            //    {
            //        IECS.AddComponent(new Acceleration() { Id = entityId });
            //    } //Если нету ускорения
            //}
            //else if (speedSV.SVSpeed < speed.SpeedFact * 1.05)
            //{
            //    if (IECS.GetComponent(entityId, out Acceleration acceleration) == false)
            //    {
            //        IECS.AddComponent(new Acceleration() { Id = entityId });
            //    } //Если нету ускорения
            //}
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec30Once)]
    [SystemPriority(5)]
    [SystemEnable]
    //[AttributeSystemParallelCountThreads(8)]
    public class MoveSystem : SystemExistComponents<Pozition, Speed>, ISystemAction//, ISystemParallel
    {
        public override void Action(Guid entityId, Pozition pozition, Speed speed, float deltatime)
        {
            pozition.X += speed.dX * DeltaTime;
            pozition.Y += speed.dY * DeltaTime;
            pozition.Z += speed.dZ * DeltaTime;
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(10)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class EnargyRegenerationSystem : SystemExistComponents<Enargy, EnargyReGeneration>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, Enargy enargy, EnargyReGeneration enargyReGeneration, float deltatime)
        {
            if (enargy.EnargyFact < enargy.EnargyMax)
            {
                enargy.EnargyFact += enargyReGeneration.EnargyReGen * DeltaTime;
                if (enargy.EnargyFact > enargy.EnargyMax)
                {
                    enargy.EnargyFact = enargy.EnargyMax;
                    IECS.RemoveComponent<EnargyReGeneration>(entityId);
                }
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Min5Once)]
    [SystemPriority(10)]
    [SystemEnable]
    [ExcludeComponentSystem(typeof(EnargyReGeneration))]
    public class EnargyRegenerationStartSystem : SystemExistComponents<Enargy>, ISystemAction
    {
        public override void Action(Guid entityId, Enargy enargy, float deltatime)
        {
            if (enargy.EnargyFact < enargy.EnargyMax*0.9)
            {
                if (IECS.GetComponent(entityId, out EnargyReGeneration _) == false)
                {
                    IECS.AddComponent(new EnargyReGeneration() { EnargyReGen = 5f, Id = entityId });
                }
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(20)]
    [SystemEnable]
    public class ControlSpeedSystemRemove : SystemExistComponents<Way>, ISystemAction, ISystemActionRemove
    {
        public override void Action(Guid entityId, Way way, float deltatime)
        {
            if (way.InitOk && way.Len < 1)
            {
                IECS.RemoveComponent<PozitionSV>(entityId); //Удалим точку перемещения
                IECS.RemoveComponent<Acceleration>(entityId); //Удалим ускорение
                IECS.RemoveComponent<Speed>(entityId); //Удалим скорость
                IECS.RemoveComponent<SpeedSV>(entityId); //Удалим заданную скорость
                IECS.RemoveComponent<Way>(entityId); //Удалим путь
            }
        }

        public override void ActionRemove(Guid entityId)
        {
            if (IECS.GetComponent(entityId, out PozitionSV _) == false)
            {
                IECS.GetEntity(entityId, out Entity entity);
                entity.Death();
            }
        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(1)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    public class AccselerateSystem : SystemExistComponents<SpeedSV, Acceleration, Speed, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(Guid entityId, SpeedSV speedSV, Acceleration acceleration, Speed speed, Enargy enargy, float deltatime)
        {
            float enargyUse = acceleration.EnargyUse * DeltaTime;
            float acc = acceleration.Acc * DeltaTime;
            if (enargy.EnargyFact > enargyUse)
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
                                IECS.RemoveComponent<Acceleration>(entityId); //Удалить компонент ускорения
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
                acceleration.SpeedOk = false;

                speedSV.Update = false;
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

                enargy.EnargyFact -= enargyUse;
                speed.SpeedFact = Vector.Len(speed.dX, speed.dY, speed.dZ);

            }

        }
    }

    [SystemCalculate(SystemCalculateInterval.Sec60Once)]
    [SystemPriority(1)]
    [SystemEnable]
    [SystemParallelCountThreads(8)]
    [ExcludeComponentSystem(typeof(Exclude))]
    public class TestExcludeSystem : SystemExistComponents<Include>, ISystemActionAdd, ISystemActionRemove, ISystemAction, ISystemParallel
    {
        public static Include IncludeForTest;

        public override void ActionAdd(Include include, Entity entity)
        {
            include.CallOfAdd = true;
            include.CallOfAddCount++;
            IncludeForTest = include;
        }

        public override void Action(Guid entityId, Include include, float deltatime)
        {
            include.CallActionCount++;
        }

        public override void ActionRemove(Guid entityId)
        {
            if (IncludeForTest != null)
            {
                IncludeForTest.CallOfRemove = true;
                IncludeForTest.CallOfRemoveCount++;
            }
        }
    }
}