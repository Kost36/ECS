using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;

namespace GameLib.WorkFlow.WorkFlowProduction
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    [AttributeSystemPriority(50)]
    [AttributeSystemEnable]
    public class ProductionSystem : SystemExistComponents<Production>, ISystemAction
    {
        public override void Action(int entityId, Production production, float deltatime)
        {
            IECS.GetEntity(entityId, out var entity);

            var productionInfo = ProductionInfos.GetProductionInfo(production.ProductType);

            foreach(var rawItem in productionInfo.Raws)
            {
                //Проверить наличие сырьевых товаров для производства продукта
            }

            var percent = (float)(deltatime / (productionInfo.CycleTimeInSec * 0.01));
            production.PercentCycle += percent;

            if (production.PercentCycle >= 100)
            {
                //Вычитаем из существующих у сущьности Raw компонентов значения потребления

                //Если компонента нету -> добавить компонент со значением count = productionInfo.ProductionCount
                //if (entity.Get(production.ProductType, out var product))
                //{
                //    product.
                //}

                //Если компонент уже есть -> прибавить к count значение productionInfo.ProductionCount

            }
        }
    }
}