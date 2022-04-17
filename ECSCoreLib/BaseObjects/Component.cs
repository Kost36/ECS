using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс компонента.
    /// Все компоненты наследовать от данного класса
    /// </summary>
    [Serializable]
    public abstract class ComponentBase : IComponent
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }
    }
}