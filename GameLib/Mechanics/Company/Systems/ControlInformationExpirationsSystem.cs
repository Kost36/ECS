using ECSCore.Attributes;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;
using System;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система контролирует срок годности информации об известных станциях / астеройдах
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Hour30Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class ControlInformationExpirationsSystem : SystemExistComponents<KnownInformations>, ISystemAction
    {
        public override void Action(Guid entityId, KnownInformations knownInformations, float deltatime)
        {
            knownInformations.AsteroidInformationManager.ControlExpiration();
            knownInformations.StantionInformationManager.ControlExpiration();
        }
    }
}
