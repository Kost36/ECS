using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryRemove : IJobToFilter
    {
        private readonly int _entityId;

        internal JobTryRemove(int entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryRemove(_entityId);
        }
    }
}