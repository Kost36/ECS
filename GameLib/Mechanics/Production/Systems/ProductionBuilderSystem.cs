using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Production.Datas;
using GameLib.Mechanics.Products.Enums;
using GameLib.Mechanics.Stantion.Components;
using GameLib.Products;
using System;
using System.Collections.Generic;

namespace GameLib.Mechanics.Production.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class ProductionBuilderSystem : SystemExistComponents<Components.Production>, ISystemActionAdd
    {
        public override void ActionAdd(Components.Production production, Entity entity)
        {
            var productionInfo = ProductionInfoProvider.GetProductionInfo(production.ProductType);

            var productionModuleComponent = new ProductionModule()
            {
                Enable = true,
                TimeCycleInSec = productionInfo.CycleTimeInSec,
                CountProductOfCycle = productionInfo.Product.CountInCycle
            };
            var warehouseProductionModuleComponent = new WarehouseProductionModule()
            {
                PercentFillingRaws = 80,
                VolumeMax = 1000
            };

            productionModuleComponent.ProductType = productionInfo.Product.ProductType;
            productionModuleComponent.TimeCycleInSec = productionInfo.CycleTimeInSec;
            productionModuleComponent.CountProductOfCycle = productionInfo.Product.CountInCycle;

            warehouseProductionModuleComponent.Product = new KeyValuePair<ProductType, Count>(
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
                warehouseProductionModuleComponent.Raws.Add(
                    rawInfo.ProductType,
                    new Count()
                    {
                        Value = 0,
                        MaxLimit = rawInfo.CountInCycle * 5
                    });
            }

            PrepareStantion(entity, productionInfo);

            entity.AddComponent(productionModuleComponent);
            entity.AddComponent(warehouseProductionModuleComponent);
            entity.AddComponent(productionInfo);
            entity.AddComponent(new BridgeProductionModulToStantion());

            entity.RemoveComponent<ProductionModuleBuild>();
        }

        private void PrepareStantion(Entity entity, ProductionInfo productionInfo)
        {
            if (entity.ExternalEntity == null)
            {
                //Todo Add component error or component msg
                return;
            }

            if (!entity.ExternalEntity.TryGetComponent<Warehouse>(out var warehouseStantion))
            {
                warehouseStantion = new Warehouse();
                entity.ExternalEntity.AddComponent(warehouseStantion);
            }

            AddProductInfo(warehouseStantion, productionInfo);
            AddRawInfos(warehouseStantion, productionInfo);
        }

        private void AddProductInfo(Warehouse warehouseStantion, ProductionInfo productionInfo)
        {
            if (!warehouseStantion.Products.TryGetValue(productionInfo.Product.ProductType, out var warehouseProductInfo))
            {
                warehouseProductInfo = new WarehouseProductInfo();
                warehouseStantion.Products.Add(productionInfo.Product.ProductType, warehouseProductInfo);
            }

            warehouseProductInfo.IsProduct = true;
        }

        private void AddRawInfos(Warehouse warehouseStantion, ProductionInfo productionInfo)
        {
            foreach (var raw in productionInfo.Raws)
            {
                if (!warehouseStantion.Products.TryGetValue(raw.ProductType, out var warehouseProductInfo))
                {
                    warehouseProductInfo = new WarehouseProductInfo();
                    warehouseStantion.Products.Add(raw.ProductType, warehouseProductInfo);
                }

                warehouseProductInfo.IsRaw = true;
            }
        }
    }
}
