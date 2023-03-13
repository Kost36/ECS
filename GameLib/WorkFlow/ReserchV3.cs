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
    public static class ProductionInfos
    {
        private static readonly Dictionary<string, ProductionInfo> Collection = new Dictionary<string, ProductionInfo>()
        {
            { typeof(Enargy).FullName, new ProductionInfo<Enargy>(cycleTimeInSec: 60, productCountInCycle: 300)},
            { typeof(Iron).FullName, new ProductionInfo<Iron, Enargy, Ore>(cycleTimeInSec: 60, productCountInCycle: 10, raw1CountInCycle: 100, raw2CountInCycle: 20)},
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
            int cycleTimeInSec, 
            int productCountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>();
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
    {
        public ProductionInfo(
            int cycleTimeInSec, 
            int productCountInCycle,
            int raw1CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>() 
            { 
                new ProductInfo<TRaw1>(countInCycle: raw1CountInCycle)
            };
        }
    }
    public sealed class ProductionInfo<TProduct, TRaw1, TRaw2> : ProductionInfo
        where TProduct : Product
        where TRaw1 : Product
        where TRaw2 : Product
    {
        public ProductionInfo(
            int cycleTimeInSec,
            int productCountInCycle, 
            int raw1CountInCycle,
            int raw2CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(countInCycle: raw2CountInCycle)
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
            int cycleTimeInSec, 
            int productCountInCycle, 
            int raw1CountInCycle, 
            int raw2CountInCycle, 
            int raw3CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(countInCycle: productCountInCycle);
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(countInCycle: raw2CountInCycle),
                new ProductInfo<TRaw3>(countInCycle: raw3CountInCycle)
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
            int cycleTimeInSec,
            int productCountInCycle,
            int raw1CountInCycle,
            int raw2CountInCycle,
            int raw3CountInCycle,
            int raw4CountInCycle) : base(cycleTimeInSec)
        {
            Product = new ProductInfo<TProduct>(countInCycle: productCountInCycle); 
            Raws = new List<ProductInfo>()
            {
                new ProductInfo<TRaw1>(countInCycle: raw1CountInCycle),
                new ProductInfo<TRaw2>(countInCycle: raw2CountInCycle),
                new ProductInfo<TRaw3>(countInCycle: raw3CountInCycle),
                new ProductInfo<TRaw4>(countInCycle: raw4CountInCycle)
            };
        }
    }

    public abstract class ProductInfo
    {
        public Type ProductType;
        public int CountInCycle;
    }
    public sealed class ProductInfo<TProduct> : ProductInfo
    {
        public ProductInfo(int countInCycle)
        {
            ProductType = typeof(TProduct);
            CountInCycle = countInCycle;
        }
    }

    #region Entitys
    public class ModuleProduction : Entity { }
    #endregion

    #region Components
    public abstract class ProductionModuleBuild : ComponentBase { }
    public sealed class ProductionModuleBuild<TProduct> : ProductionModuleBuild
        where TProduct : Product { }

    public /*abstract*/ class ProductionModule : ComponentBase
    {
        public bool Enable;

        public int CountProductOfCycle;

        public int TimeCycleInSec;
        public float CycleCompletionPercentage;
    }
    public /*abstract*/ class ProductionModule1 : ProductionModule
    {
        public int CountRaw1OfCycle;
    }
    public /*abstract*/ class ProductionModule2 : ProductionModule
    {
        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;
    }
    public /*abstract*/ class ProductionModule3 : ProductionModule
    {
        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;
        public int CountRaw3OfCycle;
    }
    public /*abstract*/ class ProductionModule4 : ProductionModule
    {
        public int CountRaw1OfCycle;
        public int CountRaw2OfCycle;
        public int CountRaw3OfCycle;
        public int CountRaw4OfCycle;
    }

    public class WarehouseProductionModul : ComponentBase
    {
        public int Volume;
        public int VolumeMax;
        public int PercentFillingRaws;

        public Type ProductType;
        public int CountProduct;
    }
    public class WarehouseProductionModulRaw1 : WarehouseProductionModul
    {
        public Type TypeRaw1;
        public int CountRaw1;
        public int MaxCountRaw1;
    }
    public class WarehouseProductionModulRaw2 : WarehouseProductionModul
    {
        public Type TypeRaw1;
        public int CountRaw1;
        public int MaxCountRaw1;
        public Type TypeRaw2;
        public int CountRaw2;
        public int MaxCountRaw2;
    }
    public class WarehouseProductionModulRaw3 : WarehouseProductionModul
    {
        public Type TypeRaw1;
        public int CountRaw1;
        public int MaxCountRaw1;
        public Type TypeRaw2;
        public int CountRaw2;
        public int MaxCountRaw2;
        public Type TypeRaw3;
        public int CountRaw3;
        public int MaxCountRaw3;
    }
    public class WarehouseProductionModulRaw4 : WarehouseProductionModul
    {
        public Type TypeRaw1;
        public int CountRaw1;
        public int MaxCountRaw1;
        public Type TypeRaw2;
        public int CountRaw2;
        public int MaxCountRaw2;
        public Type TypeRaw3;
        public int CountRaw3;
        public int MaxCountRaw3;
        public Type TypeRaw4;
        public int CountRaw4;
        public int MaxCountRaw4;
    }

    public class BridgeProductionModulToStantion : ComponentBase
    {

    }
    #endregion

    #region Systems
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionBuilderSystem : SystemExistComponents<ProductionModuleBuild>, ISystemActionAdd
    {
        public override void ActionAdd(ProductionModuleBuild productionModuleBuild, Entity entity)
        {
            var componentType = productionModuleBuild.GetType();
            if (componentType.IsGenericType)
            {
                var productType = componentType.GetGenericArguments().First();
                if (ProductionInfos.GetProductionInfo(productType, out var productionInfo)) 
                {
                    switch (productionInfo.Raws.Count())
                    {
                        case 0:
                            AddProductionWithoutRaws(entity, productionInfo);
                            break;
                        case 1:
                            AddProductionWithOneRaws(entity, productionInfo);
                            break;
                        case 2:
                            AddProductionWithTwoRaws(entity, productionInfo);
                            break;
                        case 3:
                            AddProductionWithThreeRaws(entity, productionInfo);
                            break;
                        case 4:
                            AddProductionWithFourRaws(entity, productionInfo);
                            break;
                        default:
                            break;
                    }

                    entity.Add(productionInfo);
                    entity.Add(new BridgeProductionModulToStantion());
                    entity.Remove<ProductionModuleBuild>();
                };
            }
        }

        private void AddProductionWithoutRaws(Entity entity, ProductionInfo productionInfo)
        {
            var productionModuleComponent = new ProductionModule();
            FillProductionModule(productionModuleComponent, productionInfo);
            entity.Add(productionModuleComponent);

            var warehouseComponent = new WarehouseProductionModul();
            FillWarehouseProductionModul(warehouseComponent, productionInfo);
            entity.Add(warehouseComponent);
        }
        private void AddProductionWithOneRaws(Entity entity, ProductionInfo productionInfo)
        {
            var productionModuleComponent = new ProductionModule1();
            FillProductionModule(productionModuleComponent, productionInfo);
            productionModuleComponent.CountRaw1OfCycle = productionInfo.Raws.Take(1).First().CountInCycle;
            entity.Add(productionModuleComponent);

            var warehouseComponent = new WarehouseProductionModulRaw1();
            FillWarehouseProductionModul(warehouseComponent, productionInfo);
            warehouseComponent.MaxCountRaw1 = 1000;
            warehouseComponent.TypeRaw1 = productionInfo.Raws.Take(1).First().ProductType;
            entity.Add(warehouseComponent);
        }
        private void AddProductionWithTwoRaws(Entity entity, ProductionInfo productionInfo)
        {
            var productionModuleComponent = new ProductionModule2();
            FillProductionModule(productionModuleComponent, productionInfo);
            productionModuleComponent.CountRaw1OfCycle = productionInfo.Raws.Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw2OfCycle = productionInfo.Raws.Skip(1).Take(1).First().CountInCycle;
            entity.Add(productionModuleComponent);

            var warehouseComponent = new WarehouseProductionModulRaw2();
            FillWarehouseProductionModul(warehouseComponent, productionInfo);
            warehouseComponent.MaxCountRaw1 = 1000;
            warehouseComponent.TypeRaw1 = productionInfo.Raws.Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw2 = 1000;
            warehouseComponent.TypeRaw2 = productionInfo.Raws.Skip(1).Take(1).First().ProductType;
            entity.Add(warehouseComponent);
        }
        private void AddProductionWithThreeRaws(Entity entity, ProductionInfo productionInfo)
        {
            var productionModuleComponent = new ProductionModule3();
            FillProductionModule(productionModuleComponent, productionInfo);
            productionModuleComponent.CountRaw1OfCycle = productionInfo.Raws.Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw2OfCycle = productionInfo.Raws.Skip(1).Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw3OfCycle = productionInfo.Raws.Skip(2).Take(1).First().CountInCycle;
            entity.Add(productionModuleComponent);

            var warehouseComponent = new WarehouseProductionModulRaw3();
            FillWarehouseProductionModul(warehouseComponent, productionInfo);
            warehouseComponent.MaxCountRaw1 = 1000;
            warehouseComponent.TypeRaw1 = productionInfo.Raws.Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw2 = 1000;
            warehouseComponent.TypeRaw2 = productionInfo.Raws.Skip(1).Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw3 = 1000;
            warehouseComponent.TypeRaw3 = productionInfo.Raws.Skip(2).Take(1).First().ProductType;
            entity.Add(warehouseComponent);
        }
        private void AddProductionWithFourRaws(Entity entity, ProductionInfo productionInfo)
        {
            var productionModuleComponent = new ProductionModule4();
            FillProductionModule(productionModuleComponent, productionInfo);
            productionModuleComponent.CountRaw1OfCycle = productionInfo.Raws.Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw2OfCycle = productionInfo.Raws.Skip(1).Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw3OfCycle = productionInfo.Raws.Skip(2).Take(1).First().CountInCycle;
            productionModuleComponent.CountRaw4OfCycle = productionInfo.Raws.Skip(3).Take(1).First().CountInCycle;
            entity.Add(productionModuleComponent);

            var warehouseComponent = new WarehouseProductionModulRaw4();
            FillWarehouseProductionModul(warehouseComponent, productionInfo);
            warehouseComponent.MaxCountRaw1 = 1000;
            warehouseComponent.TypeRaw1 = productionInfo.Raws.Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw2 = 1000;
            warehouseComponent.TypeRaw2 = productionInfo.Raws.Skip(1).Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw3 = 1000;
            warehouseComponent.TypeRaw3 = productionInfo.Raws.Skip(2).Take(1).First().ProductType;
            warehouseComponent.MaxCountRaw4 = 1000;
            warehouseComponent.TypeRaw4 = productionInfo.Raws.Skip(3).Take(1).First().ProductType;
            entity.Add(warehouseComponent);

        }

        private void FillProductionModule(ProductionModule productionModuleComponent, ProductionInfo productionInfo)
        {
            productionModuleComponent.Enable = true;
            productionModuleComponent.TimeCycleInSec = productionInfo.CycleTimeInSec;
            productionModuleComponent.CountProductOfCycle = productionInfo.Product.CountInCycle;
        }
        private void FillWarehouseProductionModul(WarehouseProductionModul warehouseComponent, ProductionInfo productionInfo)
        {
            warehouseComponent.PercentFillingRaws = 80;
            warehouseComponent.VolumeMax = 1000;
            warehouseComponent.ProductType = productionInfo.Product.ProductType;
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Min1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class BridgeProductionModulToStantionSystem : SystemExistComponents<BridgeProductionModulToStantion, ProductionInfo>, ISystemAction
    {
        public override void Action(int entityId, BridgeProductionModulToStantion bridgeProductionModulToStantion, ProductionInfo productionInfo, float deltaTime)
        {
            //Должна быть отдельная система для изменения процента заполнения склада Raw материалами (при добавлении компонента должно присвоиться значение процента и рассчитаться значения по каждому компоненту)

            if(IECS.GetEntity(entityId, out var entity))
            {
                //Нужно переделать склад под формат со списком Raw 
            }
            //Проверить Warehouse
            //Для всех Raw меньше максимального заполнения перетащить со станции.
            //Ищем на родительской станции компонент продукта
            //Если он есть
            //Если хватает -> Вычитаем необходимое кол-во и прибавляем в Warehouse
            //Если не хватает -> вычитаем остатки и прибавляем в Warehouse

            //Перемещаем имеющиеся продукты с Warehouse на станцию.

            WarehouseProductionModul warehouse;

            if (entity.ParentEntity != null)
            {
                if (entity.ParentEntity.Get(productionInfo.Product.ProductType, out var component))
                {
                    var product = (Product)component;

                    //Получить склад станции
                    //Если склада нету?
                    if (true) //Если у станции достаточно места на складе
                    {
                        //product.Count += warehouse.CountProduct;
                        //warehouse.CountProduct = 0;
                    }
                    else //Если у станции недостаточно места на складе
                    {
                        var moveQuantity = 0; //Сколько продукта поместится в свободный объем
                        product.Count += moveQuantity;
                        warehouse.CountProduct -= moveQuantity;
                    }
                }
            }
        }

        private void CalculateRawMaxValuesInWarehouse()
        {
            //Рачсет перенести в систему обработки реакции на изменение значения процента заполнения
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
                //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                productionModule.CycleCompletionPercentage += percent;

                if (productionModule.CycleCompletionPercentage >= 100)
                {
                    productionModule.CycleCompletionPercentage -= 100;
                    warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                }
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem1 : SystemExistComponents<ProductionModule1, WarehouseProductionModulRaw1>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule1 productionModule, WarehouseProductionModulRaw1 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem2 : SystemExistComponents<ProductionModule2, WarehouseProductionModulRaw2>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule2 productionModule, WarehouseProductionModulRaw2 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem3 : SystemExistComponents<ProductionModule3, WarehouseProductionModulRaw3>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule3 productionModule, WarehouseProductionModulRaw3 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle
                    && warehouseProductionModul.CountRaw3 >= productionModule.CountRaw3OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountRaw3 -= productionModule.CountRaw3OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem4 : SystemExistComponents<ProductionModule4, WarehouseProductionModulRaw4>, ISystemAction
    {
        public override void Action(int entityId, ProductionModule4 productionModule, WarehouseProductionModulRaw4 warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                if (warehouseProductionModul.CountRaw1 >= productionModule.CountRaw1OfCycle
                    && warehouseProductionModul.CountRaw2 >= productionModule.CountRaw2OfCycle
                    && warehouseProductionModul.CountRaw3 >= productionModule.CountRaw3OfCycle
                    && warehouseProductionModul.CountRaw4 >= productionModule.CountRaw4OfCycle)
                {
                    //Производительность = deltatime / (цикл производства(сек) / 100%)  <= деление на 100, заменено умножением на 0.01
                    var percent = deltatime / (productionModule.TimeCycleInSec * 0.01f);
                    productionModule.CycleCompletionPercentage += percent;

                    if (productionModule.CycleCompletionPercentage >= 100)
                    {
                        productionModule.CycleCompletionPercentage -= 100;
                        warehouseProductionModul.CountRaw1 -= productionModule.CountRaw1OfCycle;
                        warehouseProductionModul.CountRaw2 -= productionModule.CountRaw2OfCycle;
                        warehouseProductionModul.CountRaw3 -= productionModule.CountRaw3OfCycle;
                        warehouseProductionModul.CountRaw4 -= productionModule.CountRaw4OfCycle;
                        warehouseProductionModul.CountProduct += productionModule.CountProductOfCycle;
                    }
                } //Если сырья хватает
            } //Если модуль работает
        }
    }
    #endregion
}