using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Exceptions;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.ECS;
using ECSCore.Managers;
using System;
using System.Reflection;
using System.Text;

namespace ECSCore
{
    /// <summary>
    /// Модуль Entity Component System
    /// </summary>
    [Serializable]
    public class ECS : IECSSystem, IECS, IECSDebug
    {
        #region Конструткоры
        private ECS() { }
        #endregion

        #region Singlton
        /// <summary>
        /// Получить экземпляр ECS
        /// </summary>
        /// <returns></returns>
        public static ECS Instance
        {
            get
            {
                return _ecs; //Вернуть экземпляр
            }
        }

        /// <summary>
        /// Получить экземпляр ECS
        /// </summary>
        /// <returns></returns>
        public static IECS InstanceIECS
        {
            get
            {
                return _ecs; //Вернуть экземпляр
            }
        }

        /// <summary>
        /// Получить отладочный экземпляр ECS
        /// </summary>
        /// <returns></returns>
        public static IECSDebug InstanceDebug
        {
            get
            {
                return _ecs; //Вернуть экземпляр
            }
        }

        /// <summary>
        /// Инициализация ECS
        /// </summary>
        /// <param name="assembly"> ссылка на сборку (Где находятся: Компоненты / Cистемы) </param>
        /// <exception cref="ECSIsAlreadyInitializedException"> </exception>
        public static void Initialization(Assembly assembly) //ECSSetting ecsSetting, 
        {
            if (_ecs == null)
            {
                _ecs = new ECS(); //Создали
            } //Если экземпляра нету
            else
            {
                throw new ECSIsAlreadyInitializedException("ECS was initialized before");
            }
            _ecs._managerEntitys = new ManagerEntitys(); //Инициализация менеджера сущностей
            _ecs._managerFilters = new ManagerFilters(_ecs); //Создадим менеджера фильтров
            _ecs._managerSystems = new ManagerSystems(_ecs, assembly, _ecs._managerFilters); //Создадим менеджера систем

            //_ecs.ECSetting = ecsSetting; //Зададим параметры работы ECS
        }
        #endregion

        #region Поля
        /// <summary>
        /// Синглтон
        /// </summary>
        private static ECS _ecs;

        /// <summary>
        /// Менеджер сущностей
        /// </summary>
        private ManagerEntitys _managerEntitys;

        /// <summary>
        /// Менеджер фильтров компонент
        /// </summary>
        private ManagerFilters _managerFilters;

        /// <summary>
        /// Менеджер систем
        /// </summary>
        private ManagerSystems _managerSystems;
        #endregion

        #region Свойства
        /// <summary>
        /// Менеджер сущностей
        /// </summary>
        public ManagerEntitys ManagerEntitys { get { return _managerEntitys; } }

        /// <summary>
        /// Менеджер фильтров компонент
        /// </summary>
        public ManagerFilters ManagerFilters { get { return _managerFilters; } }

        /// <summary>
        /// Менеджер систем
        /// </summary>
        public ManagerSystems ManagerSystems { get { return _managerSystems; } }
        #endregion

        #region Публичные методы

        #region Сущности
        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity"> сущность </param>
        /// <returns> IEntity (с присвоенным id) / null </returns>
        public Entity AddEntity(Entity entity)
        {
            return _managerEntitys.Add(entity);
        }

        /// <summary>
        /// Получить сущность по Id, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        /// <param name="Entity"> Сущность (Если есть) / null </param>
        /// <returns> Флаг наличия сущности </returns>
        public bool GetEntity(Guid id, out Entity Entity)
        {
            return _managerEntitys.Get(id, out Entity);
        }

        /// <summary>
        /// Уничтожить сущность по Id (компоненты сущности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        public void RemoveEntity(Guid id)
        {
            _managerEntitys.Remove(id);
            _managerFilters.Remove(id);
        }
        #endregion

        #region Компоненты 
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущности, которой он пренадлежит </param>
        public void AddComponent<T>(T component)
            where T : IComponent
        {
            if (_managerEntitys.Get(component.Id, out Entity entity) == false)
            {
                return;
            } //Получим сущность от менеджера сущностей
            entity.AddComponentInternal(component); //Добавить к сущности
            _managerFilters.Add<T>(component); //Передать менеджеру фильтров 
        } // TODO При добавлении компонента, который уже есть на сущности кинет исключение: продумать действия в данной ситуации, и синхронизировать действие для Entity и Filters, что бы небыло разногласий

        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонентов
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool GetComponent<T>(Guid idEntity, out T component)
            where T : IComponent
        {
            if (_managerEntitys.Get(idEntity, out Entity entity) == false)
            {
                component = default;
                return false;
            } //Если у менеджера сущностей нету сущности
            return entity.TryGetComponent(out component); //Получим компонент у сущности
        }

        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущности </param>
        /// <returns></returns>
        public void RemoveComponent<T>(Guid idEntity)
            where T : IComponent
        {
            if (_managerEntitys.Get(idEntity, out Entity Entity) == false)
            {
                return;
            }
            Entity.RemoveComponentInternal<T>();
            _managerFilters.Remove<T>(idEntity);
        }

