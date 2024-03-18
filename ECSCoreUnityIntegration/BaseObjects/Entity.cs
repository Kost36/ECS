using ECSCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущности.
    /// Все сущности наследовать от данного класса
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        public void Add<T>(T component)
            where T : Component
        {
            component.Id = this.Id;
            ECS.Instance.AddComponent(component, this);
        }
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool Get<T>(out T component)
            where T : Component
        {
            return GetComponent(out component);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : Component
        {
            ECS.Instance.RemoveComponent<T>(this.Id, this);
        }
        /// <summary>
        /// Уничтожить сущность
        /// </summary>
        public void Death()
        {
            ECS.Instance.RemoveEntity(this.Id);
        }
        /// <summary>
        /// Для отслеживания в тестах
        /// </summary>
        public List<Component> Components { get; } = new List<Component>();

        /// <summary>
        /// Добавить компонент в коллекцию сущности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        internal void AddComponent<T>(T component)
            where T:Component
        {
            if (GetComponent(out T Component))
            {
                Component = component;
                return;
            } //Если нету
            lock (Components)
            {
                Components.Add(component);
            }
        }
        /// <summary>
        /// Удалить компонент из коллекции сущности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        internal void RemoveComponent<T>()
            where T : Component
        {
            if (GetComponent(out T Component))
            {
                lock (Components)
                {
                    Components.Remove(Component);
                }
            } //Если есть в коллекции
        }
        /// <summary>
        /// Получить компонент из своего списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        private bool GetComponent<T>(out T component)
            where T : Component
        {
            lock (Components)
            {
                foreach (Component Component in Components)
                {
                    if (Component is T)
                    {
                        component = (T)Component;
                        return true;
                    }
                }
            }
            component = default(T);
            return false;
        }
    }
}