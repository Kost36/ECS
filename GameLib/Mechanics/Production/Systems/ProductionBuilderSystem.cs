﻿using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Datas;
using GameLib.Mechanics.Production.Components;
using GameLib.Mechanics.Production.Datas;
using GameLib.Mechanics.Products.Enums;
using GameLib.Products;
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
            var warehouseComponent = new WarehouseProductionModule()
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

            entity.AddComponent(productionModuleComponent);
            entity.AddComponent(warehouseComponent);
            entity.AddComponent(productionInfo);
            entity.AddComponent(new BridgeProductionModulToStantion());

            entity.RemoveComponent<ProductionModuleBuild>();
        }
    }
}
