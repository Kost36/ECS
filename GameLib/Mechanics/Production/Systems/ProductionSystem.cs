using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Production.Components;

namespace GameLib.Mechanics.Production.Systems
{
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
}
