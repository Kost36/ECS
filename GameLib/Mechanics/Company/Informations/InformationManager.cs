using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Менежер информации
    /// </summary>
    public class InformationManager<Type>
        where Type : Information
    {
        /// <summary>
        /// Коллекция известных объектов
        /// </summary>
        private Dictionary<Guid, Type> _collection = new Dictionary<Guid, Type>();

        /// <summary>
        /// Добавить информацию об объекте
        /// </summary>
        /// <param name="information"></param>
        public void Add(Type information)
        {
            information.ExpirationTime = DateTimeOffset.UtcNow.Add(
                TypeInformationToTimeSpanConverter.ToTimeSpan(information.TypeInformation));

            _collection.Remove(information.EntityId);
            _collection.Add(information.EntityId, information);
        }

        /// <summary>
        /// Удалить информацию об объекте
        /// </summary>
        /// <param name="information"></param>
        public void ControlExpiration()
        {
            var dateTimeNow = DateTimeOffset.UtcNow;
            var keysForRemove = new List<Guid>();
            foreach (var item in _collection)
            {
                if (dateTimeNow > item.Value.ExpirationTime)
                {
                    keysForRemove.Add(item.Key);
                }
            }

            foreach (var key in keysForRemove)
            {
                _collection.Remove(key);
            }
        }

        /// <summary>
        /// Получить информацию
        /// </summary>
        public List<Type> GetInformations()
        {
            return _collection.Values.ToList();
        }
    }
}