using ECSCore.Interfaces.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Аттрибут исключающего компонента системы;
    /// </summary>
    public class AttributeExcludeComponentSystem : Attribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public AttributeExcludeComponentSystem(Type typeComponent)
        {
            if (typeComponent == null)
            {
                throw new NullReferenceException("Исключающий компонент незадан");
            } //Если не задан тип компонента
            if (typeComponent.GetInterfaces().Contains(typeof(IComponent))==false)
            {
                throw new ArgumentException("Переданый тип не реализует IComponent");
            } //Если задан тип, который не наследуется от IComponent
            ExcludeComponentType = typeComponent;
        }
        /// <summary>
        /// Тип исключающего компонента
        /// </summary>
        public Type ExcludeComponentType { get; set; }
    }
}