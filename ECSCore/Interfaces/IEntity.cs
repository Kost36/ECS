using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interface
{
    /// <summary>
    /// Интерфейс сущьности
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        public void Add(IComponent component);
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns> BaseComponent / null </returns>
        public IComponent Get<T>()
            where T : IComponent;
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : IComponent;
        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        public void Death();
    }
}