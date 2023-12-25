using ECSCore.Attributes;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система контролирует срок годности информации об известных кораблях
    /// </summary>
    [SystemCalculate(10000)]
    [SystemPriority(50)]
    [SystemEnable]
    public class ControlShipInformationExpirationsSystem : SystemExistComponents<KnownInformations>, ISystemAction
    {
        public override void Action(int entityId, KnownInformations knownInformations, float deltatime)
        {
            knownInformations.ShipInformationManager.ControlExpiration();
        }
    }
}
