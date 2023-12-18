using ECSCore.BaseObjects;
using System.Collections.Generic;
using System.Linq;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер сущьностей
    /// </summary>
    public class ManagerEntitys
    {
        /// <summary>
        /// Последний использованный Id
        /// </summary>
        private int _endUseId = 0;
        /// <summary>
        /// Очередь свободных Id
        /// </summary>
        private readonly Queue<int> _queueFreeID = new Queue<int>();
        /// <summary>
        /// Коллекция сущьностей
        /// </summary>
        private readonly Dictionary<int, Entity> _entitys;

        internal ManagerEntitys()
        {
            _entitys = new Dictionary<int, Entity>();
        }

        /// <summary>
        /// Количество существующих сущьностей
        /// </summary>
        public int CountEntitys
        {
            get { return _entitys.Count; }
        }

        /// <summary>
        /// Получить первый id сущьности из коллекции
        /// </summary>
        /// <returns></returns>
        public int GetIdFirstEntity()
        {
            return _entitys.Keys.FirstOrDefault();
        }

        internal Entity Add(Entity entity)
        {
            return Registration(entity);
        }

        internal bool Get(int id, out Entity Entity)
        {
            lock (_entitys)
            {
                return _entitys.TryGetValue(id, out Entity);
            }
        }

        internal bool Remove(int id)
        {
            return RemoveEntity(id);
        }

        private Entity Registration(Entity entity)
        {
            lock (_entitys)
            {
                if (_queueFreeID.Count > 0)
                {
                    entity.Id = _queueFreeID.Dequeue();
                }
                else
                {
                    _endUseId++;
                    entity.Id = _endUseId;
                }

                _entitys.Add(entity.Id, entity);
            }
            return entity;
        }

        private bool RemoveEntity(int id)
        {
            lock (_entitys)
            {
                _queueFreeID.Enqueue(id);
                return _entitys.Remove(id);
            }
        }
    }
}