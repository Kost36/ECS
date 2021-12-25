using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Аттрибут приоритета системы
    /// 50(Min) => 0(Max) Priority;
    /// 0 - Максимальный приоритет;
    /// 49 - Минимальный приоритет;
    /// 50 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
    /// </summary>
    public class AttributeSystemPriority : Attribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="priority">
        /// 50(Min) => 0(Max) Priority;
        /// 0 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
        /// 1 - Максимальный приоритет;
        /// 50 - Минимальный приоритет;
        /// </param>
        public AttributeSystemPriority(int priority)
        {
            Priority = priority;
            if (Priority < 0)
            {
                Priority = 0;
            }
            if (Priority > 50)
            {
                Priority = 50;
            }
        }
        /// <summary>
        /// 50(Min) => 0(Max) Priority;
        /// 0 - Максимальный приоритет;
        /// 49 - Минимальный приоритет;
        /// 50 - Неприоритетная система (Невыполняется, если более приоритетные системы не успевают вовремя обрабатываться);
        /// </summary>
        public int Priority { get; set; } = 50;
    }
}
