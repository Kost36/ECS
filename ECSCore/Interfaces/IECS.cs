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
        /// Добавить сущьность в ECS
        /// </summary>
        /// <param name="entity"> Сущьность, которая будет добавлена в ECS (Произойдет автоматическое присвоентие Id)</param>
        /// <returns></returns>
        public EntityBase AddEntity(EntityBase entity);
        /// <summary>
        /// Получить сущьность из ECS, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="entityBase"> Сущьность (Если найдена) / null </param>
        /// <returns> Результат получения сущьности </returns>
        public bool GetEntity(int id, out EntityBase entityBase);
        /// <summary>
        /// Уничтожить сущьность по Id (компоненты сущьности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void RemoveEntity(int id);
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <typeparam name="T"> Generic тип компонента </typeparam>
        /// <param name="component"> Компонент с заданным Id сущьности, которой он пренадлежит </param>
        public void AddComponent<T>(T component) where T : ComponentBase;
        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонент
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <param name="idEntity"> Идентификатор сущьности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool GetComponent<T>(int idEntity, out T component) where T : ComponentBase;
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void RemoveComponent<T>(int idEntity) where T : ComponentBase;
    }
}