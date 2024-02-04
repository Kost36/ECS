namespace ECSCore.Enums
{
    /// <summary>
    /// Контроль задержки выполнения / вычисления
    /// </summary>
    internal enum ControlTypeDelay
    {
        /// <summary>
        /// Не контролировать задержку выполнения
        /// </summary>
        Not,
        /// <summary>
        /// Контролировать выполнение системы
        /// </summary>
        DelayRunSystem,
        /// <summary>
        /// Контролировать вычиследние фильтра системы
        /// </summary>
        DelayCalculateFiltersSystem
    }
}