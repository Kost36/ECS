using ECSCore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interfaces
{
    /// <summary>
    /// Интерфейс системы;
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    internal interface ISystem {

        /// <summary>
        /// Система включена (Работает)
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        int Priority { get; set; }
        /// <summary>
        /// Интервал выполнения системы
        /// </summary>
        long IntervalTicks { get; set; }
        /// <summary>
        /// Интерфейс взаимодействия с ECS из систем
        /// </summary>
        IECSSystem IECS { get; }
    }
}