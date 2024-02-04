using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Компания - владелец
    /// </summary>
    public class OwnerСompany : ComponentBase
    {
        /// <summary>
        /// Идентификатор сущьности компании
        /// </summary>
        public Guid CompanyEntityId;
    }
}
