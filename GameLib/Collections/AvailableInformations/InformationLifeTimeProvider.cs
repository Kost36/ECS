﻿using ECSCore.Interfaces;
using ECSCore.Interfaces.Components;
using GameLib.Attributes;
using GameLib.Extensions;
using System;
using System.Collections.Generic;

namespace GameLib.Collections.AvailableInformations
{
    /// <summary>
    /// Поставщик времени жизни информации
    /// </summary>
    public sealed class InformationLifeTimeProvider
    {
        private readonly Dictionary<string, TimeSpan> _lifeTimeCollection = new Dictionary<string, TimeSpan>();

        /// <summary>
        /// Получить время жизни информации о сущности
        /// </summary>
        public TimeSpan GetLifeTime(string key, IEntity entity)
        {
            if (!_lifeTimeCollection.TryGetValue(key, out var lifeTime))
            {
                lifeTime = entity.GetType().GetAttributeValue(
                    (LifeTimeInformationAttribute lifeTimeInformation) => lifeTimeInformation.LifeTime);

                _lifeTimeCollection.Add(key, lifeTime);
            }

            return lifeTime;
        }

        /// <summary>
        /// Получить время жизни информации о сущности с наличием компонента
        /// </summary>
        public TimeSpan GetLifeTime(string key, IComponent component)
        {
            if (!_lifeTimeCollection.TryGetValue(key, out var lifeTime))
            {
                lifeTime = component.GetType().GetAttributeValue(
                    (LifeTimeInformationAttribute lifeTimeInformation) => lifeTimeInformation.LifeTime);

                _lifeTimeCollection.Add(key, lifeTime);
            }

            return lifeTime;
        }
    }
}
