using ECSCore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Interface
{
    /// <summary>
    /// Интерфейс системы;
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemInterestComponent] | [AttributeSystemInterestComponents] ;
    /// </summary>
    public interface ISystem {
        public int Priority { get; set; }
        /// <summary>
        /// Интервал выполнения
        /// </summary>
        public long IntervalTicks { get; set; }
        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        public ManagerFilters ManagerFilters { get; set; }
        /// <summary>
        /// Иньекция данных
        /// </summary>
        /// <param name="managerFilters"></param>
        public abstract void Injection(ManagerFilters managerFilters);
        public abstract void PreInitialization();
        /// <summary>
        /// Инициализация системы
        /// </summary>
        public abstract void Initialization();
        /// <summary>
        /// Предобработка системы
        /// </summary>
        public abstract void PreAсtion();
        /// <summary>
        /// Обработка системы
        /// </summary>
        public abstract void Aсtion();
    }
}