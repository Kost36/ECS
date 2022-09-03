using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components.WorkFlow;

namespace GameLib.WorkFlow.ReserchV2
{
    public class Product : ComponentBase
    {
        public int Count;
        public float Volume;
    }


    public abstract class RawMaterial : ComponentBase
    {
        public int RawCount;
    }
    public class RawMaterial<Prod> : RawMaterial
        where Prod : Product
    {
    }


    public abstract class ProductionModul : ComponentBase
    {
        public bool Enable;

        public int ProductionCount;
        public int ProdVolume;
        public int ProductionСycle;

        public float Percent;
        public float Performance;
    }

    public abstract class ProductionModul<Prod, Raw1> : ProductionModul
        where Prod : Product
        where Raw1 : RawMaterial
    {
        public int Raw1Count;
    }
    public abstract class ProductionModul<Prod, Raw1, Raw2> : ProductionModul<Prod, Raw1>
        where Prod : Product
        where Raw1 : RawMaterial
        where Raw2 : RawMaterial
    {
        public int Raw2Count;
    }
    public abstract class ProductionModul<Prod, Raw1, Raw2, Raw3> : ProductionModul<Prod, Raw1, Raw2>
        where Prod : Product
        where Raw1 : RawMaterial
        where Raw2 : RawMaterial
        where Raw3 : RawMaterial
    {
        public int Raw3Count;
    }
    public abstract class ProductionModul<Prod, Raw1, Raw2, Raw3, Raw4> : ProductionModul<Prod, Raw1, Raw2, Raw3>
        where Prod : Product
        where Raw1 : RawMaterial
        where Raw2 : RawMaterial
        where Raw3 : RawMaterial
        where Raw4 : RawMaterial
    {
        public int Raw4Count;
    }
    public abstract class ProductionModul<Prod, Raw1, Raw2, Raw3, Raw4, Raw5> : ProductionModul<Prod, Raw1, Raw2, Raw3, Raw4>
        where Prod : Product
        where Raw1 : RawMaterial
        where Raw2 : RawMaterial
        where Raw3 : RawMaterial
        where Raw4 : RawMaterial
        where Raw5 : RawMaterial
    {
        public int Raw5Count;
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    public abstract class ProductionSystem<ProdModul, Prod, Raw1> : SystemExistComponents<ProdModul, Raw1>, ISystemAction
        where ProdModul : ProductionModul<Prod, RawMaterial>
        where Prod : Product, new()
        where Raw1 : Product
    {
        public override void Action(int entityId, ProdModul prodModul, Raw1 raw1, float _)
        {
            if (raw1.Count < prodModul.Raw1Count)
            {
                return;
            }
            if (prodModul.Percent < 100)
            {
                //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
                prodModul.Performance = 1 / (prodModul.ProductionСycle * 0.01f);
                prodModul.Percent += prodModul.Performance;
            }
            if (prodModul.Percent >= 100)
            {
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if (warehouse.VolumeMax - warehouse.Volume > prodModul.ProdVolume * prodModul.ProductionCount)
                    {
                        if (IECS.GetComponent(entityId, out Prod product))
                        {
                            product.Count += prodModul.ProductionCount;
                        }
                        else
                        {
                            IECS.AddComponent(new Prod() { Id = entityId, Count = prodModul.ProductionCount });
                        }
                        raw1.Count -= prodModul.Raw1Count;
                        prodModul.Percent -= 100;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    public abstract class ProductionSystem<ProdModul, Prod, Raw1, Raw2> : SystemExistComponents<ProdModul, Raw1, Raw2>, ISystemAction
        where ProdModul : ProductionModul<Prod, RawMaterial, RawMaterial>
        where Prod : Product, new() 
        where Raw1 : Product
        where Raw2 : Product
    {
        public override void Action(int entityId, ProdModul prodModul, Raw1 raw1, Raw2 raw2, float _)
        {
            if (raw1.Count < prodModul.Raw1Count)
            {
                return;
            }
            if (raw2.Count < prodModul.Raw2Count)
            {
                return;
            }
            if (prodModul.Percent < 100)
            {
                //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
                prodModul.Performance = 1 / (prodModul.ProductionСycle * 0.01f);
                prodModul.Percent += prodModul.Performance;
            }
            if (prodModul.Percent >= 100)
            {
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if (warehouse.VolumeMax - warehouse.Volume > prodModul.ProdVolume * prodModul.ProductionCount)
                    {
                        if (IECS.GetComponent(entityId, out Prod product))
                        {
                            product.Count += prodModul.ProductionCount;
                        }
                        else
                        {
                            IECS.AddComponent(new Prod() { Id = entityId, Count = prodModul.ProductionCount });
                        }
                        raw1.Count -= prodModul.Raw1Count;
                        raw2.Count -= prodModul.Raw2Count;
                        prodModul.Percent -= 100;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    public abstract class ProductionSystem<ProdModul, Prod, Raw1, Raw2, Raw3> : SystemExistComponents<ProdModul, Raw1, Raw2, Raw3>, ISystemAction
        where ProdModul : ProductionModul<Prod, RawMaterial, RawMaterial, RawMaterial>
        where Prod : Product, new()
        where Raw1 : Product
        where Raw2 : Product
        where Raw3 : Product
    {
        public override void Action(int entityId, ProdModul prodModul, Raw1 raw1, Raw2 raw2, Raw3 raw3, float _)
        {
            if (raw1.Count < prodModul.Raw1Count)
            {
                return;
            }
            if (raw2.Count < prodModul.Raw2Count)
            {
                return;
            }
            if (raw3.Count < prodModul.Raw3Count)
            {
                return;
            }
            if (prodModul.Percent < 100)
            {
                //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
                prodModul.Performance = 1 / (prodModul.ProductionСycle * 0.01f);
                prodModul.Percent += prodModul.Performance;
            }
            if (prodModul.Percent >= 100)
            {
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if (warehouse.VolumeMax - warehouse.Volume > prodModul.ProdVolume * prodModul.ProductionCount)
                    {
                        if (IECS.GetComponent(entityId, out Prod product))
                        {
                            product.Count += prodModul.ProductionCount;
                        }
                        else
                        {
                            IECS.AddComponent(new Prod() { Id = entityId, Count = prodModul.ProductionCount });
                        }
                        raw1.Count -= prodModul.Raw1Count;
                        raw2.Count -= prodModul.Raw2Count;
                        raw3.Count -= prodModul.Raw3Count;
                        prodModul.Percent -= 100;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    public abstract class ProductionSystem<ProdModul, Prod, Raw1, Raw2, Raw3, Raw4> : SystemExistComponents<ProdModul, Raw1, Raw2, Raw3, Raw4>, ISystemAction
        where ProdModul : ProductionModul<Prod, RawMaterial<Product>, RawMaterial, RawMaterial, RawMaterial>
        where Prod : Product, new()
        where Raw1 : Product
        where Raw2 : Product
        where Raw3 : Product
        where Raw4 : Product
    {
        public override void Action(int entityId, ProdModul prodModul, Raw1 raw1, Raw2 raw2, Raw3 raw3, Raw4 raw4, float _)
        {
            if (raw1.Count < prodModul.Raw1Count)
            {
                return;
            }
            if (raw2.Count < prodModul.Raw2Count)
            {
                return;
            }
            if (raw3.Count < prodModul.Raw3Count)
            {
                return;
            }
            if (raw4.Count < prodModul.Raw4Count)
            {
                return;
            }
            if (prodModul.Percent < 100)
            {
                //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
                prodModul.Performance = 1 / (prodModul.ProductionСycle * 0.01f);
                prodModul.Percent += prodModul.Performance;
            }
            if (prodModul.Percent >= 100)
            {
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if (warehouse.VolumeMax - warehouse.Volume > prodModul.ProdVolume * prodModul.ProductionCount)
                    {
                        if (IECS.GetComponent(entityId, out Prod product))
                        {
                            product.Count += prodModul.ProductionCount;
                        }
                        else
                        {
                            IECS.AddComponent(new Prod() { Id = entityId, Count = prodModul.ProductionCount });
                        }
                        raw1.Count -= prodModul.Raw1Count;
                        raw2.Count -= prodModul.Raw2Count;
                        raw3.Count -= prodModul.Raw3Count;
                        raw4.Count -= prodModul.Raw4Count;
                        prodModul.Percent -= 100;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    public abstract class ProductionSystem<ProdModul, Prod, Raw1, Raw2, Raw3, Raw4, Raw5> : SystemExistComponents<ProdModul, Raw1, Raw2, Raw3, Raw4, Raw5>, ISystemAction
        where ProdModul : ProductionModul<Prod, RawMaterial, RawMaterial, RawMaterial, RawMaterial, RawMaterial>
        where Prod : Product, new()
        where Raw1 : Product
        where Raw2 : Product
        where Raw3 : Product
        where Raw4 : Product
        where Raw5 : Product
    {
        public override void Action(int entityId, ProdModul prodModul, Raw1 raw1, Raw2 raw2, Raw3 raw3, Raw4 raw4, Raw5 raw5, float _)
        {
            if (raw1.Count < prodModul.Raw1Count)
            {
                return;
            }
            if (raw2.Count < prodModul.Raw2Count)
            {
                return;
            }
            if (raw3.Count < prodModul.Raw3Count)
            {
                return;
            }
            if (raw4.Count < prodModul.Raw4Count)
            {
                return;
            }
            if (raw5.Count < prodModul.Raw5Count)
            {
                return;
            }
            if (prodModul.Percent < 100)
            {
                //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
                prodModul.Performance = 1 / (prodModul.ProductionСycle * 0.01f);
                prodModul.Percent += prodModul.Performance;
            }
            if (prodModul.Percent >= 100)
            {
                if (IECS.GetComponent(entityId, out Warehouse warehouse))
                {
                    if (warehouse.VolumeMax - warehouse.Volume > prodModul.ProdVolume * prodModul.ProductionCount)
                    {
                        if (IECS.GetComponent(entityId, out Prod product))
                        {
                            product.Count += prodModul.ProductionCount;
                        }
                        else
                        {
                            IECS.AddComponent(new Prod() { Id = entityId, Count = prodModul.ProductionCount });
                        }
                        raw1.Count -= prodModul.Raw1Count;
                        raw2.Count -= prodModul.Raw2Count;
                        raw3.Count -= prodModul.Raw3Count;
                        raw4.Count -= prodModul.Raw4Count;
                        raw5.Count -= prodModul.Raw5Count;
                        prodModul.Percent -= 100;
                    }
                    else
                    {
                        if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
                        {
                            IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
                        }
                    }
                }
            }
        }
    }





    //Реализация
    public class Enargy : Product { }
    public class Ore : Product { }
    public class Metal : Product { }
    public class ProductionModulMetalS : ProductionModul<Metal, RawMaterial<Enargy>, RawMaterial<Ore>> 
    {
        public ProductionModulMetalS() { Enable = true; ProductionCount = 50; ProductionСycle = 120; Raw1Count = 100; Raw2Count = 100; }
    }

    //public class ProductionSystemMetalS : ProductionSystem<ProductionModulMetalS, Metal, RawMaterial<Enargy>, RawMaterial<Ore>> { }
}