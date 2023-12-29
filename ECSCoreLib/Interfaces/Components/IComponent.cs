using System;

namespace ECSCore.Interfaces.Components
{
    /// <summary>
    /// Интерфейс компонента
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Идентификатор сущьности, на которой находится компонент
        /// </summary>
        Guid Id { get; set; }
    }
}