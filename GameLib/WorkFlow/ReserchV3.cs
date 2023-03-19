using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.WorkFlow.NewProduct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.WorkFlow
{
    public static class ProductTypeMatcher
    {
        //TODO Генерировать подготовку коллекции при запуске через рефлексию.
        //Уйти от ручного заполнения коллекции при добавлении нового вида продукта

        public static readonly Dictionary<ProductType, Type> Collection = new Dictionary<ProductType, Type>()
        {
            { ProductType.Enargy, typeof(Enargy) },
            { ProductType.Ore, typeof(Ore) },
            { ProductType.Iron, typeof(Iron) }
        };

        public static Type ToType(ProductType productType)
        {
            return Collection[productType];
        }
    }

    public static class ProductionInfos
    {
        private static readonly Dictionary<string, ProductionInfo> Collection = new Dictionary<string, ProductionInfo>()
        {
            { typeof(Enargy).FullName, new ProductionInfo<Enargy>(productType: ProductType.Enargy, cycleTimeInSec: 60, productCountInCycle: 300)},
            { typeof(Iron).FullName, new ProductionInfo<Iron, Enargy, Ore>(productType: ProductType.Iron, cycleTimeInSec: 90, productCountInCycle: 10, raw1Type: ProductType.Enargy, raw1CountInCycle: 100, raw2Type: ProductType.Ore, raw2CountInCycle: 20)},
        };

        public static bool GetProductionInfo(Type typeProduct, out ProductionInfo productionInfo)
        {
            return Collection.TryGetValue(typeProduct.FullName, out productionInfo);
        }
    }

    public abstract class ProductionInfo : ComponentBase
    {
        public ProductionInfo(int cycleTimeInSec)
        {
            CycleTimeInSec = cycleTimeInSec;
        }

        public int CycleTimeInSec;
        public ProductInfo Product;
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

    public abstract class ProductInfo
    {
        public Type TypeProduct;
        public ProductType ProductType;
        public int CountInCycle;
    }
    public sealed class ProductInfo<TProduct> : ProductInfo
    {
        public ProductInfo(ProductType productType, int countInCycle)
        {
            TypeProduct = typeof(TProduct);
            ProductType = productType;
            CountInCycle = countInCycle;
        }
    }

    public abstract class Production : ComponentBase
    {
        public Type ProductType;
        public float PercentCycle;
    }
    public sealed class Production<TProduct> : Production
        where TProduct : Product
    {
        public Production()
        {
            ProductType = typeof(TProduct);
        }
    }

    #region Entitys
    public class ModuleProduction : Entity { }
    #endregion

    #region Components
    public abstract class ProductionModuleBuild : ComponentBase { }
    public sealed class ProductionModuleBuild<TProduct> : ProductionModuleBuild
        where TProduct : Product { }

    public class ProductionModule : ComponentBase
    {
        public bool Enable;
        public bool Work;

        public int CountProductOfCycle;
        public ProductType ProductType;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;

        public Dictionary<ProductType, Expense> RawExpenses = new Dictionary<ProductType, Expense>();
    }
    public class WarehouseProductionModul : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public KeyValuePair<ProductType, Count> Product = new KeyValuePair<ProductType, Count>();
        public Dictionary<ProductType, Count> Raws = new Dictionary<ProductType, Count>();
    }

    public class Expense
    {
        /// <summary>
        /// Расход сырья за производственный цикл
        /// </summary>
        public int Value;
    }
    public class Count
    {
        public int Value;
        public int MaxValue;
    }

    public class BridgeProductionModulToStantion : ComponentBase
    {

    }
    #endregion

    #region Systems
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionBuilderSystem : SystemExistComponents<Production>, ISystemActionAdd
    {
        public override void ActionAdd(Production production, Entity entity)
        {
            var componentType = production.GetType();
            if (componentType.IsGenericType)
            {
                var productType = componentType.GetGenericArguments().First();
                if (ProductionInfos.GetProductionInfo(productType, out var productionInfo)) 
                {
                    var productionModuleComponent = new ProductionModule() {
                        Enable = true,
                        TimeCycleInSec = productionInfo.CycleTimeInSec,
                        CountProductOfCycle = productionInfo.Product.CountInCycle };
                    var warehouseComponent = new WarehouseProductionModul() {
                        PercentFillingRaws = 80,
                        VolumeMax = 1000 };

                    productionModuleComponent.ProductType = productionInfo.Product.ProductType;
                    productionModuleComponent.TimeCycleInSec = productionInfo.CycleTimeInSec;
                    productionModuleComponent.CountProductOfCycle = productionInfo.Product.CountInCycle;

                    warehouseComponent.Product = new KeyValuePair<ProductType, Count>(
                        productionInfo.Product.ProductType,
                        new Count() {
                            Value = 0 });

                    foreach (var rawInfo in productionInfo.Raws)
                    {
                        productionModuleComponent.RawExpenses.Add(
                            rawInfo.ProductType,
                            new Expense() {
                                Value = rawInfo.CountInCycle }); 
                        warehouseComponent.Raws.Add(
                            rawInfo.ProductType,
                            new Count() {
                                Value = 0,
                                MaxValue = rawInfo.CountInCycle + rawInfo.CountInCycle });
                    }

                    entity.Add(productionModuleComponent);
                    entity.Add(warehouseComponent);
                    entity.Add(productionInfo);
                    entity.Add(new BridgeProductionModulToStantion());
                    
                    entity.Remove<ProductionModuleBuild>();
                };
            }
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Min1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class BridgeProductionModulToStantionSystem : SystemExistComponents<BridgeProductionModulToStantion, WarehouseProductionModul>, ISystemActionAdd, ISystemAction
    {
        //TODO При добавлении моста есть смысл включить закупку сырья? Или этим должны заниматься AI станции \ Игрок? 

        public override void ActionAdd(BridgeProductionModulToStantion bridgeProductionModulToStantion, WarehouseProductionModul warehouseProductionModul, Entity entity)
        {
            MoveProduct(entity, warehouseProductionModul);
        }

        public override void Action(int entityId, BridgeProductionModulToStantion bridgeProductionModulToStantion, WarehouseProductionModul warehouseProductionModul, float deltaTime)
        {
            //Должна быть отдельная система для изменения процента заполнения склада Raw материалами (при добавлении компонента должно присвоиться значение процента и рассчитаться значения по каждому компоненту)
            //Вызывается при измнении настроек и при добавлении (инициализации)

            //Перемещение товара между производственным модулем и станцией
            if (IECS.GetEntity(entityId, out var entity))
            {
                MoveProduct(entity, warehouseProductionModul);
            }
        }

        private void MoveProduct(Entity entity, WarehouseProductionModul warehouse)
        {
            if (entity.ParentEntity != null)
            {
                if (warehouse.Product.Value.Value > 0)
                {
                    if (entity.ParentEntity.Get(ProductTypeMatcher.ToType(warehouse.Product.Key), out var component))
                    {
                        var product = (Product)component;
                        if (true) //TODO Если на станции достаточно свободного места
                        {
                            product.Count += warehouse.Product.Value.Value;
                            warehouse.Product.Value.Value = 0;
                        }
                        //TODO Если места не хватает, то переместить то кол-во товара, под которое есть место
                    }
                    else
                    {
                        //var product = (ComponentBase)Activator.CreateInstance(ProductTypeMatcher.ToType(warehouseProductionModul.Product.Key));
                        //TODO Переместить продукт в компонент станции.
                        //entity.ParentEntity.Add(product); //TODO Исключение
                    }
                }

                foreach (var raw in warehouse.Raws)
                {
                    if (entity.ParentEntity.Get(ProductTypeMatcher.ToType(raw.Key), out var component1))
                    {
                        var product = (Product)component1;
                        var needValue = raw.Value.MaxValue - raw.Value.Value;
                        if (product.Count > needValue)
                        {
                            raw.Value.Value += needValue;
                            product.Count -= needValue;
                        } //Если на станции хватает сырья для передачи производственному модулю
                        else if (product.Count > 0)
                        {
                            raw.Value.Value += product.Count;
                            product.Count = 0;
                        } //Если на станции не хватает сырья для передачи производственному модулю
                    }
                } //Перемещаем на склад производственного модуля сырье со станции
            } //Перемещение товара между производственным модулем и станцией
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem : SystemExistComponents<ProductionModule, WarehouseProductionModul>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule productionModule, WarehouseProductionModul warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                //Контроль производства
                productionModule.Work = true;
                foreach (var raw in warehouseProductionModul.Raws)
                {
                    if (productionModule.RawExpenses[raw.Key].Value > raw.Value.Value)
                    {
                        productionModule.Work = false;
                        return;
                    }
                }

                if (productionModule.Work)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;

                        warehouseProductionModul.Product.Value.Value += productionModule.CountProductOfCycle;

                        foreach (var rawExpense in productionModule.RawExpenses)
                        {
                            warehouseProductionModul.Raws[rawExpense.Key].Value -= rawExpense.Value.Value;
                        }
                    } //Если производственный цикл завершен
                } //Если модуль работает
            }
        }
    }
    #endregion
}