using ECSCore.Attributes;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;
using System;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система контролирует время хранеия информации об известных станциях
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Hour30Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class StantionInformationExpirationsControlSystem : SystemExistComponents<KnownInformations>, ISystemAction
    {
        public override void Action(Guid entityId, KnownInformations knownInformations, float deltatime)
        {
            knownInformations.AvailableInformations.ControlExpiration(typeof(Entitys.Stantion));
        }
    }
}
