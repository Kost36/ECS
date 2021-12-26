using ECSCore.Attributes;
using ECSCore.Interface;
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
            Initialization();
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

    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class SystemBaseGeneric<T0> : SystemBase
        where T0 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        public Filter<T0> Filter { get; set; }

        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        public override void Initialization() 
        {
            Filter = (Filter<T0>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0>))); 
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        public override void Aсtion()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                ActionUser(t0);
            }
        }

        public abstract void ActionUser(T0 t0);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class SystemBaseGeneric<T0, T1> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        public Filter<T0, T1> Filter { get; set; } = new Filter<T0, T1>();

        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        public override void Initialization()
        {
            Filter = (Filter<T0,T1>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0,T1>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        public override void Aсtion()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                ActionUser(t0, Filter.ComponentsT1[t0.Id]);
            }
        }

        public abstract void ActionUser(T0 t0, T1 t1);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class SystemBaseGeneric<T0, T1, T2> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        public Filter<T0, T1, T2> Filter { get; set; } = new Filter<T0, T1, T2>();

        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        public override void Initialization()
        {
            Filter = (Filter<T0, T1, T2>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        public override void Aсtion()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                ActionUser(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id]);
            }
        }

        public abstract void ActionUser(T0 t0, T1 t1, T2 t2);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class SystemBaseGeneric<T0, T1, T2, T3> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
        where T3 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        public Filter<T0, T1, T2, T3> Filter { get; set; } = new Filter<T0, T1, T2, T3>();

        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        public override void Initialization()
        {
            Filter = (Filter<T0, T1, T2, T3>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2, T3>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        public override void Aсtion()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                ActionUser(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id], Filter.ComponentsT3[t0.Id]);
            }
        }

        public abstract void ActionUser(T0 t0, T1 t1, T2 t2, T3 t3);
    }
}