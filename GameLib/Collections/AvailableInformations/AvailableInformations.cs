using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using System;
using System.Collections.Generic;

namespace GameLib.Collections.AvailableInformations
{
    /// <summary>
    /// Доступная информация о сущностях / сущностях с наличием компонента
    /// </summary>
    public sealed class AvailableInformations
    {
        private readonly InformationLifeTimeProvider _informationLifeTimeProvider = new InformationLifeTimeProvider();
        private readonly Dictionary<string, Informations> _collections = new Dictionary<string, Informations>();

        /// <summary>
        /// Добавить или обновить инофрмацию о сущности
        /// </summary>
        public void AddOrUpdate(Entity entity)
        {
            var type = entity.GetType();
            var key = type.FullName; //Todo размазано по коду

            if (!_collections.TryGetValue(key, out var informations))
            {
                informations = CreateCollection(key, type, entity);
            }

            informations.AddOrUpdate(entity);
        }

        /// <summary>
        /// Добавить или обновить инофрмацию о сущности имеющей компонент
        /// </summary>
        public void AddOrUpdate(Entity entity, ComponentBase component)
        {
            var componentType = component.GetType();
            var key = componentType.FullName; //Todo размазано по коду

            if (!_collections.TryGetValue(key, out var informations))
            {
                informations = CreateCollection(key, componentType, component);
            }

            informations.AddOrUpdate(entity);
        }

        /// <summary>
        /// Контроль времени хранения информации
        /// </summary>
        public void ControlExpiration(Type entityOrComponentType)
        {
            var key = entityOrComponentType.FullName; //Todo размазано по коду

            if (_collections.TryGetValue(key, out var informations))
            {
                informations.ControlExpiration();
            }
        }

        /// <summary>
        /// Удалить информацию о сущности
        /// </summary>
        public void Remove(Guid guid)
        {
            foreach (var collection in _collections.Values)
            {
                collection.Remove(guid);
            }
        }

        /// <summary>
        /// Получить все известные сущности определенного типа
        /// </summary>
        public List<Entity> GetAllEntites(Type entityType)
        {
            var key = entityType.FullName; //Todo размазано по коду

            if (_collections.TryGetValue(key, out var informations))
            {
                return informations.GetAllEntites();
            }

            return new List<Entity>();
        }

        private Informations CreateCollection(string collectionKey, Type entityType, IEntity entity)
        {
            var lifeTime = _informationLifeTimeProvider.GetLifeTime(entityType.FullName, entity);
            var collection = new Informations(lifeTime);

            _collections.Add(collectionKey, collection);
            return collection;
        }
        private Informations CreateCollection(string collectionKey, Type componentType, IComponent component)
        {
            var lifeTime = _informationLifeTimeProvider.GetLifeTime(componentType.FullName, component);
            var collection = new Informations(lifeTime);

            _collections.Add(collectionKey, collection);
            return collection;
        }
    }
}
