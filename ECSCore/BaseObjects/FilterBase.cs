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
    public abstract class FilterBase : IFilter
    {
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Список заданий для фильтра
        /// </summary>
        public Queue<IJobToFilter> JobToFilters { get; set; } = new Queue<IJobToFilter>();
        /// <summary>
        /// Рассчитать входные данные
        /// </summary>
        public void Сalculate()
        {
            while (JobToFilters.Count > 0)
            {
                IJobToFilter jobToFilter = JobToFilters.Dequeue();
                jobToFilter.Action(this);
            } //Пока в коллекции что то есть
        }
        /// <summary>
        /// Проверяет группу на выбранные типы компонент
        /// </summary>
        /// <param name="typesComponet"></param>
        public abstract bool CheckFilter(List<Type> typesComponet);
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
            JobToFilters.Enqueue(new JobTryAdd(component, entity));
        }
        /// <summary>
        /// Удалить компонент из фильтра
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Remove(ComponentBase component, EntityBase entity)
        {
            JobToFilters.Enqueue(new JobTryRemove(component, entity));
        }
        /// <summary>
        /// Удалить из фильтра компоненты с Id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOfId(int id)
        {
            JobToFilters.Enqueue(new JobTryRemoveId(id));
        }

        public abstract void TryAdd(ComponentBase component, EntityBase entity);
        public abstract void TryRemove(ComponentBase component, EntityBase entity);
        public abstract void TryRemove(int id);
    }
}