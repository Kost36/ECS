using ECSCore.BaseObjects;
using GameLib.Mechanics.Mining.AI.Enums;

namespace GameLib.Mechanics.Mining.AI.Components
{
    /// <summary>
    /// Искусственный интелект шахтера
    /// </summary>
    public class MiningAI : ComponentBase
    {
        /// <summary>
        /// Состояние
        /// </summary>
        public MiningAIState MiningAIState;
    }
}
