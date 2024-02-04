using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;
using System;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryRemove : IJobToFilter
    {
        private readonly Guid _entityId;

        internal JobTryRemove(Guid entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryRemove(_entityId);
        }
    }
}