using ECSCore.BaseObjects;

namespace GameLib.Mechanics.MineralExtraction.Components.Commands
{
    /// <summary>
    /// Команда добычи с астеройда
    /// </summary>
    public class ShipCommandMining : ComponentBase
    {
        /// <summary>
        /// Компонент минералов на целевом астеройде
        /// </summary>
        public AsteroidMineral RefAsteroidMineral;

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
