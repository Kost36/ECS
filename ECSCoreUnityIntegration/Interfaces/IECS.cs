using ECSCore.BaseObjects;
using ECSCore.Managers;
using System.Collections.Generic;
using System.Text;

namespace ECSCore.Interfaces
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
        bool GetEntity(int id, out Entity Entity);
        /// <summary>
        /// Уничтожить сущность по Id (компоненты сущности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        void RemoveEntity(int id);
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <typeparam name="T"> Generic тип компонента </typeparam>
        /// <param name="component"> Компонент с заданным Id сущности, которой он пренадлежит </param>
        void AddComponent<T>(T component) where T : Component;
        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонент
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущности, на которой должен быть компонент </param>
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