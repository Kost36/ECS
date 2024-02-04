using ECSCore.BaseObjects;
using GameLib.Mechanics.MineralExtraction.AI.Enums;

namespace GameLib.Mechanics.MineralExtraction.AI.Components
{
    /// <summary>
    /// Задать состояние искуственного интелекта шахтера
    /// </summary>
    public class SetMiningAIState : ComponentBase
    {
        /// <summary>
        /// Состояние
        /// </summary>
        public MiningAIState MiningAIState;
    }
}
