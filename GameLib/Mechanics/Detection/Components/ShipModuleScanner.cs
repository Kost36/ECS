using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Detection.Components
{
    /// <summary>
    /// Разведовательный модуль корабля
    /// </summary>
    public class ShipModuleScanner : ComponentBase
    {
        /// <summary>
        /// Диапазон сканирования
        /// </summary>
        public long ScanerRange = 25000;

        /// <summary>
        /// Энергопотребление сканирования
        /// </summary>
        public int EnergyConsumption;

        /// <summary>
        /// Интервал сканирования (от 1 сек до 60 секунд)
        /// </summary>
        public int IntervalScanInSec = 60;

        /// <summary>
        /// Время предыдущего сканирования в тиках
        /// </summary>
        public long PreviousScanTicks;

        /// <summary>
        /// Время следующего сканирования в тиках
        /// </summary>
        public long NextScanTimeInTicks;

        /// <summary>
        /// Включено / Выключено
        /// </summary>
        public bool Enable = true;
    }

    //Модуль lvl1 = 5000 метров; 50 энергии
    //Модуль lvl2 = 10000 метров; 100 энергии
    //Модуль lvl3 = 20000 метров; 200 энергии
    //Модуль lvl4 = 40000 метров; 400 энергии
    //Модуль lvl5 = 80000 метров; 800 энергии
}
