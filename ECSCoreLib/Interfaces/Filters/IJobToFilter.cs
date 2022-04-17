using ECSCore.BaseObjects;

namespace ECSCore.Interfaces.Filters
{
    /// <summary>
    /// Задание для фильтра
    /// </summary>
    internal interface IJobToFilter
    {
        /// <summary>
        /// Действие
        /// </summary>
        /// <param name="filter"></param>
        void Action(FilterBase filter);
    }
}