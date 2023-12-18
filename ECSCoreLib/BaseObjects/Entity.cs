using ECSCore.Exceptions;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using System;
using System.Collections.Generic;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс сущьности.
    /// Все сущьности должны наследоваться от данного класса
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ссылка на внешнюю сущьность, в которой находится текущая сущьность
        /// </summary>
        public IEntity ExternalEntity { get; set; }

        /// <summary>
        /// Вложенные сущьности
        /// </summary>
        public Dictionary<int, IEntity> NestedEntites { get; } = new Dictionary<int, IEntity>();

        /// <summary>
        /// Компоненты
        /// </summary>
        public List<IComponent> Components { get; } = new List<IComponent>();

        /// <summary>
        /// Добавить вложенную сущьность
        /// </summary>
        /// <param name="entity"> вложенная сущьность </param>
        public IEntity AddNestedEntity<T>(T entity)
            where T : IEntity
        {
            if (entity.Id == 0)
            {
                ECS.Instance.AddEntity(entity as Entity);
            } //Если сущьность не проинициализирована

            lock (NestedEntites)
            {
                NestedEntites.Add(entity.Id, entity);
            }

            entity.ExternalEntity = this;

            return entity;
        }

        /// <summary>
        /// Получить вложенную сущьность
        /// </summary>
        public bool TryGetNestedEntity<T>(int idChildEntity, out T entity)
            where T : IEntity
        {
            lock (NestedEntites)
            {
                bool result = NestedEntites.TryGetValue(idChildEntity, out IEntity entityOut);
                entity = (T)entityOut;
                return result;
            }
        }

        /// <summary>
        /// Удалить вложенную сущьность
        /// </summary>
        public bool RemoveNestedEntity<T>(int idChildEntity, out T entity)
            where T : IEntity
        {
            lock (NestedEntites)
            {
                if (NestedEntites.TryGetValue(idChildEntity, out IEntity entityOut))
                {
                    entity = (T)entityOut;
                    entityOut.ExternalEntity = null;
                    return NestedEntites.Remove(idChildEntity);
                }
                entity = default;
                return false;
            }
        }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent<T>(T component)
            where T : IComponent
        {
            component.Id = this.Id;
            ECS.Instance.AddComponent(component);
        }

        /// <summary>
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool TryGetComponent<T>(out T component)
            where T : IComponent
        {
            return TryGetComponentInternal<T>(out component);
        }

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <param name="component"> Компонент(если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool TryGetComponent(Type typeComponent, out IComponent component)
        {
            return TryGetComponentInternal(typeComponent, out component);
        }

        /// <summary>
        /// Проверить наличие компонента у сущьности
        /// </summary>
        /// <param name="typeComponent"> Тип компонента </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool CheckExistComponent(Type typeComponent)
        {
            lock (Components)
            {
                foreach (var item in Components)
                {
                    if (item.GetType().FullName == typeComponent.FullName)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Удалить компонент
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        public void RemoveComponent<T>()
            where T : IComponent
        {
            ECS.Instance.RemoveComponent<T>(this.Id);
        }

        /// <summary>
        /// Уничтожить сущьность
        /// </summary>
        public void Death()
        {
            UnbindParentAndChild(); //Отвязать родительские и дочерние сущьности
            ECS.Instance.RemoveEntity(this.Id); //Удалимся
        }

        internal void AddComponentInternal<T>(T component)
            where T : IComponent
        {
            if (TryGetComponentInternal(out T _))
            {
                throw new ExceptionEntityHaveComponent($"У сущьности: {Id} уже есть компонент: {typeof(T).Name}");
            }

            lock (Components)
            {
                Components.Add(component);
            }
        }
        
        internal bool TryGetComponentInternal<T>(out T component)
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

        internal bool TryGetComponentInternal(Type typeComponent, out IComponent component)
        {
            lock (Components)
            {
                foreach (var item in Components)
                {
                    if (item.GetType().FullName == typeComponent.FullName)
                    {
                        component = item;
                        return true;
                    }
                }
                component = default;
                return false;
            }
        }

        internal void RemoveComponentInternal<T>()
            where T : IComponent
        {
            if (TryGetComponent(out T Component))
            {
                lock (Components)
                {
                    Components.Remove(Component);
                }
            }
        }

        /// <summary>
        /// Отвязать внешние и вложенные сущьности
        /// </summary>
        private void UnbindParentAndChild()
        {
            foreach (IEntity entity in NestedEntites.Values)
            {
                entity.ExternalEntity = null;
            }

            NestedEntites.Clear();

            ExternalEntity?.RemoveNestedEntity(this.Id, out IEntity _);
        }
    }
}