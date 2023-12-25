namespace GameLib.Mechanics.Mining.AI.Enums
{
    //Начальное состояние Wait -> В этом состоянии AI анализирует последующие действия (снабжать станцию/добывать на продажу/какой минерал будет добывать и т.д.)
    //Далее состояние может перейти в SearchAsteroid -> поиск астеройда для добычи.
    //Если AI нашел астеройд для добычи или пришла команда добывать с астеройда, то состояние меняется на MovingToAsteroid
    //После подлета к астеройду происходит проверка и после завершение проверки состояние становится Mining
    //После завершения добычи, 

    /// <summary>
    /// Состояние искуственного интелекта шахтера
    /// </summary>
    public enum MiningAIState
    {
        /// <summary>
        /// Ожидание
        /// </summary>
        Wait,
        /// <summary>
        /// Поиск астеройда
        /// </summary>
        SearchAsteroid,
        /// <summary>
        /// Перемещение к астеройду
        /// </summary>
        MovingToAsteroid,
        /// <summary>
        /// Добыча
        /// </summary>
        Mining,
        /// <summary>
        /// Продажа минералов
        /// </summary>
        SearchStantionForSale,
        /// <summary>
        /// Перемещение к станции
        /// </summary>
        MovingToStantion,
        /// <summary>
        /// Продажа
        /// </summary>
        Sale,
    }
}
