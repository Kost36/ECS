﻿using ECSCore.Managers;
using System.Text;

namespace ECSCore.Interfaces.ECS
{
    /// <summary>
    /// ECS Debug
    /// </summary>
    public interface IECSDebug
    {
        /// <summary>
        /// Менеджер сущностей
        /// </summary>
        ManagerEntitys ManagerEntitys { get; }

        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        ManagerFilters ManagerFilters { get; }

        /// <summary>
        /// Менеджер систем
        /// </summary>
        ManagerSystems ManagerSystems { get; }

        /// <summary>
        /// Информация о состоянии ECS
        /// </summary>
        /// <param name="smallInfo"> Флаг кораткой информации </param>
        /// <returns></returns>
        StringBuilder GetInfo(bool smallInfo = false);
    }
}