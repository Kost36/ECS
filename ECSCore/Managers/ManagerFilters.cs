using ECSCore.BaseObjects;
using ECSCore.Interface;
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
        public ManagerFilters(ECS ecs, Assembly assembly)
        {
            _ecs = ecs;
            Init(assembly);
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Список фильтров групп компонент
        /// </summary>
        private List<IFilter> _filters = new List<IFilter>();
        #endregion

        #region Свойства
        #endregion

        #region Публичные методы
        /// <summary>
        /// Получить фильтр компонентов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IFilter GetFilter<T>()
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
        /// Добавить компонент к фильтрам
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public void Add(ComponentBase component)
        {
            EntityBase entity = (EntityBase)_ecs.GetEntity(component.Id);
            foreach (IFilter filter in _filters)
            {
                filter.Add(component, entity); //Добавляем (Попытка, добавить или нет проверяет группа) 
            } //Проходимся по всем группам 
        }
        /// <summary>
        /// Удалить заданный тип компонента, имеющий заданный id сущьности из фильтров
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void Remove<T>(int id)
        {
            EntityBase entity = (EntityBase)_ecs.GetEntity(id);
            foreach (IFilter filter in _filters)
            {
                filter.Remove<T>(entity); //Удаляем (Попытка, удалить или нет проверяет группа) 
            } //Проходимся по всем группам 
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
            Type typeIFilter = typeof(IFilter); //Получим тип интерфейса
            Type[] types = assembly.GetTypes(); //Получаем все типы сборки 
            List<Type> typesFilters = types.Where(t => typeIFilter.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();
            foreach(Type typeFilter in typesFilters)
            {
                IFilter filter = (IFilter)Activator.CreateInstance(typeFilter); //Создадим объект
                AddFilter(filter); //Добавим в список
            } //Пройдемся по всем группам 
        }
        #endregion
    }
}