using ECSCore.BaseObjects;
using ECSCore.Filters;
using ECSCore.Interfaces;
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
        internal ManagerFilters(ECS ecs, Assembly assembly, int startCapacityCollections)
        {
            _ecs = ecs;
            if (startCapacityCollections > 10)
            {
                _startCapacityCollections = startCapacityCollections;
            }
            Init(assembly);
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Стартовая вместимость коллекций
        /// </summary>
        private int _startCapacityCollections = 10;
        /// <summary>
        /// Список фильтров групп компонент
        /// </summary>
        private List<IFilter> _filters = new List<IFilter>();
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

        #region Публичные методы
        /// <summary>
        /// Получить фильтр компонентов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal IFilter GetFilter<T>()
            where T : IFilter
        {
            Type type = typeof(T); //Получим тип
            foreach (IFilter filter in _filters)
            {
                if (filter.GetType().Equals(type))
                {
                    return filter;
                } //Если тип совпал
            } //Пройдемся по всем фильтрам
            throw new Exception($"Тип фильтра {typeof(T).FullName} не зарегистрирован в ECSCore");
        }
        /// <summary>
        /// Получить фильтр компонентов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal IFilter GetFilter(Type type)
        {
            foreach (IFilter filter in _filters)
            {
                if (filter.GetType().Equals(type))
                {
                    return filter;
                } //Если тип совпал
            } //Пройдемся по всем фильтрам
            throw new Exception($"Тип фильтра {type.FullName} не зарегистрирован в ECSCore");
        }
        /// <summary>
        /// Добавить компонент к фильтрам
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public void Add(ComponentBase component)
        {
            if (_ecs.GetEntity(component.Id, out EntityBase entity))
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
        public void Remove<T>(int id)
        {
            if (_ecs.GetEntity(id, out EntityBase entity))
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
        public void Remove(int id)
        {
            foreach (IFilter filter in _filters)
            {
                filter.RemoveOfId(id); //Удаляем (Попытка, удалить или нет проверяет группа) 
            } //Проходимся по всем группам 
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private void AddFilter(IFilter filter)
        {
            List<Type> typesComponent = filter.GetTypesComponents();
            foreach (IFilter selectedFilter in _filters)
            {
                if (selectedFilter.CheckFilter(typesComponent))
                {
                    throw new Exception($"Дублирование фильтров! Класс: {filter.GetType().FullName} пересекается с классом: {selectedFilter.GetType().FullName}");
                }; //Проверить наличие подобного фильтра
            } //Проходимся по всем фильтрам
            _filters.Add(filter);
        }
        /// <summary>
        /// Инициализация 
        /// </summary>
        private void Init(Assembly assembly)
        {
            Type typeISystem = typeof(ISystem); //Получим тип интерфейса
            Type[] types = assembly.GetTypes(); //Получаем все типы сборки 
            List<Type> typesSystems = types.Where(t => typeISystem.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList(); //Получим все системы в сборке
            foreach (Type typesSystem in typesSystems)
            {
                Type[] GenericTypes = typesSystem.BaseType.GenericTypeArguments;
                Type filterRunTimeImplementation = null;
                switch (GenericTypes.Length)
                {
                    case 1:
                        filterRunTimeImplementation = typeof(Filter<>);
                        break;
                    case 2:
                        filterRunTimeImplementation = typeof(Filter<,>);
                        break;
                    case 3:
                        filterRunTimeImplementation = typeof(Filter<,,>);
                        break;
                    case 4:
                        filterRunTimeImplementation = typeof(Filter<,,>);
                        break;
                }
                Type makeme = filterRunTimeImplementation.MakeGenericType(GenericTypes);
                IFilter filter = (IFilter)Activator.CreateInstance(makeme);
                filter.Init(_startCapacityCollections); //Инициализация
                AddFilter(filter); //Добавим в список
            } //Пройдемся по всем группам 
        }
        /// <summary>
        /// Получить общее количество сущьностей в фильтрах
        /// </summary>
        private int GetAllEntityCount()
        {
            int countAllEntitys = 0;
            foreach (IFilter filter in _filters)
            {
                countAllEntitys = countAllEntitys + filter.Count;
            } //Пройдемся по существующим коллекциям
            return countAllEntitys;
        }
        #endregion
    }
}