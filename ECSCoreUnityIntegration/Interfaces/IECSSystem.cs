using ECSCore.BaseObjects;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с ECS из систем
    /// </summary>
    public interface IECSSystem
    {
        /// <summary>
        /// Добавить сущьность в ECS
        /// </summary>
        /// <param name="entity"> Сущьность, которая будет добавлена в ECS (Произойдет автоматическое присвоентие Id)</param>
        /// <returns></returns>
        Entity AddEntity(Entity entity);
        /// <summary>
        /// Получить сущьность из ECS, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="Entity"> Сущьность (Если найдена) / null </param>
        /// <returns> Результат получения сущьности </returns>
        bool GetEntity(int id, out Entity Entity);
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущьности, которой он пренадлежит </param>
        void AddComponent<T>(T component) where T : Component;
        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонентов
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущьности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool GetComponent<T>(int idEntity, out T component) where T : Component;
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void RemoveComponent<T>(int idEntity) where T : Component;
    }
}