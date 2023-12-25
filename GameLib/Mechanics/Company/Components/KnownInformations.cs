using ECSCore.BaseObjects;
using GameLib.Mechanics.Company.Informations;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Компонент с информацией, которой владеет компания
    /// </summary>
    public class KnownInformations : ComponentBase
    {
        /// <summary>
        /// Информация о кораблях
        /// </summary>
        public InformationManager<ShipInformation> ShipInformationManager { get; set; } = new InformationManager<ShipInformation>();

        /// <summary>
        /// Информация об астеройдах
        /// </summary>
        public InformationManager<AsteroidInformation> AsteroidInformationManager { get; set; } = new InformationManager<AsteroidInformation>();

        /// <summary>
        /// Информация о станциях
        /// </summary>
        public InformationManager<StantionInformation> StantionInformationManager { get; set; } = new InformationManager<StantionInformation>();
    }
}