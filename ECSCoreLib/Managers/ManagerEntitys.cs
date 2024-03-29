﻿using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECSCore.Managers
{
    /// <summary>
    /// Менеджер сущностей
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
        //private readonly Queue<Guid> _queueFreeID = new Queue<int>();
        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        private readonly Dictionary<Guid, Entity> _entitys;

        internal ManagerEntitys()
        {
            _entitys = new Dictionary<Guid, Entity>();
        }

        /// <summary>
        /// Количество существующих сущностей
        /// </summary>
        public int CountEntitys
        {
            get { return _entitys.Count; }
        }

        /// <summary>
        /// Получить первый id сущности из коллекции
        /// </summary>
        /// <returns></returns>
        public Guid GetIdFirstEntity()
        {
            return _entitys.Keys.FirstOrDefault();
        }

        internal Entity Add(Entity entity)
        {
            return Registration(entity);
        }

        internal bool Get(Guid id, out Entity Entity)
        {
            lock (_entitys)
            {
                return _entitys.TryGetValue(id, out Entity);
            }
        }

        internal bool Remove(Guid id)
        {
            return RemoveEntity(id);
        }

        private Entity Registration(Entity entity)
        {
            entity.Id = Guid.NewGuid();
            lock (_entitys)
            {
                //if (_queueFreeID.Count > 0)
                //{
                //    entity.Id = _queueFreeID.Dequeue();
                //}
                //else
                //{
                //    _endUseId++;
                //    entity.Id = _endUseId;
                //}

                _entitys.Add(entity.Id, entity);
            }
            return entity;
        }

        private bool RemoveEntity(Guid id)
        {
            lock (_entitys)
            {
                //_queueFreeID.Enqueue(id);
                return _entitys.Remove(id);
            }
        }
    }
}