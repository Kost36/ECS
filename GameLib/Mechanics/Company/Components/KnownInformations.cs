using ECSCore.BaseObjects;
using GameLib.Collections.AvailableInformations;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Компонент с информацией, которой владеет компания
    /// </summary>
    public class KnownInformations : ComponentBase
    {
        /// <summary>
        /// Доступная компании информация
        /// </summary>
        public AvailableInformations AvailableInformations;
    }
}