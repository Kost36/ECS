using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        internal ManagerEntitys(ECS ecs)
        {
            _ecs = ecs;
            _entitys = new Dictionary<int, Entity>(_startCountCapacity);
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
        /// Коллекция сущьностей
        /// </summary>
        private Dictionary<int, Entity> _entitys;
        #endregion

        #region Свойства
        /// <summary>
        /// Количество существующих сущьностей
        /// </summary>
        public int CountEntitys
        {
            get { return _entitys.Count; }
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Получить первый id сущьности из коллекции
        /// </summary>
        /// <returns></returns>
        public int GetIdFirstEntity()
        {
            return _entitys.Keys.FirstOrDefault();
        }
        #endregion

        #region Внутренние методы
        /// <summary>
        /// Добавить сущьность
        /// 1) Присваивает Id
        /// 2) Добавляет сущьность в коллекцию 
        /// </summary>
        /// <param name="entity"> Экземпляр сущьности </param>
        /// <returns> IEntity (с присвоенным Id) / null </returns>
        internal Entity Add(Entity entity)
        {
            return Registration(entity); //Присвоим id и добавим в коллекцию
        }
        /// <summary>
        /// Получить сущьность по id, если есть
        /// </summary>
        /// <param name="id"> Идентификатор сущьности</param>
        /// <param name="Entity"> Сущьность (Если есть) / null </param>
        /// <returns> Флаг наличия сущьности </returns>
        internal bool Get(int id, out Entity Entity)
        {
            return _entitys.TryGetValue(id, out Entity);
        }
        /// <summary>
        /// Удаление сущьности по id
        /// </summary>
        /// <param name="id"> Идентификатор сущьности </param>
        internal bool Remove(int id)
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
            _entitys.Add(entity.Id, entity);
            return entity;
            //if (_entities.TryAdd(entity.Id, entity)) //Добавим в коллекцию
            //{
            //    return entity; //Вернем сущьность
            //} //Если добавлено
            //_queueFreeID.Enqueue(entity.Id); //Вернем id в очередь свободных id
            //return null;
        }
        /// <summary>
        /// Удаление сущьности
        /// </summary>
        /// <param name="entity"> экземпляр сущьности </param>
        private bool RemoveEntity(int id)
        {
            _queueFreeID.Enqueue(id); //Запишем освободившийся id в очередь
            return _entitys.Remove(id); //Удалим сущьность из коллекции
        }
        #endregion
    }
}