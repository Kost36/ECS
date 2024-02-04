namespace GameLib.Constants
{
    /// <summary>
    /// Константы
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Максимальная скорость = 15.000 м/сек.
        /// На основе:
        /// 28.000 Км/ч - МКС
        /// 60.000 Км/ч - Вояджер ~ 16.666 М/сек
        /// </summary>
        public const float MaxSpeed = 15000;
        /// <summary>
        /// Размер сектора в км
        /// </summary>
        public static readonly SectorSize SectorSize = new SectorSize();
    }
}
