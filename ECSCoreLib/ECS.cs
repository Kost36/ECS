using ECSCore.BaseObjects;
using ECSCore.Exceptions;
using ECSCore.Interfaces;
using ECSCore.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECSCore
{
    /// <summary>
    /// Модуль Entity Component System
    /// </summary>
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
        internal static ECS Instance
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
        /// <param name="startCapacityCollections"></param>
        /// <exception cref="ExceptionECSIsInitializated"> </exception>
        public static void Initialization(Assembly assembly, int startCapacityCollections = 10) //ECSSetting ecsSetting, 
        {
            if (_ecs == null)
            {
                _ecs = new ECS(); //Создали
            } //Если экземпляра нету
            else
            {
                throw new ExceptionECSIsInitializated("ECS was initialized before");
            }
            if (startCapacityCollections > 10)
            {
                _ecs._startCapacityCollections = startCapacityCollections;
            }
            _ecs._managerEntitys = new ManagerEntitys(_ecs, _ecs._startCapacityCollections); //Инициализация менеджера сущьностей
            _ecs._managerComponents = new ManagerComponents(_ecs, _ecs._startCapacityCollections); //Инициализация менеджера компонент
            _ecs._managerFilters = new ManagerFilters(_ecs, assembly, _ecs._startCapacityCollections); //Создадим менеджера фильтров
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
        /// Менеджер сущьностей
        /// </summary>
        private ManagerEntitys _managerEntitys;
        /// <summary>
        /// Менеджер компонентов
        /// </summary>
        private ManagerComponents _managerComponents;
        /// <summary>
        /// Менеджер фильтров компонент
        /// </summary>
        private ManagerFilters _managerFilters;
        /// <summary>
        /// Менеджер систем
        /// </summary>
        private ManagerSystems _managerSystems;
        /// <summary>
        /// Стартовая вместимость коллекций
        /// </summary>
        private int _startCapacityCollections;
        #endregion

        #region Свойства
        /// <summary>
        /// Менеджер сущьностей
        /// </summary>
        public ManagerEntitys ManagerEntitys { get { return _managerEntitys; } }
        /// <summary>
        /// Менеджер компонентов
        /// </summary>
        public ManagerComponents ManagerComponents { get { return _managerComponents; } }
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

        #region Сущьности
        /// <summary>
        /// Добавить сущьность
        /// </summary>
        /// <param name="entity"> Сущьность </param>
        /// <returns> IEntity (с присвоенным id) / null </returns>
        public Entity AddEntity(Entity entity)
        {
            return _managerEntitys.Add(entity);
        }
        /// <summary>
        /// Получить сущьность по Id, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="Entity"> Сущьность (Если есть) / null </param>
        /// <returns> Флаг наличия сущьности </returns>
        public bool GetEntity(int id, out Entity Entity)
        {
            return _managerEntitys.Get(id, out Entity);
        }
        /// <summary>
        /// Уничтожить сущьность по Id (компоненты сущьности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void RemoveEntity(int id)
        {
            _managerEntitys.Remove(id);
            _managerFilters.Remove(id);
            _managerComponents.Remove(id);
        }
        #endregion

        #region Компоненты
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущьности, которой он пренадлежит </param>
        public void AddComponent<T>(T component)
            where T : IComponent
        {
            if (_managerEntitys.Get(component.Id, out Entity Entity) == false)
            {
                return;
            } //Получим сущьность от менеджера сущьностей
            Entity.AddComponent(component); //Добавить к сущьности
            _managerComponents.Add(component); //Передать менеджеру компонент
            _managerFilters.Add(component); //Передать менеджеру фильтров
        }
        /// <summary>
        /// Добавить компонент.
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущьности, которой он пренадлежит </param>
        public void AddComponent<T>(T component, Entity Entity)
            where T : IComponent
        {
            if (Entity == null)
            {
                if (_managerEntitys.Get(component.Id, out Entity) == false)
                {
                    return;
                } //Получим сущьность от менеджера сущьностей
            } //Если сущьность не задана
            Entity.AddComponent(component); //Добавить к сущьности
            _managerComponents.Add(component); //Передать менеджеру компонент
            _managerFilters.Add(component); //Передать менеджеру фильтров
        }
        /// <summary>
        /// Получить компонент, если есть.
        /// Возвращает компонент из менеджера компонентов
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <param name="idEntity"> Идентификатор сущьности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool GetComponent<T>(int idEntity, out T component)
            where T : IComponent
        {
            return _managerComponents.Get(idEntity, out component);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        public void RemoveComponent<T>(int idEntity)
            where T : IComponent
        {
            if (_managerEntitys.Get(idEntity, out Entity Entity) == false)
            {
                return;
            }
            Entity.RemoveComponent<T>();
            _managerComponents.Remove<T>(idEntity);
            _managerFilters.Remove<T>(idEntity);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от Component) </typeparam>
        /// <returns></returns>
        public void RemoveComponent<T>(int idEntity, Entity Entity)
            where T : IComponent
        {
            if (Entity == null)
            {
                if (_managerEntitys.Get(idEntity, out Entity) == false)
                {
                    return;
                }
            }
            Entity.RemoveComponent<T>();
            _managerComponents.Remove<T>(idEntity);
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
                info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} \r\n");
                info = info.Append($"ECSHaveN'tTimeToBeCalculateFilterSystems: {this.ManagerSystems.IsNotHaveTimeToBeCalculateFilters} \r\n");
                info = info.Append($"CountEntity: {this.ManagerEntitys.CountEntitys} \r\n");
                info = info.Append($"CountComponents: {this.ManagerComponents.CountComponents} \r\n");
                info = info.Append($"CountComponentCollections: {this.ManagerComponents.CountCollectionsComponent} \r\n");
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
                info = info.Append($"CountComponents: {this.ManagerComponents.CountComponents} \r\n");
                info = info.Append($"CountComponentCollections: {this.ManagerComponents.CountCollectionsComponent} \r\n");
                info = info.Append($"Systems: \r\n");
                info = info.Append($"CountRegistredSystems: {this.ManagerSystems.CountSystems} \r\n");
                info = info.Append($"CountEnableSystems: {this.ManagerSystems.CountEnableSystems} \r\n");
                info = info.Append($"CountDisableSystems: {this.ManagerSystems.CountDisableSystems} \r\n");
                info = info.Append($"ECSHaveN'tTimeToBeExecutedSystems: {this.ManagerSystems.IsNotHaveTimeToBeExecuted} \r\n");
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

        #endregion
    }
}