using ECSCore.Interface;
using ECSCore.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        #region Конструктор
        /// <summary>
        /// Анализ и компоновка данных
        /// </summary>
        public ManagerSystems(ECS ecs, Assembly assembly, ManagerFilters managerFilters)
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
        private List<SystemInfo> _systemQueue = new List<SystemInfo>();
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
        /// Системы не успевают выполняться
        /// </summary>
        private bool _flagHaveDelay;
        /// <summary>
        /// Диагностический таймер выполнения системы
        /// </summary>
        private Stopwatch _stopwatchRunSystem = new Stopwatch();
        /// <summary>
        /// Сумарное время работы в тиках
        /// </summary>
        private long _sumWorkTimeTicks;
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
        public bool IsNotHaveTimeToBeExecuted { get { return _flagHaveDelay; } }
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
                foreach (SystemInfo systemInfo in _systemQueue)
                {
                    if (small)
                    {
                        info = info.Append($"PercentTimeUsePerformance: {systemInfo.PercentTimeUsePerformance.ToString("P")} MaxTimeUseInMs: {systemInfo.MaxTimeUseMs.ToString("F4")} Name: {systemInfo.System.GetType().FullName} IsEnable: {systemInfo.System.IsEnable} IntervalRunInMs: {systemInfo.System.IntervalTicks / TimeSpan.TicksPerMillisecond} \r\n");
                    }
                    else
                    {
                        info = info.Append($"<<<<<<<<<<InfoOfSystem>>>>>>>>>> \r\n");
                        info = info.Append($"Name: {systemInfo.System.GetType().FullName} \r\n");
                        info = info.Append($"IsEnable: {systemInfo.System.IsEnable} \r\n");
                        info = info.Append($"IntervalRunInTicks: {systemInfo.System.IntervalTicks} \r\n");
                        info = info.Append($"IntervalRunInMs: {systemInfo.System.IntervalTicks / TimeSpan.TicksPerMillisecond} \r\n");
                        info = info.Append($"Priority: {systemInfo.System.Priority} (0-NotUse; 1-MaxPriority; 50-MinPriority) \r\n");
                        info = info.Append($"PercentTimeUsePerformance: {systemInfo.PercentTimeUsePerformance} \r\n");
                        info = info.Append($"MaxTimeUseInMs: {systemInfo.MaxTimeUseMs} \r\n");
                        info = info.Append($"UseCount: {systemInfo.SummUseCount} \r\n");
                    }
                }
            }
            return info;
        }
        #endregion

        #region Приватные методы инициализации
        /// <summary>
        /// инициализация систем
        /// </summary>
        /// <param name="assembly"></param>
        private void _Init(Assembly assembly, ManagerFilters managerFilters)
        {
            Type typeISystem = typeof(ISystem); //Получим тип интерфейса
            Type[] types = assembly.GetTypes(); //Получаем все типы сборки 
            List<Type> typesSystems = types.Where(t => typeISystem.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList(); //Получим все системы в сборке
            foreach (Type typeSystem in typesSystems)
            {
                ISystem system = (ISystem)Activator.CreateInstance(typeSystem); //Создадим объект
                system.PreInitialization(); //Предварительная инициализация
                system.Injection(managerFilters, _ecs); //Инекция данных
                system.Initialization(); //Инициализация систем
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

        #region Приватные методы работы
        /// <summary>
        /// Запуск потока
        /// </summary>
        private void Start()
        {
            FillingQueue();
            Run();
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
                _systemQueue.Insert(0, new SystemInfo(system, _dateTimePoint));
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
            while (true)
            {
                if (_systemQueue[0].DateTimeNextRun.Ticks <= _dateTimePoint.Ticks)
                {
                    if (_systemQueue.Count > 1)
                    {
                        if (_systemQueue[1].DateTimeNextRun.Ticks <= _dateTimePoint.Ticks)
                        {
                            _flagHaveDelay = true;
                        } //Если следующая система должна выполняться
                    } //Если систем больше 1
                    if (_systemQueue[0].System.IsEnable == false)
                    {
                        _systemQueue[0].DateTimeNextRun = DateTime.MaxValue;
                        _countEnableSystems--;
                        _сountDisableSystems++;
                        SortSystemQueue(); //Сортировка очереди
                        continue;
                    } //Если первая система выключена, то смещаем ее в конец
                    _stopwatchRunSystem.Reset();
                    _stopwatchRunSystem.Start();
                    _systemQueue[0].DateTimeNextRun = _systemQueue[0].DateTimeNextRun.AddTicks(_systemQueue[0].System.IntervalTicks); //Следующее время выполнения системы
                    _systemQueue[0].System.CalculateDeltaTime(_dateTimePoint); //Расчет DeltaTime
                    _systemQueue[0].System.PreAсtion(); //Подготовка к действию
                    _systemQueue[0].System.Aсtion(); //Обработка системы
                    _stopwatchRunSystem.Stop();
                    _sumWorkTimeTicks = _dateTimePoint.Ticks - _dateTimeStart.Ticks; //Сумарное время работы менеджера
                    _systemQueue[0].CalculatePerformance(_stopwatchRunSystem.ElapsedTicks, _sumWorkTimeTicks);
                    SortSystemQueue(); //Сортировка очереди
                } //Если наступило время выполнения системы
                else
                {
                    _dateTimePoint = DateTime.Now; //Новая метка времени
                    long sleepInTicks = _systemQueue[0].DateTimeNextRun.Ticks - _dateTimePoint.Ticks; //Интервал сна
                    if (sleepInTicks < 0)
                    {
                        _flagHaveDelay = true; //Не успеваем выполнять
                        continue; //След итерация
                    } //Если время сна отрицательное
                    _flagHaveDelay = false; //Успеваем выполнять
                    Thread.Sleep(1); //Спим 1 мс
                    _dateTimePoint = DateTime.Now; //Новая метка времени

                    //_flagHaveDelay = false;
                    //TimeSpan timeSpan = _systemQueue[0].DateTimeNextRun - _dateTimePoint; //Интервал сна
                    //Thread.Sleep(timeSpan); //Спим
                    //_dateTimePoint = DateTime.Now; //Метка времени
                }
            } //Постоянно в цикле 
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
                        SystemInfo systemInfo = _systemQueue[i];
                        _systemQueue[i] = _systemQueue[i + 1];
                        _systemQueue[i + 1] = systemInfo;
                    }
                } //Сдвигает выполняемую систему на свое место
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
                                    SystemInfo systemInfo = _systemQueue[j];
                                    _systemQueue[j] = _systemQueue[j - 1];
                                    _systemQueue[j - 1] = systemInfo;
                                }
                            } //Сдвинем только 1 систему, следующая система будет сдвинута на след. итерации
                        } //Если система только включилась
                        return;
                    } //Если система включена
                } //Проверяем включение систем, и сдвигаем на соответствующее место
            }
        }
        #endregion
    }

    /// <summary>
    /// Класс информации о системе в очереди
    /// </summary>
    public class SystemInfo
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="system"></param>
        /// <param name="dateTime"></param>
        public SystemInfo(SystemBase system, DateTime dateTime)
        {
            System = system;
            DateTimeNextRun = dateTime.AddTicks(system.IntervalTicks);
        }
        #endregion

        #region Поля
        /// <summary>
        /// Сумарное время выполнения
        /// </summary>
        private float _sumUseTimeTikcs;
        /// <summary>
        /// Максимальное время выполнения системы в тиках
        /// </summary>
        private long _timeMaxRunInTicks;
        #endregion

        #region Свойства
        /// <summary>
        /// Система
        /// </summary>
        public SystemBase System { get; }
        /// <summary>
        /// Время следующего выполнения
        /// </summary>
        public DateTime DateTimeNextRun { get; set; }
        /// <summary>
        /// Процент времени использования от производительности
        /// </summary>
        public float PercentTimeUsePerformance { get; private set; }
        /// <summary>
        /// Максимально время использования в мс
        /// </summary>
        public float MaxTimeUseMs { get { return _timeMaxRunInTicks / TimeSpan.TicksPerMillisecond; } }
        /// <summary>
        /// Сумарное количество выполнений
        /// </summary>
        public int SummUseCount { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Рассчитать производительность системы
        /// </summary>
        /// <param name="timeRunInTicks"></param>
        /// <param name="timeWorkInTicks"></param>
        public void CalculatePerformance(long timeRunInTicks, long sumWorkTimeTicks)
        {
            SummUseCount++; 
            _sumUseTimeTikcs += timeRunInTicks;
            if (timeRunInTicks > _timeMaxRunInTicks) { _timeMaxRunInTicks = timeRunInTicks; }
            PercentTimeUsePerformance = _sumUseTimeTikcs / sumWorkTimeTicks;
        }
        #endregion
    }
}

//TODO 1) Выполнение системы в отдельном потоке из пула потоков.

//TODO 2) Аттрибут разрешение предварительного запуска систем.
//        Допустимая погрешность, для возможности предварительной обработки систем, 0-50%,
//        Где 0 - Предварительный запуск запрещен!
//            1-50 - Временной интервал предварительного запуска, рассчитать от интервала системы (как 100%)

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