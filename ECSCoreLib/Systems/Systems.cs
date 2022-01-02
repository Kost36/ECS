using ECSCore.BaseObjects;
using ECSCore.Filters;
using ECSCore.Filters.GroupComponents;
using ECSCore.Interfaces;
using ECSCore.Managers;
using ECSCore.System;
using System;

namespace ECSCore.Systems
{
    //public abstract class System<TGroupComponents> : SystemBase
    //    where TGroupComponents : GroupComponents
    //{
    //    /// <summary>
    //    /// Фильтр
    //    /// </summary>
    //    internal Filter<GroupComponents> Filter { get; set; }
    //    /// <summary>
    //    /// Получение ссылки на фильтр
    //    /// </summary>
    //    internal override void GetFilter(ManagerFilters managerFilters)
    //    {
    //        Filter = (Filter<GroupComponents>)(managerFilters.GetFilter(typeof(Filter<GroupComponents>)));
    //    }
    //    /// <summary>
    //    /// Подготовка к выполнению, вызывается перед каждым выполнением
    //    /// </summary>
    //    internal override void CalculateFilter(long limitTimeTicks = 0)
    //    {
    //        if (limitTimeTicks == 0)
    //        {
    //            Filter.СalculateJob();
    //        }
    //        else
    //        {
    //            Filter.СalculateJob(limitTimeTicks);
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение системы.
    //    /// (Вызывается с интервалом, заданным через атрибут)
    //    /// </summary>
    //    internal override void Aсtion()
    //    {
    //        foreach (T0 t0 in Filter.ComponentsT0.Values)
    //        {
    //            Action(t0);
    //        }
    //    }
    //    /// <summary>
    //    /// Работа системы
    //    /// </summary>
    //    public abstract void Action(T0 t0);
    //}

