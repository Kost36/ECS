namespace ECSCore.Enums
{
    /// <summary>
    /// Тип выполнения системы
    /// </summary>
    internal enum SystemActionType
    {
        /// <summary>
        /// Выполнить в текущем потоке
        /// </summary>
        RunInThisThread,
        /// <summary>
        /// Выполнить в одном отдельном потоке
        /// </summary>
        RunInOneThread,
        /// <summary>
        /// Выполнить в нескольких отдельных потоках
        /// </summary>
        RunInThreads,
        /// <summary>
        /// Выполнить в введенном потоке
        /// </summary>
        RunInInjectThread,
    }
}