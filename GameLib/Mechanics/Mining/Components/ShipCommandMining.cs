using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Команда добычи с астеройда
    /// </summary>
    public class ShipCommandMining : ComponentBase
    {
        /// <summary>
        /// Целевой астеройд
        /// </summary>
        public int TargetAsteroidId;
    }
}
