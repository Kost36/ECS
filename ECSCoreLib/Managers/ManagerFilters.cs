using ECSCore.BaseObjects;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.Filters;
using System;
using System.Collections.Generic;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер фильтров
    /// </summary>
    public class ManagerFilters
    {
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private readonly ECS _ecs;
        /// <summary>
        /// Список фильтров групп компонент
        /// </summary>
        private readonly List<IFilter> _filters = new List<IFilter>();

        internal ManagerFilters(ECS ecs)
        {
            _ecs = ecs;
            Init();
        }

        /// <summary>
        /// Количество коллекций компонент
        /// </summary>
        public int CountFilters
        {
            get
            {
                return _filters.Count;
            }
        }

        /// <summary>
        /// Количество объектов в фильртрах
        /// </summary>
        public int CountEntitys
        {
            get
            {
                return GetAllEntityCount();
            }
        }

        internal IFilter GetFilter(Type type, List<Type> typesWithoutComponents)
        {
            foreach (IFilter filter in _filters)
            {
                if (filter.GetType().Equals(type))
                {
                    if (filter.TypesWithoutComponents.Count == typesWithoutComponents.Count)
                    {
                        foreach (Type filter1TypesWithoutComponent in filter.TypesWithoutComponents)
                        {
                            bool next = false;
                            foreach (Type filter2TypesWithoutComponent in typesWithoutComponents)
                            {
                                if (filter1TypesWithoutComponent.FullName == filter2TypesWithoutComponent.FullName)
                                {
                                    next = true;
                                    break;
                                }
                            }

                            if (next)
                            {
                                continue;
                            }
                            throw new Exception($"Тип фильтра {type.FullName} с исключающими компонентами не зарегистрирован в ECSCore");
                        }

                        return filter;
                    }
                } 
            }
            throw new Exception($"Тип фильтра {type.FullName} не зарегистрирован в ECSCore");
        }

        internal void Add<T>(T component)
            where T: IComponent
        {
            if (_ecs.GetEntity(component.Id, out Entity entity))
            {
                foreach (IFilter filter in _filters)
                {
                    filter.Add(component, entity);
                }
            }
        }

        internal void Remove<T>(Guid id)
        {
            if (_ecs.GetEntity(id, out Entity entity))
            {
                foreach (IFilter filter in _filters)
                {
                    filter.Remove<T>(entity);
                }
            }
        }

        internal void Remove(Guid id)
        {
            foreach (IFilter filter in _filters)
            {
                filter.RemoveEntity(id);
            }
        }

        internal void AddFilter(FilterBase filter)
        {
            foreach (FilterBase selectedFilter in _filters)
            {
                if (CheckFilters(selectedFilter, filter))
                {
                    return;
                };
            }
            _filters.Add(filter);
        }

        private bool CheckFilters(FilterBase filter1, FilterBase filter2)
        {
            if (filter1.TypesExistComponents.Count == filter2.TypesExistComponents.Count)
            {
                if (filter1.TypesWithoutComponents.Count == filter2.TypesWithoutComponents.Count)
                {
                    foreach (Type filter1TypeExistComponent in filter1.TypesExistComponents)
                    {
                        bool next = false;
                        foreach (Type filter2TypeExistComponent in filter2.TypesExistComponents)
                        {
                            if (filter1TypeExistComponent.FullName == filter2TypeExistComponent.FullName)
                            {
                                next = true;
                                break;
                            }
                        }
                        if (next)
                        {
                            continue;
                        }
                        return false;
                    }

                    foreach (Type filter1TypesWithoutComponent in filter1.TypesWithoutComponents)
                    {
                        bool next = false;
                        foreach (Type filter2TypesWithoutComponent in filter2.TypesWithoutComponents)
                        {
                            if (filter1TypesWithoutComponent.FullName == filter2TypesWithoutComponent.FullName)
                            {
                                next = true;
                                break;
                            }
                        }
                        if (next)
                        {
                            continue;
                        }
                        return false;
                    }

                    for (int i=0; i<filter1.TypesExistComponents.Count; i++)
                    {
                        if (filter1.TypesExistComponents[i].FullName != filter2.TypesExistComponents[i].FullName)
                        {
                            throw new Exception($"Дублирование фильтров! " +
                                $""); //TODO Вернуть тип фильтров / систем
                        }
                    } 

                    for (int i = 0; i < filter1.TypesWithoutComponents.Count; i++)
                    {
                        if (filter1.TypesWithoutComponents[i].FullName != filter2.TypesWithoutComponents[i].FullName)
                        {
                            throw new Exception($"Дублирование фильтров! " +
                                $""); //TODO Вернуть тип фильтров / систем
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private void Init() { }

        private int GetAllEntityCount()
        {
            int countAllEntitys = 0;
            foreach (IFilter filter in _filters)
            {
                countAllEntitys += filter.Count;
            }
            return countAllEntitys;
        }
    }
}