﻿using ECSCore.Enums;
using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут частоты обработки системы;
    /// Задается одно из:
    /// * Желательный CPS (CalculatePerSecond),
    /// * Желательный интервал (мс),
    /// * Перечисление
    /// </summary>
    public sealed class SystemCalculate : Attribute
    {
        /// <summary>
        /// Интервал обработки системы (ms)
        /// </summary>
        public int CalculateInterval { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxСPS">
        /// Желательный CPS (CalculatePerSecond);
        /// Пределы: 0,016 - 60;
        /// 60    -> 60 раз  в    сек;
        /// 30    -> 30 раз  в    сек;
        /// 10    -> 10 раз  в    сек;
        /// 5     -> 5  раз  в    сек;
        /// 2     -> 2  раза в    сек;
        /// 1     -> 1  раз  в    сек;
        /// 0,5   -> 1  раз  в 2  сек;
        /// 0,3   -> 1  раз  в 3  сек;
        /// 0,2   -> 1  раз  в 5  сек;
        /// 0,1   -> 1  раз  в 10 сек;
        /// 0,05  -> 1  раз  в 20 сек;
        /// 0,033 ~> 1  раз  в 30 сек;
        /// 0,025 ~> 1  раз  в 40 сек;
        /// 0,02  -> 1  раз  в 50 сек;
        /// 0,016 ~> 1  раз  в    мин;
        /// </param>
        public SystemCalculate(float maxСPS)
        {
            if (maxСPS > 60)
            {
                maxСPS = 60;
            } //Верхний лимит
            else if (maxСPS < 0.016)
            {
                maxСPS = 0.016f;
            } //Нижний лимит

            CalculateInterval = (int)(1000 / maxСPS);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="interval">
        /// Желательный интервал (CalculateInterval) в милисекундах.
        /// Пределы: 17 - 86.400.000;
        /// 17         ~ 60 раз в секунду;
        /// 33         ~ 30 раз в секунду;
        /// 100        ~ 10 раз в секунду;
        /// 1000       = 1  раз в секунду;
        /// 60.000     = 1  раз в минуту;
        /// 3.600.000  = 1  раз в час;
        /// 43.200.000 = 1  раз в 12 часов;
        /// 86.400.000 = 1  раз в сутки;
        /// </param>
        public SystemCalculate(int interval)
        {
            if (interval > 86400000)
            {
                CalculateInterval = 86400000;
            } //Верхний лимит
            else if (interval < 17)
            {
                CalculateInterval = 17;
            } //Нижний лимит
            else
            {
                CalculateInterval = interval;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="systemCalculateInterval">
        /// Перечисление интервала обработки систем
        /// </param>
        public SystemCalculate(SystemCalculateInterval systemCalculateInterval)
        {
            CalculateInterval = (int)systemCalculateInterval;
        }
    }
}