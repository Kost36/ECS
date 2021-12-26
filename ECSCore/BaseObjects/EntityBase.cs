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
            ECS.Instance.AddComponent(component, this);
        }
        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool Get<T>(out T component)
            where T : ComponentBase
        {
            //return ECS.Instance.GetComponent<T>(this.Id, out component);
            return GetComponent(out component);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : ComponentBase
        {
            ECS.Instance.RemoveComponent<T>(this.Id, this);
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
        public List<ComponentBase> Components { get; } = new List<ComponentBase>();

        /// <summary>
        /// Добавить компонент в коллекцию сущьности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public void AddComponent<T>(T component)
            where T:ComponentBase
        {
            if (GetComponent(out T componentBase))
            {
                componentBase = component;
                return;
            } //Если нету
            lock (Components)
            {
                Components.Add(component);
            }
        }
        /// <summary>
        /// Удалить компонент из коллекции сущьности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public void RemoveComponent<T>()
            where T : ComponentBase
        {
            if (GetComponent(out T componentBase))
            {
                lock (Components)
                {
                    Components.Remove(componentBase);
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
            where T : ComponentBase
        {
            lock (Components)
            {
                foreach (ComponentBase componentBase in Components)
                {
                    if (componentBase is T)
                    {
                        component = (T)componentBase;
                        return true;
                    }
                }
            }
            component = default(T);
            return false;
        }
    }
}