using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут работы системы 
    /// </summary>
    public class AttributeSystemEnable : Attribute
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isEnable"></param>
        public AttributeSystemEnable(bool isEnable = true)
        {
            IsEnable = isEnable;
        }
        
        #endregion
        /// <summary>
        /// Работа системы
        /// </summary>
        public bool IsEnable { get; private set; }
    }
}