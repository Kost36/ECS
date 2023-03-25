namespace GameLib.Products
{
    /// <summary>
    /// Тип продукта
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// Неизвестный
        /// </summary>
        Unknown,

        #region Восполняемые
        /// <summary>
        /// Аккумулятор / батарея без энергии.
        /// Пустой
        /// </summary>
        BatteryEmpty,
        /// <summary>
        /// Аккумулятор / батарея с энергией.
        /// 3 кВт
        /// </summary>
        Battery,
        /// <summary>
        /// Грязная вода
        /// </summary>
        WaterDirty,
        /// <summary>
        /// Вода
        /// </summary>
        Water,
        #endregion

        #region Сырьё 0 уровень (Добываемые)
        /// <summary>
        /// Руда
        /// </summary>
        Ore,
        /// <summary>
        /// Песок
        /// </summary>
        Sand,
        /// <summary>
        /// Углерод
        /// </summary>
        Carbon,
        /// <summary>
        /// Водород
        /// </summary>
        Hydrogen,
        /// <summary>
        /// Азот
        /// </summary>
        Nitrogen,
        /// <summary>
        /// Метан
        /// </summary>
        Methane,
        /// <summary>
        /// Гелий
        /// </summary>
        Helium,
        #endregion

        #region Материалы 1 уровень (Производимое)
        /// <summary>
        /// Железо
        /// </summary>
        Iron,
        /// <summary>
        /// Медь
        /// </summary>
        Сopper,
        /// <summary>
        /// Алюминий
        /// </summary>
        Aluminum,
        /// <summary>
        /// Кремний
        /// </summary>
        Silicon,
        /// <summary>
        /// Сера
        /// </summary>
        Sulfur,
        /// <summary>
        /// Амиак
        /// </summary>
        Ammonia,
        /// <summary>
        /// Каучук
        /// </summary>
        Elastic,
        #endregion

        #region Материалы 2 уровень (Производимое)
        /// <summary>
        /// Металл
        /// </summary>
        Metal,
        /// <summary>
        /// Резина
        /// </summary>
        Rubber,
        /// <summary>
        /// Пластик
        /// </summary>
        Plastic,
        /// <summary>
        /// Провода
        /// </summary>
        Wiring,
        /// <summary>
        /// Кремниевая пластина
        /// </summary>
        SiliconWafer,
        /// <summary>
        /// Удобрение
        /// </summary>
        Fertilizer,
        /// <summary>
        /// Стекло
        /// </summary>
        Glass,
        #endregion

        #region Товары 3 уровень (Производимое) => Высокотехнологичные  
        /// <summary>
        /// Электроника
        /// </summary>
        Electronics,
        /// <summary>
        /// Системы управления
        /// </summary>
        ControlSystems,
        /// <summary>
        /// Корпус
        /// </summary>
        Body,
        /// <summary>
        /// Обшивка
        /// </summary>
        Sheathing,
        #endregion

        #region Товары 4 уровень (Производимое) => Оружие, дроны, турели, модули, ракеты

        #endregion

        #region Товары 5 уровень (Производимое) => Станции, корабли

        #endregion

        #region Еда (Производимое)
        /// <summary>
        /// Зерно
        /// </summary>
        Grain,
        /// <summary>
        /// Овощи 
        /// </summary>
        Vegetables,
        /// <summary>
        /// Фрукты  
        /// </summary>
        Fruit,
        /// <summary>
        /// Комбикорм  
        /// </summary>
        CompoundFeed,
        /// <summary>
        /// Рыба  
        /// </summary>
        Fish,
        /// <summary>
        /// Мясо  
        /// </summary>
        Meat,
        #endregion
    }
}
