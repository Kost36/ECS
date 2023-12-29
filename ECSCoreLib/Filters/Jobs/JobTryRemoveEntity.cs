using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;
using System;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryRemoveEntity : IJobToFilter
    {
        private readonly Guid _entityId;

        internal JobTryRemoveEntity(Guid entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryRemoveEntity(_entityId);
        }
    }
}