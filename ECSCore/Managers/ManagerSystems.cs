using ECSCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Время фактического состояния
        /// </summary>
        private DateTime _dateTimePoint;
        #endregion

        #region Свойства

        #endregion

        #region Публичные методы

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
            List<Type> typesSystems = types.Where(t => t.IsAssignableFrom(typeISystem) && !t.IsInterface && !t.IsAbstract).ToList(); //Получим все системы в сборке
            foreach (Type typeSystem in typesSystems)
            {
                ISystem system = (ISystem)Activator.CreateInstance(typeSystem); //Создадим объект
                system.PreInitialization(); //Предварительная инициализация
                system.Injection(managerFilters); //Инекция данных
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
            _dateTimePoint = DateTime.Now;
            foreach (ISystem system in _systems)
            {
                _systemQueue.Insert(0, new SystemInfo(system, _dateTimePoint));
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
                if (_systemQueue[0].DateTimeNextRun <= _dateTimePoint)
                {
                    _systemQueue[0].DateTimeNextRun = _systemQueue[0].DateTimeNextRun.AddTicks(_systemQueue[0].System.IntervalTicks); //Следующее время выполнения системы
                    _systemQueue[0].System.PreAсtion(); //Подготовка к действию
                    _systemQueue[0].System.Aсtion(); //Обработка системы
                    SortSystemQueue(); //Сортировка очереди
                } //Если наступило время выполнения системы
                else
                {
                    TimeSpan timeSpan = _systemQueue[0].DateTimeNextRun - _dateTimePoint; //Интервал сна
                    Thread.Sleep(timeSpan); //Спим
                    _dateTimePoint = DateTime.Now; //Метка времени
                }
            } //Постоянно в цикле 
        }
        /// <summary>
        /// Сортировака очереди систем
        /// </summary>
        private void SortSystemQueue()
        {
            for (int i = 0; i < _systemQueue.Count-1; i++)
            {
                if(_systemQueue[i].DateTimeNextRun > _systemQueue[i+1].DateTimeNextRun)
                {
                    SystemInfo systemInfo = _systemQueue[i];
                    _systemQueue[i] = _systemQueue[i+1];
                    _systemQueue[i+1] = systemInfo;
                }
            } 
        }
        #endregion
    }

    /// <summary>
    /// Класс информации о системе в очереди
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="system"></param>
        /// <param name="dateTime"></param>
        public SystemInfo(ISystem system, DateTime dateTime)
        {
            System = system;
            DateTimeNextRun = dateTime.AddTicks(system.IntervalTicks);
        }

        /// <summary>
        /// Система
        /// </summary>
        public ISystem System;
        /// <summary>
        /// Время выполнения
        /// </summary>
        public DateTime DateTimeNextRun;
    }
}