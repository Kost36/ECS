using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components;
using GameLib.Components.Move;
using GameLib.LibHelp;

// Системы механники перемещения 
namespace GameLib.Systems.Move
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
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

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(10)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
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
            if (way.InitOk == false)
            {
                way.InitOk = true;
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(9)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class ControlWayToStopSystem : SystemExistComponents<WayToStop, Speed, Acceleration, Enargy>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, WayToStop wayToStop, Speed speed, Acceleration acceleration, Enargy enargy, float deltatime)
        {
            float timeDeAcc = speed.Fact / acceleration.Acc; //Сколько времени нужно замедляться
            wayToStop.Len = (speed.Fact * timeDeAcc) / 2f; //Путь останова
            float enargyNeed = timeDeAcc * acceleration.EnargyUse; //Необходимое колличество энергии для останова
            if (enargyNeed > enargy.Fact)
            {
                wayToStop.EnargyHave = false;
                return;
            } //Если энергии меньше, чем нужно
            wayToStop.EnargyHave = true;
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class ControlSpeedSystem : SystemExistComponents<Speed, SpeedSV, Way, WayToStop>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Speed speed, SpeedSV speedSV, Way way, WayToStop wayToStop, float deltatime)
        {
            //Замедление
            if (wayToStop.EnargyHave == true && way.Len < wayToStop.Len * 1.1)
            {
                SpeedDown(speed, speedSV);
            } //Если энергии хватает на останов и оставшийся путь меньше пути останова*1.1
            else if (wayToStop.EnargyHave == false && way.Len < wayToStop.Len * 2)
            {
                SpeedDown(speed, speedSV);
            } //Если энергии нехватает на останов и оставшийся путь меньше пути останова*2
            //Ускорение
            else if (way.Len > wayToStop.Len * 3)
            {
                if (speedSV.SVSpeed == speed.Max)
                {
                    return;
                } //Есди заданная скорость равна максимальной
                SpeedUp(speed, speedSV);
            } //Если оставшийся путь в 3 раз больше пути останова
            else
            {
                return;
            }

            //Рассчет необходимой скорости в направлении
            speedSV.dXSV = way.NormX * speedSV.SVSpeed;
            speedSV.dYSV = way.NormY * speedSV.SVSpeed;
            speedSV.dZSV = way.NormZ * speedSV.SVSpeed;
        }

        private void SpeedUp(Speed speed, SpeedSV speedSV)
        {
            //if (speed.Fact > speedSV.SVSpeed)
            //{
            //    speedSV.SVSpeed = speed.Fact;
            //}
            if (speed.Fact >= speedSV.SVSpeed * 0.98)
            {
                speedSV.SVSpeed += (float)(speed.Max * 0.05); //Увеличиваем на 5% от максимальной скорости
                speedSV.Update = true;
                if (speedSV.SVSpeed > speed.Max)
                {
                    speedSV.SVSpeed = speed.Max;
                }
            }
        }
        private void SpeedDown(Speed speed, SpeedSV speedSV)
        {
            if (speedSV.SVSpeed > 0)
            {
                if (speed.Fact < speedSV.SVSpeed)
                {
                    speedSV.SVSpeed = speed.Fact;
                }
                speedSV.SVSpeed -= (float)(speed.Max * 0.1); //Снижаем на 10% от максимальной скорости
                speedSV.Update = true;
                if (speedSV.SVSpeed < 0)
                {
                    speedSV.SVSpeed = 0;
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(15)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(Acceleration))]
    public class ControlAccelerateSystem : SystemExistComponents<SpeedSV>, ISystemAction
    {
        public override void Action(int entityId, SpeedSV speedSV, float deltatime)
        {
            if (speedSV.Update)
            {
                if (IECS.GetComponent(entityId, out Acceleration _) == false)
                {
                    IECS.AddComponent(new Acceleration() { Id = entityId });
                } //Если нету ускорения
                speedSV.Update = false;
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(1)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
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

    [AttributeSystemCalculate(SystemCalculateInterval.Sec30Once)]
    [AttributeSystemPriority(5)]
    [AttributeSystemEnable]
    [AttributeSystemParallelCountThreads(8)]
    public class MoveSystem : SystemExistComponents<Pozition, Speed>, ISystemAction, ISystemParallel
    {
        public override void Action(int entityId, Pozition pozition, Speed speed, float deltatime)
        {
            pozition.X += speed.dX * DeltaTime;
            pozition.Y += speed.dY * DeltaTime;
            pozition.Z += speed.dZ * DeltaTime;
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(20)]
    [AttributeSystemEnable]
    [AttributeExcludeComponentSystem(typeof(Acceleration))]
    public class ControlEndPositionSystem : SystemExistComponents<Way, Speed>, ISystemAction
    {
        public override void Action(int entityId, Way way, Speed speed, float deltatime)
        {
            if (way.InitOk && way.Len < 1 && speed.Fact < 0.1)
            {
                IECS.RemoveComponent<Way>(entityId); //Удалим путь
                IECS.RemoveComponent<PozitionSV>(entityId); //Удалим точку перемещения
                IECS.RemoveComponent<Speed>(entityId); //Удалим скорость
                IECS.RemoveComponent<SpeedSV>(entityId); //Удалим заданную скорость
            }
        }
    }
}