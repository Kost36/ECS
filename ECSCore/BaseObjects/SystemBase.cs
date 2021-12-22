using ECSCore.Attributes;
using ECSCore.Interface;
using ECSCore.Managers;
using ECSCore.Exceptions;
using System;
using System.Reflection;
using ECSCoreCore.Attributes;

namespace ECSCore.System
{
    /// <summary>
    /// Базовый класс систем
    /// </summary>
    public abstract class SystemBase : ISystem
    {
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Интервал выполнения системы
        /// </summary>
        public long IntervalTicks { get; set; }
        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        public ManagerFilters ManagerFilters { get; set; }
        /// <summary>
        /// Метод введения зависимостей
        /// </summary>
        /// <param name="managerFilters"></param>
        public void Injection(ManagerFilters managerFilters)
        {
            ManagerFilters = managerFilters;
        }
        /// <summary>
        /// Предварительная инициализация системы
        /// </summary>
        public void PreInitialization()
        {
            Type type = this.GetType();
            AttributeSystemPriority attributeSystemPriority = type.GetCustomAttribute<AttributeSystemPriority>();
            if (attributeSystemPriority == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemPriority), type);
            } //Если у системы нету атрибута приоритетности
            Priority = attributeSystemPriority.Priority;
            AttributeSystemCalculate attributeSystemCalculate = type.GetCustomAttribute<AttributeSystemCalculate>();
            if (attributeSystemCalculate == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemCalculate), type);
            } //Если у системы нету атрибута интервала обработки 
            IntervalTicks = attributeSystemCalculate.CalculateInterval * TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        public abstract void Initialization();
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public abstract void PreAсtion();
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        public abstract void Aсtion();   
    }
}