using ECSCore.Attributes;
using ECSCore.BaseObjects;
using ECSCore.Interfaces.Systems;
using ECSCore.Systems;
using GameLib.Mechanics.Company.Components;

namespace GameLib.Mechanics.Company.Systems
{
    /// <summary>
    /// Система получает ссылку на компанию владельца сущности
    /// </summary>
    [SystemCalculate(ECSCore.Enums.SystemCalculateInterval.Sec1Once)]
    [SystemPriority(50)]
    [SystemEnable]
    public class CompanyGetReferenceSystem : SystemExistComponents<GetCompany>, ISystemActionAdd
    {
        public override void ActionAdd(GetCompany getCompany, Entity entity)
        {
            if (!IECS.GetEntity(getCompany.CompanyEntityId, out Entity companyEntity))
            {
                //entity.AddComponent(new Error() { "companyEntity not found" });
                entity.RemoveComponent<GetCompany>();
                return;
            }

            entity.AddComponent(new RefOwnerCompany() { Company = companyEntity });
            entity.RemoveComponent<GetCompany>();
        }
    }
}
