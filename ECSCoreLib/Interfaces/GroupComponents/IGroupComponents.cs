using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using System;
using System.Collections.Generic;

namespace ECSCore.Interfaces.GroupComponents
{
    /// <summary>
    /// Интерфейс объекта группы компонент
    /// </summary>
    internal interface IGroupComponents
    {
        /// <summary>
        /// Проверить, удовлетворяет ли сущность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущность </param>
        /// <returns> </returns>
        bool TryAddComponentForEntity(Guid entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Проверить, удовлетворяет ли сущность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущность </param>
        /// <returns> </returns>
        bool TryRemoveComponentForEntity(Guid entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущности
        /// </summary>
        /// <returns></returns>
        List<Type> GetTypesExistComponents();
    }
}