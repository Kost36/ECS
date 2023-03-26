﻿using GameLib.Attributes;
using GameLib.Components;

namespace GameLib.Products.Lvl0
{
    /// <summary>
    /// Аккумулятор / батарея с энергией.
    /// 3 кВт
    /// </summary>
    [ProductType(ProductType.Battery)]
    public sealed class Battery : Product { }
}