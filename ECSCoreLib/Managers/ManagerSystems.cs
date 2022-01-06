using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Exceptions;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
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
            Init(assembly, managerFilters); //Инициализация 
            _thread = new Thread(Start); //Инициализация потока
            _thread.Start(); //Пуск 
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECS
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Поток работы систем
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// Команда на останов менеджера (останов всех потоков)
        /// </summary>
        private bool _stopCMD;
        /// <summary>
        /// Список систем
        /// </summary>
        private List<ISystem> _systems = new List<ISystem>();
        /// <summary>
        /// Очередь выполнения систем
        /// </summary>
        private List<JobSystem> _systemQueue = new List<JobSystem>();
        /// <summary>
        /// Метка времени запуска приложения
        /// </summary>
        private long _ticksStart;
        /// <summary>
        /// Метка фактического времени
        /// </summary>
        private long _ticksPoint;

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
        /// Время задержки выполнения систем в мс
        /// </summary>
        public float TimeDelayExecuted { get { return _timeDelayExecuted; } }
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
                if (system is T)
                {
                    t = (T)system;
                    return true;
                }
            }
            t = default(T);
            return false;
        } //TODO сокрытие
        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        internal void Despose()
        {
            //TODO Завершить потоки менеджера систем
            _stopCMD = true; //Команда на останов
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
                } //Если бит сбросился в false. значит менеджер остановил все системы и завершил все потоки
            }  //Пока бит останова не сбросится в false
        }
        #endregion

        #region Приватные методы инициализации
        /// <summary>
        /// инициализация систем
        /// </summary>
        /// <param name="assembly"> Ссылка на сборку с реализациями систем </param>
        /// <param name="managerFilters"> Ссылка на менеджер фильтров </param>
        private void Init(Assembly assembly, ManagerFilters managerFilters)
        {
            Type typeISystem = typeof(ISystem); //Получим тип интерфейса
            Type[] types = assembly.GetTypes(); //Получаем все типы сборки 
            List<Type> typesSystems = types.Where(t => typeISystem.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList(); //Получим все системы в сборке
            foreach (Type typeSystem in typesSystems)
            {
                SystemBase system = (SystemBase)Activator.CreateInstance(typeSystem); //Создадим систему
                system.GetAttributes(); //Подтяжка аттребут
                system.Init(managerFilters, _ecs); //Инициализация системы
                AddSystem(system); //Добавим в список
            } //Пройдемся по всем системам 
        }
        /// <summary>
        /// Добавить систему в список
        /// </summary>
        /// <param name="system"> Система </param>
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
            FillingQueue(); //Заполнение очереди выполнения систем
            if (_systemQueue.Count == 0)
            {
                throw new ExceptionECSHaveNotSystem("Нет реализованных систем");
            } //Если систем нету 
            while (true)
            {
                Run(); //Анализ и выполнение систем
                if (_stopCMD)
                {
                    return; //Выход из метода
                } //Если есть команда на останов
            } //Постоянно выполняем
        }
        /// <summary>
        /// Заполнить очередь выполнения систем
        /// </summary>
        private void FillingQueue()
        {
            _ticksStart = DateTime.Now.Ticks;
            _ticksPoint = _ticksStart;
            foreach (SystemBase system in _systems)
            {
                _systemQueue.Insert(0, new JobSystem(system, _ticksStart));
                if (system.IsEnable)
                {
                    _countEnableSystems++; //Кол-во включенных систем
                } //Считаем кол-во включенных систем
                SortSystemQueue(); //Переместим систему на свою позицию выполнения
            } //Добавим все системы в очередь
        }
        /// <summary>
        /// Работа систем
        /// </summary>
        private void Run()
        {
            SetTicksPoint(); //Зададим новую фактическую метку времени
            lock (_systemQueue)
            {
                if (_systemQueue[0].TicksNextRun <= _ticksPoint)
                {
                    StartCalculateJobSystem(_systemQueue[0]); //Запускаем выполнение системы
                    if (_systemQueue.Count>1)
                    {
                        if (_systemQueue[1].TicksNextRun <= _ticksPoint)
                        {
                            _flagHaveDelayRunSystem = true; //Не успеваем выполнять системы
                            _timeDelayExecuted = (float)(_ticksPoint - _systemQueue[1].TicksNextRun) / (float)TimeSpan.TicksPerMillisecond; //Считаем время задержки выполнения систем
                        } //Если следующая система тоже должна выполниться
                    } //Если систем больше 1
                    return;
                } //Если настало время выполнения первой в очереди системы

                foreach (JobSystem jobSystem in _systemQueue)
                {
                    if (jobSystem.TicksEarlyExecution <= _ticksPoint)
                    {
                        StartCalculateJobSystem(jobSystem); //Запускаем выполнение системы
                        return; //TODO Проверка необходимости запуска м запуск след системы
                    } //Если настало время выполнения или настало время предварительного выполнения
                } //Проходимся по очереди системам на запуск (Контролим время предварительного запуска)

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
                Thread.Sleep(1); //Значение 0 - недает процессору простаивать => в холостую грузит процессор до 25-40%
            }
        }
        /// <summary>
        /// Запуск обработки системы (Вынесенный метод) 
        /// </summary>
        private void StartCalculateJobSystem(JobSystem jobSystem)
        {
            if (jobSystem.System.IsEnable == false)
            {
                jobSystem.SetTicks(DateTime.MaxValue.Ticks); //Максимальное время запуска выполнения системы
                _countEnableSystems--;
                _сountDisableSystems++;
                SortSystemQueue(); //Сортировка очереди
                return;
            } //Если система выключена, то смещаем ее в конец
            jobSystem.CalculateNextRun(); //Вычислить время след. выполнения
            SortSystemQueue(); //Сортировка очереди
            //TODO Фиксировать связанные компоненты!!!!
            jobSystem.Run(_ticksPoint, _timeWorkManagerSystemTicks); //Обработка системы
        }
        /// <summary>
        /// Задать метку фактического времени
        /// </summary>
        private void SetTicksPoint()
        {
            _ticksPoint = DateTime.Now.Ticks; //Новая метка времени
            _timeWorkManagerSystemTicks = _ticksPoint - _ticksStart; //Время работы приложения
        }
        /// <summary>
        /// Вычисляем свободное время в тиках
        /// </summary>
        /// <param name="controlTypeDelay"> Перечисление нужного контроля </param>
        /// <returns> свободное время в тиках </returns>
        private long ControlFreeTimeTicks(ControlTypeDelay controlTypeDelay = ControlTypeDelay.Not)
        {
            SetTicksPoint(); //Зададим новую фактическую метку времени
            long freeTicks = _systemQueue[0].TicksNextRun - _ticksPoint; //Вычисляем свободное время на вычисление фильтров
            if (freeTicks < 0)
            {
                switch (controlTypeDelay)
                {
                    case ControlTypeDelay.DelayRunSystem:
                        //_flagHaveDelayRunSystem = true; //Не успеваем выполнять системы
                        break;
                    case ControlTypeDelay.DelayCalculateFiltersSystem:
                        _flagHaveDelayCalculateFilters = true; //Не успеваем вычислять фильтра
                        break;
                }
                return 0;
            } //Если свободного времени нету
            else
            {
                switch (controlTypeDelay)
                {
                    case ControlTypeDelay.DelayRunSystem:
                        _flagHaveDelayRunSystem = false; //Успеваем выполнять системы
                        _timeDelayExecuted = 0;
                        break;
                    case ControlTypeDelay.DelayCalculateFiltersSystem:
                        _flagHaveDelayCalculateFilters = false; //Успеваем вычислять фильтра
                        break;
                }
                return freeTicks;
            } //Если есть свободное время
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
                    if (_systemQueue[i].TicksNextRun > _systemQueue[i + 1].TicksNextRun)
                    {
                        //Меняем системы местами
                        JobSystem jobSystem = _systemQueue[i];
                        _systemQueue[i] = _systemQueue[i + 1];
                        _systemQueue[i + 1] = jobSystem;
                    } //Если следующая в очереди система должна выполниться раньше
                } //Сдвигает первую систему на новую позицию, соответствующую времени выполнения системы
                
                long ticksMaxValue = DateTime.MaxValue.Ticks;
                for (int i = _systemQueue.Count - 1; i > 0; i--)
                {
                    if (_systemQueue[i].System.IsEnable)
                    {
                        if (_systemQueue[i].TicksNextRun == ticksMaxValue)
                        {
                            _systemQueue[i].SetTicks(_ticksPoint); //Сброс данных
                            _systemQueue[i].CalculateNextRun(); //Рассчитать след выполнение системы
                            _countEnableSystems++;
                            _сountDisableSystems--;
                            for (int j = i; j > 0; j--)
                            {
                                if (_systemQueue[j].TicksNextRun < _systemQueue[j - 1].TicksNextRun)
                                {
                                    //Меняем системы местами
                                    JobSystem jobSystem = _systemQueue[j];
                                    _systemQueue[j] = _systemQueue[j - 1];
                                    _systemQueue[j - 1] = jobSystem;
                                } //Если предидущая в очереди система должна выполниться позже
                            } //Сдвигает систему на новую позицию, соответствующую времени выполнения системы
                        } //Если система только включилась
                        return; //Если нашли включенную систему, смещаем только ее и выходим.
                    } //Если система включена
                } //Проверяем включение систем, и сдвигаем на соответствующее место
            }
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