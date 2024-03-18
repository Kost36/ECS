using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Компонент получения ссылки на компанию
    /// </summary>
    public class GetCompany : ComponentBase
    {
        /// <summary>
        /// Id сущности компанию
        /// </summary>
        public Guid CompanyEntityId;
    }
}
