using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.Interfaces
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