using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Ссылка на внешнюю сущность, в которой находится текущая сущность
        /// </summary>
        IEntity ExternalEntity { get; set; }

        /// <summary>
        /// Добавить вложенную сущность
        /// </summary>
        /// <param name="entity"> вложенная сущность </param>
        IEntity AddNestedEntity<T>(T entity)
            where T : IEntity;

        /// <summary>
        /// Получить вложенную сущность
        /// </summary>
        bool TryGetNestedEntity<T>(Guid idChildEntity, out T entity)
            where T : IEntity;

        /// <summary>
        /// Удалить вложенную сущность
        /// </summary>
        bool RemoveNestedEntity<T>(Guid idChildEntity, out T entity)
            where T : IEntity;

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        void AddComponent<T>(T component)
            where T : IComponent;

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool TryGetComponent<T>(out T component)
            where T : IComponent;

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool TryGetComponent(Type typeComponent, out IComponent component);

        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void RemoveComponent<T>()
            where T : IComponent;

        /// <summary>
        /// Уничтожить сущность
        /// </summary>
        void Death();
    }
}