        #endregion

        #region Состояние ECS
        /// <summary>
        /// Получить полную информацию о состоянии ECSCore
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetInfo(bool smallInfo = false)
        {
            StringBuilder info = new StringBuilder();
            if (smallInfo)
            {
                info = info.Append($"ECSCore: Version: {this.GetType().Assembly.GetName().Version} \r\n");
                if (this.ManagerSystems.IsNotHaveTimeToBeExecuted)
                {
                    info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} DelayTime: {this.ManagerSystems.TimeDelayExecuted} ms \r\n");
                }
                else
                {
                    info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} FreeTime: {this.ManagerSystems.FreeTime} ms \r\n");
                }
                info = info.Append($"ECSHaveN'tTimeToBeCalculateFilterSystems: {this.ManagerSystems.IsNotHaveTimeToBeCalculateFilters} \r\n");
                info = info.Append($"CountEntity: {this.ManagerEntitys.CountEntitys} \r\n");
                info = info.Append($"CountRegistredSystems: {this.ManagerSystems.CountSystems} \r\n");
                info = info.Append($"CountEnableSystems: {this.ManagerSystems.CountEnableSystems} \r\n");
                info = info.Append($"CountDisableSystems: {this.ManagerSystems.CountDisableSystems} \r\n");
                info = info.Append($"CountFiltersForSystems: {this.ManagerFilters.CountFilters} \r\n");
                info = info.Append($"CountEntitysInFilters: {this.ManagerFilters.CountEntitys} \r\n");
                info = info.Append($"Systems info: \r\n");
                info = info.Append($"{this.ManagerSystems.GetInfo(smallInfo)} \r\n");
            }
            else
            {
                info = info.Append($"ECSCore: \r\n");
                info = info.Append($"Version: {this.GetType().Assembly.GetName().Version} \r\n");
                info = info.Append($"Entitys: \r\n");
                info = info.Append($"CountEntity: {this.ManagerEntitys.CountEntitys} \r\n");
                info = info.Append($"Components: \r\n");
                info = info.Append($"Systems: \r\n");
                info = info.Append($"CountRegistredSystems: {this.ManagerSystems.CountSystems} \r\n");
                info = info.Append($"CountEnableSystems: {this.ManagerSystems.CountEnableSystems} \r\n");
                info = info.Append($"CountDisableSystems: {this.ManagerSystems.CountDisableSystems} \r\n");
                if (this.ManagerSystems.IsNotHaveTimeToBeExecuted)
                {
                    info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} DelayTime: {this.ManagerSystems.TimeDelayExecuted} ms \r\n");
                }
                else
                {
                    info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} FreeTime: {this.ManagerSystems.FreeTime} ms \r\n");
                }
                info = info.Append($"ECSHaveN'tTimeToBeCalculateFilterSystems: {this.ManagerSystems.IsNotHaveTimeToBeCalculateFilters} \r\n");
                info = info.Append($"Filters: \r\n");
                info = info.Append($"CountFiltersForSystems: {this.ManagerFilters.CountFilters} \r\n");
                info = info.Append($"CountEntitysInFilters: {this.ManagerFilters.CountEntitys} \r\n");
                info = info.Append($"Systems info: \r\n");
                info = info.Append($"{this.ManagerSystems.GetInfo(smallInfo)} \r\n");
            }
            return info;
        }
        #endregion

        #region Управление ECS
        /// <summary>
        /// Пауза
        /// </summary>
        public void Pause()
        {
            SetSpeed(ECSSpeed.Pause);
        }

        /// <summary>
        /// Работа
        /// </summary>
        public void Run()
        {
            SetSpeed(ECSSpeed.Run);
        }

        /// <summary>
        /// Задать скорость
        /// </summary>
        public void SetSpeed(ECSSpeed eCSSpeed)
        {
            if (_managerSystems != null)
            {
                _managerSystems.SetSpeed(eCSSpeed);
            } //Если модуль проинициализорован
        }

        /// <summary>
        /// Задать скорость
        /// </summary>
        /// <param name="speedRun"> Скорость в пределах: от 0.1 до 32 </param>
        /// <returns> Устанговленная скорость </returns>
        public float SetSpeed(float speedRun)
        {
            if (_managerSystems != null)
            {
                return _managerSystems.SetSpeed(speedRun);
            } //Если модуль проинициализорован
            return 0;
        }

        /// <summary>
        /// Очистить все и освободить ресурсы
        /// </summary>
        public void Despose()
        {
            if (_ecs != null)
            {
                _ecs._managerSystems.Despose();
                _ecs._managerEntitys = null;
                _ecs._managerFilters = null;
                _ecs._managerSystems = null;
                _ecs = null;
            }
        }
        #endregion

        #endregion
    }
}