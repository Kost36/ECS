using ECSCore.BaseObjects;
using ECSCoreTests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCoreTests.Components
{
    /// <summary>
    /// Компонент интеллекта корабля
    /// </summary>
    public class ShipAi : Component { }
    /// <summary>
    /// Компонент интеллекта корабля (Торговли)
    /// </summary>
    public class ShipAiTrade : Component { }
    /// <summary>
    /// Компонент интеллекта корабля (Боевой)
    /// </summary>
    public class ShipAiWar : Component { }
    /// <summary>
    /// Компонент состояния корабля
    /// </summary>
    public class ShipState : Component
    {
        /// <summary>
        /// Состояние корабля
        /// </summary>
        public StateShip StateShip;
    }

    /// <summary>
    /// Компонент позиции
    /// </summary>
    public class Pozition : Component
    {
        /// <summary>
        /// Позиция X
        /// </summary>
        public float X;
        /// <summary>
        /// Позиция Y
        /// </summary>
        public float Y;
        /// <summary>
        /// Позиция Z
        /// </summary>
        public float Z;
    }
    /// <summary>
    /// Компонент заданной позиции.
    /// Позиция, в которую нужно переместиться
    /// </summary>
    public class PozitionSV : Component
    {
        /// <summary>
        /// Позиция X
        /// </summary>
        public float X;
        /// <summary>
        /// Позиция Y
        /// </summary>
        public float Y;
        /// <summary>
        /// Позиция Z
        /// </summary>
        public float Z;
    }
    /// <summary>
    /// Компонент направления (Нормализованный)
    /// </summary>
    public class Direction : Component
    {
        /// <summary>
        /// Направление X (Номрализованное значение)
        /// </summary>
        public float XNorm;
        /// <summary>
        /// Направление Y (Номрализованное значение)
        /// </summary>
        public float YNorm;
        /// <summary>
        /// Направление Z (Номрализованное значение)
        /// </summary>
        public float ZNorm;
    }

    /// <summary>
    /// Компонент энерги
    /// </summary>
    public class Enargy : Component
    {
        /// <summary>
        /// Максимальный запас энергии
        /// </summary>
        public float EnargyMax;
        /// <summary>
        /// Фактический запас энергии
        /// </summary>
        public float EnargyFact;
    }
    /// <summary>
    /// Компонент прочности / здоровья
    /// </summary>
    public class Health : Component
    {
        /// <summary>
        /// Максимальный запас прочности / здоровья
        /// </summary>
        public float HealthMax;
        /// <summary>
        /// Фактическая прочности / здоровья
        /// </summary>
        public float HealthFact;
    }
    /// <summary>
    /// Компонент щита
    /// </summary>
    public class Shild : Component
    {
        /// <summary>
        /// Максимальный запас щита
        /// </summary>
        public float ShildMax;
        /// <summary>
        /// Фактическая запас щита
        /// </summary>
        public float ShildFact;
    }
    /// <summary>
    /// Компонент трюма (вместимости в м³) 
    /// м³
    /// </summary>
    public class Hold : Component
    {
        /// <summary>
        /// Максимальная вместимость в м³
        /// </summary>
        public float HoldMax;
        /// <summary>
        /// Занято емкости в м³
        /// </summary>
        public float HoldUse;
    }
    /// <summary>
    /// Компонент веса
    /// Кг
    /// </summary>
    public class Weight : Component
    {
        /// <summary>
        /// Вес корабля (Кг)
        /// </summary>
        public float WeightFact;
    }

    /// <summary>
    /// Компонент скорости
    /// (m/sec)
    /// </summary>
    public class Speed : Component
    {
        /// <summary>
        /// Cкорости по оси X
        /// (m/sec)
        /// </summary>
        public float dX;
        /// <summary>
        /// Cкорости по оси Y
        /// (m/sec)
        /// </summary>
        public float dY;
        /// <summary>
        /// Cкорости по оси Z
        /// (m/sec)
        /// </summary>
        public float dZ;
        /// <summary>
        /// Фактической скорости
        /// (m/sec)
        /// </summary>
        public float SpeedFact;
        /// <summary>
        /// Максимальной скорости
        /// (m/sec)
        /// </summary>
        public float SpeedMax;
    }
    /// <summary>
    /// Компонент заданной скорости.
    /// (m/sec)
    /// </summary>
    public class SpeedSV : Component
    {
        /// <summary>
        /// Заданная скорость по оси X
        /// (m/sec)
        /// </summary>
        public float dXSV;
        /// <summary>
        /// Заданная скорость по оси Y
        /// (m/sec)
        /// </summary>
        public float dYSV;
        /// <summary>
        /// Заданная скорость по оси Z
        /// (m/sec)
        /// </summary>
        public float dZSV;
        /// <summary>
        /// Заданная скорость
        /// (m/sec)
        /// </summary>
        public float SVSpeed;
    }
    /// <summary>
    /// Компонент скорости вращения
    /// (value/sec)
    /// </summary>
    public class SpeedRotation : Component
    {
        /// <summary>
        /// Cкорость вращения по оси X
        /// (m/sec)
        /// </summary>
        public float dX = 0.02f;
        /// <summary>
        /// Cкорость вращения по оси Y
        /// (m/sec)
        /// </summary>
        public float dY = 0.02f;
        /// <summary>
        /// Cкорость вращения по оси Z
        /// (m/sec)
        /// </summary>
        public float dZ = 0.02f;
    }
    /// <summary>
    /// Компонент ускорения
    /// (dm/sec)
    /// </summary>
    public class Acceleration : Component
    {
        /// <summary>
        /// Ускорение 
        /// (dm/sec)
        /// </summary>
        public float Acc = 0.05f;
        /// <summary>
        /// Использование энергии, для ускорения
        /// (value/sec)
        /// </summary>
        public float EnargyUse = 5;
    }
    /// <summary>
    /// Компонент торможения
    /// (dm/sec)
    /// </summary>
    public class Deceleration : Component
    {
        /// <summary>
        /// Торможение 
        /// (dm/sec)
        /// </summary>
        public float Dec = 0.05f;
        /// <summary>
        /// Использование энергии, для торможения
        /// (value/sec)
        /// </summary>
        public float EnargyUse = 5;
    }
    /// <summary>
    /// Компонент регенерации энергии
    /// Value/сек
    /// </summary>
    public class EnargyReGeneration : Component
    {
        /// <summary>
        /// Регенерации энергии в секунду
        /// </summary>
        public float EnargyReGen = 15;
    }
    /// <summary>
    /// Компонент регенерации щита
    /// Value/сек
    /// </summary>
    public class ShildReGeneration : Component
    {
        /// <summary>
        /// Регенерации щита в секунду
        /// </summary>
        public float ShildReGen = 1;
        /// <summary>
        /// Использование энергии в секунду
        /// </summary>
        public float EnargyUse = 10;
    }
    /// <summary>
    /// Компонент ремонта / исцеления
    /// </summary>
    public class HealthReGeneration : Component
    {
        /// <summary>
        /// Ремонт / исцеление в секунду
        /// </summary>
        public float HealthReGen = 1;
        /// <summary>
        /// Использование энергии в секунду
        /// </summary>
        public float EnargyUse = 10;
    }

    /// <summary>
    /// Компонент вектора заданного перемещения
    /// </summary>
    public class Way : Component
    {
        /// <summary>
        /// Длинна пути в метрах
        /// </summary>
        public float Len;
        /// <summary>
        /// Длинна пути в метрах по оси X
        /// </summary>
        public float LenX;
        /// <summary>
        /// Длинна пути в метрах по оси Y
        /// </summary>
        public float LenY;
        /// <summary>
        /// Длинна пути в метрах по оси Z
        /// </summary>
        public float LenZ;
        /// <summary>
        /// Нормализованный вектор направления по оси X
        /// </summary>
        public float NormX;
        /// <summary>
        /// Нормализованный вектор направления по оси Y
        /// </summary>
        public float NormY;
        /// <summary>
        /// Нормализованный вектор направления по оси Z
        /// </summary>
        public float NormZ;
    }
}