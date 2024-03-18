using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. Система не имеет необходимого атрибута
    /// </summary>
    public sealed class SystemDoesNotHaveAttributeException : Exception
    {
        internal SystemDoesNotHaveAttributeException(Type attributeType, Type systemType)
        {
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
}