using ECSCore.BaseObjects;
using GameLib.Components;
using System;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Компонент производимого товара на производственном модуле
    /// </summary>
    public abstract class Production : ComponentBase
    {
        /// <summary>
        /// Тип производимого товара
        /// </summary>
        public Type ProductType;
        /// <summary>
        /// Процент выполнения производственного цикла
        /// </summary>
        public float PercentCycle;
    }

    /// <summary>
    /// Компонент производимого товара на производственном модуле
    /// </summary>
    /// <typeparam name="TProduct"> Производимый продукт </typeparam>
    public sealed class Production<TProduct> : Production
        where TProduct : Product
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Production()
        {
            ProductType = typeof(TProduct);
        }
    }
}
