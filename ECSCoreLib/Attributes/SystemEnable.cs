using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут работы системы 
    /// </summary>
    public class SystemEnable : Attribute
    {
        /// <summary>
        /// Работа системы
        /// </summary>
        public bool IsEnable { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isEnable"></param>
        public SystemEnable(bool isEnable = true)
        {
            IsEnable = isEnable;
        }
    }
}