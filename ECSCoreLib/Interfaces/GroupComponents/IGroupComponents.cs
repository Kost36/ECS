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
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity);

        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        List<Type> GetTypesExistComponents();
    }
}