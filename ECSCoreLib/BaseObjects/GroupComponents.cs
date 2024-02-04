using ECSCore.Interfaces.ECS;
using ECSCore.Interfaces.GroupComponents;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый объект группы компонент
    /// </summary>
    public abstract class GroupComponents : IGroupComponents
    {
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public abstract bool TryAddComponentForEntity(Guid entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public abstract bool TryRemoveComponentForEntity(Guid entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public abstract List<Type> GetTypesExistComponents();
    }
}