﻿using ECSCore.BaseObjects;
using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.Interfaces.ECS
{
    /// <summary>
    /// Интерфейс взаимодействия с ECS из систем
    /// </summary>
    public interface IECSSystem
    {
        /// <summary>
        /// Добавить сущность в ECS
        /// </summary>
        /// <param name="entity"> Сущность, которая будет добавлена в ECS (Произойдет автоматическое присвоентие Id)</param>
        /// <returns></returns>
        Entity AddEntity(Entity entity);

        /// <summary>
        /// Получить сущность из ECS, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        /// <param name="Entity"> Сущность (Если найдена) / null </param>
        /// <returns> Результат получения сущности </returns>
        bool GetEntity(Guid id, out Entity Entity);

        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущности, которой он пренадлежит </param>
        void AddComponent<T>(T component) where T : IComponent;

        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонентов
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool GetComponent<T>(Guid idEntity, out T component) where T : IComponent;

        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void RemoveComponent<T>(Guid idEntity) where T : IComponent;
    }
}