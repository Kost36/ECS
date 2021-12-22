using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Задание для фильтра
    /// </summary>
    public interface IJobToFilter
    {
        /// <summary>
        /// Действие
        /// </summary>
        /// <param name="filter"></param>
        public void Action(FilterBase filter);
    }
}