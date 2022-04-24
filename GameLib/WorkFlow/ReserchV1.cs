using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Components.WorkFlow;

namespace GameLib.WorkFlow.ReserchV1
{
    //public abstract class Product : ComponentBase
    //{
    //    public int Count;
    //    public int ProductionCount;
    //    public int ProductionСycle;
    //    public float Volume;
    //}

    //public class Product<Raw> : Product
    //    where Raw : Product
    //{
    //    public int RawCount;
    //}

    //public class Product<Raw1, Raw2> : Product
    //    where Raw1 : Product
    //    where Raw2 : Product
    //{
    //    public int Raw1Count;
    //    public int Raw2Count;
    //}

    //public class Product<Raw1, Raw2, Raw3> : Product
    //    where Raw1 : Product
    //    where Raw2 : Product
    //    where Raw3 : Product
    //{
    //    public int Raw1Count;
    //    public int Raw2Count;
    //    public int Raw3Count;
    //}

    //public class Product<Raw1, Raw2, Raw3, Raw4> : Product
    //    where Raw1 : Product
    //    where Raw2 : Product
    //    where Raw3 : Product
    //    where Raw4 : Product
    //{
    //    public int Raw1Count;
    //    public int Raw2Count;
    //    public int Raw3Count;
    //    public int Raw4Count;
    //}

    //public class Product<Raw1, Raw2, Raw3, Raw4, Raw5> : Product
    //    where Raw1 : Product
    //    where Raw2 : Product
    //    where Raw3 : Product
    //    where Raw4 : Product
    //    where Raw5 : Product
    //{
    //    public int Raw1Count;
    //    public int Raw2Count;
    //    public int Raw3Count;
    //    public int Raw4Count;
    //    public int Raw5Count;
    //}

    //public abstract class ProductionModul : ComponentBase
    //{
    //    public bool Enable;
    //    public float Percent;
    //    public float Performance;
    //}

    //[AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    //[AttributeSystemPriority(1)]
    //[AttributeSystemEnable]
    //public class ProductionSystem<ProdModul, Prod, Raw> : SystemExistComponents<ProdModul, Prod, Raw>, ISystemAction
    //    where ProdModul : ProductionModul
    //    where Prod : Product<Raw>
    //    where Raw : Product
    //{
    //    public override void Action(int entityId, ProdModul prodModul, Prod product, Raw raw, float _)
    //    {
    //        if (raw.Count < product.RawCount)
    //        {
    //            return;
    //        }
    //        if (prodModul.Percent < 100)
    //        {
    //            //Производительность  = 1сек / (цикл производства(сек)  / 100%)  <= деление на 100, заменено умножением на 0.01
    //            prodModul.Performance = 1    / (product.ProductionСycle * 0.01f); 
    //            prodModul.Percent += prodModul.Performance;
    //        }
    //        if (prodModul.Percent >= 100)
    //        {
    //            if (IECS.GetComponent(entityId, out Warehouse warehouse))
    //            {
    //                if (warehouse.VolumeMax - warehouse.Volume > product.Volume * product.ProductionCount)
    //                {
    //                    product.Count += product.ProductionCount;
    //                    raw.Count -= product.RawCount;
    //                    prodModul.Percent -= 100;
    //                }
    //                else
    //                {
    //                    if (!IECS.GetComponent(entityId, out WarehouseOverFlow _))
    //                    {
    //                        IECS.AddComponent(new WarehouseOverFlow { Id = entityId });
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}