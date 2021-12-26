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
        public EntityBase AddEntity(EntityBase entity);
        /// <summary>
        /// Получить сущьность из ECS, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="entityBase"> Сущьность (Если найдена) / null </param>
        /// <returns> Результат получения сущьности </returns>
        public bool GetEntity(int id, out EntityBase entityBase);
    }
}