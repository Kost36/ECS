using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.MineralExtraction.Components.Commands
{
    /// <summary>
    /// Команда переместиться к астеройду
    /// </summary>
    public class ShipCommandMoveToAsteroid : ComponentBase
    {
        /// <summary>
        /// Целевой астеройд
        /// </summary>
        public Guid TargetAsteroidId;
    }
}
