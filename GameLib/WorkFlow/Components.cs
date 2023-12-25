using ECSCore.BaseObjects;
using System;

namespace GameLib.WorkFlow
{
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
}