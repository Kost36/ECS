using ECSCore.BaseObjects;
using ECSCore.Interface;
using System;
using System.Collections.Generic;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс фильтра группы компонент
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public abstract int Count { get;}
        /// <summary>
        /// Проверяет группу на выбранные типы компонент
        /// </summary>
        /// <param name="typesComponet"></param>
        public bool CheckFilter(List<Type> typesComponet);
        /// <summary>
        /// Получить список типов компонент в фильтре
        /// </summary>
        public List<Type> GetTypesComponents();
        /// <summary>
        /// Добавь, если подходит
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Add(ComponentBase component, EntityBase entity);
        /// <summary>
        /// Удали, если есть
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Remove<T>(EntityBase entity);
        /// <summary>
        /// Удали, если есть
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOfId(int id);
    }
}