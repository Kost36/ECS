using ECSCore.BaseObjects;
using ECSCore.Interface;
using ECSCore.Managers;
using System;
using System.Reflection;

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
        /// Получить сущьность по Id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <returns> IEntity / null </returns>
        public IEntity GetEntity(int id)
        {
            return _managerEntitys.Get(id);
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
        /// Получить компонент (Если есть)
        /// </summary>
        /// <typeparam name="T"> Generic компонента (Настледуется от ComponentBase) </typeparam>
        /// <returns> BaseComponent / null </returns>
        public ComponentBase GetComponent<T>(int idEntity)
            where T : ComponentBase
        {
            return _managerComponents.Get<T>(idEntity);
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
        #endregion
    }
}