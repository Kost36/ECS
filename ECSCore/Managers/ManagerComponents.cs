﻿using ECSCore.BaseObjects;
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
        internal ManagerComponents(ECS ecs, int startCountEntityCapacity)
        {
            _ecs = ecs;
            if (startCountEntityCapacity > 10)
            {
                _startCountCapacity = startCountEntityCapacity;
            }
            _collections = new List<Components>(_startCountCapacity);
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Стартовая вместимость коллекции
        /// </summary>
        private int _startCountCapacity = 10;
        /// <summary>
        /// Коллекция одинаковых компонентов
        /// </summary>
        private List<Components> _collections;
        #endregion

        #region Свойства
        /// <summary>
        /// Количество коллекций компонент
        /// </summary>
        public int CountCollectionsComponent
        {
            get
            {
                lock (_collections)
                {
                    return _collections.Count;
                }
            }
        }
        /// <summary>
        /// Количество компонент
        /// </summary>
        public int CountComponents
        {
            get
            {
                return GetAllComponentCount();
            }
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Получить количество компонент заданного типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal int GetCountComponents<T>()
            where T : IComponent
        {
            lock (_collections)
            {
                Type typeNeedComponent = typeof(T);
                foreach (Components components in _collections)
                {
                    if (components.IsType(typeNeedComponent))
                    {
                        return components.Count; //Вернем количество
                    } //Если тип соответствует
                } //Пройдемся по существующим коллекциям
                return 0;
            }
        }
        /// <summary>
        /// Добавить компонент в коллекцию
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        internal void Add(IComponent component)
        {
            Registration(component); //Добавим в коллекцию
        }
        /// <summary>
        /// Получить компонент, если есть
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        internal bool Get<T>(int id, out T component)
            where T : IComponent
        {
            return Search<T>(id, out component);
        }
        /// <summary>
        /// Получить все компоненты сущьности
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal List<IComponent> Get(int idEntity)
        {
            List<IComponent> Components = new();
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    if (components.Get<IComponent>(idEntity, out IComponent component))
                    {
                        Components.Add(component);
                    } //Если компонент есть
                } //Пройдемся по существующим коллекциям
            }
            return Components;
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        internal bool Remove<T>(int id)
        {
            return RemoveComponent(id, typeof(T)); //Удалим компонент
        }
        /// <summary>
        /// Удалить все компоненты с id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        internal void Remove(int id)
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
        private void Registration(IComponent component)
        {
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    if (components.IsType(component.GetType()))
                    {
                        if (components.Get(component.Id, out IComponent Component))
                        {
                            //TODO Присвоить значение, вместо присвоения ссылки
                            Component = component; //Передали компонент
                        } //Если компонент есть
                        else
                        {
                            components.Add(component); //Добавим компонент в коллекцию
                        }
                        return;
                    } //Если тип совпал
                } //Пройдемся по существующим коллекциям
                Components componentsNew = new Components(component); //Создадим новую коллекцию
                _collections.Add(componentsNew); //Добавим новую коллекцию в список
                componentsNew.Add(component); //Добавим компонент в новую коллекцию
            }
        }
        /// <summary>
        /// Получить компонент, если есть
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        private bool Search<T>(int id, out T component)
            where T : IComponent
        {
            Type typeComponent = typeof(T);
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    if (components.IsType(typeComponent))
                    {
                        return components.Get(id, out component); //Вернем компонент из коллекции с определенным id
                    } //Если тип совпал
                } //Пройдемся по существующим коллекциям
            }
            component = default;
            return false;
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="typeComponent"> Тип компонента </param>
        private bool RemoveComponent(int id, Type typeComponent)
        {
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    if (components.IsType(typeComponent))
                    {
                        return components.Remove(id); //Удалим компонент из коллекции с определенным id
                    } //Если тип совпал
                } //Пройдемся по существующим коллекциям
            }
            return false;
        }
        /// <summary>
        /// Удалить все компоненты с id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        private void RemoveComponents(int id)
        {
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    components.Remove(id); //Вернем компонент из коллекцию с определенным id
                } //Пройдемся по существующим коллекциям
            }
        }
        /// <summary>
        /// Получить общее количество компонент
        /// </summary>
        private int GetAllComponentCount()
        {
            int countAllComponents = 0;
            lock (_collections)
            {
                foreach (Components components in _collections)
                {
                    countAllComponents = countAllComponents + components.Count;
                } //Пройдемся по существующим коллекциям
            }
            return countAllComponents;
        }
        #endregion
    }

    /// <summary>
    /// Коллекция компонентов
    /// </summary>
    /// <typeparam name="T"> Тип компонентов в коллекции </typeparam>
    internal class Components
    {
        #region Конструктор
        internal Components(IComponent component)
        {
            _componentType = component.GetType();
        }
        #endregion

        #region Поля
        private Type _componentType; 
        /// <summary>
        /// Коллекция компонентов
        /// </summary>
        private Dictionary<int, IComponent> _components = new Dictionary<int, IComponent>();
        #endregion

        #region Свойства
        /// <summary>
        /// Количество компонент в коллекции
        /// </summary>
        public int Count { get { return _components.Count; } }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Проверить тип коллекции на соответствие типа зпданному объекту
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
        public void Add(IComponent component)
        {
            lock (_components)
            {
                if (_components.TryGetValue(component.Id, out IComponent Component))
                {
                    Component = component;
                }
                else
                {
                    _components.Add(component.Id, component);
                }
            }
        }
        /// <summary>
        /// Получить компонент, если есть
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool Get<T>(int id, out T component)
            where T : IComponent
        {
            lock (_components)
            {
                if (_components.TryGetValue(id, out IComponent componentOut))
                {
                    component = (T)componentOut;
                    return true;
                };
                component = default(T);
                return false;
            }
        }
        /// <summary>
        /// Удалить компонент из коллекции
        /// </summary>
        /// <returns></returns>
        public bool Remove(int id)
        {
            lock (_components)
            {
                return _components.Remove(id);
            }
        }
        #endregion
    }
}