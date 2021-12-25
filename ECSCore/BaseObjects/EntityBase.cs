using ECSCore.Interface;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущьности
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        public void Add<T>(T component)
            where T : ComponentBase
        {
            component.Id = this.Id;
            ECS.Instance.AddComponent<T>(component);
        }
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns> BaseComponent / null </returns>
        public ComponentBase Get<T>()
            where T : ComponentBase
        {
            return ECS.Instance.GetComponent<T>(this.Id);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : ComponentBase
        {
            ECS.Instance.RemoveComponent<T>(this.Id);
        }
        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        public void Death()
        {
            ECS.Instance.RemoveEntity(this.Id);
        }

        /// <summary>
        /// Для отслеживания в тестах
        /// </summary>
        public List<ComponentBase> _components { get { return ECS.Instance.GetComponents(Id); } }
    }
}