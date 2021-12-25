using ECSCore.BaseObjects;
using ECSCore.Interface;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Filters
{
    /// <summary>
    /// Группа для 1 компонента
    /// </summary>
    public abstract class Filter<T0> : FilterBase
        where T0 : ComponentBase
    {
        #region Конструктор
        #endregion

        #region Поля
        #endregion

        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public List<T0> ComponentsT0 = new List<T0>();
        #endregion

        #region Публичные методы
        public override bool CheckFilter(List<Type> typesComponet)
        {
            if (typesComponet.Count == 1)
            {
                foreach (Type type in typesComponet)
                {
                    if (type.Equals(typeof(T0)))
                    {
                        return false;
                    } //Если не совпал ни с одним
                }
                return true;
            } //Если в списке 2 компонента
            return false;
        }
        public override bool ComponetTypeIsInteresting(Type typeComponet)
        {
            if (typeof(T0) == typeComponet)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0) };
        }
        public override void TryAdd(ComponentBase component, EntityBase entity)
        {
            if (typeof(T0) == component.GetType())
            {
                Add(component);
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, EntityBase entity)
        {
            if (typeComponent == typeof(T0))
            {
                Remove(entity.Id);
            } //Если тип совпадает
        }
        public override void TryRemove(int id)
        {
            Remove(id);
        }
        #endregion

        #region Приватные методы
        private void Add(ComponentBase component)
        {
            if (ComponentsT0.Exists(t=> t.Id == component.Id) == false)
            {
                ComponentsT0.Add((T0)component);
            }
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
        }
        #endregion
    }
    /// <summary>
    /// Группа для 2 компонент
    /// </summary>
    public abstract class Filter<T0, T1> : FilterBase
        where T0 : ComponentBase
        where T1 : ComponentBase
    {
        #region Конструктор
        #endregion

        #region Поля
        #endregion

        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public List<T0> ComponentsT0 = new List<T0>();
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public List<T1> ComponentsT1 = new List<T1>();
        #endregion

        #region Публичные методы
        public override bool CheckFilter(List<Type> typesComponet)
        {
            if (typesComponet.Count == 2)
            {
                foreach (Type type in typesComponet)
                {
                    if (!type.Equals(typeof(T0)) & !type.Equals(typeof(T1)))
                    {
                        return false;
                    } //Если не совпал ни с одним
                }
                return true;
            } //Если в списке 2 компонента
            return false;
        }
        public override bool ComponetTypeIsInteresting(Type typeComponet)
        {
            if (typeof(T0) == typeComponet || typeof(T1) == typeComponet)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1) };
        }
        public override void TryAdd(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    Add(component, componentT1);
                    return;
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    Add(componentT0, component);
                    return;
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, EntityBase entity)
        {
            if (typeComponent == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    Remove(entity.Id);
                    return;
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    Remove(entity.Id);
                    return;
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(int id)
        {
            Remove(id);
        }
        #endregion

        #region Приватные методы
        private void Add(ComponentBase componentT0, ComponentBase componentT1)
        {
            if (ComponentsT0.Exists(t => t.Id == componentT0.Id) == false)
            {
                ComponentsT0.Add((T0)componentT0);
                ComponentsT1.Add((T1)componentT1);
            }
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
            ComponentsT1.RemoveAll(t => t.Id == id);
        }
        #endregion
    }
    /// <summary>
    /// Группа для 3 компонент
    /// </summary>
    public abstract class Filter<T0, T1, T2> : FilterBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
    {
        #region Конструктор
        #endregion

        #region Поля
        #endregion

        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public List<T0> ComponentsT0 = new List<T0>();
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public List<T1> ComponentsT1 = new List<T1>();
        /// <summary>
        /// Список компонентов T2
        /// </summary>
        public List<T2> ComponentsT2 = new List<T2>();
        #endregion

        #region Публичные методы
        public override bool CheckFilter(List<Type> typesComponet)
        {
            if (typesComponet.Count == 3)
            {
                foreach (Type type in typesComponet)
                {
                    if (!type.Equals(typeof(T0)) & !type.Equals(typeof(T1)) & !type.Equals(typeof(T2)))
                    {
                        return false;
                    } //Если не совпал ни с одним
                }
                return true;
            } //Если в списке 3 компонента
            return false;
        }
        public override bool ComponetTypeIsInteresting(Type typeComponet)
        {
            if (typeof(T0) == typeComponet || typeof(T1) == typeComponet || typeof(T2) == typeComponet)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1), typeof(T2) };
        }
        public override void TryAdd(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        Add(component, componentT1, componentT2);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        Add(componentT0, component, componentT2);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T2))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        Add(componentT0, componentT1, component);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, EntityBase entity)
        {
            if (typeComponent == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        Remove(entity.Id);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        Remove(entity.Id);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T2))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        Remove(entity.Id);
                        return;
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(int id)
        {
            Remove(id);
        }
        #endregion

        #region Приватные методы
        private void Add(ComponentBase componentT0, ComponentBase componentT1, ComponentBase componentT2)
        {
            if (ComponentsT0.Exists(t => t.Id == componentT0.Id) == false)
            {
                ComponentsT0.Add((T0)componentT0);
                ComponentsT1.Add((T1)componentT1);
                ComponentsT2.Add((T2)componentT2);
            }
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
            ComponentsT1.RemoveAll(t => t.Id == id);
            ComponentsT2.RemoveAll(t => t.Id == id);
        }
        #endregion
    }
    /// <summary>
    /// Группа для 4 компонент
    /// </summary>
    public abstract class Filter<T0, T1, T2, T3> : FilterBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
        where T3 : ComponentBase
    {
        #region Конструктор
        #endregion

        #region Поля
        #endregion

        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public List<T0> ComponentsT0 = new List<T0>();
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public List<T1> ComponentsT1 = new List<T1>();
        /// <summary>
        /// Список компонентов T2
        /// </summary>
        public List<T2> ComponentsT2 = new List<T2>();
        /// <summary>
        /// Список компонентов T3
        /// </summary>
        public List<T3> ComponentsT3 = new List<T3>();
        #endregion

        #region Публичные методы
        public override bool CheckFilter(List<Type> typesComponet)
        {
            if (typesComponet.Count == 4)
            {
                foreach (Type type in typesComponet)
                {
                    if (!type.Equals(typeof(T0)) & !type.Equals(typeof(T1)) & !type.Equals(typeof(T2)) & !type.Equals(typeof(T3)))
                    {
                        return false;
                    } //Если не совпал ни с одним
                }
                return true;
            } //Если в списке 4 компонента
            return false;
        }
        public override bool ComponetTypeIsInteresting(Type typeComponet)
        {
            if (typeof(T0) == typeComponet || typeof(T1) == typeComponet || typeof(T2) == typeComponet || typeof(T3) == typeComponet)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1), typeof(T2), typeof(T3) };
        }
        public override void TryAdd(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Add(component, componentT1, componentT2, componentT3);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Add(componentT0, component, componentT2, componentT3);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T2))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Add(componentT0, componentT1, component, componentT3);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T3))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T2 componentT2))
                        {
                            Add(componentT0, componentT1, componentT2, component);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, EntityBase entity)
        {
            if (typeComponent == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Remove(entity.Id);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T1))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Remove(entity.Id);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T2))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            Remove(entity.Id);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (typeComponent == typeof(T3))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T2 componentT2))
                        {
                            Remove(entity.Id);
                            return;
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(int id)
        {
            Remove(id);
        }
        #endregion

        #region Приватные методы
        private void Add(ComponentBase componentT0, ComponentBase componentT1, ComponentBase componentT2, ComponentBase componentT3)
        {
            if (ComponentsT0.Exists(t => t.Id == componentT0.Id) == false)
            {
                ComponentsT0.Add((T0)componentT0);
                ComponentsT1.Add((T1)componentT1);
                ComponentsT2.Add((T2)componentT2);
                ComponentsT2.Add((T2)componentT3);
            }
        }
        private void Remove(int id)
        {
            ComponentsT1.RemoveAll(t => t.Id == id);
            ComponentsT1.RemoveAll(t => t.Id == id);
            ComponentsT2.RemoveAll(t => t.Id == id);
            ComponentsT3.RemoveAll(t => t.Id == id);
        }
        #endregion
    }
}