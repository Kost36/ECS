﻿using ECSCore.Filters;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public abstract int Count { get; }
        /// <summary>
        /// Стартовая вместимость элементов в фильтре
        /// </summary>
        public int Capacity { get; set; } = 10;
        /// <summary>
        /// Список заданий для фильтра
        /// </summary>
        public Queue<IJobToFilter> JobToFilters { get; set; } = new Queue<IJobToFilter>();

        /// <summary>
        /// Рассчитать входные данные
        /// </summary>
        public void Сalculate()
        {
            lock (JobToFilters)
            {
                while (JobToFilters.Count > 0)
                {
                    IJobToFilter jobToFilter = JobToFilters.Dequeue();
                    jobToFilter.Action(this);
                } //Пока в коллекции что то есть
            }
        }
        /// <summary>
        /// Проверяет группу на выбранные типы компонент
        /// </summary>
        /// <param name="typesComponet"></param>
        public abstract bool CheckFilter(List<Type> typesComponet);
        /// <summary>
        /// Проверяет группу на необходимость обрабатывать компонент
        /// </summary>
        /// <param name="typesComponet"></param>
        public abstract bool ComponetTypeIsInteresting(Type typeComponet);
        /// <summary>
        /// Получить список типов компонент в фильтре
        /// </summary>
        public abstract List<Type> GetTypesComponents();
        /// <summary>
        /// Добавить компонент к фильтру
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Add(ComponentBase component, EntityBase entity)
        {
            if (ComponetTypeIsInteresting(component.GetType()))
            {
                lock (JobToFilters)
                {
                    JobToFilters.Enqueue(new JobTryAdd(component, entity));
                }
            } //Если фильтр интересуется данным компонентом
        }
        /// <summary>
        /// Удалить компонент из фильтра
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Remove<T>(EntityBase entity)
        {
            if (ComponetTypeIsInteresting(typeof(T)))
            {
                lock (JobToFilters)
                {
                    JobToFilters.Enqueue(new JobTryRemove(typeof(T), entity));
                }
            } //Если фильтр интересуется данным компонентом
        }
        /// <summary>
        /// Удалить из фильтра компоненты с Id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOfId(int id)
        {
            lock (JobToFilters)
            {
                JobToFilters.Enqueue(new JobTryRemoveId(id));
            }
        }

        public abstract void Init(int capacity);
        public abstract void TryAdd(ComponentBase component, EntityBase entity);
        public abstract void TryRemove(Type typeComponent, EntityBase entity);
        public abstract void TryRemove(int id);
    }
}