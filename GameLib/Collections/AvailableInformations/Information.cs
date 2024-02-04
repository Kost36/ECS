﻿using ECSCore.BaseObjects;
using System;

namespace GameLib.Collections.AvailableInformations
{
    /// <summary>
    /// Известная информация
    /// </summary>
    public sealed class Information
    {
        /// <summary>
        /// Сущьность
        /// </summary>
        public Entity Entity;
        /// <summary>
        /// Срок годности информации
        /// </summary>
        public DateTimeOffset ExpirationTime;
    }
}
