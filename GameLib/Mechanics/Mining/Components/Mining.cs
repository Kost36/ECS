using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Добыча
    /// </summary>
    public class Mining : ComponentBase
    {
        /// <summary>
        /// Процент завершения цикла
        /// </summary>
        public float CycleCompletionPercentage;

        /// <summary>
        /// Пауза добычи
        /// </summary>
        public bool Pause;
    }
}
