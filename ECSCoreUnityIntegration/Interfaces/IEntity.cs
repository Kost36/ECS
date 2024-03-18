using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    internal interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        void Add<T>(T component)
            where T : Component;
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        bool Get<T>(out T component)
            where T : Component;
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        void Remove<T>()
            where T : Component;
        /// <summary>
        /// Уничтожить сущность
        /// </summary>
        void Death();
    }
}