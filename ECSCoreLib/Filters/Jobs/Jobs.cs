using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Filters;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryAdd : IJobToFilter
    {
        #region Конструкторы
        internal JobTryAdd(int entityId)
        {
            _entityId = entityId;
        }
        #endregion

        #region Поля
        private int _entityId;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryAdd(_entityId);
        }
        #endregion
    }
    internal class JobTryRemove : IJobToFilter
    {
        #region Конструкторы
        internal JobTryRemove(int entityId)
        {
            _entityId = entityId;
        }
        #endregion

        #region Поля
        private int _entityId;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryRemove(_entityId);
        }
        #endregion
    }
    internal class JobTryRemoveEntity : IJobToFilter
    {
        #region Конструкторы
        internal JobTryRemoveEntity(int entityId)
        {
            _entityId = entityId;
        }
        #endregion

        #region Поля
        private int _entityId;
        #endregion

        #region Действия
        public void Action(FilterBase filter)
        {
            filter.TryRemoveEntity(_entityId);
        }
        #endregion
    }
}