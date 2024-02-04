using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;
using System;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryAdd : IJobToFilter
    {
        private readonly Guid _entityId;

        internal JobTryAdd(Guid entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryAdd(_entityId);
        }
    }
}