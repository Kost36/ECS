using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Collections.AvailableInformations
{
    /// <summary>
    /// Коллекция доступной информации о сущностях одного типа
    /// </summary>
    public sealed class Informations
    {
        private TimeSpan _lifeTime;
        private readonly Dictionary<Guid, Information> _informations = new Dictionary<Guid, Information>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lifeTime"> Время жизни информации</param>
        public Informations(TimeSpan lifeTime)
        {
            _lifeTime = lifeTime;
        }

        /// <summary>
        /// Добавить или обновить инофрмацию о сущности
        /// </summary>
        public void AddOrUpdate(Entity entity)
        {
            if (_informations.TryGetValue(entity.Id, out var information))
            {
                information.ExpirationTime = DateTimeOffset.UtcNow.Add(_lifeTime);
                return;
            }

            _informations.Add(
                entity.Id,
                new Information()
                {
                    Entity = entity,
                    ExpirationTime = DateTimeOffset.UtcNow.Add(_lifeTime)
                });
        }

        /// <summary>
        /// Контроль актуальности информации
        /// </summary>
        public void ControlExpiration()
        {
            var dateTimeNow = DateTimeOffset.UtcNow;
            var keysForRemove = new List<Guid>();
            
            foreach (var item in _informations)
            {
                if (dateTimeNow > item.Value.ExpirationTime)
                {
                    keysForRemove.Add(item.Key);
                }
            }

            foreach (var key in keysForRemove)
            {
                _informations.Remove(key);
            }
        }

        /// <summary>
        /// Удалить инофрмацию о сущности
        /// </summary>
        public void Remove(Guid id)
        {
            _informations.Remove(id);
        }

        /// <summary>
        /// Получить всю доступную информацию о сущностях
        /// </summary>
        public List<Entity> GetAllEntites()
        {
            return _informations.Values.Select(item => item.Entity).ToList();
        }
    }
}