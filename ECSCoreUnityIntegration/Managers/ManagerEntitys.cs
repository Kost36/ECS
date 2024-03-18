using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System.Collections.Generic;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер сущностей
    /// </summary>
    public class ManagerEntitys
    {
        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ecs"> Ссылка на ecs </param>
        internal ManagerEntitys(ECS ecs, int startCountEntityCapacity)
        {
            _ecs = ecs;
            if (startCountEntityCapacity > 10)
            {
                _startCountCapacity = startCountEntityCapacity;
            }
            _entities = new Dictionary<int, Entity>(_startCountCapacity);
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
        /// Стартовая вместимость коллекции
        /// </summary>
        private int _startCountCapacity = 10;
        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        private Dictionary<int, Entity> _entities;
        #endregion

        #region Свойства
        /// <summary>
        /// Количество существующих сущностей
        /// </summary>
        public int CountEntitys
        {
            get { return _entities.Count; }
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить сущность
        /// 1) Присваивает Id
        /// 2) Добавляет сущность в коллекцию 
        /// </summary>
        /// <param name="entity"> Экземпляр сущности </param>
        /// <returns> IEntity (с присвоенным Id) / null </returns>
        internal Entity Add(Entity entity)
        {
            return Registration(entity); //Присвоим id и добавим в коллекцию
        }
        /// <summary>
        /// Получить сущность по id, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущности</param>
        /// <param name="Entity"> Сущность (Если есть) / null </param>
        /// <returns> Флаг наличия сущности </returns>
        internal bool Get(int id, out Entity Entity)
        {
            return _entities.TryGetValue(id, out Entity);
        }
        /// <summary>
        /// Удаление сущности по id
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        internal bool Remove(int id)
        {
            return RemoveEntity(id); //Удалим сущность
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавим сущность в коллекцию
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> IEntity / null </returns>
        private Entity Registration(Entity entity)
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
            _entities.Add(entity.Id, entity);
            return entity;
            //TODO проверить!!!
            //if (_entities.TryAdd(entity.Id, entity)) //Добавим в коллекцию
            //{
            //    return entity; //Вернем сущность
            //} //Если добавлено
            _queueFreeID.Enqueue(entity.Id); //Вернем id в очередь свободных id
            return null;
        }
        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity"> экземпляр сущности </param>
        private bool RemoveEntity(int id)
        {
            _queueFreeID.Enqueue(id); //Запишем освободившийся id в очередь
            return _entities.Remove(id); //Удалим сущность из коллекции
        }
        #endregion
    }
}