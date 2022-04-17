using ECSCore.Interfaces.Components;

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
        /// Родительская сущьность
        /// </summary>
        IEntity ParentEntity { get; set; }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        void Add<T>(T component)
            where T : IComponent;

        /// <summary>
        /// Добавить дочернюю сущьность
        /// </summary>
        /// <param name="entity"> дочерняя сущьность </param>
        IEntity AddChild<T>(T entity)
            where T : IEntity;

        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool Get<T>(out T component)
            where T : IComponent;

        /// <summary>
        /// Удалить дочернюю сущьность
        /// </summary>
        bool GetChild<T>(int idChildEntity, out T entity)
            where T : IEntity;

        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void Remove<T>()
            where T : IComponent;

        /// <summary>
        /// Удалить дочернюю сущьность
        /// </summary>
        bool RemoveChild<T>(int idChildEntity, out T entity)
            where T : IEntity;

        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        void Death();
    }
}