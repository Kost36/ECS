﻿using ECSCore.Interfaces.ECS;

namespace ECSCore.Interfaces.Systems
{
    /// <summary>
    /// Интерфейс системы;
    /// </summary>
    internal interface ISystem : ISystemSetting, ISystemRunTime { }

    /// <summary>
    /// Интерфейс параметров системы
    /// </summary>
    internal interface ISystemSetting
    {
        /// <summary>
        /// Система включена (Работает)
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// Приоритет выполнения системы
        /// </summary>
        int Priority { get; set; }
        /// <summary>
        /// Интервал выполнения системы
        /// </summary>
        long IntervalTicks { get; set; }
        /// <summary>
        /// Возможное предварительное выполнение системы (Предварительный интервал ticks)
        /// </summary>
        long EarlyExecutionTicks { get; set; }
        /// <summary>
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        bool IsActionAdd { get; set; }
        /// <summary>
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        bool IsAction { get; set; }
        /// <summary>
        /// Флаг наличия соответствующего интерфейса у системы 
        /// </summary>
        bool IsActionRemove { get; set; }
    }
    /// <summary>
    /// Интерфейс RunTime данных системы
    /// </summary>
    internal interface ISystemRunTime
    {
        /// <summary>
        /// Интервал времени между предидущим выполнением и фактическим.
        /// Размерность: sec
        /// </summary>
        float DeltaTime { get; set; }
        /// <summary>
        /// Интерфейс взаимодействия с ECS из систем
        /// </summary>
        IECSSystem IECS { get; }
    }

    /// <summary>
    /// Интерфейс однократного действия для добавленных в систему сущностей;
    /// Метод ActionAdd() будет вызываться у систем, которые реализуют данный интерфейс; 
    /// Метод ActionAdd() необходимо пометить ключевым словом override; 
    /// </summary>
    public interface ISystemActionAdd { }
    /// <summary>
    /// Интерфейс постоянного действия для обрабатываемых системой сущностей;
    /// Метод Action() будет вызываться у систем, которые реализуют данный интерфейс; 
    /// Метод Action() необходимо пометить ключевым словом override; 
    /// </summary>
    public interface ISystemAction { }
    /// <summary>
    /// Интерфейс однократного действия для удаленных из системы сущностей;
    /// Метод ActionRemove() будет вызываться у систем, которые реализуют данный интерфейс; 
    /// Метод ActionRemove() необходимо пометить ключевым словом override; 
    /// </summary>
    public interface ISystemActionRemove { }

    /// <summary>
    /// Интерфейс - флаг. 
    /// Система имеющая данный интерфейс будет распараллеливать на заданное количество потоков вызовы Action
    /// </summary>
    public interface ISystemParallel { }
    /// <summary>
    /// Интерфейс - флаг. 
    /// Система имеющая данный интерфейс будет передавать управление Action наследнику
    /// </summary>
    public interface ISystemManualControlAction { }
    /// <summary>
    /// Интерфейс - флаг. 
    /// Система имеющая данный интерфейс будет выполнять все ActionAdd/Action/ActionRemove синхронно в введенном потоке.
    /// </summary>
    public interface ISystemUseInjectThread { }
}