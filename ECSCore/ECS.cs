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
            _ecs._mnagerFilters = new(_ecs, assembly); //Создадим менеджера фильтров
            _ecs._managerSystems = new(_ecs, assembly, _ecs._mnagerFilters); //Создадим менеджера систем

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
        private ManagerFilters _mnagerFilters;
        /// <summary>
        /// Менеджер систем
        /// </summary>
        private ManagerSystems _managerSystems;
        #endregion

        #region Свойства

        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить сущьность
        /// </summary>
        /// <param name="entity"> Сущьность </param>
        /// <returns> IEntity (с присвоенным id) / null </returns>
        public IEntity AddEntity(IEntity entity)
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
        public bool RemoveEntity(int id)
        {
            return _managerEntitys.Remove(id);
        }
        #endregion

        /// <summary>
        /// Удалить все компоненты по идентификатору сущьности
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public void RemoveComponentsOfId(int id)
        {
            throw new NotImplementedException();
        }
    }
}