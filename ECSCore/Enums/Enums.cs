using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Enums
{
    /// <summary>
    /// Интервал обработки системы
    /// </summary>
    public enum SystemCalculateInterval
    {
        /// <summary>
        /// 60 Раз в секунду
        /// </summary>
        Sec60Once = 17,
        /// <summary>
        /// 30 Раз в секунду
        /// </summary>
        Sec30Once = 33,
        /// <summary>
        /// 20 Раз в секунду
        /// </summary>
        Sec20Once = 50,
        /// <summary>
        /// 10 Раз в секунду
        /// </summary>
        Sec10Once = 100,
        /// <summary>
        /// 5 Раз в секунду
        /// </summary>
        Sec5Once = 200,
        /// <summary>
        /// 3 Раза в секунду
        /// </summary>
        Sec3Once = 333,
        /// <summary>
        /// 2 Раза в секунду
        /// </summary>
        Sec2Once = 500,
        /// <summary>
        /// 1 Раз в секунду
        /// </summary>
        Sec1Once = 1000,
        /// <summary>
        /// 30 Раз в минуту / 1 Раз в 2 секунды
        /// </summary>
        Min30Once = 2000,
        /// <summary>
        /// 20 Раз в минуту / 1 Раз в 3 секунды
        /// </summary>
        Min20Once = 3000,
        /// <summary>
        /// 10 Раз в минуту / 1 Раз в 6 секунд
        /// </summary>
        Min10Once = 6000,
        /// <summary>
        /// 5 Раз в минуту / 1 Раз в 12 секунд
        /// </summary>
        Min5Once = 12000,
        /// <summary>
        /// 3 Раза в минуту / 1 Раз в 20 секунд
        /// </summary>
        Min3Once = 20000,
        /// <summary>
        /// 1 Раз в минуту / 1 Раз в 60 секунд
        /// </summary>
        Min1Once = 60000,
        /// <summary>
        /// 30 Раз в час / 1 Раз в 2 минуты
        /// </summary>
        Hour30Once = 120000,
        /// <summary>
        /// 10 Раз в час / 1 Раз в 6 минут
        /// </summary>
        Hour10Once = 360000,
        /// <summary>
        /// 5 Раз в час / 1 Раз в 12 минут
        /// </summary>
        Hour5Once = 720000,
        /// <summary>
        /// 1 Раз в час / 1 Раз в 60 минут
        /// </summary>
        Hour1Once = 3600000,
        /// <summary>
        /// 16 Раз в день / 1 Раз в 2 часа
        /// </summary>
        Day16Once = 7200000,
        /// <summary>
        /// 8 Раз в день / 1 Раз в 3 часа
        /// </summary>
        Day8Once = 10800000,
        /// <summary>
        /// 4 Раза в день / 1 Раз в 6 часов
        /// </summary>
        Day4Once = 21600000,
        /// <summary>
        /// 2 Раза в день / 1 Раз в 12 часов
        /// </summary>
        Day2Once = 43200000,
        /// <summary>
        /// 1 Раза в день / 1 Раз в 24 часа
        /// </summary>
        Day1Once = 86400000
    }
}
