using ECSCore.Interfaces.Components;

namespace ECSCore.Interfaces.Entitys
{
    /// <summary>
    /// Интерфейс сущьности
    /// </summary>
    internal interface IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        void Add<T>(T component)
            where T : IComponent;

        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool Get<T>(out T component)
            where T : IComponent;

        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void Remove<T>()
            where T : IComponent;

        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        void Death();
    }
}