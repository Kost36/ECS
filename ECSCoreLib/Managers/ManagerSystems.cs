using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Exceptions;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер систем
    /// </summary>
    public class ManagerSystems
    {
        /// <summary>
        /// Ссылка на ECS
        /// </summary>
        private readonly ECS _ecs;
        /// <summary>
        /// Поток работы систем
        /// </summary>
        private readonly Thread _thread;
        /// <summary>
        /// Команда на останов менеджера (останов всех потоков)
        /// </summary>
        private bool _stopCMD;
        /// <summary>
        /// Список систем
        /// </summary>
        private readonly List<ISystem> _systems = new List<ISystem>();
        /// <summary>
        /// Очередь выполнения систем
        /// </summary>
        private readonly List<JobSystem> _systemQueue = new List<JobSystem>();
        /// <summary>
        /// Метка времени запуска приложения
        /// </summary>
        private long _ticksStart;
        /// <summary>
        /// Метка фактического времени
        /// </summary>
        private long _ticksPoint;
        /// <summary>
        /// Скорость выполнения систем
        /// </summary>
        private float _speedRun = 1f;
        /// <summary>
        /// Количесвто активных систем
        /// </summary>
        private int _countEnableSystems;
        /// <summary>
        /// Количесвто неактивных систем
        /// </summary>
        private int _сountDisableSystems;
        /// <summary>
        /// Флаг наличия задержки выполнения систем.
        /// (Системы не успевают выполняться - необходима оптимизация)
        /// </summary>
        private bool _flagHaveDelayRunSystem;
        /// <summary>
        /// Время задержки выполнения систем в мс
        /// </summary>
        private float _timeDelayExecuted;
        /// <summary>
        /// Свободное время в мс
        /// </summary>
        private float _freeTime;
        /// <summary>
        /// Флаг наличия задержки вычисления фильтров.
        /// (Вильтры не успевают вычисляться - необходима оптимизация)
        /// </summary>
        private bool _flagHaveDelayCalculateFilters;
        /// <summary>
        /// Время работы менеджера
        /// </summary>
        private long _timeWorkManagerSystemTicks;

        internal ManagerSystems(ECS ecs, Assembly assembly, ManagerFilters managerFilters)
        {
            _ecs = ecs;
            Init(assembly, managerFilters);
            _thread = new Thread(Start);
            _thread.Start();
        }

        /// <summary>
        /// Количество существующих систем
        /// </summary>
        public int CountSystems { get { return _systems.Count; } }

        /// <summary>
        /// Количество активных систем
        /// </summary>
        public int CountEnableSystems { get { return _countEnableSystems; } }

        /// <summary>
        /// Количество отключенных систем
        /// </summary>
        public int CountDisableSystems { get { return _сountDisableSystems; } }

        /// <summary>
        /// Системы не успевают выполняться c заданными интервалами
        /// Необходимо оптимизировать нагрузку
        /// </summary>
        public bool IsNotHaveTimeToBeExecuted { get { return _flagHaveDelayRunSystem; } }

        /// <summary>
        /// Свободное время в мс
        /// </summary>
        public float FreeTime { get { return _freeTime; } }

        /// <summary>
        /// Время задержки выполнения систем в мс
        /// </summary>
        public float TimeDelayExecuted { get { return _timeDelayExecuted; } }

        /// <summary>
        /// Фильтры не успевают вычисляться вовремя
        /// Необходимо оптимизировать нагрузку
        /// </summary>
        public bool IsNotHaveTimeToBeCalculateFilters { get { return _flagHaveDelayCalculateFilters; } }

        /// <summary>
        /// Получить полную информацию о состоянии систем
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetInfo(bool small = false)
        {
            StringBuilder info = new StringBuilder();

            var systems = new List<JobSystem>();
            lock (_systemQueue)
            {
                systems = _systemQueue.OrderByDescending(system => system.SystemStatistic.PercentTimeUsePerformance).ToList();
            }

            foreach (JobSystem jobSystem in systems)
            {
                if (small)
                {
                    info = info.Append(
                        $" PercentTimeUsePerformance: {jobSystem.SystemStatistic.PercentTimeUsePerformance:P};" +
                        $" MaxTimeUseInMs: {jobSystem.SystemStatistic.MaxTimeUseMs:F4}; " +
                        $" AverTimeUseInMs: {jobSystem.SystemStatistic.AverTimeUseMs:F4}; " +
                        $" MaxTimeFilterCalculateMs: {jobSystem.SystemStatistic.MaxTimeFilterCalculateMs:F4}; " +
                        $" AverTimeFilterCalculateMs: {jobSystem.SystemStatistic.AverTimeFilterCalculateMs:F4}; " +
                        $" Name: {jobSystem.System.GetType().FullName}; " +
                        $" IsEnable: {jobSystem.System.IsEnable}; " +
                        $" IntervalRunInMs: {jobSystem.System.IntervalTicks / TimeSpan.TicksPerMillisecond} \r\n");
                }
                else
                {
                    info = info.Append($"<<<<<<<<<<InfoOfSystem>>>>>>>>>> \r\n");
                    info = info.Append($"Name: {jobSystem.System.GetType().FullName} \r\n");
                    info = info.Append($"IsEnable: {jobSystem.System.IsEnable} \r\n");
                    info = info.Append($"Priority: {jobSystem.System.Priority} (0-NotUse; 1-MaxPriority; 50-MinPriority) \r\n");
                    info = info.Append($"IntervalRunInTicks: {jobSystem.System.IntervalTicks} \r\n");
                    info = info.Append($"IntervalRunInMs: {jobSystem.System.IntervalTicks / TimeSpan.TicksPerMillisecond} \r\n");
                    info = info.Append($"UseCount: {jobSystem.SystemStatistic.SummUseCount} \r\n");
                    info = info.Append($"MaxTimeUseInMs: {jobSystem.SystemStatistic.MaxTimeUseMs} \r\n");
                    info = info.Append($"AverTimeUseMs: {jobSystem.SystemStatistic.AverTimeUseMs} \r\n");
                    info = info.Append($"MaxTimeFilterCalculateMs: {jobSystem.SystemStatistic.MaxTimeFilterCalculateMs} \r\n");
                    info = info.Append($"AverTimeFilterCalculateMs: {jobSystem.SystemStatistic.AverTimeFilterCalculateMs} \r\n");
                    info = info.Append($"PercentTimeUsePerformance: {jobSystem.SystemStatistic.PercentTimeUsePerformance} \r\n");
                }
            }
            return info;
        }

        /// <summary>
        /// Очистить статистику выполнения систем
        /// </summary>
        public void ClearStatisticSystems()
        {
            _ticksStart = System.DateTime.Now.Ticks;
            _ticksPoint = _ticksStart;
            lock (_systemQueue)
            {
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    jobSystem.SystemStatistic.ClearStatistic();
                }
            }
        }

        /// <summary>
        /// Получить систему по типу, для отладки
        /// </summary>
        /// <typeparam name="T"> Generic тип системы </typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool GetSystem<T>(out T t)
            where T : SystemBase
        {
            foreach(ISystem system in _systems)
            {
                if (system is T t1)
                {
                    t = t1;
                    return true;
                }
            }
            t = default;
            return false;
        } //TODO сокрытие

        internal void SetSpeed(ECSSpeed eCSSpeed)
        {
            switch (eCSSpeed)
            {
                case ECSSpeed.Pause:
                    _speedRun = 0;
                    break;
                case ECSSpeed.Run:
                    _speedRun = 1;
                    break;
                case ECSSpeed.X_0_5:
                    _speedRun = 0.5f;
                    break;
                case ECSSpeed.X_2_0:
                    _speedRun = 2;
                    break;
                case ECSSpeed.X_4_0:
                    _speedRun = 4;
                    break;
                case ECSSpeed.X_8_0:
                    _speedRun = 8;
                    break;
                case ECSSpeed.X_16_0:
                    _speedRun = 16;
                    break;
            }
        }

        internal float SetSpeed(float speedRun)
        {
            if (speedRun < 0.1)
            {
                speedRun = 0.1f;
            }
            else if (speedRun > 32)
            {
                speedRun = 32f;
            }

            return _speedRun = speedRun;
        }

        internal void Despose()
        {
            //TODO Завершить потоки менеджера систем
            _stopCMD = true;
            while (true)
            {
                if (_thread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    _stopCMD = false;
                }
                if (_stopCMD == false)
                {
                    Thread.Sleep(1000); //TODO Завершить потоки менеджера систем
                    return;
                }
            }
        }

        private void Init(Assembly assembly, ManagerFilters managerFilters)
        {
            Type typeISystem = typeof(ISystem);
            Type[] types = assembly.GetTypes();
            List<Type> typesSystems = types
                .Where(t => typeISystem.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();
            
            foreach (Type typeSystem in typesSystems)
            {
                SystemBase system = (SystemBase)Activator.CreateInstance(typeSystem);
                system.GetAttributes();
                system.Init(managerFilters, _ecs);
                AddSystem(system);
            }
        }

        private void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        private void Start()
        {
            FillingQueue();
            if (_systemQueue.Count == 0)
            {
                throw new ExceptionECSHaveNotSystem("Нет реализованных систем");
            }

            while (true)
            {
                if (_stopCMD)
                {
                    return;
                }

                if (_speedRun == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }

                Run();
            }
        }

        private void FillingQueue()
        {
            _ticksStart = DateTime.Now.Ticks;
            _ticksPoint = _ticksStart;

            foreach (SystemBase system in _systems)
            {
                _systemQueue.Insert(0, new JobSystem(system, _ticksStart));

                if (system.IsEnable)
                {
                    _countEnableSystems++;
                }

                SortSystemQueue();
            }
        }

        private void Run()
        {
            SetTicksPoint();

            lock (_systemQueue)
            {
                if (_systemQueue[0].TicksNextRun <= _ticksPoint)
                {
                    StartCalculateJobSystem(_systemQueue[0]);

                    if (_systemQueue.Count>1)
                    {
                        if (_systemQueue[1].TicksNextRun <= _ticksPoint)
                        {
                            _flagHaveDelayRunSystem = true;
                            //_freeTime = 0; //Сброс свободного времени
                            _timeDelayExecuted = (float)(_ticksPoint - _systemQueue[1].TicksNextRun) / (float)TimeSpan.TicksPerMillisecond;
                            return;
                        }
                    }

                    return;
                }

                foreach (JobSystem jobSystem in _systemQueue)
                {
                    if (jobSystem.TicksEarlyExecution <= _ticksPoint)
                    {
                        StartCalculateJobSystem(jobSystem);
                        return; //TODO Проверка необходимости запуска м запуск след системы
                    }
                }

                //Контроль успеваемости ECS за обработкой систем
                long freeTimeTicks = ControlFreeTimeTicks(ControlTypeDelay.DelayRunSystem);
                if (freeTimeTicks == 0) 
                { 
                    return;
                }

                //Вычисление фильтров систем в свободное время
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    jobSystem.FilterCalculateTime(freeTimeTicks);
                    freeTimeTicks = ControlFreeTimeTicks();
                    if (freeTimeTicks == 0)
                    {
                        return;
                    }
                }

                //Контроль успеваемости ECS за обработкой фильтров
                freeTimeTicks = ControlFreeTimeTicks(ControlTypeDelay.DelayCalculateFiltersSystem);
                if (freeTimeTicks == 0)
                {
                    return;
                }

                Thread.Sleep(1);
            }
        }

        private void StartCalculateJobSystem(JobSystem jobSystem)
        {
            if (jobSystem.System.IsEnable == false)
            {
                jobSystem.SetTicks(DateTime.MaxValue.Ticks);
                _countEnableSystems--;
                _сountDisableSystems++;
                SortSystemQueue();
                return;
            }

            jobSystem.CalculateNextRun();
            SortSystemQueue();
            CalculateFreeTime();
            //TODO Фиксировать связанные компоненты!!!!

            jobSystem.Run(_ticksPoint, _timeWorkManagerSystemTicks, _speedRun);
        }

        private void CalculateFreeTime()
        {
            _freeTime = (float)(_systemQueue[0].TicksNextRun - _ticksPoint) / (float)TimeSpan.TicksPerMillisecond;
            //_timeDelayExecuted = 0; // Время задержки выполнения систем
        }

        private void SetTicksPoint()
        {
            _ticksPoint = DateTime.Now.Ticks;
            _timeWorkManagerSystemTicks = _ticksPoint - _ticksStart;
        }

        private long ControlFreeTimeTicks(ControlTypeDelay controlTypeDelay = ControlTypeDelay.Not)
        {
            SetTicksPoint();
            long freeTicks = _systemQueue[0].TicksNextRun - _ticksPoint;
            if (freeTicks < 0)
            {
                switch (controlTypeDelay)
                {
                    case ControlTypeDelay.DelayRunSystem:
                        //_flagHaveDelayRunSystem = true; //Не успеваем выполнять системы
                        break;
                    case ControlTypeDelay.DelayCalculateFiltersSystem:
                        _flagHaveDelayCalculateFilters = true;
                        break;
                }
                return 0;
            }
            else
            {
                switch (controlTypeDelay)
                {
                    case ControlTypeDelay.DelayRunSystem:
                        _flagHaveDelayRunSystem = false; 
                        _timeDelayExecuted = 0;
                        break;
                    case ControlTypeDelay.DelayCalculateFiltersSystem:
                        _flagHaveDelayCalculateFilters = false;
                        break;
                }
                return freeTicks;
            }
        }

        private void SortSystemQueue()
        {
            lock (_systemQueue)
            {
                for (int i = 0; i < _systemQueue.Count - 1; i++)
                {
                    if (_systemQueue[i].TicksNextRun > _systemQueue[i + 1].TicksNextRun)
                    {
                        JobSystem jobSystem = _systemQueue[i];
                        _systemQueue[i] = _systemQueue[i + 1];
                        _systemQueue[i + 1] = jobSystem;
                    }
                }
                
                long ticksMaxValue = DateTime.MaxValue.Ticks;
                for (int i = _systemQueue.Count - 1; i > 0; i--)
                {
                    if (_systemQueue[i].System.IsEnable)
                    {
                        if (_systemQueue[i].TicksNextRun == ticksMaxValue)
                        {
                            _systemQueue[i].SetTicks(_ticksPoint);
                            _systemQueue[i].CalculateNextRun();
                            _countEnableSystems++;
                            _сountDisableSystems--;
                            for (int j = i; j > 0; j--)
                            {
                                if (_systemQueue[j].TicksNextRun < _systemQueue[j - 1].TicksNextRun)
                                {
                                    JobSystem jobSystem = _systemQueue[j];
                                    _systemQueue[j] = _systemQueue[j - 1];
                                    _systemQueue[j - 1] = jobSystem;
                                }
                            }
                        }
                        return;
                    }
                }
            }
        }
    }
}

//TODO 2) Контроль пересечения компонент между системами, выполняемыми в разных потоках

//TODO 3) Если по времени можно выполнять след. систему.
//        И у след. системы нет пересечения по данным с одной из уже запущенных систем.
//        То осуществить запуск последующих систем в отдельных потоках из пула потоков. До n штук параллейно. (n задается через инициализатор ECS)

//TODO 4) Распараллеливаемые системы. 
//        Если система помечена, как распараллеленная, то такая система запускается параллельно в m потоках. (m задается через инициализатор ECS)
//        Данные фильтра такой системы делятся на m частей и каждая часть вызывается в отдельном потоке из пула потоков
//        Последовательность:
//                           1) Подготовка фильтра
//                           2) Разделение фильтра
//                           3) Распараллеленный вызов Action()
//                           4) Ожидание завершения всех потоков
//                           5) 
//         