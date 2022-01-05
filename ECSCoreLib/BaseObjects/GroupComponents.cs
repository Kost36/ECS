using ECSCore.Interfaces;
using ECSCore.Interfaces.ECS;
using ECSCore.Interfaces.GroupComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый объект группы компонент
    /// </summary>
    public abstract class GroupComponents : IGroupComponents
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности</param>
        /// <param name="eCS"> ссылка на ECS</param>
        /// <returns> </returns>
        public abstract bool TryAddComponentForEntity(int entityId, IECSSystem eCS);
        /// <summary>
        ///
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности</param>
        /// <param name="eCS"> ссылка на ECS</param>
        /// <returns> </returns>
        public abstract bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS);
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public abstract List<Type> GetTypesExistComponents();
        /// <summary>
        /// Получить список типов компонент, которых недолжно быть на сущьности
        /// </summary>
        /// <returns></returns>
        public abstract List<Type> GetTypesWithoutComponents();
    }
}