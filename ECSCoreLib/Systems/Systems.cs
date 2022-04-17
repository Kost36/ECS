﻿using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Filters;
using ECSCore.GroupComponents;
using ECSCore.Interfaces.Components;
using ECSCore.Managers;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace ECSCore.Systems
{
    /// <summary>
    /// Абстрактный класс системы, которая обрабатывает группу компонент из 1 включающего компонента;
    /// 
    /// Возможные атрибуты: (* - Обязательный атрибут)
    /// * AttributeSystemCalculate - Задается интервал обработки системы
    /// * AttributeSystemPriority - Приоритет выполнения систем
    /// AttributeSystemEnable - Возможность отключить систему
    /// AttributeExcludeComponentSystem - Возможность задать исключающие компоненты
    /// AttributeSystemEarlyExecution - Возможность разрешить предварительное выполнение системы, для выравнивания нагрузки на CPU (Система будет выполняться предварительно, если есть свободное время - простаивание CPU)
    /// AttributeSystemParallelCountThreads - Возможность распараллелить систему на n потоков
    /// 
    /// Возможные интерфейсы: 
    /// ISystemActionAdd - Активировать вызов соответствующего метода, при добавлении группы компонент одной сущьности в фильтр системы
    /// ISystemAction - Активировать периодический вызов соответствующего метода обработки группы компонент, которые находятся в фильтре 
    /// ISystemActionRemove - Активировать вызов соответствующего метода, при удалении группы компонент одной сущьности из фильтра системы
    /// ISystemParallel - Активировать распараллеливание вызовов Action системы на n потоков
    /// ISystemManualControlAction - Активировать периодический вызов метода ручной обработки коллекции фильтра, для оптимизации
    /// ISystemUseInjectThread - Активировать вызов методов обработки в введенном STA потоке зависимого приложения
    /// </summary>
    /// <typeparam name="ExistComponentT1"> Компонент №1 (Имеющийся у сущьности, для обработки системой) </typeparam>
    public abstract class SystemExistComponents<ExistComponentT1> : SystemBase
        where ExistComponentT1 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1>>();

        /// <summary>
        /// 
        /// </summary>
        internal override FilterBase FilterBase => Filter;

        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1>>)(managerFilters.GetFilter(typeof(Filter<GroupComponentsExist<ExistComponentT1>>), Filter.TypesWithoutComponents));
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

        /// <summary>
        /// Выполнение системы
        /// </summary>
        /// <param name="systemActionType"></param>
        /// <param name="maxCountOnThread"></param>
        internal override void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue)
        {
            switch (systemActionType)
            {
                case SystemActionType.RunInThisThread:
                    Run(null); //Выполнить в текущем потоке
                    break;
                case SystemActionType.RunInOneThread:
                    ThreadPool.QueueUserWorkItem(Run); //Выполнить в отдельном потоке
                    break;
                case SystemActionType.RunInThreads:
                    RunInCountThread(maxCountOnThread); //Выполнить в нескольких потоках
                    break;
                case SystemActionType.RunInInjectThread:
                    Run(null); //TODO Выполнить в введенном потоке
                    break;
            } //Взависимости от типа выполнения
        }

        /// <summary>
        /// Выполнение итерирования по коллекции
        /// </summary>
        /// <param name="state"></param>
        private void Run(object state)
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по части коллекции
        /// </summary>
        /// <param name="sliceCollection"> Часть коллекции </param>
        private void RunPart(List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1>>> sliceCollection)
        {
            foreach (var item in sliceCollection)
            {
                Action(item.Key, item.Value.ExistComponent1, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по коллекции в нескольких потоках
        /// </summary>
        private void RunInCountThread(int maxCountOnThread)
        {
            int i = 0; //Количество элементов для пропуска
            while (true)
            {
                List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1>>> items = Filter.Collection.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                ThreadPool.QueueUserWorkItem(o => { RunPart(items); }); //Вызываем в отдельном потоке
                if (i > Filter.Collection.Count)
                {
                    return;
                } //Если вся коллекция обработана
                i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
            } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroupComponents"></typeparam>
        /// <param name="groupComponents"></param>
        /// <param name="entity"></param>
        internal override void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
        {
            GroupComponentsExist<ExistComponentT1> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1>;
            ActionAdd(groupComponentsExist.ExistComponent1, entity);
        }

        /// <summary>
        /// Метод обработки добавленной группы компонент в фильтр
        /// </summary>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        public virtual void ActionAdd(ExistComponentT1 existComponentT1, Entity entity) { }

        /// <summary>
        /// Метод периодической оброаботки имеющихся в фильтре групп компонент
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="deltaTime"> Изменение времени с предидущего вызова данной системы (Размерность: секунды) </param>
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, float deltaTime) { }
    }

    /// <summary>
    /// Абстрактный класс системы, которая обрабатывает группу компонент из 2 включающих компонентов;
    /// 
    /// Возможные атрибуты: (* - Обязательный атрибут)
    /// * AttributeSystemCalculate - Задается интервал обработки системы
    /// * AttributeSystemPriority - Приоритет выполнения систем
    /// AttributeSystemEnable - Возможность отключить систему
    /// AttributeExcludeComponentSystem - Возможность задать исключающие компоненты
    /// AttributeSystemEarlyExecution - Возможность разрешить предварительное выполнение системы, для выравнивания нагрузки на CPU (Система будет выполняться предварительно, если есть свободное время - простаивание CPU)
    /// AttributeSystemParallelCountThreads - Возможность распараллелить систему на n потоков
    /// 
    /// Возможные интерфейсы: 
    /// ISystemActionAdd - Активировать вызов соответствующего метода, при добавлении группы компонент одной сущьности в фильтр системы
    /// ISystemAction - Активировать периодический вызов соответствующего метода обработки группы компонент, которые находятся в фильтре 
    /// ISystemActionRemove - Активировать вызов соответствующего метода, при удалении группы компонент одной сущьности из фильтра системы
    /// ISystemParallel - Активировать распараллеливание вызовов Action системы на n потоков
    /// ISystemManualControlAction - Активировать периодический вызов метода ручной обработки коллекции фильтра, для оптимизации
    /// ISystemUseInjectThread - Активировать вызов методов обработки в введенном STA потоке зависимого приложения
    /// </summary>
    /// <typeparam name="ExistComponentT1"> Компонент №1 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT2"> Компонент №2 (Имеющийся у сущьности, для обработки системой) </typeparam>
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

        /// <summary>
        /// 
        /// </summary>
        internal override FilterBase FilterBase => Filter;

        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2>>), Filter.TypesWithoutComponents));
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

        /// <summary>
        /// Выполнение системы
        /// </summary>
        /// <param name="systemActionType"></param>
        /// <param name="maxCountOnThread"></param>
        internal override void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue)
        {
            switch (systemActionType)
            {
                case SystemActionType.RunInThisThread:
                    Run(null); //Выполнить в текущем потоке
                    break;
                case SystemActionType.RunInOneThread:
                    ThreadPool.QueueUserWorkItem(Run); //Выполнить в отдельном потоке
                    break;
                case SystemActionType.RunInThreads:
                    RunInCountThread(maxCountOnThread); //Выполнить в нескольких потоках
                    break;
                case SystemActionType.RunInInjectThread:
                    Run(null); //TODO Выполнить в введенном потоке
                    break;
            } //Взависимости от типа выполнения
        }

        /// <summary>
        /// Выполнение итерирования по коллекции
        /// </summary>
        /// <param name="state"></param>
        private void Run(object state)
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по части коллекции
        /// </summary>
        /// <param name="sliceCollection"> Часть коллекции </param>
        private void RunPart(List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2>>> sliceCollection)
        {
            foreach (var item in sliceCollection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по коллекции в нескольких потоках
        /// </summary>
        private void RunInCountThread(int maxCountOnThread)
        {
            int i = 0; //Количество элементов для пропуска
            while (true)
            {
                //if (Filter.Collection.Count > 0)
                //{
                //    ThreadPool.GetAvailableThreads(out int a, out int b);
                //} //Тест для точки останова
                List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2>>> items = Filter.Collection.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                ThreadPool.QueueUserWorkItem(o => { RunPart(items); }); //Вызываем в отдельном потоке
                if (i > Filter.Collection.Count)
                {
                    return;
                } //Если вся коллекция обработана
                i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
            } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroupComponents"></typeparam>
        /// <param name="groupComponents"></param>
        /// <param name="entity"></param>
        internal override void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2>;
            ActionAdd(groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, entity);
        }

        /// <summary>
        /// Метод обработки добавленной группы компонент в фильтр
        /// </summary>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        public virtual void ActionAdd(ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, Entity entity) { }

        /// <summary>
        /// Метод периодической оброаботки имеющихся в фильтре групп компонент
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="deltaTime"> Изменение времени с предидущего вызова данной системы (Размерность: секунды) </param>
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, float deltaTime) { }
    }

    /// <summary>
    /// Абстрактный класс системы, которая обрабатывает группу компонент из 3 включающих компонентов;
    /// 
    /// Возможные атрибуты: (* - Обязательный атрибут)
    /// * AttributeSystemCalculate - Задается интервал обработки системы
    /// * AttributeSystemPriority - Приоритет выполнения систем
    /// AttributeSystemEnable - Возможность отключить систему
    /// AttributeExcludeComponentSystem - Возможность задать исключающие компоненты
    /// AttributeSystemEarlyExecution - Возможность разрешить предварительное выполнение системы, для выравнивания нагрузки на CPU (Система будет выполняться предварительно, если есть свободное время - простаивание CPU)
    /// AttributeSystemParallelCountThreads - Возможность распараллелить систему на n потоков
    /// 
    /// Возможные интерфейсы: 
    /// ISystemActionAdd - Активировать вызов соответствующего метода, при добавлении группы компонент одной сущьности в фильтр системы
    /// ISystemAction - Активировать периодический вызов соответствующего метода обработки группы компонент, которые находятся в фильтре 
    /// ISystemActionRemove - Активировать вызов соответствующего метода, при удалении группы компонент одной сущьности из фильтра системы
    /// ISystemParallel - Активировать распараллеливание вызовов Action системы на n потоков
    /// ISystemManualControlAction - Активировать периодический вызов метода ручной обработки коллекции фильтра, для оптимизации
    /// ISystemUseInjectThread - Активировать вызов методов обработки в введенном STA потоке зависимого приложения
    /// </summary>
    /// <typeparam name="ExistComponentT1"> Компонент №1 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT2"> Компонент №2 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT3"> Компонент №3 (Имеющийся у сущьности, для обработки системой) </typeparam>
    public abstract class SystemExistComponents<ExistComponentT1, ExistComponentT2, ExistComponentT3> : SystemBase
        where ExistComponentT1 : IComponent
        where ExistComponentT2 : IComponent
        where ExistComponentT3 : IComponent
    {
        /// <summary>
        /// Фильтр
        /// </summary>
        internal Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>> Filter { get; set; } = new Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>();

        /// <summary>
        /// 
        /// </summary>
        internal override FilterBase FilterBase => Filter;

        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>), Filter.TypesWithoutComponents));
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

        /// <summary>
        /// Выполнение системы
        /// </summary>
        /// <param name="systemActionType"></param>
        /// <param name="maxCountOnThread"></param>
        internal override void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue)
        {
            switch (systemActionType)
            {
                case SystemActionType.RunInThisThread:
                    Run(null); //Выполнить в текущем потоке
                    break;
                case SystemActionType.RunInOneThread:
                    ThreadPool.QueueUserWorkItem(Run); //Выполнить в отдельном потоке
                    break;
                case SystemActionType.RunInThreads:
                    RunInCountThread(maxCountOnThread); //Выполнить в нескольких потоках
                    break;
                case SystemActionType.RunInInjectThread:
                    Run(null); //TODO Выполнить в введенном потоке
                    break;
            } //Взависимости от типа выполнения
        }

        /// <summary>
        /// Выполнение итерирования по коллекции
        /// </summary>
        /// <param name="state"></param>
        private void Run(object state)
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по части коллекции
        /// </summary>
        /// <param name="sliceCollection"> Часть коллекции </param>
        private void RunPart(List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>> sliceCollection)
        {
            foreach (var item in sliceCollection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по коллекции в нескольких потоках
        /// </summary>
        private void RunInCountThread(int maxCountOnThread)
        {
            int i = 0; //Количество элементов для пропуска
            while (true)
            {
                List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>>> items = Filter.Collection.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                ThreadPool.QueueUserWorkItem(o => { RunPart(items); }); //Вызываем в отдельном потоке
                if (i > Filter.Collection.Count)
                {
                    return;
                } //Если вся коллекция обработана
                i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
            } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroupComponents"></typeparam>
        /// <param name="groupComponents"></param>
        /// <param name="entity"></param>
        internal override void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3>;
            ActionAdd(groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3, entity);
        }

        /// <summary>
        /// Метод обработки добавленной группы компонент в фильтр
        /// </summary>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        public virtual void ActionAdd(ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, Entity entity) { }

        /// <summary>
        /// Метод периодической оброаботки имеющихся в фильтре групп компонент
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="deltaTime"> Изменение времени с предидущего вызова данной системы (Размерность: секунды) </param>
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, float deltaTime) { }
    }

    /// <summary>
    /// Абстрактный класс системы, которая обрабатывает группу компонент из 4 включающих компонентов;
    /// 
    /// Возможные атрибуты: (* - Обязательный атрибут)
    /// * AttributeSystemCalculate - Задается интервал обработки системы
    /// * AttributeSystemPriority - Приоритет выполнения систем
    /// AttributeSystemEnable - Возможность отключить систему
    /// AttributeExcludeComponentSystem - Возможность задать исключающие компоненты
    /// AttributeSystemEarlyExecution - Возможность разрешить предварительное выполнение системы, для выравнивания нагрузки на CPU (Система будет выполняться предварительно, если есть свободное время - простаивание CPU)
    /// AttributeSystemParallelCountThreads - Возможность распараллелить систему на n потоков
    /// 
    /// Возможные интерфейсы: 
    /// ISystemActionAdd - Активировать вызов соответствующего метода, при добавлении группы компонент одной сущьности в фильтр системы
    /// ISystemAction - Активировать периодический вызов соответствующего метода обработки группы компонент, которые находятся в фильтре 
    /// ISystemActionRemove - Активировать вызов соответствующего метода, при удалении группы компонент одной сущьности из фильтра системы
    /// ISystemParallel - Активировать распараллеливание вызовов Action системы на n потоков
    /// ISystemManualControlAction - Активировать периодический вызов метода ручной обработки коллекции фильтра, для оптимизации
    /// ISystemUseInjectThread - Активировать вызов методов обработки в введенном STA потоке зависимого приложения
    /// </summary>
    /// <typeparam name="ExistComponentT1"> Компонент №1 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT2"> Компонент №2 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT3"> Компонент №3 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT4"> Компонент №4 (Имеющийся у сущьности, для обработки системой) </typeparam>
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

        /// <summary>
        /// 
        /// </summary>
        internal override FilterBase FilterBase => Filter;

        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>), Filter.TypesWithoutComponents));
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

        /// <summary>
        /// Выполнение системы
        /// </summary>
        /// <param name="systemActionType"></param>
        /// <param name="maxCountOnThread"></param>
        internal override void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue)
        {
            switch (systemActionType)
            {
                case SystemActionType.RunInThisThread:
                    Run(null); //Выполнить в текущем потоке
                    break;
                case SystemActionType.RunInOneThread:
                    ThreadPool.QueueUserWorkItem(Run); //Выполнить в отдельном потоке
                    break;
                case SystemActionType.RunInThreads:
                    RunInCountThread(maxCountOnThread); //Выполнить в нескольких потоках
                    break;
                case SystemActionType.RunInInjectThread:
                    Run(null); //TODO Выполнить в введенном потоке
                    break;
            } //Взависимости от типа выполнения
        }

        /// <summary>
        /// Выполнение итерирования по коллекции
        /// </summary>
        /// <param name="state"></param>
        private void Run(object state)
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по части коллекции
        /// </summary>
        /// <param name="sliceCollection"> Часть коллекции </param>
        private void RunPart(List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>> sliceCollection)
        {
            foreach (var item in sliceCollection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по коллекции в нескольких потоках
        /// </summary>
        private void RunInCountThread(int maxCountOnThread)
        {
            int i = 0; //Количество элементов для пропуска
            while (true)
            {
                List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>>> items = Filter.Collection.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                ThreadPool.QueueUserWorkItem(o => { RunPart(items); }); //Вызываем в отдельном потоке
                if (i > Filter.Collection.Count)
                {
                    return;
                } //Если вся коллекция обработана
                i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
            } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroupComponents"></typeparam>
        /// <param name="groupComponents"></param>
        /// <param name="entity"></param>
        internal override void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4>;
            ActionAdd(groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3, groupComponentsExist.ExistComponent4, entity);
        }

        /// <summary>
        /// Метод обработки добавленной группы компонент в фильтр
        /// </summary>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="existComponentT4"> Компонент №4 </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        public virtual void ActionAdd(ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, Entity entity) { }

        /// <summary>
        /// Метод периодической оброаботки имеющихся в фильтре групп компонент
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="existComponentT4"> Компонент №4 </param>
        /// <param name="deltaTime"> Изменение времени с предидущего вызова данной системы (Размерность: секунды) </param>
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, float deltaTime) { }
    }

    /// <summary>
    /// Абстрактный класс системы, которая обрабатывает группу компонент из 5 включающих компонентов;
    /// 
    /// Возможные атрибуты: (* - Обязательный атрибут)
    /// * AttributeSystemCalculate - Задается интервал обработки системы
    /// * AttributeSystemPriority - Приоритет выполнения систем
    /// AttributeSystemEnable - Возможность отключить систему
    /// AttributeExcludeComponentSystem - Возможность задать исключающие компоненты
    /// AttributeSystemEarlyExecution - Возможность разрешить предварительное выполнение системы, для выравнивания нагрузки на CPU (Система будет выполняться предварительно, если есть свободное время - простаивание CPU)
    /// AttributeSystemParallelCountThreads - Возможность распараллелить систему на n потоков
    /// 
    /// Возможные интерфейсы: 
    /// ISystemActionAdd - Активировать вызов соответствующего метода, при добавлении группы компонент одной сущьности в фильтр системы
    /// ISystemAction - Активировать периодический вызов соответствующего метода обработки группы компонент, которые находятся в фильтре 
    /// ISystemActionRemove - Активировать вызов соответствующего метода, при удалении группы компонент одной сущьности из фильтра системы
    /// ISystemParallel - Активировать распараллеливание вызовов Action системы на n потоков
    /// ISystemManualControlAction - Активировать периодический вызов метода ручной обработки коллекции фильтра, для оптимизации
    /// ISystemUseInjectThread - Активировать вызов методов обработки в введенном STA потоке зависимого приложения
    /// </summary>
    /// <typeparam name="ExistComponentT1"> Компонент №1 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT2"> Компонент №2 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT3"> Компонент №3 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT4"> Компонент №4 (Имеющийся у сущьности, для обработки системой) </typeparam>
    /// <typeparam name="ExistComponentT5"> Компонент №4 (Имеющийся у сущьности, для обработки системой) </typeparam>
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

        /// <summary>
        /// 
        /// </summary>
        internal override FilterBase FilterBase => Filter;

        /// <summary>
        /// Получение ссылки на фильтр
        /// </summary>
        internal override void GetFilter(ManagerFilters managerFilters)
        {
            Filter = (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>)
                (managerFilters.GetFilter(typeof
                (Filter<GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>), Filter.TypesWithoutComponents));
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

        /// <summary>
        /// Выполнение системы
        /// </summary>
        /// <param name="systemActionType"></param>
        /// <param name="maxCountOnThread"></param>
        internal override void Aсtion(SystemActionType systemActionType = SystemActionType.RunInThisThread, int maxCountOnThread = int.MaxValue)
        {
            switch (systemActionType)
            {
                case SystemActionType.RunInThisThread:
                    Run(null); //Выполнить в текущем потоке
                    break;
                case SystemActionType.RunInOneThread:
                    ThreadPool.QueueUserWorkItem(Run); //Выполнить в отдельном потоке
                    break;
                case SystemActionType.RunInThreads:
                    RunInCountThread(maxCountOnThread); //Выполнить в нескольких потоках
                    break;
                case SystemActionType.RunInInjectThread:
                    Run(null); //TODO Выполнить в введенном потоке
                    break;
            } //Взависимости от типа выполнения
        }

        /// <summary>
        /// Выполнение итерирования по коллекции
        /// </summary>
        /// <param name="state"></param>
        private void Run(object state)
        {
            foreach (var item in Filter.Collection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, item.Value.ExistComponent5, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по части коллекции
        /// </summary>
        /// <param name="sliceCollection"> Часть коллекции </param>
        private void RunPart(List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>> sliceCollection)
        {
            foreach (var item in sliceCollection)
            {
                Action(item.Key, item.Value.ExistComponent1, item.Value.ExistComponent2, item.Value.ExistComponent3, item.Value.ExistComponent4, item.Value.ExistComponent5, DeltaTime);
            } //Проходимся по коллекции и вызываем Action для каждого элемента
        }

        /// <summary>
        /// Выполнение итерирования по коллекции в нескольких потоках
        /// </summary>
        private void RunInCountThread(int maxCountOnThread)
        {
            int i = 0; //Количество элементов для пропуска
            while (true)
            {
                List<KeyValuePair<int, GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>>> items = Filter.Collection.Skip(i).Take(maxCountOnThread).ToList(); //Получим часть коллекции
                ThreadPool.QueueUserWorkItem(o => { RunPart(items); }); //Вызываем в отдельном потоке
                if (i > Filter.Collection.Count)
                {
                    return;
                } //Если вся коллекция обработана
                i += maxCountOnThread; //Вычисляем кол-во элементов для пропуска
            } //Делим коллекцию и обрабатываем части коллекции в отдельных потоках
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroupComponents"></typeparam>
        /// <param name="groupComponents"></param>
        /// <param name="entity"></param>
        internal override void AсtionAdd<TGroupComponents>(TGroupComponents groupComponents, Entity entity)
        {
            GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5> groupComponentsExist = groupComponents as GroupComponentsExist<ExistComponentT1, ExistComponentT2, ExistComponentT3, ExistComponentT4, ExistComponentT5>;
            ActionAdd(groupComponentsExist.ExistComponent1, groupComponentsExist.ExistComponent2, groupComponentsExist.ExistComponent3, groupComponentsExist.ExistComponent4, groupComponentsExist.ExistComponent5, entity);
        }

        /// <summary>
        /// Метод обработки добавленной группы компонент в фильтр
        /// </summary>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="existComponentT4"> Компонент №4 </param>
        /// <param name="existComponentT5"> Компонент №5 </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        public virtual void ActionAdd(ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, ExistComponentT5 existComponentT5, Entity entity) { }

        /// <summary>
        /// Метод периодической оброаботки имеющихся в фильтре групп компонент
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="existComponentT1"> Компонент №1 </param>
        /// <param name="existComponentT2"> Компонент №2 </param>
        /// <param name="existComponentT3"> Компонент №3 </param>
        /// <param name="existComponentT4"> Компонент №4 </param>
        /// <param name="existComponentT5"> Компонент №5 </param>
        /// <param name="deltaTime"> Изменение времени с предидущего вызова данной системы (Размерность: секунды) </param>
        public virtual void Action(int entityId, ExistComponentT1 existComponentT1, ExistComponentT2 existComponentT2, ExistComponentT3 existComponentT3, ExistComponentT4 existComponentT4, ExistComponentT5 existComponentT5, float deltaTime) { }
    }
}