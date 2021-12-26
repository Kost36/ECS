using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;

namespace ECSCore.Filters
{
    /// <summary>
    /// Задание на добавление компонента в фильтр
    /// </summary>
    internal class JobTryAdd : IJobToFilter
    {
        #region Конструкторы
        internal JobTryAdd(ComponentBase component, EntityBase entity)
        {
            _component = component;
            _entity = entity;
        }
        #endregion

        #region Поля
        private ComponentBase _component;
        private EntityBase _entity;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryAdd(_component, _entity);
        }
        #endregion
    }
    /// <summary>
    /// Задание на удаление компонента из фильтра
    /// </summary>
    internal class JobTryRemove : IJobToFilter
    {
        #region Конструкторы
        internal JobTryRemove(Type typeComponent, EntityBase entity)
        {
            _typeComponent = typeComponent;
            _entity = entity;
        }
        #endregion

        #region Поля
        private Type _typeComponent;
        private EntityBase _entity;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryRemove(_typeComponent, _entity);
        }
        #endregion
    }
    /// <summary>
    /// Задание на удаление компонентов сущьности из фильтра
    /// </summary>
    internal class JobTryRemoveId : IJobToFilter
    {
        #region Конструкторы
        internal JobTryRemoveId(int id)
        {
            _id = id;
        }
        #endregion

        #region Поля
        private int _id;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryRemove(_id);
        }
        #endregion
    }
}