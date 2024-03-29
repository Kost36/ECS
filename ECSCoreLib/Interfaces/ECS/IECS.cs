﻿using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.Interfaces.ECS
{
    /// <summary>
    /// Интерфейс ECS
    /// </summary>
    public interface IECS
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
        /// Уничтожить сущность по Id (компоненты сущности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        void RemoveEntity(Guid id);

        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <typeparam name="T"> Generic тип компонента </typeparam>
        /// <param name="component"> Компонент с заданным Id сущности, которой он пренадлежит </param>
        void AddComponent<T>(T component) where T : IComponent;

        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонент
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

        /// <summary>
        /// Пауза
        /// </summary>
        void Pause();

        /// <summary>
        /// Работа
        /// </summary>
        void Run();

        /// <summary>
        /// Задать скорость
        /// </summary>
        void SetSpeed(ECSSpeed eCSSpeed);

        /// <summary>
        /// Задать скорость
        /// </summary>
        /// <param name="speedRun"> Скорость в пределах: от 0.1 до 32 </param>
        /// <returns> Устанговленная скорость </returns>
        float SetSpeed(float speedRun);

        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        void Despose();
    }
}