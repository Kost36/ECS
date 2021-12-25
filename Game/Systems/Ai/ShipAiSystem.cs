using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.Ai;
using Game.Components.ObjectStates;
using Game.Components.Propertys;
using Game.Components.Tasks;
using Game.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Systems.Regeneration
{
    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(5)]
    public class ShipAiSystem : SystemBase
    {
        public FilterShipAi Filter;
        public override void Initialization()
        {
            Filter = (FilterShipAi)ManagerFilters.GetFilter<FilterShipAi>();
        }
        public override void PreAсtion()
        {
            Filter.Сalculate();
        }
        public override void Aсtion()
        {
            for (int i = 0; i < Filter.Count; i++)
            {
                int entityId = Filter.ComponentsT0[i].Id;
                if (ECS.GetComponent(entityId, out Enargy enargy))
                {
                    if (enargy.EnargyFact< enargy.EnargyMax)
                    {
                        if (ECS.GetComponent(entityId, out EnargyReGeneration enargyReGeneration) == false)
                        {
                            ECS.AddComponent(new EnargyReGeneration { Id = entityId, EnargyReGen = 10 });
                        }
                    }
                } //Если энергии мало, и нету регенерации. => Добавить компонент

                if (ECS.GetComponent(entityId, out Health health))
                {
                    if (health.HealthFact < health.HealthMax)
                    {
                        if (ECS.GetComponent(entityId, out HealthReGeneration healthReGeneration) == false)
                        {
                            ECS.AddComponent(new HealthReGeneration { Id = entityId, HealthReGen = 1, EnargyUse = 5 });
                        }
                    }
                } //Если жизни не полные и нету регенерации => добавить компонент

                if (ECS.GetComponent(entityId, out Shild shild))
                {
                    if (shild.ShildFact < shild.ShildMax)
                    {
                        if (ECS.GetComponent(entityId, out ShildReGeneration shildReGeneration) == false)
                        {
                            ECS.AddComponent(new ShildReGeneration { Id = entityId, ShildReGen = 1, EnargyUse = 5 });
                        }
                    }
                } //Если щиты не полные и нету регенерации => добавить компонент

                if (ECS.GetComponent(entityId, out PozitionSV pozitionSV))
                {
                    if (ECS.GetComponent(entityId, out Way way) == false)
                    {
                        ECS.AddComponent(new Way { Id = entityId });
                    }
                } //Если есть задание на полет и нету way => добавить компонент

                if (ECS.GetComponent(entityId, out ShipState shipState))
                {
                    if (shipState.StateShip == Enums.StateShip.TRADE)
                    {
                        if (ECS.GetComponent(entityId, out ShipAiTrade trade) == false)
                        {
                            ECS.AddComponent(new ShipAiTrade { Id = entityId });
                        }
                    }
                } //Если состояние торговли и нету компонента торговли => добавить компонент
            }
        }
    }
}