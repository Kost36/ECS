﻿using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSCore.BaseObjects
{
    /// <summary>
    /// Базовый класс компонента.
    /// Все компоненты наследовать от данного класса
    /// </summary>
    public abstract class Component : IComponent
    {
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public int Id { get; set; }
    }
}