using ECSCore.Interface;
using ECSCore.Interfaces;
using System;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущьности
    /// </summary>
    public class EntityBase : IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        public void Add(ComponentBase component)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns> BaseComponent / null </returns>
        public ComponentBase Get<T>()
            where T : ComponentBase
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : ComponentBase
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        public void Death()
        {
            throw new NotImplementedException();
        }
    }
}