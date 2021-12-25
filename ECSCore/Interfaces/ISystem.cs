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
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public interface ISystem {

        /// <summary>
        /// Иньекция данных
        /// </summary>
        /// <param name="managerFilters"></param>
        public abstract void Injection(ManagerFilters managerFilters, ECS eCS);
        public abstract void PreInitialization();
        public void CalculateDeltaTime(DateTime dateTimeFact);
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