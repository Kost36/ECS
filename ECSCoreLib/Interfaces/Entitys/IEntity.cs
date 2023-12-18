using ECSCore.Interfaces.Components;
using System;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс сущьности
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Ссылка на внешнюю сущьность, в которой находится текущая сущьность
        /// </summary>
        IEntity ExternalEntity { get; set; }

        /// <summary>
        /// Добавить вложенную сущьность
        /// </summary>
        /// <param name="entity"> вложенная сущьность </param>
        IEntity AddNestedEntity<T>(T entity)
            where T : IEntity;

        /// <summary>
        /// Получить вложенную сущьность
        /// </summary>
        bool TryGetNestedEntity<T>(int idChildEntity, out T entity)
            where T : IEntity;

        /// <summary>
        /// Удалить вложенную сущьность
        /// </summary>
        bool RemoveNestedEntity<T>(int idChildEntity, out T entity)
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
        /// Уничтожить сущьность
        /// </summary>
        void Death();
    }
}