using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;
using GameLib.Mechanics.Company.Informations;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система регистрирует информацию об известных сущьностях
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Hour30Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class RegistrationInformationSystem : SystemExistComponents<KnownInformations, RegistrationInformation>, ISystemActionAdd
    {
        public override void ActionAdd(KnownInformations knownInformations, RegistrationInformation registrationInformation, Entity entity)
        {
            switch (registrationInformation.TypeInformation)
            {
                case TypeInformation.Asteroid:
                    knownInformations.AsteroidInformationManager.Add(new AsteroidInformation(registrationInformation.EntityId));
                    break;
                case TypeInformation.Ship:
                    knownInformations.ShipInformationManager.Add(new ShipInformation(registrationInformation.EntityId));
                    break;
                case TypeInformation.Stantion:
                    knownInformations.StantionInformationManager.Add(new StantionInformation(registrationInformation.EntityId));
                    break;
            }
        }
    }
}
