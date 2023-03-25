using ECSCore.BaseObjects;
using GameLib.Mechanics.Production.Datas;
using System.Collections.Generic;
using GameLib.Products;
using GameLib.Components;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Информация о производстве продукта
    /// </summary>
    public abstract class ProductionInfo : ComponentBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="cycleTimeInSec"> Время производственного цикла </param>
        public ProductionInfo(int cycleTimeInSec)
        {
            CycleTimeInSec = cycleTimeInSec;
        }

        /// <summary>
        /// Время производственного цикла
        /// </summary>
        public int CycleTimeInSec;
        /// <summary>
        /// Информация о продукте
        /// </summary>
        public ProductInfo Product;
        /// <summary>
        /// Список сырьевых продуктов
        /// </summary>
        public IEnumerable<ProductInfo> Raws;
    }

    public sealed class ProductionInfo<TProduct> : ProductionInfo
        where TProduct : Product
    {
        public ProductionInfo(
            ProductType productType,
            int cycleTimeInSec,
            int productCountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>();
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
    {
        public ProductionInfo(
            ProductType productType,
            int cycleTimeInSec,
            int productCountInCycle,
            ProductType raw1Type,
            int raw1CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle)
            };
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1, TRaw2> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
        where TRaw2 : Product
    {
        public ProductionInfo(
            ProductType productType,
            int cycleTimeInSec,
            int productCountInCycle,
            ProductType raw1Type,
            int raw1CountInCycle,
            ProductType raw2Type,
            int raw2CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle)
            };
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1, TRaw2, TRaw3> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
        where TRaw2 : Product
        where TRaw3 : Product
    {
        public ProductionInfo(
            ProductType productType,
            int cycleTimeInSec,
            int productCountInCycle,
            ProductType raw1Type,
            int raw1CountInCycle,
            ProductType raw2Type,
            int raw2CountInCycle,
            ProductType raw3Type,
            int raw3CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle),
                new ProductInfo<TRaw3>(productType: raw3Type, countInCycle: raw3CountInCycle)
            };
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1, TRaw2, TRaw3, TRaw4> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
        where TRaw2 : Product
        where TRaw3 : Product
        where TRaw4 : Product
    {
        public ProductionInfo(
            ProductType productType,
            int cycleTimeInSec,
            int productCountInCycle,
            ProductType raw1Type,
            int raw1CountInCycle,
            ProductType raw2Type,
            int raw2CountInCycle,
            ProductType raw3Type,
            int raw3CountInCycle,
            ProductType raw4Type,
            int raw4CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle),
                new ProductInfo<TRaw3>(productType: raw3Type, countInCycle: raw3CountInCycle),
                new ProductInfo<TRaw4>(productType: raw4Type, countInCycle: raw4CountInCycle)
            };
        }
    }
}
