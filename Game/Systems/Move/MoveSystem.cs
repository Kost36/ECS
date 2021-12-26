using ECSCore.Attributes;
using ECSCore.BaseObjects;
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

namespace Game.Systems.Move
{
    [AttributeSystemCalculate(SystemCalculateInterval.Sec30Once)]
    [AttributeSystemPriority(1)]
    public class MovementSystem : SystemBaseGeneric<Pozition, Speed>
    {
        public override void ActionUser(Pozition pozition, Speed speed)
        {
            pozition.X = pozition.X + speed.dX;
            pozition.Y = pozition.Y + speed.dY;
            pozition.Z = pozition.Z + speed.dZ;
        }
    }

    [AttributeSystemCalculate(SystemCalculateInterval.Min5Once)]
    [AttributeSystemPriority(5)]
    public class ShipAiSystem : SystemBaseGeneric<ShipAi>
    {
        public override void ActionUser(ShipAi shipAi)
        {
            int entityId = shipAi.Id;
            if (ECS.ManagerEntitys.Get(entityId, out EntityBase entityBase) == false)
            {
                return;
            }
            if (entityBase.Get(out Enargy enargy))
            {
                if (enargy.EnargyFact < enargy.EnargyMax)
                {
                    if (entityBase.Get(out EnargyReGeneration enargyReGeneration) == false)
                    {
                        entityBase.Add(new EnargyReGeneration { Id = entityId, EnargyReGen = 10 });
                    }
                }
            } //Если энергии мало, и нету регенерации. => Добавить компонент
            if (entityBase.Get(out Health health))
            {
                if (health.HealthFact < health.HealthMax)
                {
                    if (entityBase.Get(out HealthReGeneration healthReGeneration) == false)
                    {
                        entityBase.Add(new HealthReGeneration { Id = entityId, HealthReGen = 1, EnargyUse = 5 });
                    }
                }
            } //Если жизни не полные и нету регенерации => добавить компонент
            if (entityBase.Get(out Shild shild))
            {
                if (shild.ShildFact < shild.ShildMax)
                {
                    if (entityBase.Get(out ShildReGeneration shildReGeneration) == false)
                    {
                        entityBase.Add(new ShildReGeneration { Id = entityId, ShildReGen = 1, EnargyUse = 5 });
                    }
                }
            } //Если щиты не полные и нету регенерации => добавить компонент
            if (entityBase.Get(out PozitionSV pozitionSV))
            {
                if (entityBase.Get(out Way way) == false)
                {
                    entityBase.Add(new Way { Id = entityId });
                }
            } //Если есть задание на полет и нету way => добавить компонент
            if (entityBase.Get(out ShipState shipState))
            {
                if (shipState.StateShip == Enums.StateShip.TRADE)
                {
                    if (entityBase.Get(out ShipAiTrade trade) == false)
                    {
                        entityBase.Add(new ShipAiTrade { Id = entityId });
                    }
                }
            } //Если состояние торговли и нету компонента торговли => добавить компонент
        }
    }
}