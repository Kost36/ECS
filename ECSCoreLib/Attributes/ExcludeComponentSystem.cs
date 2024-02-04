using ECSCore.Interfaces.Components;
using System;
using System.Linq;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Аттрибут исключающего компонента системы;
    /// Если у системы задан аттрибут с исключающим компонентом, 
    /// то в систему могут попадать только те сущьности, у которых отсутствует заданный компонент.
    /// </summary>
    public class ExcludeComponentSystem : Attribute
    {
        /// <summary>
        /// Тип исключающего компонента
        /// </summary>
        public Type ExcludeComponentType { get; set; }

        public ExcludeComponentSystem(Type typeComponent)
        {
            if (typeComponent == null)
            {
                throw new NullReferenceException("Исключающий компонент не задан");
            }

            if (typeComponent.GetInterfaces().Contains(typeof(IComponent))==false)
            {
                throw new ArgumentException("Переданый тип не реализует IComponent");
            }

            ExcludeComponentType = typeComponent;
        }
    }
}