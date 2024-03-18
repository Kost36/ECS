using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент запуска перемещения к сущности.
    /// </summary>
    [Serializable]
    public class StartMoveToEntity : ComponentBase
    {
        /// <summary>
        /// Сущность к которой нужно переместиться
        /// </summary>
        public Guid TargetEntityId;
    }
}
