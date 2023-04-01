using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;

namespace GameLib.WorkFlow.WorkFlowProduction
{
    //public static class ProductionInfos
    //{
    //    public static readonly IEnumerable<ProductionInfo> Collection = new List<ProductionInfo>()
    //    {
    //        { new ProductionInfo<Iron>() {
    //            CycleTimeInSec = 60,
    //            ProductionCount = 10,
    //            Raws = new List<Raw>() {
    //                new Raw<Enargy>() { Count = 100 },
    //                new Raw<Ore>() { Count = 100 } } } },
    //        { new ProductionInfo<Silicon>() {
    //            CycleTimeInSec = 120,
    //            ProductionCount = 10,
    //            Raws = new List<Raw>() {
    //                new Raw<Enargy>() { Count = 200 },
    //                new Raw<Sand>() { Count = 200 } } } },
    //    };

    //    public static ProductionInfo GetProductionInfo(Type productType)
    //    {
    //        foreach(var item in Collection)
    //        {
    //            if (item.ProductType == productType)
    //            {
    //                return item;
    //            }
    //        }
    //        throw new NotImplementedException($"ProductionInfo has not in collection for type [{productType.FullName}]");
    //    }
    //}

    //public abstract class Production : ComponentBase
    //{
    //    public Type ProductType;
    //    public float PercentCycle;
    //}
    //public sealed class Production<TProduct> : Production
    //    where TProduct : Product
    //{
    //    public Production()
    //    {
    //        ProductType = typeof(TProduct);
    //    }
    //}

    //public abstract class Raw
    //{
    //    public Type Type;
    //    public int Count;
    //}
    //public sealed class Raw<Product> : Raw
    //{
    //    public Raw()
    //    {
    //        Type = typeof(Product);
    //    }
    //}

    //public abstract class ProductionInfo
    //{
    //    public Type ProductType;
    //    public int CycleTimeInSec;
    //    public int ProductionCount;
    //    public IEnumerable<Raw> Raws;
    //}
    //public sealed class ProductionInfo<TProduct> : ProductionInfo 
    //    where TProduct : Product
    //{
    //    public ProductionInfo()
    //    {
    //        ProductType = typeof(TProduct);
    //    }
    //}

    //public abstract class Product : ComponentBase
    //{
    //    public int Count;
    //}
    
    //Base
    //public sealed class Enargy : Product { }
    //public sealed class Water : Product { }

    ////Lvl 0 (Raw)
    //public sealed class Ore : Product { }
    //public sealed class Sand : Product { }
    //public sealed class Carbon : Product { }
    //public sealed class Hydrogen : Product { }
    //public sealed class Nitrogen : Product { }
    //public sealed class Methane : Product { }
    //public sealed class Helium : Product { }

    ////Lvl 1
    //public sealed class Iron : Product { }
    //public sealed class Copper : Product { }
    //public sealed class Aluminium : Product { }
    //public sealed class Silicon : Product { }
    //public sealed class Sulfur : Product { }
    //public sealed class Ammonia : Product { }
    //public sealed class Elastic : Product { }

    ////Lvl 2
    //public sealed class Metal : Product { }
    //public sealed class Rubber : Product { }
    //public sealed class Plastic : Product { }
    //public sealed class Wiring : Product { }
    //public sealed class SiliconWafer : Product { }
    //public sealed class Fertilizer : Product { }
    //public sealed class Glass : Product { }

    ////Lvl 3
    //public sealed class Electronucs : Product { }
    //public sealed class ControlSystems : Product { }
    //public sealed class Body : Product { }
    //public sealed class Sheathing : Product { }

    ////Lvl 4
    //public sealed class Turret : Product { } //abstract
    //public sealed class Bullet : Product { } //abstract
    //public sealed class Rocket : Product { } //abstract

    ////Lvl 5 
    //public sealed class Ship : Product { } //abstract
    //public sealed class StantionModule : Product { } //abstract

    ////Lvl 6 (Food)
    //public sealed class Grain : Product { }
    //public sealed class Vegetables : Product { }
    //public sealed class Fruit : Product { }
    //public sealed class CompoundFeed : Product { }
    //public sealed class Fish : Product { }
    //public sealed class Meat : Product { }
}
