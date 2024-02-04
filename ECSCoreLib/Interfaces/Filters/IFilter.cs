using ECSCore.BaseObjects;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.ECS;
using System;
using System.Collections.Generic;

namespace ECSCore.Interfaces.Filters
{
    /// <summary>
    /// Интерфейс фильтра группы компонент
    /// </summary>
    internal interface IFilter : IFilterActionGroup, IFilterAction, IFilterInit, IFilterDebug 
    { 
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        IECSSystem ECSSystem { get; set; }

        /// <summary>
        /// Заинтересованные в фильтре системы
        /// </summary>
        List<SystemBase> InterestedSystems { get; set; }

        /// <summary>
        /// Добавить в фильтр заинтересеванную в нем систему
        /// </summary>
        /// <param name="system"> Ссылка на систему </param>
        void AddInterestedSystem(SystemBase system);
    }

    /// <summary>
    /// Интерфейс инициализации фильтра
    /// </summary>
    internal interface IFilterInit
    {
        /// <summary>
        /// Типы имеющихся компонент
        /// </summary>
        List<Type> TypesExistComponents { get; set; }
        /// <summary>
        /// Типы исключающихся компонент
        /// </summary>
        List<Type> TypesWithoutComponents { get; set; }

        /// <summary>
        /// Инициализация фильтра
        /// </summary>
        void Init();
    }

    /// <summary>
    /// Интерфейс проверки фильтра
    /// </summary>
    internal interface IFilterCheck
    {
        /// <summary>
        /// Проверяет группу на выбранные типы компонент
        /// </summary>
        /// <param name="typesExistComponents"> Типы компонент, которые должны быть на сущьности </param>
        /// <param name="typesWithoutComponents"> Типы компонент, которых недолжно быть на сущьности </param>
        /// <returns></returns>
        bool CheckFilter(List<Type> typesExistComponents, List<Type> typesWithoutComponents);

        /// <summary>
        /// Проверяет группу на необходимость обрабатывать компонент
        /// </summary>
        /// <param name="typeComponet"> Тип компонента </param>
        bool ComponetTypeIsInteresting(Type typeComponet);
    }

    /// <summary>
    /// Интерфейс отладочной информации
    /// </summary>
    internal interface IFilterDebug
    {
        /// <summary>
        /// Колличество сущьностей в фильтре
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// Интерфейс действия для фильтра
    /// </summary>
    internal interface IFilterAction
    {
        void СalculateJob();

        void СalculateJob(long limitTimeTicks);

        /// <summary>
        /// Список заданий для фильтра
        /// </summary>
        Queue<IJobToFilter> JobToFilters { get; set; }

        /// <summary>
        /// Добавить, если сущьность подходит под фильтр
        /// </summary>
        /// <param name="component"> Добавленный к сущьности компонент </param>
        /// <param name="entity"> ссылка на сущьность </param>
        void Add(IComponent component, Entity entity);
        /// <summary>
        /// Удалить, если есть в фильтре
        /// </summary>
        /// <param name="entity"> ссылка на сущьность </param>
        void Remove<T>(Entity entity);
        /// <summary>
        /// Удалить сущьность
        /// </summary>
        /// <param name="entityId"> Идентификатор уничтоженной сущьности</param>
        void RemoveEntity(Guid entityId);
    }

    /// <summary>
    /// Интерфейс действия для группы компонент в фильтре
    /// </summary>
    internal interface IFilterActionGroup
    {
        void TryAdd(Guid entityId);
        void TryRemove(Guid entityId);
        void TryRemoveEntity(Guid entityId);
    }
}