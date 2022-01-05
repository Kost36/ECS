using ECSCore.Attributes;
using ECSCore.Interfaces;
using ECSCore.Managers;
using ECSCore.Exceptions;
using System;
using System.Reflection;
using ECSCore.Interfaces.ECS;
using ECSCore.Interfaces.Systems;
using ECSCore.Interfaces.GroupComponents;

namespace ECSCore.BaseObjects
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

        #region ISystemRunTime Реализация
        /// <summary>
        /// Интервал времени между предидущим выполнением и фактическим.
        /// Размерность: sec
        /// </summary>
        public float DeltaTime { get; set; }
        /// <summary>
        /// Интерфейс взаимодействия с ECS из систем
        /// </summary>
        public IECSSystem IECS { get => ECS; }
        #endregion

        #region ISystemSetting Реализация
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
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        public bool IsActionAdd { get; set; }
        /// <summary>
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        public bool IsAction { get; set; }
        /// <summary>
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        public bool IsActionRemove { get; set; }
        #endregion

        #region Свойства
        /// <summary>
        /// ECS
        /// </summary>
        internal ECS ECS { get; set; }
        /// <summary>
        /// Фильтр системы
        /// </summary>
        internal abstract FilterBase FilterBase { get; }
        #endregion

        #region Реализация в данном класcе на уровне ECSCore
        /// <summary>
        /// Метод инициализации системы
        /// </summary>
        /// <param name="managerFilters"></param>
        /// <param name="eCS"></param>
        internal void Init(ManagerFilters managerFilters, ECS eCS)
        {
            ECS = eCS;
            managerFilters.AddFilter(FilterBase); //Проверить наличие и зарегистрировать фильтр
            GetFilter(managerFilters); //Подтянуть нужный фильтр
            FilterBase.ECSSystem = eCS; //Ввод зависимости
            FilterBase.AddInterestedSystem(this); //Добавить в фильтр заинтересованную систему
        }
        /// <summary>
        /// Предварительная инициализация системы (Подтяжка аттребутов)
        /// </summary>
        internal void GetAttributes()
        {
            Type type = this.GetType();
            //Аттребуты
            AttributeSystemPriority attributeSystemPriority = type.GetCustomAttribute<AttributeSystemPriority>();
            if (attributeSystemPriority == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(AttributeSystemPriority), type);
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
            AttributeSystemEarlyExecution attributeSystemEarlyExecution = type.GetCustomAttribute<AttributeSystemEarlyExecution>();
            if (attributeSystemEarlyExecution == null)
            {
                EarlyExecutionTicks = 0;
            } //Если у системы нету атрибута предварительного выполнения 
            else
            {
                EarlyExecutionTicks = (long)(((float)IntervalTicks / 100f) * attributeSystemEarlyExecution.PercentThresholdTime);
            }//Если у системы есть атрибута предварительного выполнения  

            //Интерфейсы
            IsActionAdd = false;
            IsAction = false;
            IsActionRemove = false;
            if (this is ISystemActionAdd)
            {
                IsActionAdd = true;
            }
            if (this is ISystemAction)
            {
                IsAction = true;
            }
            if (this is ISystemActionRemove)
            {
                IsActionRemove = true;
            }
        }
        /// <summary>
        /// Получить количество элементов в фильтре
        /// </summary>
        /// <returns></returns>
        public int GetFilterCount()
        {
            return FilterBase.Count;
        }
        /// <summary>
        /// Установить в филтре флаг теста
        /// </summary>
        public void SetTestFlag()
        {
            FilterBase.FlagTest = true;
        }
        #endregion

        #region Реализация в наследниках на уровне ECSCore
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal abstract void GetFilter(ManagerFilters managerFilters);
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal abstract void CalculateFilter(long limitTimeTicks = 0);
        /// <summary>
        /// Вызвать ActionAdd у реализации
        /// </summary>
        internal abstract void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
            where TGroupComponents : IGroupComponents;
        /// <summary>
        /// Проход по коллекции и вызов Action для всех item
        /// </summary>
        internal abstract void Aсtion();
        /// <summary>
        /// Вызвать ActionRemove у реализации
        /// </summary>
        internal abstract void AсtionRemove(int entityId);
        #endregion
    }
}