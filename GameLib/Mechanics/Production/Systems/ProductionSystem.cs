using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Production.Components;
using System;

namespace GameLib.Mechanics.Production.Systems
{
    [SystemCalculate(SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class ProductionSystem : SystemExistComponents<ProductionModule, WarehouseProductionModule>, ISystemAction
    {
        public override void Action(Guid entityId, ProductionModule productionModule, WarehouseProductionModule warehouseProductionModul, float deltatime)
        {
            if (productionModule.Enable)
            {
                //Контроль производства
                productionModule.Work = true;

                //Проверка, что сырья достаточно
                foreach (var raw in warehouseProductionModul.Raws)
                {
                    if (productionModule.RawExpenses[raw.Key].Value > raw.Value.Value)
                    {
                        productionModule.Work = false;
                        //Todo IECS.AddComponent(new MsgComponent(id= entityId, msg = "msg"));
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

                        foreach (var rawExpense in productionModule.RawExpenses)
                        {
                            warehouseProductionModul.Raws[rawExpense.Key].Value -= rawExpense.Value.Value;
                        }

                        warehouseProductionModul.Product.Value.Value += productionModule.CountProductOfCycle;
                    }
                }
            }
        }
    }
}
