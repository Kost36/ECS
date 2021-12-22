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
        public override void TryRemove(ComponentBase component, EntityBase entity)
        {
            if (typeof(T0) == component.GetType())
            {
                Remove(component.Id);
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
            ComponentsT0.Add((T0)component);
            Count++;
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
            Count--;
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
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1) };
        }
        public override void TryAdd(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                ComponentBase componentT1 = entity.Get<T1>();
                if (componentT1 != null)
                {
                    Add(component, componentT1);
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    Add(componentT0, component);
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
        }
        public override void TryRemove(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                ComponentBase componentT1 = entity.Get<T1>();
                if (componentT1 != null)
                {
                    Remove(component.Id);
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    Remove(component.Id);
                } //Если есть второй компонент у сущьности
                return;
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
            ComponentsT0.Add((T0)componentT0);
            ComponentsT1.Add((T1)componentT1);
            Count++;
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
            ComponentsT1.RemoveAll(t => t.Id == id);
            Count--;
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
            } //Если в списке 2 компонента
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
                ComponentBase componentT1 = entity.Get<T1>();
                if (componentT1 != null)
                {
                    ComponentBase componentT2 = entity.Get<T2>();
                    if (componentT2 != null)
                    {
                        Add(component, componentT1, componentT2);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    ComponentBase componentT2 = entity.Get<T2>();
                    if (componentT2 != null)
                    {
                        Add(componentT0, component, componentT2);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T2))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    ComponentBase componentT1 = entity.Get<T1>();
                    if (componentT1 != null)
                    {
                        Add(componentT0, componentT1, component);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
        }
        public override void TryRemove(ComponentBase component, EntityBase entity)
        {
            Type type = component.GetType();
            if (type == typeof(T0))
            {
                ComponentBase componentT1 = entity.Get<T1>();
                if (componentT1 != null)
                {
                    ComponentBase componentT2 = entity.Get<T2>();
                    if (componentT2 != null)
                    {
                        Remove(component.Id);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T1))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    ComponentBase componentT2 = entity.Get<T2>();
                    if (componentT2 != null)
                    {
                        Remove(component.Id);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
            } //Если тип совпадает
            if (type == typeof(T2))
            {
                ComponentBase componentT0 = entity.Get<T0>();
                if (componentT0 != null)
                {
                    ComponentBase componentT1 = entity.Get<T1>();
                    if (componentT1 != null)
                    {
                        Remove(component.Id);
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
                return;
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
            ComponentsT0.Add((T0)componentT0);
            ComponentsT1.Add((T1)componentT1);
            ComponentsT2.Add((T2)componentT2);
            Count++;
        }
        private void Remove(int id)
        {
            ComponentsT0.RemoveAll(t => t.Id == id);
            ComponentsT1.RemoveAll(t => t.Id == id);
            ComponentsT2.RemoveAll(t => t.Id == id);
            Count--;
        }
        #endregion
    }


    
}