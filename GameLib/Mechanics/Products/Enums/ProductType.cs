namespace GameLib.Mechanics.Products.Enums
{
    /// <summary>
    /// Тип продукта
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// Неизвестный
        /// </summary>
        Unknown = -1,

        #region 0 Уровень 
        // Восполняемые; 1 - 9

        /// <summary>
        /// Энергия
        /// </summary>
        Enargy = 1,
        /// <summary>
        /// Аккумулятор / батарея с энергией.
        /// 3 кВт
        /// </summary>
        Battery = 2,
        /// <summary>
        /// Вода
        /// </summary>
        Water = 3,

        #endregion

        #region 1 уровень - сырьё
        // Добываемые; 10 - 49 

        /// <summary>
        /// Лед
        /// </summary>
        Ice = 10,

        /// <summary>
        /// Железо
        /// </summary>
        Iron = 11,
        /// <summary>
        /// Медь
        /// </summary>
        Copper = 12,
        /// <summary>
        /// Титан
        /// </summary>
        Titanium = 13,
        /// <summary>
        /// Хром
        /// </summary>
        Сhrome = 14,
        /// <summary>
        /// Алюминий
        /// </summary>
        Aluminum = 23,
        //Олово
        //Свинец
        //Цинк
        //Серебро 
        //Золото

        /// <summary>
        /// Кремний
        /// </summary>
        Silicon = 15,
        /// <summary>
        /// Углерод
        /// </summary>
        Carbon = 16,
        /// <summary>
        /// Сера
        /// </summary>
        Sulfur = 24,

        /// <summary>
        /// Литий
        /// </summary>
        Lithium = 17,
        /// <summary>
        /// Магний
        /// </summary>
        Magnesium = 18,
        /// <summary>
        /// Кальций
        /// </summary>
        Calcium = 19,

        /// <summary>
        /// Гелий
        /// </summary>
        Helium = 20,
        /// <summary>
        /// Метан
        /// </summary>
        Methane = 21,
        /// <summary>
        /// Азот
        /// </summary>
        Nitrogen = 22,
        //Аргон
        //Неон

        #endregion

        #region 2 уровень - материалы
        // Производимое - простое; 50 - 99

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
        /// <summary>
        /// Водород
        /// </summary>
        Hydrogen,

        #endregion

        #region 3 уровень - еда 
        // Производимое; 100 - 199

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

        #region 4 уровень - материалы
        // Производимое - обычное; 200 - 299

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

        #region 5 уровень - товары
        // Производимое - сложное; 300 - 399   

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

        #region 6 уровень - оружие, дроны, турели, модули, ракеты
        // Производимое - высокотехнологичное; 1000 - 1999

        #endregion

        #region 7 уровень - cтанции, корабли
        // Производимое - высокотехнологичное; 2000 - 2999 

        #endregion
    }
}