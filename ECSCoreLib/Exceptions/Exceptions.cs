using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// <summary>
    /// Исключение. Нет проинициализированных систем
    /// </summary>
    public class ExceptionECSHaveNotSystem : Exception
    {
        internal ExceptionECSHaveNotSystem(string msg) : base(message: msg)
        {
        }
    }
    /// <summary>
    /// Исключение. У сущьности уже есть данный компонент
    /// </summary>
    public class ExceptionEntityHaveComponent : Exception
    {
        internal ExceptionEntityHaveComponent(string msg) : base(message: msg)
        {
        }
    }
}