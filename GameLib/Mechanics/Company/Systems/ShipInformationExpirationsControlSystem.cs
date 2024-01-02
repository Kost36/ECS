using ECSCore.Attributes;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Entitys;
using GameLib.Mechanics.Company.Components;
using System;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система контролирует время хранеия информации об известных кораблях
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Min10Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class ShipInformationExpirationsControlSystem : SystemExistComponents<KnownInformations>, ISystemAction
    {
        public override void Action(Guid entityId, KnownInformations knownInformations, float deltatime)
        {
            knownInformations.AvailableInformations.ControlExpiration(typeof(Ship));
        }
    }
}
