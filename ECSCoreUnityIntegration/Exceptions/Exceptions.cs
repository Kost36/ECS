using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. Система не имеет необъодимый атрибут
    /// </summary>
    public class ExceptionSystemNotHaveAttribute : Exception {
        internal ExceptionSystemNotHaveAttribute(Type attributeType, Type systemType) {
            AttributeType = attributeType;
            SystemType = systemType;
        }

        /// <summary>
        /// Тип атрибута, который должен быть задан для системы
        /// </summary>
        public Type AttributeType { get; set; }
        /// <summary>
        /// Тип системы, у которой не задан атрибут
        /// </summary>
        public Type SystemType { get; set; }
    }
    /// <summary>
    /// Исключение. ECS уже проинициализирован
    /// </summary>
    public class ExceptionECSIsInitializated : Exception
    {
        internal ExceptionECSIsInitializated(string msg) : base(message: msg)
        {
        }
    }
}