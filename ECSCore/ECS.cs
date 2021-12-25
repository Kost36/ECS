using ECSCore.BaseObjects;
using ECSCore.Interface;
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
    public class ECS
    {
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
        /// Инициализация ECS
        /// </summary>
        public static void Initialization(Assembly assembly) //ECSSetting ecsSetting, 
        {
            if (_ecs == null)
            {
                _ecs = new(); //Создали
            } //Если экземпляра нету
            _ecs._managerEntitys = new(_ecs); //Инициализация менеджера сущьностей
            _ecs._managerComponents = new(_ecs); //Инициализация менеджера компонент
            _ecs._managerFilters = new(_ecs, assembly); //Создадим менеджера фильтров
            _ecs._managerSystems = new(_ecs, assembly, _ecs._managerFilters); //Создадим менеджера систем

            //_ecs.ECSetting = ecsSetting; //Зададим параметры работы ECS
        }
        #endregion

        #region Конструткоры
        private ECS() { }
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
        public EntityBase AddEntity(EntityBase entity)
        {
            return _managerEntitys.Add(entity);
        }
        /// <summary>
        /// Получить сущьность по Id, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <param name="entityBase"> Сущьность (Если есть) / null </param>
        /// <returns> Флаг наличия сущьности </returns>
        public bool GetEntity(int id, out EntityBase entityBase)
        {
            return _managerEntitys.Get(id, out entityBase);
        }
        /// <summary>
        /// Уничтожить сущьность по Id (компоненты сущьности тоже будут уничтожены)
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void RemoveEntity(int id)
        {
            _managerComponents.Remove(id);
            _managerEntitys.Remove(id);
        }
        #endregion

        #region Компоненты
        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="component"> Компонент с заданным Id сущьности, которой он пренадлежит </param>
        public void AddComponent<T>(T component)
            where T : ComponentBase
        {
            _managerComponents.Add(component);
            _managerFilters.Add(component);
        }
        /// <summary>
        /// Получить компонент, если есть
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <param name="idEntity"> Идентификатор сущьности, на которой должен быть компонент </param>
        /// <param name="component"> Компонент (Если есть) / null </param>
        /// <returns> Флаг наличия компонента </returns>
        public bool GetComponent<T>(int idEntity, out T component)
            where T : ComponentBase
        {
            return _managerComponents.Get<T>(idEntity, out component);
        }
        /// <summary>
        /// Получить все компоненты сущьности
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ComponentBase> GetComponents(int idEntity)
        {
            return _managerComponents.Get(idEntity);
        }
        /// <summary>
        /// Удалить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns></returns>
        public void RemoveComponent<T>(int idEntity)
            where T : ComponentBase
        {
            _managerComponents.Remove<T>(idEntity);
            _managerFilters.Remove<T>(idEntity);
        }
        #endregion

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
                info = info.Append($"Filters: \r\n");
                info = info.Append($"CountFiltersForSystems: {this.ManagerFilters.CountFilters} \r\n");
                info = info.Append($"CountEntitysInFilters: {this.ManagerFilters.CountEntitys} \r\n");
                info = info.Append($"Systems info: \r\n");
                info = info.Append($"{this.ManagerSystems.GetInfo(smallInfo)} \r\n");
            }
            return info;
        }
        #endregion
    }
}