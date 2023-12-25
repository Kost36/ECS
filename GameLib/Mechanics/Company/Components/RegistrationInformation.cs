using ECSCore.BaseObjects;
using GameLib.Mechanics.Company.Informations;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Регистрация информации
    /// </summary>
    public class RegistrationInformation : ComponentBase
    {
        /// <summary>
        /// Тип информации
        /// </summary>
        public TypeInformation TypeInformation;

        /// <summary>
        /// Идентификатор сущьности, по которой получена информация
        /// </summary>
        public int EntityId;
    }
}
