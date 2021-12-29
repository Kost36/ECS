using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс компонента
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Идентификатор сущьности, на которой находится компонент
        /// </summary>
        int Id { get; set; }
    }
}