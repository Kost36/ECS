using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Регистрация информации
    /// </summary>
    public class RegistrationInformation : ComponentBase
    {
        /// <summary>
        /// Идентификатор сущности, по которой получена информация
        /// </summary>
        public Guid EntityId;
    }
}
