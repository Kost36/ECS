using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.MineralExtraction.Components.Commands
{
    /// <summary>
    /// Команда снабжать станцию сырьем
    /// </summary>
    public class ShipCommandSupplyStantion : ComponentBase
    {
        /// <summary>
        /// Идентификатор станции, которую необходимо снабжать
        /// </summary>
        public Guid StantionId;
    }
}
