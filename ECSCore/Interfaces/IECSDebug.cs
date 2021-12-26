using ECSCore.BaseObjects;
using ECSCore.Managers;
using System.Collections.Generic;
using System.Text;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// ECS Debug
    /// </summary>
    public interface IECSDebug
    {
        /// <summary>
        /// Менеджер сущьностей
        /// </summary>
        public ManagerEntitys ManagerEntitys { get; }
        /// <summary>
        /// Менеджер компонент
        /// </summary>
        public ManagerComponents ManagerComponents { get; }
        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        public ManagerFilters ManagerFilters { get; }
        /// <summary>
        /// Менеджер систем
        /// </summary>
        public ManagerSystems ManagerSystems { get; }
        /// <summary>
        /// Информация о состоянии ECS
        /// </summary>
        /// <param name="smallInfo"> Флаг кораткой информации </param>
        /// <returns></returns>
        public StringBuilder GetInfo(bool smallInfo = false);
    }
}