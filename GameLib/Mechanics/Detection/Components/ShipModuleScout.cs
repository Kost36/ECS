using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Detection.Components
{
    /// <summary>
    /// Разведовательный модуль корабля
    /// </summary>
    public class ShipModuleScout : ComponentBase
    {
        /// <summary>
        /// Диапазон сканирования
        /// </summary>
        public long ScanerRange = 25000;

        /// <summary>
        /// Энергопотребление сканирования
        /// </summary>
        public int EnergyConsumptionPerSec;

        /// <summary>
        /// Интервал сканирования (от 1 сек до 60 секунд)
        /// </summary>
        public int IntervalScanInSec = 60;

        /// <summary>
        /// Включено / Выключено
        /// </summary>
        public bool Enable = true;
    }


    //???
    //Модуль lvl1 = 25000 метров; 50 энергии
    //Модуль lvl2 = 50000 метров; 100 энергии
    //Модуль lvl3 = 100000 метров; 200 энергии
    //Модуль lvl4 = 200000 метров; 400 энергии
    //Модуль lvl5 = 400000 метров; 800 энергии
}
