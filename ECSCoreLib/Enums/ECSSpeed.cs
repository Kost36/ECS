namespace ECSCore.Enums
{
    /// <summary>
    /// Скорость выполнения систем;
    /// Регулируется, путем множителя deltaTime у систем
    /// </summary>
    public enum ECSSpeed
    {
        /// <summary>
        /// Пауза
        /// </summary>
        Pause,
        /// <summary>
        /// Нормальная скорость X1
        /// </summary>
        Run,
        /// <summary>
        /// Замедление X0.5
        /// </summary>
        X_0_5,
        /// <summary>
        /// Ускорение X2
        /// </summary>
        X_2_0,
        /// <summary>
        /// Ускорение X4
        /// </summary>
        X_4_0,
        /// <summary>
        /// Ускорение X8
        /// </summary>
        X_8_0,
        /// <summary>
        /// Ускорение X16
        /// </summary>
        X_16_0,
    }
}