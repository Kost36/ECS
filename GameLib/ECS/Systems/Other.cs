using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(5)]
    [AttributeSystemEnable]
    public class StartMoveSystem : SystemExistComponents<Pozition, PozitionSV>, ISystemActionAdd
    {
        public override void ActionAdd(int entityId, Pozition pozition, PozitionSV pozitionSV)
        {
            IECS.AddComponent(new Way() { Id = entityId });
            IECS.AddComponent(new WayToStop() { Id = entityId });
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(9)]
    [AttributeSystemEnable]
    public class ControlWaySystem : SystemExistComponents<Pozition, PozitionSV, Way>, ISystemAction
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

            if (IECS.GetComponent(entityId, out Speed speed) == false)
            {
                IECS.AddComponent(new Speed() { SpeedMax = 10, Id = entityId });
            } //Если скорости нету
            if (IECS.GetComponent(entityId, out SpeedSV speedSV) == false)
            {
                IECS.AddComponent(new SpeedSV() { Id = entityId });
            } //Если задания скорости нету
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(9)]
    [AttributeSystemEnable]
    public class ControlWayToStopSystem : SystemExistComponents<WayToStop, Speed, Acceleration, Enargy>, ISystemAction
    {
        public override void Action(int entityId, WayToStop wayToStop, Speed speed, Acceleration acceleration, Enargy enargy, float deltatime)
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

    [AttributeSystemCalculate(1)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    public class ControlSpeedSystem : SystemExistComponents<Speed, SpeedSV, Way, WayToStop>, ISystemAction
    {
        public override void Action(int entityId, Speed speed, SpeedSV speedSV, Way way, WayToStop wayToStop, float deltatime)
        {
            if (wayToStop.EnargyHave && way.Len < wayToStop.Len*1.1)
            {
                Stop(entityId, speed, speedSV);
            }
            else if (wayToStop.EnargyHave == false && way.Len < wayToStop.Len * 2)
            {
                Stop(entityId, speed, speedSV);
            } //Если время оставшегося пути меньше пути останова *1.1
            else if (way.Len > wayToStop.Len)
            {
                if (speed.SpeedFact > speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.SpeedFact;
                }
                if (speed.SpeedFact >= speedSV.SVSpeed*0.95)
                {
                    speedSV.SVSpeed += (float)(speed.SpeedMax * 0.05); //Увеличиваем на 5% от максимальной скорости
                    if (IECS.GetComponent(entityId, out Acceleration acceleration) == false)
                    {
                        IECS.AddComponent(new Acceleration() { Id = entityId });
                    } //Если нету ускорения
                    if (speedSV.SVSpeed > speed.SpeedMax)
                    {
                        speedSV.SVSpeed = speed.SpeedMax;
                    }
                }
            } //Если время оставшегося пути больше 5 минут

            //Рассчет необходимой скорости в направлении
            speedSV.dXSV = way.NormX * speedSV.SVSpeed;
            speedSV.dYSV = way.NormY * speedSV.SVSpeed;
            speedSV.dZSV = way.NormZ * speedSV.SVSpeed;
        }


        private void Stop(int entityId, Speed speed, SpeedSV speedSV)
        {
            if (speedSV.SVSpeed > 0)
            {
                if (speed.SpeedFact < speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.SpeedFact;
                }
                speedSV.SVSpeed -= (float)(speed.SpeedMax * 0.1); //Снижаем на 10% от максимальной скорости
                if (IECS.GetComponent(entityId, out Acceleration acceleration) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = entityId });
                } //Если нету ускорения
                if (speedSV.SVSpeed < 0)
                {
                    speedSV.SVSpeed = 0;
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec30Once)]
    [AttributeSystemPriority(5)]
    [AttributeSystemEnable]
    public class MoveSystem : SystemExistComponents<Pozition, Speed>, ISystemAction
    {
        public override void Action(int entityId, Pozition pozition, Speed speed, float deltatime)
        {
            pozition.X += speed.dX * DeltaTime;
            pozition.Y += speed.dY * DeltaTime;
            pozition.Z += speed.dZ * DeltaTime;
            //pozition.Transform.position = new UnityEngine.Vector3(pozition.X, pozition.Y, pozition.Z);
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class EnargyRegenerationSystem : SystemExistComponents<Enargy, EnargyReGeneration>, ISystemAction
    {
        public override void Action(int entityId, Enargy enargy, EnargyReGeneration enargyReGeneration, float deltatime)
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

    [AttributeSystemCalculate(0.033f)]
    [AttributeSystemPriority(20)]
    [AttributeSystemEnable]
    public class ControlSpeedSystemRemove : SystemExistComponents<Speed, SpeedSV, Way>, ISystemAction
    {
        public override void Action(int entityId, Speed speed, SpeedSV speedSV, Way way, float deltatime)
        {
            if (way.Len < 1)
            {
                IECS.RemoveComponent<PozitionSV>(entityId); //Удалим точку перемещения
                IECS.RemoveComponent<Way>(entityId); //Удалим путь

                IECS.RemoveComponent<Speed>(entityId); //Удалим скорость
                IECS.RemoveComponent<SpeedSV>(entityId); //Удалим заданную скорость
                IECS.RemoveComponent<Acceleration>(entityId); //Удалим ускоре
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    [AttributeSystemEnable]
    public class AccselerateSystem : SystemExistComponents<SpeedSV, Acceleration, Speed, Enargy>, ISystemAction
    {
        public override void Action(int entityId, SpeedSV speedSV, Acceleration acceleration, Speed speed, Enargy enargy, float deltatime)
        {
            float enargyUse = acceleration.EnargyUse * DeltaTime;
            float acc = acceleration.Acc * DeltaTime;
            if (enargy.EnargyFact > enargyUse)
            {
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

            //Проверка достижения максимальной скорости
            if (speed.dX == speedSV.dXSV)
            {
                if (speed.dY == speedSV.dYSV)
                {
                    if (speed.dZ == speedSV.dZSV)
                    {
                        IECS.RemoveComponent<Acceleration>(entityId); //Удалить компонент ускорения
                    }
                }
            }
        }
    }





    [AttributeSystemCalculate(0.2f)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class ShipAiSystem : SystemExistComponents<ShipAi>, ISystemAction
    {
        public override void Action(int entityId, ShipAi shipAi, float deltatime)
        {
            if (IECS.GetEntity(entityId, out Entity entity) == false)
            {
                return;
            }
            if (entity.Get(out Enargy enargy))
            {
                if (enargy.EnargyFact < enargy.EnargyMax)
                {
                    if (entity.Get(out EnargyReGeneration enargyReGeneration) == false)
                    {
                        entity.Add(new EnargyReGeneration { Id = entityId, EnargyReGen = 10 });
                    }
                }
            } //Если энергии мало, и нету регенерации. => Добавить компонент
            if (entity.Get(out Health health))
            {
                if (health.HealthFact < health.HealthMax)
                {
                    if (entity.Get(out HealthReGeneration healthReGeneration) == false)
                    {
                        entity.Add(new HealthReGeneration { Id = entityId, HealthReGen = 1, EnargyUse = 5 });
                    }
                }
            } //Если жизни не полные и нету регенерации => добавить компонент
            if (entity.Get(out Shild shild))
            {
                if (shild.ShildFact < shild.ShildMax)
                {
                    if (entity.Get(out ShildReGeneration shildReGeneration) == false)
                    {
                        entity.Add(new ShildReGeneration { Id = entityId, ShildReGen = 1, EnargyUse = 5 });
                    }
                }
            } //Если щиты не полные и нету регенерации => добавить компонент
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class HealthRegenerationSystem : SystemExistComponents<Health, HealthReGeneration, Enargy>, ISystemAction
    {
        public override void Action(int entityId, Health health, HealthReGeneration healthReGeneration, Enargy enargy, float deltatime)
        {
            if (health.HealthFact < health.HealthMax)
            {
                float enargyUse = healthReGeneration.EnargyUse * DeltaTime;
                if (enargy.EnargyFact > enargyUse)
                {
                    health.HealthFact += healthReGeneration.HealthReGen * DeltaTime;
                    enargy.EnargyFact -= enargyUse;
                    if (health.HealthFact > health.HealthMax)
                    {
                        health.HealthFact = health.HealthMax;
                        IECS.RemoveComponent<HealthReGeneration>(entityId);
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class ShildRegenerationSystem : SystemExistComponents<Shild, ShildReGeneration, Enargy>, ISystemAction
    {
        public override void Action(int entityId, Shild shild, ShildReGeneration shildReGeneration, Enargy enargy, float deltatime)
        {
            if (shild.ShildFact < shild.ShildMax)
            {
                float enargyUse = shildReGeneration.EnargyUse * DeltaTime;
                if (enargy.EnargyFact > enargyUse)
                {
                    shild.ShildFact += shildReGeneration.ShildReGen * DeltaTime;
                    enargy.EnargyFact -= enargyUse;
                    if (shild.ShildFact > shild.ShildMax)
                    {
                        shild.ShildFact = shild.ShildMax;
                        IECS.RemoveComponent<ShildReGeneration>(entityId);
                    }
                }
            }
        }
    }
}