using ECSCore.BaseObjects;
using System;

namespace GameLib.Components
{
    /// <summary>
    /// Компонент позиции
    /// </summary>
    [Serializable]
    public class Pozition : ComponentBase
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
    [Serializable]
    public class Direction : ComponentBase
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
    [Serializable]
    public class Enargy : ComponentBase
    {
        /// <summary>
        /// Максимальный запас энергии
        /// </summary>
        public float Max;
        /// <summary>
        /// Фактический запас энергии
        /// </summary>
        public float Fact;
    }

    /// <summary>
    /// Компонент регенерации энергии
    /// Value/сек
    /// </summary>
    [Serializable]
    public class EnargyReGeneration : ComponentBase
    {
        /// <summary>
        /// Регенерации энергии в секунду
        /// </summary>
        public float Regen = 5;
    }

    /// <summary>
    /// Компонент прочности / здоровья
    /// </summary>
    [Serializable]
    public class Health : ComponentBase
    {
        /// <summary>
        /// Фактическая прочности / здоровья
        /// </summary>
        public float Fact;
        /// <summary>
        /// Максимальный запас прочности / здоровья
        /// </summary>
        public float Max;
    }

    /// <summary>
    /// Компонент ремонта / исцеления
    /// </summary>
    [Serializable]
    public class HealthReGeneration : ComponentBase
    {
        /// <summary>
        /// Ремонт / исцеление в секунду
        /// </summary>
        public float Regen = 1;
        /// <summary>
        /// Использование энергии в секунду
        /// </summary>
        public float EnargyUse = 10;
    }

    /// <summary>
    /// Компонент щита
    /// </summary>
    [Serializable]
    public class Shild : ComponentBase
    {
        /// <summary>
        /// Фактическая запас щита
        /// </summary>
        public float Fact;
        /// <summary>
        /// Максимальный запас щита
        /// </summary>
        public float Max;
    }

    /// <summary>
    /// Компонент регенерации щита
    /// Value/сек
    /// </summary>
    [Serializable]
    public class ShildReGeneration : ComponentBase
    {
        /// <summary>
        /// Регенерации щита в секунду
        /// </summary>
        public float Regen = 1;
        /// <summary>
        /// Использование энергии в секунду
        /// </summary>
        public float EnargyUse = 10;
    }

    /// <summary>
    /// Компонент трюма (вместимости в м³) 
    /// м³
    /// </summary>
    [Serializable]
    public class Hold : ComponentBase
    {
        /// <summary>
        /// Максимальная вместимость в м³
        /// </summary>
        public float Max;
        /// <summary>
        /// Занято емкости в м³
        /// </summary>
        public float Use;
    }

    /// <summary>
    /// Компонент веса
    /// Кг
    /// </summary>
    [Serializable]
    public class Weight : ComponentBase
    {
        /// <summary>
        /// Вес корабля (Кг)
        /// </summary>
        public float Fact;
    }
}