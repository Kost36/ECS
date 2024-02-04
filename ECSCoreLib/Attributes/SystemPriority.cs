using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Аттрибут приоритета системы;
    /// 50 - min priority => 1 - max priority;
    /// 1 - Максимальный приоритет;
    /// 50 - Минимальный приоритет;
    /// 0 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
    /// </summary>
    public class SystemPriority : Attribute
    {
        /// <summary>
        /// 50 - min priority => 1 - max priority;
        /// 1 - Максимальный приоритет;
        /// 50 - Минимальный приоритет;
        /// 0 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
        /// </summary>
        public int Priority { get; set; } = 50;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="priority">
        /// 50 - min priority => 1 - max priority;
        /// 1 - Максимальный приоритет;
        /// 50 - Минимальный приоритет;
        /// 0 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
        /// </param>
        public SystemPriority(int priority)
        {
            if (priority < 0)
            {
                priority = 0;
            }
            if (priority > 50)
            {
                priority = 50;
            }

            Priority = priority;
        }
    }
}