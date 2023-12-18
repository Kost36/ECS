using ECSCore.BaseObjects;
using GameLib.Products;
using System.Collections.Generic;

namespace GameLib.WorkFlow.Trade
{
    /// <summary>
    /// Цена товара
    /// </summary>
    public class ProductPrice
    {
        /// <summary>
        /// Тип товара
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Продается
        /// </summary>
        public bool Sell;
        /// <summary>
        /// Покупается
        /// </summary>
        public bool Buy;
        /// <summary>
        /// Минимальная цена товара
        /// </summary>
        public int PriceMin;
        /// <summary>
        /// Максимальная цена товара
        /// </summary>
        public int PriceMax;
        /// <summary>
        /// Фактическое количество товара на складе
        /// </summary>
        public int Quantity;
        /// <summary>
        /// Минимальный запас товара
        /// Продает товар до этого значения на складе
        /// </summary>
        public int QuantityMin;
        /// <summary>
        /// Максимальный запас товара
        /// Закупает товар до этого значения на складе
        /// </summary>
        public int QuantityMax;

        //Todo Возможно, нужно будет добавить дополнительный функционал торговли по фиксированным ценам (bool, sellMin, sellMax, buyMin, buyMax)
    }

    /// <summary>
    /// Компонент торговли
    /// </summary>
    public class Trading : ComponentBase
    {
        /// <summary>
        /// Коллекция цен на товары
        /// </summary>
        public Dictionary<ProductType, ProductPrice> ProductPrices = new Dictionary<ProductType, ProductPrice>();
    }

    public class SystemSendPrices
    {
        //Периодически отправляет цены на рынки (сектор \ система \ глобальный)
    }

    public class SystemAiSearchTransaction
    {
        //При добавлении помпонента => анализирует существующие товары и ищет выгодную сделку исходя из настроенных параметров Ai торговца и уровня торговца
        //Низкие уровни не могут торговать сложными товарами (оружием)
        // Низкие уровни анализируют ограниченное количество сделок??? Имеют меньше попыток поиска наилучшей сделки

        // Продать товар из своего склада => (продает товар на любую станцию)
        // Перемещать товар между своими станциями => (грузит со своей станции - переместить на свою другую станцию)
        // Продать товар со своей станции => (грузит со станции - продает где то)
        // Поставлять товар на свою станцию => (покупает гдето - выгружает на станцию)
        // Торговать => (купить на любой станции - продать на любой станции)
    }


}
