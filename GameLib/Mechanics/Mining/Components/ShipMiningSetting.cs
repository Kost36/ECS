using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Настройки майнинга
    /// </summary>
    public class ShipMiningSetting : ComponentBase
    {
        /// <summary>
        /// Максимальный процент заполнения добываемым материалом
        /// </summary>
        public int MaxPercentageFillingMaterial = 90;

        /// <summary>
        /// Минимальный процент энергии (сколько энергии должно оставаться у корабля при добыче)
        /// </summary>
        public int MinPercentageEnergy = 50;
    }
}
