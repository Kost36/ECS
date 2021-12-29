using ECSCore.Interfaces;
using ECSCore.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер систем
    /// </summary>
    public class ManagerSystems
    {
        #region Конструктор
        /// <summary>
        /// Анализ и компоновка данных
        /// </summary>
        internal ManagerSystems(ECS ecs, Assembly assembly, ManagerFilters managerFilters)
        {
            _ecs = ecs;
            _Init(assembly, managerFilters); //Инициализация 
            _thread = new Thread(Start); //Инициализация потока
            _thread.Start(); //Пуск 
        }
        #endregion

        #region Поля
        private ECS _ecs;
        /// <summary>
        /// Список систем
        /// </summary>
        private List<ISystem> _systems = new List<ISystem>();
        /// <summary>
        /// Поток работы систем
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// Очередь выполнения систем
        /// </summary>
        private List<JobSystem> _systemQueue = new List<JobSystem>();
        /// <summary>
        /// Время запуска
        /// </summary>
        private DateTime _dateTimeStart;
        /// <summary>
        /// Время фактического состояния
        /// </summary>
        private DateTime _dateTimePoint;

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
        /// Флаг наличия задержки вычисления фильтров.
        /// (Вильтры не успевают вычисляться - необходима оптимизация)
        /// </summary>
        private bool _flagHaveDelayCalculateFilters;
        /// <summary>
        /// Время работы менеджера
        /// </summary>
        private long _timeWorkManagerSystemTicks;
        /// <summary>
        /// Диагностический таймер выполнения системы
        /// </summary>
        private Stopwatch _stopwatchRunSystem = new Stopwatch();
        #endregion

        #region Свойства
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
        /// Фильтры не успевают вычисляться вовремя
        /// Необходимо оптимизировать нагрузку
        /// </summary>
        public bool IsNotHaveTimeToBeCalculateFilters { get { return _flagHaveDelayCalculateFilters; } }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Получить полную информацию о состоянии систем
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetInfo(bool small = false)
        {
            StringBuilder info = new StringBuilder();
            lock (_systemQueue)
            {
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    if (small)
                    {
                        info = info.Append(
                            $" PercentTimeUsePerformance: {jobSystem.SystemStatistic.PercentTimeUsePerformance.ToString("P")};" +
                            $" MaxTimeUseInMs: {jobSystem.SystemStatistic.MaxTimeUseMs.ToString("F4")}; " +
                            $" AverTimeUseInMs: {jobSystem.SystemStatistic.AverTimeUseMs.ToString("F4")}; " +
                            $" MaxTimeFilterCalculateMs: {jobSystem.SystemStatistic.MaxTimeFilterCalculateMs.ToString("F4")}; " +
                            $" AverTimeFilterCalculateMs: {jobSystem.SystemStatistic.AverTimeFilterCalculateMs.ToString("F4")}; " +
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
            }
            return info;
        }
        /// <summary>
        /// Очистить статистику выполнения систем
        /// </summary>
        public void ClearStatisticSystems()
        {
            _dateTimeStart = _dateTimePoint;
            lock (_systemQueue)
            {
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    jobSystem.SystemStatistic.ClearStatistic();
                }
            }
        }
        #endregion

        #region Приватные методы инициализации
        /// <summary>
        /// инициализация систем
        /// </summary>
        /// <param name="assembly"></param>
        internal void _Init(Assembly assembly, ManagerFilters managerFilters)
        {
            Type typeISystem = typeof(ISystem); //Получим тип интерфейса
            Type[] types = assembly.GetTypes(); //Получаем все типы сборки 
            List<Type> typesSystems = types.Where(t => typeISystem.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList(); //Получим все системы в сборке
            foreach (Type typeSystem in typesSystems)
            {
                SystemBase system = (SystemBase)Activator.CreateInstance(typeSystem); //Создадим объект
                system.GetAttributes(); //Подтяжка аттребут
                system.Injection(managerFilters, _ecs); //Инекция данных
                system.GetFilter(); //Подтяжка фильтра
                AddSystem(system); //Добавим в список
            } //Пройдемся по всем системам 
        }
        /// <summary>
        /// Добавить системы в список
        /// </summary>
        /// <param name=""></param>
        private void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }
        #endregion

        #region Приватные методы работы менеджера
        /// <summary>
        /// Запуск потока
        /// </summary>
        private void Start()
        {
            FillingQueue();
            while (true)
            {
                Run();
            } //Постоянно выполняем
        }
        /// <summary>
        /// Заполнить очередь выполнения систем
        /// </summary>
        private void FillingQueue()
        {
            _dateTimeStart = DateTime.Now;
            _dateTimePoint = _dateTimeStart;
            foreach (SystemBase system in _systems)
            {
                _systemQueue.Insert(0, new JobSystem(system, _dateTimePoint));
                _systemQueue[0].System.DateTimeOldRun = _dateTimePoint;
                _countEnableSystems++;
                SortSystemQueue();
            }
        }
        /// <summary>
        /// Работа систем
        /// </summary>
        private void Run()
        {
            lock (_systemQueue)
            {
                //Запуск систем
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    if (jobSystem.DateTimeNextRun.Ticks <= _dateTimePoint.Ticks)
                    {
                        StartCalculateJobSystem(jobSystem);
                        return;
                    } //Если настало время выполнения или настало время предварительного выполнения
                    break; //Переход к след циклу
                } //Проходимся по очереди системам на запуск (Контролим время запуска)
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    if (jobSystem.DateTimeEarlyExecution.Ticks <= _dateTimePoint.Ticks)
                    {
                        StartCalculateJobSystem(jobSystem);
                        return;
                    } //Если настало время выполнения или настало время предварительного выполнения
                    break; //Переход к след циклу
                } //Проходимся по очереди системам на запуск (Контролим время предварительного запуска)

                //Если дошли до данной точки, некоторые системы выполняются асинхронно или не выполняются

                //Контроль успеваемости ECS за обработкой систем
                long freeTimeTicks = ControlFreeTimeTicks(ControlTypeDelay.DelayRunSystem); //Вычисляем свободное время (С контролем успеваемости ECS обрабатывать все системы вовремя)
                if (freeTimeTicks == 0) 
                { 
                    return;
                } //Если нет свободного времени, выход

                //Вычисление фильтров систем в свободное время
                foreach (JobSystem jobSystem in _systemQueue)
                {
                    jobSystem.FilterCalculateTime(freeTimeTicks); //Посчитаем фильтр
                    freeTimeTicks = ControlFreeTimeTicks(); //Вычисляем свободное время
                    if (freeTimeTicks == 0)
                    {
                        return;
                    } //Если нет свободного времени, выход
                } //Проходимся по очереди системам на запуск (Вычисляем фильтры)

                //Контроль успеваемости ECS за обработкой фильтров
                freeTimeTicks = ControlFreeTimeTicks(ControlTypeDelay.DelayCalculateFiltersSystem); //Вычисляем свободное время (С контролем успеваемости ECS обрабатывать все системы и фильтра вовремя)
                if (freeTimeTicks == 0)
                {
                    return;
                } //Если нет свободного времени, выход
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// Запуск обработки системы (Вынесенный метод) 
        /// </summary>
        private void StartCalculateJobSystem(JobSystem jobSystem)
        {
            if (jobSystem.System.IsEnable == false)
            {
                jobSystem.DateTimeNextRun = DateTime.MaxValue;
                _countEnableSystems--;
                _сountDisableSystems++;
                SortSystemQueue(); //Сортировка очереди
                return;
            } //Если система выключена, то смещаем ее в конец
            jobSystem.CalculateNextRun(); //Вычислить время след. выполнения
            SortSystemQueue(); //Сортировка очереди
            //TODO Фиксировать связанные компоненты!!!!
            jobSystem.RunAsync(_dateTimePoint, _timeWorkManagerSystemTicks); //Обработка системы
        }
        /// <summary>
        /// Вычисляем свободное время
        /// </summary>
        /// <param name="flagControlHaveDelay"> Флаг контроля успевания менеджера систем обрабатывать все системы вовремя </param>
        /// <returns> свободное время </returns>
        private long ControlFreeTimeTicks(ControlTypeDelay controlTypeDelay = ControlTypeDelay.Not)
        {
            _dateTimePoint = DateTime.Now; //Новая метка времени
            _timeWorkManagerSystemTicks = _dateTimePoint.Ticks - _dateTimeStart.Ticks; //Время работы системы
            long freeTicks = _systemQueue[0].DateTimeNextRun.Ticks - _dateTimePoint.Ticks; //Считаем свободное время на вычисление фильтров
            if (freeTicks < 0)
            {
                switch (controlTypeDelay)
                {
                    case ControlTypeDelay.DelayRunSystem:
                        _flagHaveDelayRunSystem = true; //Не успеваем выполнять системы
                        break;
                    case ControlTypeDelay.DelayCalculateFiltersSystem:
                        _flagHaveDelayCalculateFilters = true; //Не успеваем вычислять фильтра
                        break;
                }
                return 0;
            } //Если свободного времени нету
            switch (controlTypeDelay)
            {
                case ControlTypeDelay.DelayRunSystem:
                    _flagHaveDelayRunSystem = false; //Успеваем выполнять системы
                    break;
                case ControlTypeDelay.DelayCalculateFiltersSystem:
                    _flagHaveDelayCalculateFilters = false; //Успеваем вычислять фильтра
                    break;
            }
            return freeTicks;
        }
        /// <summary>
        /// Сортировака очереди систем
        /// </summary>
        private void SortSystemQueue()
        {
            lock (_systemQueue)
            {
                for (int i = 0; i < _systemQueue.Count - 1; i++)
                {
                    if (_systemQueue[i].DateTimeNextRun > _systemQueue[i + 1].DateTimeNextRun)
                    {
                        JobSystem jobSystem = _systemQueue[i];
                        _systemQueue[i] = _systemQueue[i + 1];
                        _systemQueue[i + 1] = jobSystem;
                    }
                } //Сдвигает выполненную систему на свое место
                for (int i = _systemQueue.Count - 1; i > 0; i--)
                {
                    if (_systemQueue[i].System.IsEnable)
                    {
                        if (_systemQueue[i].DateTimeNextRun == DateTime.MaxValue)
                        {
                            _systemQueue[i].DateTimeNextRun = _dateTimePoint.AddTicks(_systemQueue[i].System.IntervalTicks); //Следующее время выполнения системы
                            _countEnableSystems++;
                            _сountDisableSystems--;
                            for (int j = i; j > 0; j--)
                            {
                                if (_systemQueue[j].DateTimeNextRun < _systemQueue[j - 1].DateTimeNextRun)
                                {
                                    JobSystem jobSystem = _systemQueue[j];
                                    _systemQueue[j] = _systemQueue[j - 1];
                                    _systemQueue[j - 1] = jobSystem;
                                }
                            } //Сдвинем только 1 систему, следующая система будет сдвинута на след. итерации
                        } //Если система только включилась
                        return;
                    } //Если система включена
                } //Проверяем включение систем, и сдвигаем на соответствующее место
            }
        }
        #endregion

        #region Перечисления
        /// <summary>
        /// Контроль задержки выполнения / вычисления
        /// </summary>
        private enum ControlTypeDelay
        {
            Not,
            DelayRunSystem,
            DelayCalculateFiltersSystem
        }
        #endregion
    }

    /// <summary>
    /// Контроллер системы
    /// </summary>
    internal class JobSystem
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        public JobSystem(SystemBase system, DateTime dateTime)
        {
            System = system;
            DateTimeNextRun = dateTime;
            CalculateNextRun();
        }
        #endregion

        #region Поля
        private Stopwatch _stopwatch = new Stopwatch();
        #endregion

        #region Свойства
        /// <summary>
        /// Система
        /// </summary>
        public SystemBase System { get; }
        /// <summary>
        /// Статистика системы
        /// </summary>
        public SystemStatistic SystemStatistic { get; } = new SystemStatistic();
        /// <summary>
        /// Время возможного предварительного выполнения
        /// </summary>
        public DateTime DateTimeEarlyExecution { get; set; }
        /// <summary>
        /// Время следующего выполнения
        /// </summary>
        public DateTime DateTimeNextRun { get; set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Вычислить время следующего выполнения системы
        /// </summary>
        public void CalculateNextRun()
        {
            DateTimeNextRun = DateTimeNextRun.AddTicks(System.IntervalTicks);
            DateTimeEarlyExecution = DateTimeNextRun.AddTicks(-System.EarlyExecutionTicks);
        }
        /// <summary>
        /// Вычислять фильтр системы заданное время
        /// </summary>
        /// <param name="limitTime"> Лимит времени на обработку Ticks </param>
        /// <returns></returns>
        public void FilterCalculateTime(long limitTimeTicks = 0)
        {
            //lock (System)
            //{
                _stopwatch.Reset();
                _stopwatch.Start();
                System.CalculateFilter(limitTimeTicks);
                _stopwatch.Stop();
                SystemStatistic.AddFilterCalculateStatistic(_stopwatch.ElapsedTicks); //Добавить в статистику
            //}
        }
        /// <summary>
        /// Действие системы
        /// </summary>
        /// <param name="dateTimePoint"> Метка времени, на которой выполняестся система </param>
        public void RunAsync(DateTime dateTimePoint, long timeWorkManagerSystemTicks)
        {
            //Task.Run(() =>
            //{
                //lock (System)
                //{
                    FilterCalculateTime(); //Вычислить фильтр

                    _stopwatch.Reset();
                    _stopwatch.Start();
                    System.CalculateDeltaTime(dateTimePoint); //Расчет DeltaTime
                    System.AсtionForeach();
                    _stopwatch.Stop();
                    SystemStatistic.AddRunStatistic(_stopwatch.ElapsedTicks, timeWorkManagerSystemTicks); //Добавить в статистику
                //}
            //}); //В отдельном потоке
        }
        #endregion
    }

    /// <summary>
    /// Класс информации о системе в очереди
    /// </summary>
    internal class SystemStatistic
    {
        #region Конструкторы
        #endregion

        #region Поля
        /// <summary>
        /// Сумарное количество вычислений фильтра
        /// </summary>
        private int _sumFilterCalculateCount;
        /// <summary>
        /// Сумарное время вычисления фильтра
        /// </summary>
        private float _sumFilterCalculateTimeTikcs;
        /// <summary>
        /// Среднее время вычисления фильтра
        /// </summary>
        private float _timeAverFilterCalculateInTicks;
        /// <summary>
        /// Максимальное время вычисления фильтра
        /// </summary>
        private float _timeMaxFilterCalculateInTicks;
        /// <summary>
        /// Сумарное время выполнения
        /// </summary>
        private float _sumRunTimeTikcs;
        /// <summary>
        /// Среднее время выполнения системы в тиках
        /// </summary>
        private float _timeAverRunInTicks;
        /// <summary>
        /// Максимальное время выполнения системы в тиках
        /// </summary>
        private float _timeMaxRunInTicks;
        #endregion

        #region Свойства
        /// <summary>
        /// Процент времени использования от производительности
        /// </summary>
        public float PercentTimeUsePerformance { get; private set; }
        /// <summary>
        /// Среднее время выполнения системы в мс
        /// </summary>
        public float AverTimeUseMs { get { return _timeAverRunInTicks / TimeSpan.TicksPerMillisecond; } }
        /// <summary>
        /// Максимально время выполнения системы в мс
        /// </summary>
        public float MaxTimeUseMs { get { return _timeMaxRunInTicks / TimeSpan.TicksPerMillisecond; } }
        /// <summary>
        /// Среднее время вычисления фильтра системы в мс
        /// </summary>
        public float AverTimeFilterCalculateMs { get { return _timeAverFilterCalculateInTicks / TimeSpan.TicksPerMillisecond; } }
        /// <summary>
        /// Максимально время вычисления фильтра системы в мс
        /// </summary>
        public float MaxTimeFilterCalculateMs { get { return _timeMaxFilterCalculateInTicks / TimeSpan.TicksPerMillisecond; } }
        /// <summary>
        /// Сумарное количество выполнений
        /// </summary>
        public int SummUseCount { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить статистику по выполнению
        /// </summary>
        /// <param name="timeRunInTicks"></param>
        /// <param name="timeWorkManagerSystemTicks"></param>
        public void AddRunStatistic(long timeRunInTicks, long timeWorkManagerSystemTicks)
        {
            SummUseCount++;
            _sumRunTimeTikcs += timeRunInTicks;
            _timeAverRunInTicks = _sumRunTimeTikcs / SummUseCount;
            if (timeRunInTicks > _timeMaxRunInTicks) { _timeMaxRunInTicks = timeRunInTicks; }
            PercentTimeUsePerformance = (_sumRunTimeTikcs + _sumFilterCalculateTimeTikcs) / (float)timeWorkManagerSystemTicks;
        }
        /// <summary>
        /// Добавить статистику по вычислению фильтра
        /// </summary>
        /// <param name="timeFilterCalculateInTicks"></param>
        public void AddFilterCalculateStatistic(long timeFilterCalculateInTicks)
        {
            _sumFilterCalculateCount++;
            _sumFilterCalculateTimeTikcs += timeFilterCalculateInTicks;
            _timeAverFilterCalculateInTicks = _sumFilterCalculateTimeTikcs / _sumFilterCalculateCount;
            if (timeFilterCalculateInTicks > _timeMaxFilterCalculateInTicks) { _timeMaxFilterCalculateInTicks = timeFilterCalculateInTicks; }
        }
        /// <summary>
        /// Очистить статистику системы
        /// </summary>
        public void ClearStatistic()
        {
            SummUseCount = 0; 
            _sumRunTimeTikcs = 0;
            _timeMaxRunInTicks = 0;
            _timeAverRunInTicks = 0;

            _sumFilterCalculateCount = 0;
            _sumFilterCalculateTimeTikcs = 0;
            _timeMaxFilterCalculateInTicks = 0;
            _timeAverFilterCalculateInTicks = 0;

            PercentTimeUsePerformance = 0;
        }
        #endregion
    }
}

//TODO 1) Выполнение системы в отдельном потоке из пула потоков.

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