using ECSCore.BaseObjects;
using ECSCore.Interfaces.Filters;

namespace ECSCore.Filters.Jobs
{
    internal class JobTryRemoveEntity : IJobToFilter
    {
        private readonly int _entityId;

        internal JobTryRemoveEntity(int entityId)
        {
            _entityId = entityId;
        }

        public void Action(FilterBase filter)
        {
            filter.TryRemoveEntity(_entityId);
        }
    }
}