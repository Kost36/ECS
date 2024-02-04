namespace GameLib.Mechanics.Production.Datas
{
    /// <summary>
    /// Количество товара
    /// </summary>
    public sealed class Count
    {
        /// <summary>
        /// Количество
        /// </summary>
        public int Value;
        /// <summary>
        /// Максимальное количество
        /// </summary>
        public int MaxLimit = int.MaxValue;
    }
}
