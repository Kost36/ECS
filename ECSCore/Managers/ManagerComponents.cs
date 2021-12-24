using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер компонентов
    /// </summary>
    public class ManagerComponents
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ecs"> Ссылка на ecs </param>
        public ManagerComponents(ECS ecs)
        {
            _ecs = ecs;
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Коллекция одинаковых компонентов
        /// </summary>
        private List<Components> _collections = new List<Components>();
        #endregion

        #region Свойства
        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить компонент в коллекцию
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public void Add(ComponentBase component)
        {
            Registration(component); //Добавим в коллекцию
        }
        /// <summary>
        /// Получить компонент заданного типа, имеющий заданный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <returns> IComponent / null </returns>
        public ComponentBase Get<T>(int id)
        {
            return Search(id, typeof(T));
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        public bool Remove<T>(int id)
        {
            return RemoveComponent(id, typeof(T)); //Удалим компонент
        }
        /// <summary>
        /// Удалить все компоненты с id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void Remove(int id)
        {
            RemoveComponents(id);
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавить компонент в коллекцию
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private void Registration(ComponentBase component)
        {
            foreach (Components components in _collections)
            {
                if (components.IsType(component.GetType()))
                {
                    ComponentBase componentBase = components.Get(component.Id);
                    if (componentBase == null)
                    {
                        components.Add(component); //Добавим компонент в коллекцию
                    }
                    else
                    {
                        componentBase = component; //Передали компонент
                    }
                    return;
                } //Если тип совпал
            } //Пройдемся по существующим коллекциям
            Components componentsNew = new Components(component); //Создадим новую коллекцию
            _collections.Add(componentsNew); //Добавим новую коллекцию в список
            componentsNew.Add(component); //Добавим компонент в новую коллекцию
        }
        /// <summary>
        /// Получить компонент заданного типа, имеющий определенный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <returns> IComponent / null </returns>
        private ComponentBase Search(int id, Type typeComponent)
        {
            foreach (Components components in _collections)
            {
                if (components.IsType(typeComponent))
                {
                    return components.Get(id); //Вернем компонент из коллекции с определенным id
                } //Если тип совпал
            } //Пройдемся по существующим коллекциям
            return null;
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        private bool RemoveComponent(int id, Type typeComponent)
        {
            foreach (Components components in _collections)
            {
                if (components.IsType(typeComponent))
                {
                    return components.Remove(id); //Удалим компонент из коллекции с определенным id
                } //Если тип совпал
            } //Пройдемся по существующим коллекциям
            return false;
        }
        /// <summary>
        /// Удалить все компоненты с id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        private void RemoveComponents(int id)
        {
            foreach (Components components in _collections)
            {
                components.Remove(id); //Вернем компонент из коллекцию с определенным id
            } //Пройдемся по существующим коллекциям
        }
        #endregion
    }

    /// <summary>
    /// Коллекция компонентов
    /// </summary>
    /// <typeparam name="T"> Тип компонентов в коллекции </typeparam>
    public class Components
    {
        #region Конструктор
        public Components(IComponent component)
        {
            _componentType = component.GetType();
        }
        #endregion

        #region Поля
        private Type _componentType; 
        /// <summary>
        /// Коллекция компонентов
        /// </summary>
        private SortedList<int, ComponentBase> _components = new SortedList<int, ComponentBase>();
        #endregion

        #region Публичные методы
        /// <summary>
        /// Проверить тип коллекции на соответствие типа зпдпнному объекту
        /// </summary>
        /// <param name="component"> Компонент, который нужно проверить </param>
        /// <returns></returns>
        public bool IsType(Type typeComponent)
        {
            if (_componentType.FullName == typeComponent.FullName)
            {
                return true;
            } //Если тип совпадает
            return false;
        }

        /// <summary>
        /// Добавить компонент в коллекцию
        /// </summary>
        /// <returns></returns>
        public void Add(ComponentBase component)
        {
            _components.Add(component.Id, component);
        }
        /// <summary>
        /// Получить компонент из коллекции
        /// </summary>
        /// <returns> IComponent / null</returns>
        public ComponentBase Get(int id)
        {
            if (_components.TryGetValue(id, out ComponentBase component)) {
                return component;
            };
            return null;
        }
        /// <summary>
        /// Удалить компонент из коллекции
        /// </summary>
        /// <returns></returns>
        public bool Remove(int id)
        {
            return _components.Remove(id);
        }
        #endregion
    }
}