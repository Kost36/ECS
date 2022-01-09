using ECSCore.BaseObjects;
using ECSCore.Filters;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер фильтров
    /// </summary>
    public class ManagerFilters
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ecs"> Ссылка на ecs </param>
        internal ManagerFilters(ECS ecs)
        {
            _ecs = ecs;
            Init();
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private readonly ECS _ecs;
        /// <summary>
        /// Список фильтров групп компонент
        /// </summary>
        private readonly List<IFilter> _filters = new List<IFilter>();
        #endregion

        #region Свойства
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
        #endregion

        #region Внутренние методы
        /// <summary>
        /// Получить фильтр компонентов
        /// </summary>
        /// <returns></returns>
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
                                } //Если есть совпадение
                            } //Проверим TypesWithoutComponents
                            if (next)
                            {
                                continue;
                            }
                            throw new Exception($"Тип фильтра {type.FullName} с исключающими компонентами не зарегистрирован в ECSCore");
                        } //Проверим TypesWithoutComponents

                        return filter;
                    } //Если количество исключающих компонент совпадает
                } //Если тип совпал
            } //Пройдемся по всем фильтрам
            throw new Exception($"Тип фильтра {type.FullName} не зарегистрирован в ECSCore");
        }
        /// <summary>
        /// Добавить компонент к фильтрам
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        internal void Add<T>(T component)
            where T: IComponent
        {
            if (_ecs.GetEntity(component.Id, out Entity entity))
            {
                foreach (IFilter filter in _filters)
                {
                    filter.Add(component, entity); //Добавляем (Попытка, добавить или нет проверяет группа)
                } //Проходимся по всем группам 
            }
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности из фильтров
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        internal void Remove<T>(int id)
        {
            if (_ecs.GetEntity(id, out Entity entity))
            {
                foreach (IFilter filter in _filters)
                {
                    filter.Remove<T>(entity); //Удаляем (Попытка, удалить или нет проверяет группа) 
                } //Проходимся по всем группам
            }
        }
        /// <summary>
        /// Удалить все компоненты с id, из фильтров
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        internal void Remove(int id)
        {
            foreach (IFilter filter in _filters)
            {
                filter.RemoveEntity(id); //Удаляем (Попытка, удалить или нет проверяет группа) 
            } //Проходимся по всем группам 
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal void AddFilter(FilterBase filter)
        {
            foreach (FilterBase selectedFilter in _filters)
            {
                if (CheckFilters(selectedFilter, filter))
                {
                    return;
                }; //Проверить наличие подобного фильтра
            } //Проходимся по всем фильтрам
            _filters.Add(filter);
        }

        /// <summary>
        /// Проверить фильтры 
        /// </summary>
        /// <param name="filter1"> Фильтр 1 </param>
        /// <param name="filter2"> Фильтр 2 </param>
        /// <returns> true(Есть фильтр с точным совпадением) / false(Необходимо зарегистрировать фильтр)</returns>
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
                            } //Если есть совпадение
                        } //Проверим TypesExistComponents
                        if (next)
                        {
                            continue;
                        }
                        return false;
                    } //Проверим TypesExistComponents
                    foreach (Type filter1TypesWithoutComponent in filter1.TypesWithoutComponents)
                    {
                        bool next = false;
                        foreach (Type filter2TypesWithoutComponent in filter2.TypesWithoutComponents)
                        {
                            if (filter1TypesWithoutComponent.FullName == filter2TypesWithoutComponent.FullName)
                            {
                                next = true;
                                break;
                            } //Если есть совпадение
                        } //Проверим TypesWithoutComponents
                        if (next)
                        {
                            continue;
                        }
                        return false;
                    } //Проверим TypesWithoutComponents

                    //Есть совпадающие фильтры.
                    for (int i=0; i<filter1.TypesExistComponents.Count; i++)
                    {
                        if (filter1.TypesExistComponents[i].FullName != filter2.TypesExistComponents[i].FullName)
                        {
                            throw new Exception($"Дублирование фильтров! " +
                                $""); //TODO Вернуть тип фильтров / систем
                        } //Если есть совпадение
                    } //Проверим совпадение позиций
                    for (int i = 0; i < filter1.TypesWithoutComponents.Count; i++)
                    {
                        if (filter1.TypesWithoutComponents[i].FullName != filter2.TypesWithoutComponents[i].FullName)
                        {
                            throw new Exception($"Дублирование фильтров! " +
                                $""); //TODO Вернуть тип фильтров / систем
                        } //Если есть совпадение
                    } //Проверим совпадение позиций
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Инициализация 
        /// </summary>
        private void Init() { }
        /// <summary>
        /// Получить общее количество сущьностей в фильтрах
        /// </summary>
        private int GetAllEntityCount()
        {
            int countAllEntitys = 0;
            foreach (IFilter filter in _filters)
            {
                countAllEntitys += filter.Count;
            } //Пройдемся по существующим коллекциям
            return countAllEntitys;
        }
        #endregion
    }
}