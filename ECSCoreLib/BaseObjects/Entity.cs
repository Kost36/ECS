using ECSCore.Exceptions;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущьности.
    /// Все сущьности наследовать от данного класса
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Родительская сущьность
        /// </summary>
        public IEntity ParentEntity { get; set; }

        /// <summary>
        /// Для отслеживания в тестах
        /// </summary>
        public List<IComponent> Components { get; } = new List<IComponent>();

        /// <summary>
        /// Дочерние сущьности
        /// </summary>
        public Dictionary<int, IEntity> ChildEntitys { get; } = new Dictionary<int, IEntity>();

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
        /// Добавить дочернюю сущьность
        /// </summary>
        /// <param name="entity"> дочерняя сущьность </param>
        public IEntity AddChild<T>(T entity)
            where T : IEntity
        {
            if (entity.Id == 0)
            {
                ECS.Instance.AddEntity(entity);
            } //Если сущьность не проинициализирована
            lock (ChildEntitys)
            {
                this.ChildEntitys.Add(entity.Id, entity);
            }
            entity.ParentEntity = this;
            return entity;
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
        /// Получить дочернюю сущьность
        /// </summary>
        public bool GetChild<T>(int idChildEntity, out T entity)
            where T : IEntity
        {
            lock (ChildEntitys)
            {
                bool result = ChildEntitys.TryGetValue(idChildEntity, out IEntity entityOut);
                entity = (T)entityOut;
                return result;
            }
        }

        /// <summary>
        /// Проверить наличие компонента у сущьности
        /// </summary>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool CheckExist(Type typeComponent)
        {
            return CheckExistComponent(typeComponent);
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
        /// Удалить дочернюю сущьность
        /// </summary>
        public bool RemoveChild<T>(int idChildEntity, out T entity)
            where T : IEntity
        {
            lock (ChildEntitys)
            {
                if (ChildEntitys.TryGetValue(idChildEntity, out IEntity entityOut))
                {
                    entity = (T)entityOut;
                    entityOut.ParentEntity = null;
                    return ChildEntitys.Remove(idChildEntity);
                }
                entity = default;
                return false;
            }
        }

        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        public void Death()
        {
            foreach (IEntity entity in ChildEntitys.Values)
            {
                entity.ParentEntity = null;
            } //Отвяжемся от всех дочерних сущьностей
            ChildEntitys.Clear(); //Очистим дочерние сущьности
            ParentEntity?.RemoveChild(this.Id, out IEntity _); //Отвяжемся от родительской сущьности
            ECS.Instance.RemoveEntity(this.Id); //Удалимся
        }

        /// <summary>
        /// Добавить компонент в коллекцию сущьности
        /// </summary>
        /// <typeparam name="T"> Generic - тип компонента </typeparam>
        /// <param name="component"> Компонент </param>
        internal void AddComponent<T>(T component)
            where T : IComponent
        {
            //if (GetComponent(out T componentInEntity))
            if (GetComponent(out T _))
            {
                throw new ExceptionEntityHaveComponent($"У сущьности: {Id} уже есть компонент: {typeof(T).Name}");
                //componentInEntity = component;
                //return;
            } //Если нету
            lock (Components)
            {
                Components.Add(component);
            }
        }

        /// <summary>
        /// Получить компонент из своего списка
        /// </summary>
        /// <typeparam name="T"> Generic тип компонента </typeparam>
        /// <param name="component"> Компонент </param>
        /// <returns></returns>
        private bool GetComponent<T>(out T component)
            where T : IComponent
        {
            lock (Components)
            {
                foreach (ComponentBase Component in Components)
                {
                    if (Component is T t)
                    {
                        component = t;
                        return true;
                    }
                }
                component = default;
                return false;
            }
        }

        /// <summary>
        /// Удалить компонент из коллекции сущьности
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        /// Проверить наличие компонента у сущьности
        /// </summary>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <returns> Флаг наличия компонента </returns>
        private bool CheckExistComponent(Type typeComponent)
        {
            lock (Components)
            {
                foreach (IComponent Component in Components)
                {
                    if (Component.GetType().FullName == typeComponent.FullName)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}