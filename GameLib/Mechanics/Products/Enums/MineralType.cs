namespace GameLib.Mechanics.Products.Enums
{
    /// <summary>
    /// Минералы добываемые на астеройдах
    /// </summary>
    public enum MineralType
    {
        /// <summary>
        /// Неизвестный
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Лед
        /// -> Вода -> Водород, Кислород 
        /// </summary>
        Ice = ProductType.Ice,

        /// <summary>
        /// Железо
        /// </summary>
        Iron = ProductType.Iron,
        /// <summary>
        /// Медь
        /// </summary>
        Сopper = ProductType.Copper,
        /// <summary>
        /// Титан
        /// </summary>
        Titanium = ProductType.Titanium,
        /// <summary>
        /// Хром
        /// </summary>
        Сhrome = ProductType.Сhrome,
        /// <summary>
        /// Алюминий
        /// </summary>
        Aluminum = ProductType.Aluminum,
        //Олово
        //Свинец
        //Цинк
        //Серебро 
        //Золото

        /// <summary>
        /// Кремний
        /// </summary>
        Silicon = ProductType.Silicon,
        /// <summary>
        /// Углерод
        /// </summary>
        Carbon = ProductType.Carbon,
        /// <summary>
        /// Сера
        /// </summary>
        Sulfur = ProductType.Sulfur,

        /// <summary>
        /// Литий
        /// </summary>
        Lithium = ProductType.Lithium,
        /// <summary>
        /// Магний
        /// </summary>
        Magnesium = ProductType.Magnesium,
        /// <summary>
        /// Кальций
        /// </summary>
        Calcium = ProductType.Calcium,

        /// <summary>
        /// Азот
        /// </summary>
        Nitrogen = ProductType.Helium,
        /// <summary>
        /// Метан
        /// </summary>
        Methane = ProductType.Methane,
        /// <summary>
        /// Гелий
        /// </summary>
        Helium = ProductType.Helium,
        //Аргон
        //Неон
    }
}
