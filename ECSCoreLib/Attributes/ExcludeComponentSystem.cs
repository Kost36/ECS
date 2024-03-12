using ECSCore.Interfaces.Components;
using System;
using System.Linq;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Если у системы задан атрибут с исключающим компонентом,
    /// то в систему могут попадать только те сущности, у которых отсутствует заданный компонент.
    /// </summary>
    public sealed class ExcludeComponentSystem : Attribute
    {
        /// <summary>
        /// Тип исключающего компонента
        /// </summary>
        public Type ExcludeComponentType { get; private set; }

        public ExcludeComponentSystem(Type typeComponent)
        {
            if (typeComponent == null)
            {
                throw new NullReferenceException("Исключающий компонент не задан");
            }

            if (typeComponent.GetInterfaces().Contains(typeof(IComponent)) == false)
            {
                throw new ArgumentException($"{typeComponent.FullName} не реализует {nameof(IComponent)}");
            }

            ExcludeComponentType = typeComponent;
        }
    }
}