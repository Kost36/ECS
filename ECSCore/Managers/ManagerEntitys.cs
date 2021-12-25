using ECSCore.BaseObjects;
using ECSCore.Interface;
using System.Collections.Generic;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер сущьностей
    /// </summary>
    public class ManagerEntitys
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ecs"> Ссылка на ecs </param>
        public ManagerEntitys(ECS ecs)
        {
            _ecs = ecs;
        }
        #endregion

        #region Поля
        /// <summary>
        /// Ссылка на ECSCore
        /// </summary>
        private ECS _ecs;
        /// <summary>
        /// Последний использованный Id
        /// </summary>
        private int _endUseId = 0;
        /// <summary>
        /// Очередь свободных Id
        /// </summary>
        private Queue<int> _queueFreeID = new Queue<int>();
        /// <summary>
        /// Коллекция сущьностей
        /// </summary>
        private Dictionary<int, EntityBase> _entities = new Dictionary<int, EntityBase>();
        #endregion

        #region Свойства
        /// <summary>
        /// Количество существующих сущьностей
        /// </summary>
        public int CountEntitys
        {
            get { return _entities.Count; }
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить сущьность
        /// 1) Присваивает Id
        /// 2) Добавляет сущьность в коллекцию 
        /// </summary>
        /// <param name="entity"> Экземпляр сущьности </param>
        /// <returns> IEntity (с присвоенным Id) / null </returns>
        public EntityBase Add(EntityBase entity)
        {
            return Registration(entity); //Присвоим id и добавим в коллекцию
        }
        /// <summary>
        /// Получить сущьность по id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        /// <returns> IEntity / null </returns>
        public EntityBase Get(int id)
        {
            if (_entities.TryGetValue(id, out EntityBase entity))
            {
                return entity;
            } //Если нашли в коллекции
            return null; //Вернем null
        }
        /// <summary>
        /// Удаление сущьности по id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        public bool Remove(int id)
        {
            return RemoveEntity(id); //Удалим сущьность
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавим сущьность в коллекцию
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> IEntity / null </returns>
        private EntityBase Registration(EntityBase entity)
        {
            if (_queueFreeID.Count > 0)
            {
                entity.Id = _queueFreeID.Dequeue(); //Получим id из очереди
            } //Если в очереди есть свободные id
            else
            {
                _endUseId++; //Инкрементируем счетчик
                entity.Id = _endUseId; //Присвоим новый id
            } //Иначе
            if (_entities.TryAdd(entity.Id, entity)) //Добавим в коллекцию
            {
                return entity; //Вернем сущьность
            } //Если добавлено
            _queueFreeID.Enqueue(entity.Id); //Вернем id в очередь свободных id
            return null;
        }
        /// <summary>
        /// Удаление сущьности
        /// </summary>
        /// <param name="entity"> экземпляр сущьности </param>
        private bool RemoveEntity(int id)
        {
            _queueFreeID.Enqueue(id); //Запишем освободившийся id в очередь
            return _entities.Remove(id); //Удалим сущьность из коллекции
        }
        #endregion
    }
}

//TODO 1) Получение сущьность по формату TryGet(out EntityBase)

//TODO 2) Получение компонента по формату TryGet(out ComponentBase)

//TODO 3) Сущьность хранит ссылки на свои компоненты (ECS вызывает удаление компонента у сущьности)