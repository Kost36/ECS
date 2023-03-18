using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.WorkFlow.NewProduct
{
    /// <summary>
    /// Продукт
    /// </summary>
    public abstract class Product : ComponentBase
    {
        /// <summary>
        /// Тип
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Количество
        /// </summary>
        public int Count;
        /// <summary>
        /// Средняя цена покупки
        /// </summary>
        public float AverPricePurchase;

        /// <summary>
        /// Минимальный процент заполнения склада продуктом
        /// </summary>
        public float MinPercentFillWarehouse;
        /// <summary>
        /// Максимальный процент заполнения склада продуктом
        /// </summary>
        public float MaxPercentFillWarehouse;
        /// <summary>
        /// Минимальная цена покупки
        /// </summary>
        public float MinPriceBuy;
        /// <summary>
        /// Максимальная цена покупки
        /// </summary>
        public float MaxPriceBuy;
        /// <summary>
        /// Минимальная цена продажи
        /// </summary>
        public float MinPriceSell;
        /// <summary>
        /// Максимальная цена продажи
        /// </summary>
        public float MaxPriceSell;

        /// <summary>
        /// Вес одной еденицы товара.
        /// Тонны.
        /// </summary>
        public float Weight
        {
            get
            {
                if (ProductWeights.Weights.TryGetValue((int)ProductType, out float value))
                {
                    return value;
                };
                throw new KeyNotFoundException();
            }
        }
        /// <summary>
        /// Объем одной еденицы товара.
        /// М.Куб.
        /// </summary>
        public float Volume
        {
            get
            {
                if (ProductVolumes.Volumes.TryGetValue((int)ProductType, out float value))
                {
                    return value;
                };
                throw new KeyNotFoundException();
            }
        }
        /// <summary>
        /// Цикл производства партии товара.
        /// Секунды.
        /// </summary>
        public float Cycle
        {
            get
            {
                if (ProductProductionСycles.Сycles.TryGetValue((int)ProductType, out int value))
                {
                    return value;
                };
                throw new KeyNotFoundException();
            }
        }
    }

    /// <summary>
    /// Интерфейс инициализации типов
    /// </summary>
    public interface IInitializer 
    {
        /// <summary>
        /// Инициализация типа
        /// </summary>
        void Init();
    }

    /// <summary>
    /// Описание продукта
    /// Только для разработки, для упрощения инициализации типов
    /// </summary>
    public abstract class ProductDescribe : IInitializer
    {
        private ProductType _productType;
        private int _productionСycle;
        private float _weight;
        private float _volume;

        /// <summary>
        /// Инициализация продукта из описания
        /// </summary>
        /// <param name="productType"> Тип продукта </param>
        /// <param name="weight"> Вес единицы продукта </param>
        /// <param name="volume"> Объем единицы продукта </param>
        /// <param name="productionСycle"> Цикл производства продукта </param>
        public ProductDescribe(ProductType productType, float weight, float volume, int productionСycle)
        {
            _productType = productType;
            _productionСycle = productionСycle;
            _weight = weight;
            _volume = volume;
        }

        public void Init()
        {
            RegistrationWeights();
            RegistrationVolume();
            RegistrationСycle();
        }

        public void RegistrationWeights()
        {
            ProductWeights.Weights.Add((int)_productType, _weight);
        }
        public void RegistrationVolume()
        {
            ProductVolumes.Volumes.Add((int)_productType, _volume);
        }
        public void RegistrationСycle()
        {
            ProductProductionСycles.Сycles.Add((int)_productType, _productionСycle);
        }
    }

    public sealed class OreDescribe : ProductDescribe
    {
        public OreDescribe() : base(
            productType: ProductType.Ore,
            weight: 1,
            volume: 2,
            productionСycle: 10
            ) { }
    }
    public sealed class IronDescribe : ProductDescribe
    {
        public IronDescribe() : base(
            productType: ProductType.Iron,
            weight: 1,
            volume: 2,
            productionСycle: 10
            )
        { }
    }

    /// <summary>
    /// Энергия
    /// </summary>
    public sealed class Enargy : Product
    {
        public Enargy()
        {
            ProductType = ProductType.Enargy;
        }
    }
    /// <summary>
    /// Руда
    /// </summary>
    public sealed class Ore : Product
    {
        public Ore()
        {
            ProductType = ProductType.Ore;
        }
    }
    /// <summary>
    /// Жедезо
    /// </summary>
    public sealed class Iron : Product
    {
        public Iron()
        {
            ProductType = ProductType.Iron;
        }
    }

    /// <summary>
    /// Веса продуктов
    /// </summary>
    public static class ProductWeights
    {
        /// <summary>
        /// Коллекция весов продуктов
        /// Key - тип продукта исходя из ProdyctType
        /// Value - вес (Тонны) единицы продукта
        /// </summary>
        public static readonly Dictionary<int, float> Weights = new Dictionary<int, float>();
    }
    /// <summary>
    /// Объем продуктов
    /// </summary>
    public static class ProductVolumes
    {
        /// <summary>
        /// Коллекция объемов продуктов
        /// Key - тип продукта исходя из ProdyctType
        /// Value - объем (М.Куб.) единицы продукта
        /// </summary>
        public static readonly Dictionary<int, float> Volumes = new Dictionary<int, float>();
    }
    /// <summary>
    /// Циклы производства продуктов
    /// </summary>
    public static class ProductProductionСycles
    {
        /// <summary>
        /// Коллекция циклов производства продуктов
        /// Key - тип продукта исходя из ProdyctType
        /// Value - время (Сек) производства партии продукта
        /// </summary>
        public static readonly Dictionary<int, int> Сycles = new Dictionary<int, int>();
    }

    /// <summary>
    /// Тип продукта
    /// </summary>
    public enum ProductType 
    {
        /// <summary>
        /// Энергия
        /// </summary>
        Enargy,
        /// <summary>
        /// Руда
        /// </summary>
        Ore,
        /// <summary>
        /// Железо
        /// </summary>
        Iron,
    }

    /// <summary>
    /// Продукты
    /// </summary>
    public class Products : ComponentBase 
    {
        /// <summary>
        /// Коллекция продуктов
        /// Key - тип продукта исходя из ProdyctType
        /// Value - объект с информацией о продукте
        /// </summary>
        public Dictionary<int, Product> Collection = new Dictionary<int, Product>();

        //Дефолтное состояние для:
        //Корабля - торговец
        //Корабля - военный
        //Корабля - строитель
        //Станции - производство
        //Станции - торговля
        //Станции - жилье
    }

    /// <summary>
    /// Инициализатор продуктов
    /// </summary>
    public class Initializer
    {
        /// <summary>
        /// Инициализация типов
        /// </summary>
        public void Init()
        {
            var assembly = GetAssembly.Get();
            var typesForInit = assembly.GetTypes().Where(type => type.IsSealed && type.GetInterface(typeof(IInitializer).Name) != null).ToList();
            foreach (var type in typesForInit)
            {
                var instance = (IInitializer)Activator.CreateInstance(type);
                instance.Init();
            }
        }
    }
}