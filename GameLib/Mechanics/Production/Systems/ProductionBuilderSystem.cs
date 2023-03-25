using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Datas;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Production.Datas;
using GameLib.Products;
using GameLib.Providers;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Mechanics.Production.Systems
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionBuilderSystem : SystemExistComponents<Components.Production>, ISystemActionAdd
    {
        public override void ActionAdd(Components.Production production, Entity entity)
        {
            var componentType = production.GetType();
            if (componentType.IsGenericType)
            {
                var productType = componentType.GetGenericArguments().First();
                if (ProductionInfoProvider.GetProductionInfo(ProductTypeProvider.GetProductType(productType), out var productionInfo))
                {
                    var productionModuleComponent = new ProductionModule()
                    {
                        Enable = true,
                        TimeCycleInSec = productionInfo.CycleTimeInSec,
                        CountProductOfCycle = productionInfo.Product.CountInCycle
                    };
                    var warehouseComponent = new WarehouseProductionModul()
                    {
                        PercentFillingRaws = 80,
                        VolumeMax = 1000
                    };

                    productionModuleComponent.ProductType = productionInfo.Product.ProductType;
                    productionModuleComponent.TimeCycleInSec = productionInfo.CycleTimeInSec;
                    productionModuleComponent.CountProductOfCycle = productionInfo.Product.CountInCycle;

                    warehouseComponent.Product = new KeyValuePair<ProductType, Count>(
                        productionInfo.Product.ProductType,
                        new Count()
                        {
                            Value = 0
                        });

                    foreach (var rawInfo in productionInfo.Raws)
                    {
                        productionModuleComponent.RawExpenses.Add(
                            rawInfo.ProductType,
                            new Expense()
                            {
                                Value = rawInfo.CountInCycle
                            });
                        warehouseComponent.Raws.Add(
                            rawInfo.ProductType,
                            new Count()
                            {
                                Value = 0,
                                MaxValue = rawInfo.CountInCycle + rawInfo.CountInCycle
                            });
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
}
