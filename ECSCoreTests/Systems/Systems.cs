using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using ECSCoreTests.Components;
using LibMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCoreTests.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(5)]
    [AttributeSystemEnable]
    public class StartMoveSystem : SystemExistComponents<Pozition, PozitionSV>, ISystemAction, ISystemActionAdd, ISystemActionRemove
    {
        public override void ActionAdd(int entityId, Pozition pozition, PozitionSV pozitionSV)
        {
            IECS.AddComponent(new Way() { Id = entityId });
        }
        public override void Action(int entityId, Pozition pozition, PozitionSV pozitionSV, float deltatime)
        {
            entityId = entityId;
        }
        public override void ActionRemove(int entityId)
        {
            entityId = entityId;
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

    [AttributeSystemCalculate(1)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    public class ControlSpeedSystem : SystemExistComponents<Speed, SpeedSV, Way>, ISystemAction
    {
        public override void Action(int entityId, Speed speed, SpeedSV speedSV, Way way, float deltatime)
        {
            if (way.Len < speed.SpeedFact * 300)
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
            } //Если время оставшегося пути меньше 5 минут
            if (way.Len > speed.SpeedFact * 300)
            {
                if (speed.SpeedFact > speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.SpeedFact;
                }
                if (speed.SpeedFact == speedSV.SVSpeed)
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
            if (way.Len < 10)
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
            //if (entity.Get(out PozitionSV pozitionSV))
            //{
            //    if (entity.Get(out Way way) == false)
            //    {
            //        entity.Add(new Way { Id = entityId });
            //    }
            //} //Если есть задание на полет и нету way => добавить компонент
            if (entity.Get(out ShipState shipState))
            {
                if (shipState.StateShip == Enums.StateShip.TRADE)
                {
                    if (entity.Get(out ShipAiTrade trade) == false)
                    {
                        entity.Add(new ShipAiTrade { Id = entityId });
                    }
                }
            } //Если состояние торговли и нету компонента торговли => добавить компонент
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