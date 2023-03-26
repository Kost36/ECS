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
        /// Энергия
        /// </summary>
        Enargy,
        /// <summary>
        /// Аккумулятор / батарея с энергией.
        /// 3 кВт
        /// </summary>
        Battery,
        /// <summary>
        /// Вода
        /// </summary>
        Water,
        #endregion

        #region Сырьё 1 уровень (Добываемые)
        /// <summary>
        /// Грязная вода
        /// </summary>
        WaterDirty,
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
        /// <summary>
        /// Литий
        /// </summary>
        Lithium,
        #endregion

        #region Материалы 2 уровень (Производимое)
        /// <summary>
        /// Железо
        /// </summary>
        Iron,
        /// <summary>
        /// Медь
        /// </summary>
        Copper,
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
        /// <summary>
        /// Удобрение
        /// </summary>
        Fertilizer,
        #endregion

        #region Материалы 3 уровень (Производимое)
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
        /// Кремниевая пластина
        /// </summary>
        SiliconWafer,
        /// <summary>
        /// Стекло
        /// </summary>
        Glass,
        #endregion

        #region Товары 4 уровень (Производимое) => Высокотехнологичные          
        /// <summary>
        /// Провода
        /// </summary>
        Wiring,
        /// <summary>
        /// Аккумулятор / батарея без энергии.
        /// Пустой
        /// </summary>
        BatteryEmpty,
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

        #region Товары 5 уровень (Производимое) => Оружие, дроны, турели, модули, ракеты

        #endregion

        #region Товары 6 уровень (Производимое) => Станции, корабли

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
