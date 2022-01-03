using ECSCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущьности.
    /// Все сущьности наследовать от данного класса
    /// </summary>
    //[Serializable]
    public abstract class Entity : IEntity
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
            where T : IComponent
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
            where T : IComponent
        {
            return GetComponent(out component);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        public void Remove<T>()
            where T : IComponent
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
        public List<IComponent> Components { get; } = new List<IComponent>();

        #region Тест вывод в юнити
        //private ListComponents _listComponents;
        //public ListComponents ListComponents
        //{
        //    get
        //    {
        //        if (_listComponents is null)
        //        {
        //            _listComponents = new ListComponents();
        //        }
        //        _listComponents.Components.Clear();
        //        foreach (IComponent component in Components)
        //        {
        //            _listComponents.Components.Add((ComponentBase)component);
        //        }
        //        return _listComponents;
        //    }
            
        //}
        #endregion

        /// <summary>
        /// Добавить компонент в коллекцию сущьности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        internal void AddComponent<T>(T component)
            where T:IComponent
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
        /// Удалить компонент из коллекции сущьности
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        internal void RemoveComponent<T>()
            where T : IComponent
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
            where T : IComponent
        {
            lock (Components)
            {
                foreach (IComponent Component in Components)
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

    //public class ListComponents
    //{
    //    public List<ComponentBase> Components = new List<ComponentBase>();
    //}
}