using ECSCore.Attributes;
using ECSCore.Interface;
using ECSCore.Managers;
using ECSCore.Exceptions;
using System;
using System.Reflection;

namespace ECSCore.System
{
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class SystemBase : ISystem
    {
        #region Параметры работы системы
        /// <summary>
        /// Система включена (Работает)
        /// </summary>
        public bool IsEnable;
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        public int Priority;
        /// <summary>
        /// Интервал выполнения системы
        /// </summary>
        public long IntervalTicks;
        #endregion

        #region Зависимости
        /// <summary>
        /// Ядро
        /// </summary>
        public ECS ECS { get; set; }
        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        public ManagerFilters ManagerFilters { get; set; }
        #endregion

        #region RunTime данные
        /// <summary>
        /// Время последнего выполнения
        /// </summary>
        public DateTime DateTimeOldRun { get; set; }
        /// <summary>
        /// Интервал времени между предидущим выполнением и фактическим
        /// Размерность: Sec
        /// </summary>
        public float DeltaTime { get; private set; }
        #endregion

        #region Реализация на уровне ECSCore
        /// <summary>
        /// Метод введения зависимостей
        /// </summary>
        /// <param name="managerFilters"></param>
        /// <param name="eCS"></param>
        public void Injection(ManagerFilters managerFilters, ECS eCS)
        {
            ManagerFilters = managerFilters;
            ECS = eCS;
        }
        /// <summary>
        /// Предварительная инициализация системы (Подтяжка аттребутов)
        /// </summary>
        public void PreInitialization()
        {
            Type type = this.GetType();
            AttributeSystemPriority attributeSystemPriority = type.GetCustomAttribute<AttributeSystemPriority>();
            if (attributeSystemPriority == null)
            {
                Priority = 50; //Не приоритетная система
                //throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemPriority), type);
            } //Если у системы нету атрибута приоритетности
            else
            {
                Priority = attributeSystemPriority.Priority;
            } //Если у системы есть атрибут приоритетности
            AttributeSystemCalculate attributeSystemCalculate = type.GetCustomAttribute<AttributeSystemCalculate>();
            if (attributeSystemCalculate == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemCalculate), type);
            } //Если у системы нету атрибута интервала обработки 
            IntervalTicks = attributeSystemCalculate.CalculateInterval * TimeSpan.TicksPerMillisecond;
            AttributeSystemEnable attributeSystemEnable = type.GetCustomAttribute<AttributeSystemEnable>();
            if (attributeSystemEnable == null)
            {
                IsEnable = true;
            } //Если у системы нету атрибута активации 
            else
            {
                IsEnable = attributeSystemEnable.IsEnable;
            }//Если у системы есть атрибута активации 
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public void CalculateDeltaTime(DateTime dateTimeFact)
        {
            DeltaTime = (float)dateTimeFact.Subtract(DateTimeOldRun).TotalSeconds;
        }
        #endregion

        #region Необходимо реализовать на уровне GAME
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
        #endregion
    }
}