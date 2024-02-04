using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс компонента.
    /// Все компоненты должны наследоваться от данного класса
    /// </summary>
    [Serializable]
    public abstract class ComponentBase : IComponent
    {
        /// <summary>
        /// Идентификатор сущьности, на которой висит компонент
        /// </summary>
        public Guid Id { get; set; }
    }
}