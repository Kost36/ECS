using ECSCore.Filters;
using ECSCore.Filters.Jobs;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.ECS;
using ECSCore.Interfaces.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс фильтров
    /// </summary>
    internal abstract class FilterBase : IFilter
    {
        #region IFilter Реализация
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        public IECSSystem ECSSystem { get; set; }
        /// <summary>
        /// Заинтересованные в фильтре системы
        /// </summary>
        public List<SystemBase> InterestedSystems { get; set; } = new List<SystemBase>();
        /// <summary>
        /// Добавить в фильтр заинтересеванную в нем систему
        /// </summary>
        /// <param name="system"> Ссылка на систему </param>
        public void AddInterestedSystem(SystemBase system)
        {
            InterestedSystems.Add(system);
        }
        #endregion

        #region IFilterDebug Реализуется наследником
        /// <summary>
        /// Количество отслеживаемых сущьностей в фильтре
        /// </summary>
        public abstract int Count { get; }

        //Отладка (Заполнение фильтра)
        internal int CountJobAdd;
        internal int CountJobRemove;
        internal int CountAdd;
        internal int CountRemove;
        internal int CountJobRemoveEntity;
        internal int CountNotAdd_Have;
        internal int CountNotAdd_TryAddComponentForEntity_IsFalse;
        #endregion

        #region IFilterInit Реализуется наследником
        /// <summary>
        /// Типы имеющихся компонент
        /// </summary>
        public abstract List<Type> TypesExistComponents { get; set; }
        /// <summary>
        /// Типы исключающихся компонент
        /// </summary>
        public abstract List<Type> TypesWithoutComponents { get; set; }
        /// <summary>
        /// Инициализация фильтра
        /// </summary>
        public abstract void Init();
        #endregion

        #region IFilterCheck Реализация
        /// <summary>
        /// Проверяет группу на необходимость обрабатывать компонент
        /// </summary>
        /// <param name="typeComponet"> Тип компонента </param>
        public bool ComponetTypeIsInteresting(Type typeComponet)
        {
            foreach (Type typeExistComponent in TypesExistComponents)
            {
                if (typeComponet.FullName == typeExistComponent.FullName)
                {
                    return true;
                }
            }
            foreach (Type typeWithoutComponent in TypesWithoutComponents)
            {
                if (typeComponet.FullName == typeWithoutComponent.FullName)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Проверяет группу на выбранные типы компонент
        /// </summary>
        /// <param name="typesExistComponents"> Типы компонент, которые должны быть на сущьности </param>
        /// <param name="typesWithoutComponents"> Типы компонент, которых недолжно быть на сущьности </param>
        /// <returns></returns>
        public bool CheckFilter(List<Type> typesExistComponents, List<Type> typesWithoutComponents)
        {
            if (typesExistComponents.Count == TypesExistComponents.Count)
            {
                if (typesWithoutComponents.Count == TypesWithoutComponents.Count)
                {
                    throw new NotImplementedException();
                }
            }
            return false;
        }
        #endregion

        #region IFilterAction Реализация
        private Stopwatch _stopwatch = new Stopwatch();
        /// <summary>
        /// Список заданий для фильтра
        /// </summary>
        public Queue<IJobToFilter> JobToFilters { get; set; } = new Queue<IJobToFilter>();
        /// <summary>
        /// Вычислить все входные задания
        /// </summary>
        public void СalculateJob()
        {
            lock (JobToFilters)
            {
                while (JobToFilters.Count > 0)
                {
                    IJobToFilter jobToFilter = JobToFilters.Dequeue();
                    jobToFilter.Action(this);
                } //Пока в коллекции что то есть
            }
        } //TODO Lock только на получение объекта из очереди. Add Performance
        /// <summary>
        /// Вычислять входные задания, некоторое время
        /// </summary>
        public void СalculateJob(long limitTimeTicks)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            lock (JobToFilters)
            {
                while (JobToFilters.Count > 0)
                {
                    IJobToFilter jobToFilter = JobToFilters.Dequeue();
                    jobToFilter.Action(this);
                    if (_stopwatch.ElapsedTicks > limitTimeTicks)
                    {
                        break;
                    }
                } //Пока в коллекции что то есть
            }
            _stopwatch.Stop();
        } //TODO Lock только на получение объекта из очереди. Add Performance
        /// <summary>
        /// Добавить, если сущьность подходит под фильтр
        /// </summary>
        /// <param name="component"> Добавленный к сущьности компонент </param>
        /// <param name="entity"> ссылка на сущьность </param>
        public void Add(IComponent component, Entity entity)
        {
            if (ComponetTypeIsInteresting(component.GetType()))
            {
                lock (JobToFilters)
                {
                    JobToFilters.Enqueue(new JobTryAdd(entity.Id));
                    CountJobAdd++;
                }
            } //Если фильтр интересуется данным компонентом
        }
        /// <summary>
        /// Удалить, если есть в фильтре
        /// </summary>
        /// <param name="entity"> ссылка на сущьность </param>
        public void Remove<T>(Entity entity)
        {
            if (ComponetTypeIsInteresting(typeof(T)))
            {
                lock (JobToFilters)
                {
                    JobToFilters.Enqueue(new JobTryRemove(entity.Id));
                    CountJobRemove++;
                }
            } //Если фильтр интересуется данным компонентом
        }
        /// <summary>
        /// Удалить из фильтра сущьность
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        public void RemoveEntity(int entityId)
        {
            lock (JobToFilters)
            {
                JobToFilters.Enqueue(new JobTryRemoveEntity(entityId));
                CountJobRemoveEntity++;
            }
        }
        #endregion

        #region IFilterActionGroup Реализуется наследником
        public abstract void TryAdd(int entityId);
        public abstract void TryRemove(int entityId);
        public abstract void TryRemoveEntity(int entityId);
        #endregion
    }
}