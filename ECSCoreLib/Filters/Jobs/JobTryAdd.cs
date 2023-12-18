using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryAdd : IJobToFilter
    {
        private readonly int _entityId;

        internal JobTryAdd(int entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryAdd(_entityId);
        }
    }
}