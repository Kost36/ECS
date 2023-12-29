using ECSCore.Attributes;
using ECSCore.Managers;
using ECSCore.Exceptions;
using System;
using System.Reflection;
using ECSCore.Interfaces.ECS;
using ECSCore.Interfaces.Systems;
using ECSCore.Interfaces.GroupComponents;
using ECSCore.Enums;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс систем
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
        /// Интервал выполнения системы
        /// </summary>
        public long IntervalTicks { get; set; }
        /// <summary>
        /// Возможное предварительное выполнение системы (Предварительный интервал ticks)
        /// </summary>
        public long EarlyExecutionTicks { get; set; }
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        public int Priority { get; set; }

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

        /// <summary>
        /// Флаг распараллеливания Action системы
        /// </summary>
        public bool IsParallel { get; set; }
        /// <summary>
        /// Количество потоков для распараллеливания
        /// </summary>
        public int CountThreads { get; set; } = 1;
        /// <summary>
        /// Флаг ручного управления циклом обработки в Action
        /// </summary>
        public bool IsManualControlAction { get; set; }
        /// <summary>
        /// Флаг выполнения ActionAdd/Action/ActionRemove в введенном, в виде зависимости потоке
        /// </summary>
        public bool IsUseInjectThread { get; set; }
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
        /// <summary>
        /// Тип исключающего компонента
        /// </summary>
        internal Type ExcludeComponentType { get; set; }
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
            if (ExcludeComponentType != null)
            {
                FilterBase.TypesWithoutComponents.Add(ExcludeComponentType);//Добавим исключающий компонет в фильтр
            } //Если у систем есть исключающий компонент
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
            //Аттрибуты

            //Приоритет выполнения системы
            SystemPriority attributeSystemPriority = type.GetCustomAttribute<SystemPriority>();
            if (attributeSystemPriority == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(SystemPriority), type);
            } //Если у системы нету атрибута приоритетности
            else
            {
                Priority = attributeSystemPriority.Priority;
            } //Если у системы есть атрибут приоритетности

            //Интервал обработки системы
            SystemCalculate attributeSystemCalculate = type.GetCustomAttribute<SystemCalculate>();
            if (attributeSystemCalculate == null)
            {
                throw new ExceptionSystemNotHaveAttribute(typeof(SystemCalculate), type);
            } //Если у системы нету атрибута интервала обработки 
            IntervalTicks = attributeSystemCalculate.CalculateInterval * TimeSpan.TicksPerMillisecond;

            //Активация / блокировка системы
            IsEnable = true; //Стандартно: все системы включены
            SystemEnable attributeSystemEnable = type.GetCustomAttribute<SystemEnable>();
            if (attributeSystemEnable != null)
            {
                IsEnable = attributeSystemEnable.IsEnable;
            } //Если у системы есть атрибут активации

            //Возможность раннего/предварительного выполнения системы
            EarlyExecutionTicks = 0; //Стандартно: - предварительное выполнение системы выключено
            SystemEarlyExecution attributeSystemEarlyExecution = type.GetCustomAttribute<SystemEarlyExecution>();
            if (attributeSystemEarlyExecution != null)
            {
                EarlyExecutionTicks = (long)(((float)IntervalTicks / 100f) * attributeSystemEarlyExecution.PercentThresholdTime);
            } //Если у системы есть атрибут предварительного выполнения 

            //Количество потоков для распараллеливания
            CountThreads = 1; //Стандартно: если включено распараллеливание, то выполняется в одном отдельном потоке
            SystemParallelCountThreads attributeSystemParallelCountThreads = type.GetCustomAttribute<SystemParallelCountThreads>();
            if (attributeSystemParallelCountThreads != null)
            {
                CountThreads = attributeSystemParallelCountThreads.CountThreads;
            } //Если у системы есть атрибут - колличества потоков для параллельного выполнения 

            //Исключающиеся из системы компонент
            ExcludeComponentSystem attributeExcludeComponentSystem = type.GetCustomAttribute<ExcludeComponentSystem>();
            if (attributeExcludeComponentSystem != null)
            {
                ExcludeComponentType = attributeExcludeComponentSystem.ExcludeComponentType;
            } //Если у системы есть атрибут - исключающиеся из системы компонента 


            //Интерфейсы
            IsActionAdd = false;
            if (this is ISystemActionAdd)
            {
                IsActionAdd = true;
            }
            IsAction = false;
            if (this is ISystemAction)
            {
                IsAction = true;
            }
            IsActionRemove = false;
            if (this is ISystemActionRemove)
            {
                IsActionRemove = true;
            }
            IsParallel = false;
            if (this is ISystemParallel)
            {
                IsParallel = true;
                if (CountThreads==0)
                {
                    CountThreads = 1;
                } //Потоков не должно быть 0
            }
            IsManualControlAction = false;
            if (this is ISystemManualControlAction)
            {
                IsManualControlAction = true;
            }
            IsUseInjectThread = false;
            if (this is ISystemUseInjectThread)
            {
                IsUseInjectThread = true;
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
        /// Выполнить Action системы
        /// </summary>
        internal void RunAction()
        {
            if (IsUseInjectThread)
            {
                Aсtion(systemActionType: SystemActionType.RunInInjectThread); //Синхронно в введенном потоке
                return;
            } //Если выполнение должно быть синхронно в введенном потоке
            if (IsParallel)
            {
                if (CountThreads == 1)
                {
                    Aсtion(systemActionType: SystemActionType.RunInOneThread); //Выполить в одном отдельном потоке
                    return;
                } //Если поток всего 1

                //Считаем кол-во объектов на поток
                int maxCountOnThread = 100;
                if (FilterBase.Count > CountThreads * maxCountOnThread) //TODO Вынести в переменную, что бы не считать каждый раз
                {
                    maxCountOnThread = (int)Math.Ceiling((float)FilterBase.Count / (float)CountThreads); //Считаем количество объектов на один поток
                } //Если на один поток приходиться минимум по 100 объектов

                //Выполняем
                Aсtion(systemActionType: SystemActionType.RunInThreads, maxCountOnThread: maxCountOnThread); //Выполить в нескольких потоках
                return;
            } //Если система должна выполняться параллельно
            Aсtion(systemActionType: SystemActionType.RunInThisThread); //Выполить в текущем потоке
        }

        /// <summary>
        /// Выполнить AсtionAdd системы
        /// </summary>
        /// <typeparam name="TGroupComponents"> generic группы компонентов </typeparam>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <param name="groupComponents"> группа компонентов </param>
        internal void RunAсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
            where TGroupComponents : IGroupComponents
        {
            if (IsUseInjectThread)
            {
                AсtionAdd(groupComponents, entity); //Синхронно в введенном потоке
                return;
            } //Если выполнение должно быть синхронно в введенном потоке
            AсtionAdd(groupComponents, entity);
        }

        /// <summary>
        /// Выполнить AсtionRemove системы
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        internal void RunAсtionRemove(Guid entityId)
        {
            if (IsUseInjectThread)
            {
                ActionRemove(entityId); //Синхронно в введенном потоке
                return;
            } //Если выполнение должно быть синхронно в введенном потоке
            ActionRemove(entityId); 
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
        /// Реализация AсtionAdd системы
        /// </summary>
        /// <typeparam name="TGroupComponents"> generic группы компонентов </typeparam>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <param name="groupComponents"> группа компонентов </param>
        internal abstract void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
            where TGroupComponents : IGroupComponents;

        /// <summary>
        /// Проход по коллекции и вызов Action для всех item
        /// </summary>
        internal abstract void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue);

        /// <summary>
        /// Метод обработки удаленной группы компонент из фильтра системы
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        public virtual void ActionRemove(Guid entityId) { }
        #endregion
    }
}