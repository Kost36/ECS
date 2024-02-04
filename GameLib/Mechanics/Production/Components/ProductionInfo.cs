using System.Collections.Generic;
using ECSCore.BaseObjects;
using GameLib.Mechanics.Production.Datas;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Информация о производстве продукта
    /// </summary>
    public sealed class ProductionInfo : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cycleTimeInSec">Время производственного цикла</param>
        /// <param name="productionCountInfo">Информация по производимому товару</param>
        /// <param name="rawCountInfos">Информация по потребляемому сырью</param>
        public ProductionInfo(int cycleTimeInSec, ProductionCountInfo productionCountInfo, IEnumerable<ProductionCountInfo> rawCountInfos)
        {
            CycleTimeInSec = cycleTimeInSec;
            Product = productionCountInfo;
            Raws = rawCountInfos;
        }

        /// <summary>
        /// Время производственного цикла
        /// </summary>
        public int CycleTimeInSec { get; set; }
        /// <summary>
        /// Информация о продукте
        /// </summary>
        public ProductionCountInfo Product { get; set; }
        /// <summary>
        /// Список сырьевых продуктов
        /// </summary>
        public IEnumerable<ProductionCountInfo> Raws { get; set; }
    }

    //public sealed class ProductionInfo : ProductionInfo
    //    where TProduct : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>();
    //    }
    //}
    //public sealed class ProductionInfo<TProduct, TRaw1> : ProductionInfo
    //    where TProduct : Product
    //    where TRaw1 : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle,
    //        ProductType raw1Type,
    //        int raw1CountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>()
    //        {
    //            new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle)
    //        };
    //    }
    //}
    //public sealed class ProductionInfo<TProduct, TRaw1, TRaw2> : ProductionInfo
    //    where TProduct : Product
    //    where TRaw1 : Product
    //    where TRaw2 : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle,
    //        ProductType raw1Type,
    //        int raw1CountInCycle,
    //        ProductType raw2Type,
    //        int raw2CountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>()
    //        {
    //            new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
    //            new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle)
    //        };
    //    }
    //}
    //public sealed class ProductionInfo<TProduct, TRaw1, TRaw2, TRaw3> : ProductionInfo
    //    where TProduct : Product
    //    where TRaw1 : Product
    //    where TRaw2 : Product
    //    where TRaw3 : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle,
    //        ProductType raw1Type,
    //        int raw1CountInCycle,
    //        ProductType raw2Type,
    //        int raw2CountInCycle,
    //        ProductType raw3Type,
    //        int raw3CountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>()
    //        {
    //            new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
    //            new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle),
    //            new ProductInfo<TRaw3>(productType: raw3Type, countInCycle: raw3CountInCycle)
    //        };
    //    }
    //}
    //public sealed class ProductionInfo<TProduct, TRaw1, TRaw2, TRaw3, TRaw4> : ProductionInfo
    //    where TProduct : Product
    //    where TRaw1 : Product
    //    where TRaw2 : Product
    //    where TRaw3 : Product
    //    where TRaw4 : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle,
    //        ProductType raw1Type,
    //        int raw1CountInCycle,
    //        ProductType raw2Type,
    //        int raw2CountInCycle,
    //        ProductType raw3Type,
    //        int raw3CountInCycle,
    //        ProductType raw4Type,
    //        int raw4CountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>()
    //        {
    //            new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
    //            new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle),
    //            new ProductInfo<TRaw3>(productType: raw3Type, countInCycle: raw3CountInCycle),
    //            new ProductInfo<TRaw4>(productType: raw4Type, countInCycle: raw4CountInCycle)
    //        };
    //    }
    //}
    //public sealed class ProductionInfo<TProduct, TRaw1, TRaw2, TRaw3, TRaw4, TRaw5> : ProductionInfo
    //    where TProduct : Product
    //    where TRaw1 : Product
    //    where TRaw2 : Product
    //    where TRaw3 : Product
    //    where TRaw4 : Product
    //    where TRaw5 : Product
    //{
    //    public ProductionInfo(
    //        ProductType productType,
    //        int cycleTimeInSec,
    //        int productCountInCycle,
    //        ProductType raw1Type,
    //        int raw1CountInCycle,
    //        ProductType raw2Type,
    //        int raw2CountInCycle,
    //        ProductType raw3Type,
    //        int raw3CountInCycle,
    //        ProductType raw4Type,
    //        int raw4CountInCycle,
    //        ProductType raw5Type,
    //        int raw5CountInCycle) : base(cycleTimeInSec)
    //    {
    //        Product = new ProductInfo<TProduct>(productType: productType, countInCycle: productCountInCycle);
    //        Raws = new List<ProductInfo>()
    //        {
    //            new ProductInfo<TRaw1>(productType: raw1Type, countInCycle: raw1CountInCycle),
    //            new ProductInfo<TRaw2>(productType: raw2Type, countInCycle: raw2CountInCycle),
    //            new ProductInfo<TRaw3>(productType: raw3Type, countInCycle: raw3CountInCycle),
    //            new ProductInfo<TRaw4>(productType: raw4Type, countInCycle: raw4CountInCycle),
    //            new ProductInfo<TRaw5>(productType: raw5Type, countInCycle: raw5CountInCycle)
    //        };
    //    }
    //}
}