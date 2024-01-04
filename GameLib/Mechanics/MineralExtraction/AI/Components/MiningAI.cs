using ECSCore.BaseObjects;
using GameLib.Mechanics.MineralExtraction.AI.Enums;

namespace GameLib.Mechanics.MineralExtraction.AI.Components
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
