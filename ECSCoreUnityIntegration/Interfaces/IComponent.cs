using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс компонента
    /// </summary>
    internal interface IComponent
    {
        /// <summary>
        /// Идентификатор сущьности, на которой находится компонент
        /// </summary>
        int Id { get; set; }
    }
}