using ECSCore.BaseObjects;
using ECSCore.Filters;
using ECSCore.System;

namespace ECSCore.Systems
{
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class System<T0> : SystemBase
        where T0 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<T0> Filter { get; set; }
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal override void GetFilter()
        {
            Filter = (Filter<T0>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        internal override void AсtionForeach()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                Action(t0);
            }
        }
        /// <summary>
        /// Работа системы
        /// </summary>
        public abstract void Action(T0 t0);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class System<T0, T1> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<T0, T1> Filter { get; set; } = new Filter<T0, T1>();
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal override void GetFilter()
        {
            Filter = (Filter<T0, T1>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        internal override void AсtionForeach()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                Action(t0, Filter.ComponentsT1[t0.Id]);
            }
        }
        /// <summary>
        /// Работа системы
        /// </summary>
        public abstract void Action(T0 t0, T1 t1);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class System<T0, T1, T2> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<T0, T1, T2> Filter { get; set; } = new Filter<T0, T1, T2>();
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal override void GetFilter()
        {
            Filter = (Filter<T0, T1, T2>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        internal override void AсtionForeach()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                Action(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id]);
            }
        }
        /// <summary>
        /// Работа системы
        /// </summary>
        public abstract void Action(T0 t0, T1 t1, T2 t2);
    }
    /// <summary>
    /// Базовый класс систем
    /// Каждую систему необходимо пометить атрибутами:
    /// 1) [AttributeSystemPriority] ;
    /// 2) [AttributeSystemCalculate] ;
    /// 3) [AttributeSystemEnable] ;
    /// </summary>
    public abstract class System<T0, T1, T2, T3> : SystemBase
        where T0 : ComponentBase
        where T1 : ComponentBase
        where T2 : ComponentBase
        where T3 : ComponentBase
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<T0, T1, T2, T3> Filter { get; set; } = new Filter<T0, T1, T2, T3>();
        /// <summary>
        /// Инициализация системы, необходима для получения ссылки на фильтр
        /// </summary>
        internal override void GetFilter()
        {
            Filter = (Filter<T0, T1, T2, T3>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2, T3>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter()
        {
            Filter.Сalculate();
        }
        /// <summary>
        /// Выполнение системы.
        /// (Вызывается с интервалом, заданным через атрибут)
        /// </summary>
        internal override void AсtionForeach()
        {
            foreach (T0 t0 in Filter.ComponentsT0.Values)
            {
                Action(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id], Filter.ComponentsT3[t0.Id]);
            }
        }
        /// <summary>
        /// Работа системы
        /// </summary>
        public abstract void Action(T0 t0, T1 t1, T2 t2, T3 t3);
    }
}