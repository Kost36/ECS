using System;

namespace ECSCore.Interfaces.Components
{
    /// <summary>
    /// Интерфейс компонента
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Идентификатор сущности, на которой находится компонент
        /// </summary>
        Guid Id { get; set; }
    }
}