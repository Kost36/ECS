﻿using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс фильтра группы компонент
    /// </summary>
    internal interface IFilter
    {
        /// <summary>
        /// Колличество элементов в фильтре
        /// </summary>
        public abstract int Count { get; }
        /// <summary>
        /// Стартовая вместимость элементов в фильтре
        /// </summary>
        public abstract int Capacity { get; set; }
        /// <summary>
        /// Инициализация фильтра
        /// </summary>
        /// <param name="capacity"></param>
        public void Init(int capacity);
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
        public void Add(IComponent component, Entity entity);
        /// <summary>
        /// Удали, если есть
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity"></param>
        public void Remove<T>(Entity entity);
        /// <summary>
        /// Удали, если есть
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOfId(int id);
    }
}