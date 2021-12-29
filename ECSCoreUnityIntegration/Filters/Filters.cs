using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.Filters
{
    /// <summary>
    /// Группа для 1 компонента
    /// </summary>
    internal class Filter<T0> : FilterBase
        where T0 : Component
    {
        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public Dictionary<int, T0> ComponentsT0;
        /// <summary>
        /// Бит блокировки между потоками
        /// </summary>
        public object LockBit { get; set; } = true;
        #endregion

        #region Публичные методы
        public override void Init(int capacity)
        {
            if (capacity > 10)
            {
                Capacity = capacity;
            }
            ComponentsT0 = new Dictionary<int, T0>(Capacity);
        }
        public override bool CheckFilter(List<Type> typesComponents)
        {
            if (typesComponents.Count == 1)
            {
                foreach (Type type in typesComponents)
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
        public override bool ComponetTypeIsInteresting(Type typeComponent)
        {
            if (typeof(T0) == typeComponent)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0) };
        }
        public override void TryAdd(Component component, Entity entity)
        {
            if (typeof(T0) == component.GetType())
            {
                Add(component);
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, Entity entity)
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
        private void Add(Component component)
        {
            lock (LockBit)
            {
                if (ComponentsT0.TryGetValue(component.Id, out T0 t0))
                {
                    t0 = (T0)component;
                    return;
                }
                else
                {
                    ComponentsT0.Add(component.Id, (T0)component);
                }
            }
        }
        private void Remove(int id)
        {
            lock (LockBit)
            {
                ComponentsT0.Remove(id);
            }
        }
        #endregion
    }
    /// <summary>
    /// Группа для 2 компонент
    /// </summary>
    internal class Filter<T0, T1> : FilterBase
        where T0 : Component
        where T1 : Component
    {
        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public Dictionary<int, T0> ComponentsT0;
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public Dictionary<int, T1> ComponentsT1;
        /// <summary>
        /// Бит блокировки между потоками
        /// </summary>
        public object LockBit { get; set; } = true;
        #endregion

        #region Публичные методы
        public override void Init(int capacity)
        {
            if (capacity > 10)
            {
                Capacity = capacity;
            }
            ComponentsT0 = new Dictionary<int, T0>(Capacity);
            ComponentsT1 = new Dictionary<int, T1>(Capacity);
        }
        public override bool CheckFilter(List<Type> typesComponents)
        {
            if (typesComponents.Count == 2)
            {
                foreach (Type type in typesComponents)
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
        public override bool ComponetTypeIsInteresting(Type typeComponent)
        {
            if (typeof(T0) == typeComponent || typeof(T1) == typeComponent)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1) };
        }
        public override void TryAdd(Component component, Entity entity)
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
        public override void TryRemove(Type typeComponent, Entity entity)
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
        private void Add(Component componentT0, Component componentT1)
        {
            lock (LockBit)
            {
                int id = componentT0.Id;
                if (ComponentsT0.TryGetValue(id, out T0 t0))
                {
                    if (ComponentsT1.TryGetValue(id, out T1 t1))
                    {
                        t0 = (T0)componentT0;
                        t1 = (T1)componentT1;
                        return;
                    }
                }
                ComponentsT0.Add(id, (T0)componentT0);
                ComponentsT1.Add(id, (T1)componentT1);
            }
        }
        private void Remove(int id)
        {
            lock (LockBit)
            {
                ComponentsT0.Remove(id);
                ComponentsT1.Remove(id);
            }
        }
        #endregion
    }
    /// <summary>
    /// Группа для 3 компонент
    /// </summary>
    internal class Filter<T0, T1, T2> : FilterBase
        where T0 : Component
        where T1 : Component
        where T2 : Component
    {
        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public Dictionary<int, T0> ComponentsT0;
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public Dictionary<int, T1> ComponentsT1;
        /// <summary>
        /// Список компонентов T2
        /// </summary>
        public Dictionary<int, T2> ComponentsT2;
        /// <summary>
        /// Бит блокировки между потоками
        /// </summary>
        public object LockBit { get; set; } = true;
        #endregion

        #region Публичные методы
        public override void Init(int capacity)
        {
            if (capacity > 10)
            {
                Capacity = capacity;
            }
            ComponentsT0 = new Dictionary<int, T0>(Capacity);
            ComponentsT1 = new Dictionary<int, T1>(Capacity);
            ComponentsT2 = new Dictionary<int, T2>(Capacity);
        }
        public override bool CheckFilter(List<Type> typesComponents)
        {
            if (typesComponents.Count == 3)
            {
                foreach (Type type in typesComponents)
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
        public override bool ComponetTypeIsInteresting(Type typeComponent)
        {
            if (typeof(T0) == typeComponent || typeof(T1) == typeComponent || typeof(T2) == typeComponent)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1), typeof(T2) };
        }
        public override void TryAdd(Component component, Entity entity)
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
        public override void TryRemove(Type typeComponent, Entity entity)
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
        private void Add(Component componentT0, Component componentT1, Component componentT2)
        {
            lock (LockBit)
            {
                int id = componentT0.Id;
                if (ComponentsT0.TryGetValue(id, out T0 t0))
                {
                    if (ComponentsT1.TryGetValue(id, out T1 t1))
                    {
                        if (ComponentsT2.TryGetValue(id, out T2 t2))
                        {
                            t0 = (T0)componentT0;
                            t1 = (T1)componentT1;
                            t2 = (T2)componentT2;
                            return;
                        }
                    }
                }
                ComponentsT0.Add(id, (T0)componentT0);
                ComponentsT1.Add(id, (T1)componentT1);
                ComponentsT2.Add(id, (T2)componentT2);
            }
        }
        private void Remove(int id)
        {
            lock (LockBit)
            {
                ComponentsT0.Remove(id);
                ComponentsT1.Remove(id);
                ComponentsT2.Remove(id);
            }
        }
        #endregion
    }
    /// <summary>
    /// Группа для 4 компонент
    /// </summary>
    internal class Filter<T0, T1, T2, T3> : FilterBase
        where T0 : Component
        where T1 : Component
        where T2 : Component
        where T3 : Component
    {
        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public Dictionary<int, T0> ComponentsT0;
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public Dictionary<int, T1> ComponentsT1;
        /// <summary>
        /// Список компонентов T2
        /// </summary>
        public Dictionary<int, T2> ComponentsT2;
        /// <summary>
        /// Список компонентов T3
        /// </summary>
        public Dictionary<int, T3> ComponentsT3;
        /// <summary>
        /// Бит блокировки между потоками
        /// </summary>
        public object LockBit { get; set; } = true;
        #endregion

        #region Публичные методы
        public override void Init(int capacity)
        {
            if (capacity > 10)
            {
                Capacity = capacity;
            }
            ComponentsT0 = new Dictionary<int, T0>(Capacity);
            ComponentsT1 = new Dictionary<int, T1>(Capacity);
            ComponentsT2 = new Dictionary<int, T2>(Capacity);
            ComponentsT3 = new Dictionary<int, T3>(Capacity);
        }
        public override bool CheckFilter(List<Type> typesComponents)
        {
            if (typesComponents.Count == 4)
            {
                foreach (Type type in typesComponents)
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
        public override bool ComponetTypeIsInteresting(Type typeComponent)
        {
            if (typeof(T0) == typeComponent || typeof(T1) == typeComponent || typeof(T2) == typeComponent || typeof(T3) == typeComponent)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1), typeof(T2), typeof(T3) };
        }
        public override void TryAdd(Component component, Entity entity)
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
        public override void TryRemove(Type typeComponent, Entity entity)
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
        private void Add(Component componentT0, Component componentT1, Component componentT2, Component componentT3)
        {
            lock (LockBit)
            {
                int id = componentT0.Id;
                if (ComponentsT0.TryGetValue(id, out T0 t0))
                {
                    if (ComponentsT1.TryGetValue(id, out T1 t1))
                    {
                        if (ComponentsT2.TryGetValue(id, out T2 t2))
                        {
                            if (ComponentsT3.TryGetValue(id, out T3 t3))
                            {
                                t0 = (T0)componentT0;
                                t1 = (T1)componentT1;
                                t2 = (T2)componentT2;
                                t3 = (T3)componentT3;
                                return;
                            }
                        }
                    }
                }
                ComponentsT0.Add(id, (T0)componentT0);
                ComponentsT1.Add(id, (T1)componentT1);
                ComponentsT2.Add(id, (T2)componentT2);
                ComponentsT3.Add(id, (T3)componentT3);
            }
        }
        private void Remove(int id)
        {
            lock (LockBit)
            {
                ComponentsT1.Remove(id);
                ComponentsT1.Remove(id);
                ComponentsT2.Remove(id);
                ComponentsT3.Remove(id);
            }
        }
        #endregion
    }
    /// <summary>
    /// Группа для 5 компонент
    /// </summary>
    internal class Filter<T0, T1, T2, T3, T4> : FilterBase
        where T0 : Component
        where T1 : Component
        where T2 : Component
        where T3 : Component
        where T4 : Component
    {
        #region Свойства
        /// <summary>
        /// Количество элементов в фильтре
        /// </summary>
        public override int Count { get { return ComponentsT0.Count; } }
        /// <summary>
        /// Список компонентов T0
        /// </summary>
        public Dictionary<int, T0> ComponentsT0;
        /// <summary>
        /// Список компонентов T1
        /// </summary>
        public Dictionary<int, T1> ComponentsT1;
        /// <summary>
        /// Список компонентов T2
        /// </summary>
        public Dictionary<int, T2> ComponentsT2;
        /// <summary>
        /// Список компонентов T3
        /// </summary>
        public Dictionary<int, T3> ComponentsT3;
        /// <summary>
        /// Список компонентов T4
        /// </summary>
        public Dictionary<int, T4> ComponentsT4;
        /// <summary>
        /// Бит блокировки между потоками
        /// </summary>
        public object LockBit { get; set; } = true;
        #endregion

        #region Публичные методы
        public override void Init(int capacity)
        {
            if (capacity > 10)
            {
                Capacity = capacity;
            }
            ComponentsT0 = new Dictionary<int, T0>(Capacity);
            ComponentsT1 = new Dictionary<int, T1>(Capacity);
            ComponentsT2 = new Dictionary<int, T2>(Capacity);
            ComponentsT3 = new Dictionary<int, T3>(Capacity);
            ComponentsT4 = new Dictionary<int, T4>(Capacity);
        }
        public override bool CheckFilter(List<Type> typesComponents)
        {
            if (typesComponents.Count == 5)
            {
                foreach (Type type in typesComponents)
                {
                    if (!type.Equals(typeof(T0)) & !type.Equals(typeof(T1)) & !type.Equals(typeof(T2)) & !type.Equals(typeof(T3)) & !type.Equals(typeof(T4)))
                    {
                        return false;
                    } //Если не совпал ни с одним
                }
                return true;
            } //Если в списке 4 компонента
            return false;
        }
        public override bool ComponetTypeIsInteresting(Type typeComponent)
        {
            if (typeof(T0) == typeComponent || typeof(T1) == typeComponent || typeof(T2) == typeComponent || typeof(T3) == typeComponent || typeof(T4) == typeComponent)
            {
                return true;
            }
            return false;
        }
        public override List<Type> GetTypesComponents()
        {
            return new List<Type>() { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
        }
        public override void TryAdd(Component component, Entity entity)
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Add(component, componentT1, componentT2, componentT3, componentT4);
                                return;
                            } //Если есть пятый компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Add(componentT0, component, componentT2, componentT3, componentT4);
                                return;
                            } //Если есть пятый компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Add(componentT0, componentT1, component, componentT3, componentT4);
                                return;
                            } //Если есть пятый компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Add(componentT0, componentT1, componentT2, component, componentT4);
                                return;
                            } //Если есть пятый компонент у сущьности
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (type == typeof(T4))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T2 componentT2))
                        {
                            if (entity.Get(out T3 componentT3))
                            {
                                Add(componentT0, componentT1, componentT2, componentT3, component);
                                return;
                            } //Если есть пятый компонент у сущьности
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
        }
        public override void TryRemove(Type typeComponent, Entity entity)
        {
            if (typeComponent == typeof(T0))
            {
                if (entity.Get(out T1 componentT1))
                {
                    if (entity.Get(out T2 componentT2))
                    {
                        if (entity.Get(out T3 componentT3))
                        {
                            if (entity.Get(out T4 componentT4))
                            {
                                Remove(entity.Id);
                                return;
                            } //Если есть компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Remove(entity.Id);
                                return;
                            } //Если есть компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Remove(entity.Id);
                                return;
                            } //Если есть компонент у сущьности
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
                            if (entity.Get(out T4 componentT4))
                            {
                                Remove(entity.Id);
                                return;
                            } //Если есть компонент у сущьности
                        } //Если есть четвертый компонент у сущьности
                    } //Если есть третий компонент у сущьности
                } //Если есть второй компонент у сущьности
            } //Если тип совпадает
            if (typeComponent == typeof(T4))
            {
                if (entity.Get(out T0 componentT0))
                {
                    if (entity.Get(out T1 componentT1))
                    {
                        if (entity.Get(out T2 componentT2))
                        {
                            if (entity.Get(out T3 componentT3))
                            {
                                Remove(entity.Id);
                                return;
                            } //Если есть компонент у сущьности
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
        private void Add(Component componentT0, Component componentT1, Component componentT2, Component componentT3, Component componentT4)
        {
            lock (LockBit)
            {
                int id = componentT0.Id;
                if (ComponentsT0.TryGetValue(id, out T0 t0))
                {
                    if (ComponentsT1.TryGetValue(id, out T1 t1))
                    {
                        if (ComponentsT2.TryGetValue(id, out T2 t2))
                        {
                            if (ComponentsT3.TryGetValue(id, out T3 t3))
                            {
                                if (ComponentsT4.TryGetValue(id, out T4 t4))
                                {
                                    t0 = (T0)componentT0;
                                    t1 = (T1)componentT1;
                                    t2 = (T2)componentT2;
                                    t3 = (T3)componentT3;
                                    t4 = (T4)componentT4;
                                    return;
                                }
                            }
                        }
                    }
                }
                ComponentsT0.Add(id, (T0)componentT0);
                ComponentsT1.Add(id, (T1)componentT1);
                ComponentsT2.Add(id, (T2)componentT2);
                ComponentsT3.Add(id, (T3)componentT3);
                ComponentsT4.Add(id, (T4)componentT4);
            }
        }
        private void Remove(int id)
        {
            lock (LockBit)
            {
                ComponentsT1.Remove(id);
                ComponentsT1.Remove(id);
                ComponentsT2.Remove(id);
                ComponentsT3.Remove(id);
                ComponentsT4.Remove(id);
            }
        }
        #endregion
    }
}

//TODO 1) Для фильтра создать поле со списком типов компонент.
//                                                            Возвращать список, без typeof(T)
//                                                            Сравнивать по списку без typeof(T)