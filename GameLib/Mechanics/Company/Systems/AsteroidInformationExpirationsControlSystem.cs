using ECSCore.Attributes;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;
using GameLib.Mechanics.Mining.Entites;
using System;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система контролирует время хранеия информации об известных астеройдах
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Hour5Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class AsteroidInformationExpirationsControlSystem : SystemExistComponents<KnownInformations>, ISystemAction
    {
        public override void Action(Guid entityId, KnownInformations knownInformations, float deltatime)
        {
            knownInformations.AvailableInformations.ControlExpiration(typeof(Asteroid));
        }
    }
}
