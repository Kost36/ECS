﻿using ECSCore.Attributes;
using ECSCore.Interfaces;
using ECSCore.Managers;
using ECSCore.Exceptions;
using System;
using System.Reflection;
using ECSCore.Filters;
using ECSCore.BaseObjects;

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
        #region Конструктор
        internal SystemBase() { }
        #endregion

        #region Реализация ISystem
        /// <summary>
        /// Система включена (Работает)
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Интервал выполнения системы
        /// </summary>
        public long IntervalTicks { get; set; }
        /// <summary>
        /// Возможное предварительное выполнение системы (Предварительный интервал ticks)
        /// </summary>
        public long EarlyExecutionTicks { get; set; }
        /// <summary>
        /// Интервал времени между предидущим выполнением и фактическим.
        /// Размерность: sec
        /// </summary>
        public float DeltaTime { get; private set; }
        /// <summary>
        /// Интерфейс взаимодействия с ECS из систем
        /// </summary>
        public IECSSystem IECS { get => ECS; }
        #endregion

        #region Зависимости
        /// <summary>
        /// ECS
        /// </summary>
        internal ECS ECS { get; set; }
        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        internal ManagerFilters ManagerFilters { get; set; }
        /// <summary>
        /// Время последнего выполнения
        /// </summary>
        internal DateTime DateTimeOldRun { get; set; }
        #endregion

        #region Реализация в данном класcе на уровне ECSCore
        /// <summary>
        /// Метод введения зависимостей
        /// </summary>
        /// <param name="managerFilters"></param>
        /// <param name="eCS"></param>
        internal void Injection(ManagerFilters managerFilters, ECS eCS)
        {
            ManagerFilters = managerFilters;
            ECS = eCS;
            GetFilter();
        }
        /// <summary>
        /// Предварительная инициализация системы (Подтяжка аттребутов)
        /// </summary>
        internal void GetAttributes()
        {
            Type type = this.GetType();
            object attrebute = type.GetCustomAttributes(typeof(AttributeSystemPriority),false);
            AttributeSystemPriority attributeSystemPriority = (AttributeSystemPriority)attrebute; //type.GetCustomAttribute();
            if (attributeSystemPriority == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemPriority), type);
            } //Если у системы нету атрибута приоритетности
            else
            {
                Priority = attributeSystemPriority.Priority;//Activator.CreateInstance(attrebutes); attributeSystemPriority.Priority;
            } //Если у системы есть атрибут приоритетности

            object attrebute1 = type.GetCustomAttributes(typeof(AttributeSystemCalculate), false);
            AttributeSystemCalculate attributeSystemCalculate = (AttributeSystemCalculate)attrebute1;
            //AttributeSystemCalculate attributeSystemCalculate = type.GetCustomAttribute<AttributeSystemCalculate>();
            if (attributeSystemCalculate == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemCalculate), type);
            } //Если у системы нету атрибута интервала обработки 
            IntervalTicks = attributeSystemCalculate.CalculateInterval * TimeSpan.TicksPerMillisecond;

            object attrebute2 = type.GetCustomAttributes(typeof(AttributeSystemEnable), false);
            AttributeSystemEnable attributeSystemEnable = (AttributeSystemEnable)attrebute2;
            //AttributeSystemEnable attributeSystemEnable = type.GetCustomAttribute<AttributeSystemEnable>();
            if (attributeSystemEnable == null)
            {
                IsEnable = true;
            } //Если у системы нету атрибута активации 
            else
            {
                IsEnable = attributeSystemEnable.IsEnable;
            }//Если у системы есть атрибута активации 

            object attrebute3 = type.GetCustomAttributes(typeof(AttributeSystemEarlyExecution), false);
            AttributeSystemEarlyExecution attributeSystemEarlyExecution = (AttributeSystemEarlyExecution)attrebute3;
            //AttributeSystemEarlyExecution attributeSystemEarlyExecution = type.GetCustomAttribute<AttributeSystemEarlyExecution>();
            if (attributeSystemEarlyExecution == null)
            {
                EarlyExecutionTicks = 0;
            } //Если у системы нету атрибута предварительного выполнения 
            else
            {
                EarlyExecutionTicks = (long)(((float)IntervalTicks / 100f) * attributeSystemEarlyExecution.PercentThresholdTime);
            }//Если у системы есть атрибута предварительного выполнения  
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal void CalculateDeltaTime(DateTime dateTimeFact)
        {
            DeltaTime = (float)dateTimeFact.Subtract(DateTimeOldRun).TotalSeconds;
        }
        #endregion

        #region Реализация в наследниках на уровне ECSCore
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal abstract void GetFilter();
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal abstract void CalculateFilter(long limitTimeTicks = 0);
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        internal abstract void AсtionForeach();
        #endregion
    }
}