using ECSCore.BaseObjects;
using ECSCore.Interfaces.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interfaces.GroupComponents
{
    /// <summary>
    /// Интерфейс объекта группы компонент
    /// </summary>
    internal interface IGroupComponents
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="eCS"> Ссылка на сущьность </param>
        /// <returns> </returns>
        bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity, bool flagTest);
        /// <summary>
        ///
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности</param>
        /// <param name="eCS"> ссылка на ECS</param>
        /// <returns> </returns>
        bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity);
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        List<Type> GetTypesExistComponents();
        /// <summary>
        /// Получить список типов компонент, которых недолжно быть на сущьности
        /// </summary>
        /// <returns></returns>
        List<Type> GetTypesWithoutComponents();
    }
}