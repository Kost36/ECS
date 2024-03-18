using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система регистрирует информацию об известных сущностях
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class RegistrationInformationSystem : SystemExistComponents<KnownInformations, RegistrationInformation>, ISystemActionAdd
    {
        public override void ActionAdd(KnownInformations knownInformations, RegistrationInformation registrationInformation, Entity entity)
        {
            if (IECS.GetEntity(registrationInformation.EntityId, out var resultEntity))
            {
                knownInformations.AvailableInformations.AddOrUpdate(resultEntity);
            }

            entity.RemoveComponent<RegistrationInformation>();
        }
    }
}
