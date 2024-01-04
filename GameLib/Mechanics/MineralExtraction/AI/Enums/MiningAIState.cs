namespace GameLib.Mechanics.MineralExtraction.AI.Enums
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
        /// Ожидание / Анализ
        /// </summary>
        Wait,
        /// <summary>
        /// Запуск перемещение к астеройду
        /// </summary>
        StartMoveToAsteroid,
        /// <summary>
        /// Перемещение к астеройду
        /// </summary>
        MovingToAsteroid,
        /// <summary>
        /// Добыча c астеройда
        /// </summary>
        MiningFromAsteroid,
        /// <summary>
        /// Перемещение к станции
        /// </summary>
        MovingToStantion,
        /// <summary>
        /// Выгрузить на станцию
        /// </summary>
        UnloadToStantion,
        /// <summary>
        /// Продажа минералов
        /// </summary>
        SearchStantionForSale,
        /// <summary>
        /// Продажа
        /// </summary>
        SaleToStantion,
    }
}
