using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
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
    [AttributeSystemCalculate(SystemCalculateInterval.Sec30Once)]
    [AttributeSystemPriority(5)]
    [AttributeSystemEnable]
    public class MoveSystem : System<Pozition, Speed>
    {
        public override void Action(Pozition pozition, Speed speed)
        {
            pozition.X += speed.dX * DeltaTime;
            pozition.Y += speed.dY * DeltaTime;
            pozition.Z += speed.dZ * DeltaTime;
        }
    }

    [AttributeSystemCalculate(0.2f)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class ShipAiSystem : System<ShipAi>
    {
        public override void Action(ShipAi shipAi)
        {
            int entityId = shipAi.Id;
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
            if (entity.Get(out PozitionSV pozitionSV))
            {
                if (entity.Get(out Way way) == false)
                {
                    entity.Add(new Way { Id = entityId });
                }
            } //Если есть задание на полет и нету way => добавить компонент
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
    public class EnargyRegenerationSystem : System<Enargy, EnargyReGeneration>
    {
        public override void Action(Enargy enargy, EnargyReGeneration enargyReGeneration)
        {
            if (enargy.EnargyFact < enargy.EnargyMax)
            {
                enargy.EnargyFact += enargyReGeneration.EnargyReGen * DeltaTime;
                if (enargy.EnargyFact > enargy.EnargyMax)
                {
                    enargy.EnargyFact = enargy.EnargyMax;
                    IECS.RemoveComponent<EnargyReGeneration>(enargy.Id);
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class HealthRegenerationSystem : System<Health, HealthReGeneration, Enargy>
    {
        public override void Action(Health health, HealthReGeneration healthReGeneration, Enargy enargy)
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
                        IECS.RemoveComponent<HealthReGeneration>(health.Id);
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    public class ShildRegenerationSystem : System<Shild, ShildReGeneration, Enargy>
    {
        public override void Action(Shild shild, ShildReGeneration shildReGeneration, Enargy enargy)
        {
            if (shild.ShildFact < shild.ShildMax)
            {
                float enargyUse = shildReGeneration.EnargyUse * DeltaTime;
                if (enargy.EnargyFact > shildReGeneration.EnargyUse)
                {
                    shild.ShildFact += shildReGeneration.ShildReGen;
                    enargy.EnargyFact -= enargyUse;
                    if (shild.ShildFact > shild.ShildMax)
                    {
                        shild.ShildFact = shild.ShildMax;
                        IECS.RemoveComponent<ShildReGeneration>(shild.Id);
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(9)]
    [AttributeSystemEnable]
    public class ControlWaySystem : System<Pozition, PozitionSV, Way>
    {
        public override void Action(Pozition pozition, PozitionSV pozitionSV, Way way)
        {
            int idEntity = pozition.Id;

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

            if (IECS.GetComponent(idEntity, out Speed speed) == false)
            {
                IECS.AddComponent(new Speed() { SpeedMax = 10, Id = idEntity });
            } //Если скорости нету
            if (IECS.GetComponent(idEntity, out SpeedSV speedSV) == false)
            {
                IECS.AddComponent(new SpeedSV() { Id = idEntity });
            } //Если задания скорости нету
        }
    }

    [AttributeSystemCalculate(1)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    public class ControlSpeedSystem : System<Speed, SpeedSV, Way>
    {
        public override void Action(Speed speed, SpeedSV speedSV, Way way)
        {
            int idEntity = speed.Id;
            if (way.Len < speed.SpeedFact * 300)
            {
                if (speed.SpeedFact < speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.SpeedFact;
                }
                speedSV.SVSpeed -= (float)(speed.SpeedMax * 0.005); //Снижаем на 5% от максимальной скорости
                if (IECS.GetComponent(idEntity, out Acceleration acceleration) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = idEntity });
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
                speedSV.SVSpeed += (float)(speed.SpeedMax * 0.005); //Снижаем на 5% от максимальной скорости
                if (IECS.GetComponent(idEntity, out Acceleration acceleration) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = idEntity });
                } //Если нету ускорения
                if (speedSV.SVSpeed > speed.SpeedMax)
                {
                    speedSV.SVSpeed = speed.SpeedMax;
                }
            } //Если время оставшегося пути больше 5 минут

            //Рассчет необходимой скорости в направлении
            speedSV.dXSV = way.NormX * speedSV.SVSpeed;
            speedSV.dYSV = way.NormY * speedSV.SVSpeed;
            speedSV.dZSV = way.NormZ * speedSV.SVSpeed;

            if (way.Len < 10)
            {
                IECS.RemoveComponent<PozitionSV>(way.Id); //Удалим точку перемещения
                IECS.RemoveComponent<Way>(way.Id); //Удалим путь

                IECS.RemoveComponent<Speed>(way.Id); //Удалим скорость
                IECS.RemoveComponent<SpeedSV>(way.Id); //Удалим заданную скорость
                IECS.RemoveComponent<Acceleration>(way.Id); //Удалим ускоре
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    [AttributeSystemEnable]
    public class AccselerateSystem : System<SpeedSV, Acceleration, Speed, Enargy>
    {
        public override void Action(SpeedSV speedSV, Acceleration acceleration, Speed speed, Enargy enargy)
        {
            float enargyUse = acceleration.EnargyUse * DeltaTime;
            float acc = acceleration.Acc * DeltaTime;
            if (enargy.EnargyFact > enargyUse)
            {
                //Ускорение
                if (speedSV.dXSV < speed.dX)
                {
                    speed.dX -= acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dXSV > speed.dX)
                    {
                        speed.dX = speedSV.dXSV;
                    }
                }
                else if (speedSV.dXSV > speed.dX)
                {
                    speed.dX += acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dXSV < speed.dX)
                    {
                        speed.dX = speedSV.dXSV;
                    }
                }

                if (speedSV.dYSV < speed.dY)
                {
                    speed.dY -= acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dYSV > speed.dY)
                    {
                        speed.dY = speedSV.dYSV;
                    }
                }
                else if (speedSV.dYSV > speed.dY)
                {
                    speed.dY += acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dYSV < speed.dY)
                    {
                        speed.dY = speedSV.dYSV;
                    }
                }

                if (speedSV.dZSV < speed.dZ)
                {
                    speed.dZ -= acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dZSV > speed.dZ)
                    {
                        speed.dZ = speedSV.dZSV;
                    }
                }
                else if (speedSV.dZSV > speed.dZ)
                {
                    speed.dZ += acc;
                    enargy.EnargyFact -= enargyUse;
                    if (speedSV.dZSV < speed.dZ)
                    {
                        speed.dZ = speedSV.dZSV;
                    }
                }
            }

            //Проверка достижения максимальной скорости
            if (speed.dX == speedSV.dXSV)
            {
                if (speed.dY == speedSV.dXSV)
                {
                    if (speed.dZ == speedSV.dXSV)
                    {
                        IECS.RemoveComponent<Acceleration>(speedSV.Id); //Удалить компонент ускорения
                    }
                }
            }
        }
    }
}