    public abstract class SystemExistComponents<ExistComponentT1> : SystemBase
        where ExistComponentT1 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1>>();
        internal override FilterBase FilterBase => Filter;
        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1>>)(managerFilters.GetFilter(typeof(Filter<GroupComponentsExist<ExistComponentT1>>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter(long limitTimeTicks = 0)
        {
            if (limitTimeTicks == 0)
            {
                Filter.СalculateJob();
            }
            else
            {
                Filter.СalculateJob(limitTimeTicks);
            }
        }
        internal override void Aсtion()
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, DeltaTime);
            }
        }
        internal override void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
        {
            GroupComponentsExist<ExistComponentT1> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1>;
            ActionAdd(entityId, groupComponentsExist.ExistComponent1);
        }
        internal override void AсtionRemove(int entityId) { ActionRemove(entityId); }

        public virtual void ActionAdd(int entityId, ExistComponentT1 existComponentT1) { }
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, float deltaTime) { }
        public virtual void ActionRemove(int entityId) { }
    }
    public abstract class SystemExistComponents<ExistComponentT1, ExistComponentT2> : SystemBase
        where ExistComponentT1 : IComponent
        where ExistComponentT2 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>> Filter 
        { 
            get; 
            set; 
        } = new Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>>();
        internal override FilterBase FilterBase => Filter;
        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter(long limitTimeTicks = 0)
        {
            if (limitTimeTicks == 0)
            {
                Filter.СalculateJob();
            }
            else
            {
                Filter.СalculateJob(limitTimeTicks);
            }
        }
        internal override void Aсtion()
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, DeltaTime);
            }
        }
        internal override void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2>;
            ActionAdd(entityId, groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2);
        }
        internal override void AсtionRemove(int entityId) { ActionRemove(entityId); }

        public virtual void ActionAdd(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2) { }
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, float deltaTime) { }
        public virtual void ActionRemove(int entityId) { }
    }
    public abstract class SystemExistComponents<ExistComponentT1, ExistComponentT2, ExistComponentT3> : SystemBase
        where ExistComponentT1 : IComponent
        where ExistComponentT2 : IComponent
        where ExistComponentT3 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>();
        internal override FilterBase FilterBase => Filter;
        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter(long limitTimeTicks = 0)
        {
            if (limitTimeTicks == 0)
            {
                Filter.СalculateJob();
            }
            else
            {
                Filter.СalculateJob(limitTimeTicks);
            }
        }
        internal override void Aсtion()
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, DeltaTime);
            }
        }
        internal override void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>;
            ActionAdd(entityId, groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3);
        }
        internal override void AсtionRemove(int entityId) { ActionRemove(entityId); }

        public virtual void ActionAdd(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3) { }
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, float deltaTime) { }
        public virtual void ActionRemove(int entityId) { }
    }
    public abstract class SystemExistComponents<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4> : SystemBase
        where ExistComponentT1 : IComponent
        where ExistComponentT2 : IComponent
        where ExistComponentT3 : IComponent
        where ExistComponentT4 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>();
        internal override FilterBase FilterBase => Filter;
        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter(long limitTimeTicks = 0)
        {
            if (limitTimeTicks == 0)
            {
                Filter.СalculateJob();
            }
            else
            {
                Filter.СalculateJob(limitTimeTicks);
            }
        }
        internal override void Aсtion()
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, DeltaTime);
            }
        }
        internal override void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>;
            ActionAdd(entityId, groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3, groupComponentsExist.ExistComponent4);
        }
        internal override void AсtionRemove(int entityId) { ActionRemove(entityId); }

        public virtual void ActionAdd(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4) { }
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, float deltaTime) { }
        public virtual void ActionRemove(int entityId) { }
    }
    public abstract class SystemExistComponents<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5> : SystemBase
        where ExistComponentT1 : IComponent
        where ExistComponentT2 : IComponent
        where ExistComponentT3 : IComponent
        where ExistComponentT4 : IComponent
        where ExistComponentT5 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>();
        internal override FilterBase FilterBase => Filter;
        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>)));
        }
        /// <summary>
        /// Подготовка к выполнению, вызывается перед каждым выполнением
        /// </summary>
        internal override void CalculateFilter(long limitTimeTicks = 0)
        {
            if (limitTimeTicks == 0)
            {
                Filter.СalculateJob();
            }
            else
            {
                Filter.СalculateJob(limitTimeTicks);
            }
        }
        internal override void Aсtion()
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, item.Value.ExistComponent5, DeltaTime);
            }
        }
        internal override void AсtionAdd<TGroupComponents>(int entityId, TGroupComponents groupComponents)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>;
            ActionAdd(entityId, groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3, groupComponentsExist.ExistComponent4, groupComponentsExist.ExistComponent5);
        }
        internal override void AсtionRemove(int entityId) { ActionRemove(entityId); }

        public virtual void ActionAdd(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, ExistComponentT5 existComponentT5) { }
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, ExistComponentT5 existComponentT5, float deltaTime) { }
        public virtual void ActionRemove(int entityId) { }
    }


    ///// <summary>
    ///// Базовый класс систем
    ///// Каждую систему необходимо пометить атрибутами:
    ///// 1) [AttributeSystemPriority] ;
    ///// 2) [AttributeSystemCalculate] ;
    ///// 3) [AttributeSystemEnable] ;
    ///// </summary>
    //public abstract class System<T0, T1> : SystemBase
    //    where T0 : IComponent
    //    where T1 : IComponent
    //{
    //    /// <summary>
    //    /// Фильтр
    //    /// </summary>
    //    internal Filter<T0, T1> Filter { get; set; } = new Filter<T0, T1>();
    //    /// <summary>
    //    /// Инициализация системы, необходима для получения ссылки на фильтр
    //    /// </summary>
    //    internal override void GetFilter()
    //    {
    //        Filter = (Filter<T0, T1>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1>)));
    //    }
    //    /// <summary>
    //    /// Подготовка к выполнению, вызывается перед каждым выполнением
    //    /// </summary>
    //    internal override void CalculateFilter(long limitTimeTicks = 0)
    //    {
    //        if (limitTimeTicks == 0)
    //        {
    //            Filter.Сalculate();
    //        }
    //        else
    //        {
    //            Filter.Сalculate(limitTimeTicks);
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение системы.
    //    /// (Вызывается с интервалом, заданным через атрибут)
    //    /// </summary>
    //    internal override void AсtionForeach()
    //    {
    //        foreach (T0 t0 in Filter.ComponentsT0.Values)
    //        {
    //            Action(t0, Filter.ComponentsT1[t0.Id]);
    //        }
    //    }
    //    /// <summary>
    //    /// Работа системы
    //    /// </summary>
    //    public abstract void Action(T0 t0, T1 t1);
    //}
    ///// <summary>
    ///// Базовый класс систем
    ///// Каждую систему необходимо пометить атрибутами:
    ///// 1) [AttributeSystemPriority] ;
    ///// 2) [AttributeSystemCalculate] ;
    ///// 3) [AttributeSystemEnable] ;
    ///// </summary>
    //public abstract class System<T0, T1, T2> : SystemBase
    //    where T0 : IComponent
    //    where T1 : IComponent
    //    where T2 : IComponent
    //{
    //    /// <summary>
    //    /// Фильтр
    //    /// </summary>
    //    internal Filter<T0, T1, T2> Filter { get; set; } = new Filter<T0, T1, T2>();
    //    /// <summary>
    //    /// Инициализация системы, необходима для получения ссылки на фильтр
    //    /// </summary>
    //    internal override void GetFilter()
    //    {
    //        Filter = (Filter<T0, T1, T2>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2>)));
    //    }
    //    /// <summary>
    //    /// Подготовка к выполнению, вызывается перед каждым выполнением
    //    /// </summary>
    //    internal override void CalculateFilter(long limitTimeTicks = 0)
    //    {
    //        if (limitTimeTicks == 0)
    //        {
    //            Filter.Сalculate();
    //        }
    //        else
    //        {
    //            Filter.Сalculate(limitTimeTicks);
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение системы.
    //    /// (Вызывается с интервалом, заданным через атрибут)
    //    /// </summary>
    //    internal override void AсtionForeach()
    //    {
    //        foreach (T0 t0 in Filter.ComponentsT0.Values)
    //        {
    //            Action(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id]);
    //        }
    //    }
    //    /// <summary>
    //    /// Работа системы
    //    /// </summary>
    //    public abstract void Action(T0 t0, T1 t1, T2 t2);
    //}
    ///// <summary>
    ///// Базовый класс систем
    ///// Каждую систему необходимо пометить атрибутами:
    ///// 1) [AttributeSystemPriority] ;
    ///// 2) [AttributeSystemCalculate] ;
    ///// 3) [AttributeSystemEnable] ;
    ///// </summary>
    //public abstract class System<T0, T1, T2, T3> : SystemBase
    //    where T0 : IComponent
    //    where T1 : IComponent
    //    where T2 : IComponent
    //    where T3 : IComponent
    //{
    //    /// <summary>
    //    /// Фильтр
    //    /// </summary>
    //    internal Filter<T0, T1, T2, T3> Filter { get; set; } = new Filter<T0, T1, T2, T3>();
    //    /// <summary>
    //    /// Инициализация системы, необходима для получения ссылки на фильтр
    //    /// </summary>
    //    internal override void GetFilter()
    //    {
    //        Filter = (Filter<T0, T1, T2, T3>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2, T3>)));
    //    }
    //    /// <summary>
    //    /// Подготовка к выполнению, вызывается перед каждым выполнением
    //    /// </summary>
    //    internal override void CalculateFilter(long limitTimeTicks = 0)
    //    {
    //        if (limitTimeTicks == 0)
    //        {
    //            Filter.Сalculate();
    //        }
    //        else
    //        {
    //            Filter.Сalculate(limitTimeTicks);
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение системы.
    //    /// (Вызывается с интервалом, заданным через атрибут)
    //    /// </summary>
    //    internal override void AсtionForeach()
    //    {
    //        foreach (T0 t0 in Filter.ComponentsT0.Values)
    //        {
    //            Action(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id], Filter.ComponentsT3[t0.Id]);
    //        }
    //    }
    //    /// <summary>
    //    /// Работа системы
    //    /// </summary>
    //    public abstract void Action(T0 t0, T1 t1, T2 t2, T3 t3);
    //}
    ///// <summary>
    ///// Базовый класс систем
    ///// Каждую систему необходимо пометить атрибутами:
    ///// 1) [AttributeSystemPriority] ;
    ///// 2) [AttributeSystemCalculate] ;
    ///// 3) [AttributeSystemEnable] ;
    ///// </summary>
    //public abstract class System<T0, T1, T2, T3, T4> : SystemBase
    //    where T0 : IComponent
    //    where T1 : IComponent
    //    where T2 : IComponent
    //    where T3 : IComponent
    //    where T4 : IComponent
    //{
    //    /// <summary>
    //    /// Фильтр
    //    /// </summary>
    //    internal Filter<T0, T1, T2, T3, T4> Filter { get; set; } = new Filter<T0, T1, T2, T3, T4>();
    //    /// <summary>
    //    /// Инициализация системы, необходима для получения ссылки на фильтр
    //    /// </summary>
    //    internal override void GetFilter()
    //    {
    //        Filter = (Filter<T0, T1, T2, T3, T4>)(ECS.ManagerFilters.GetFilter(typeof(Filter<T0, T1, T2, T3, T4>)));
    //    }
    //    /// <summary>
    //    /// Подготовка к выполнению, вызывается перед каждым выполнением
    //    /// </summary>
    //    internal override void CalculateFilter(long limitTimeTicks = 0)
    //    {
    //        if (limitTimeTicks == 0)
    //        {
    //            Filter.Сalculate();
    //        }
    //        else
    //        {
    //            Filter.Сalculate(limitTimeTicks);
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение системы.
    //    /// (Вызывается с интервалом, заданным через атрибут)
    //    /// </summary>
    //    internal override void AсtionForeach()
    //    {
    //        foreach (T0 t0 in Filter.ComponentsT0.Values)
    //        {
    //            Action(t0, Filter.ComponentsT1[t0.Id], Filter.ComponentsT2[t0.Id], Filter.ComponentsT3[t0.Id], Filter.ComponentsT4[t0.Id]);
    //        }
    //    }
    //    /// <summary>
    //    /// Работа системы
    //    /// </summary>
    //    public abstract void Action(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4);
    //}
}