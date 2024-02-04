using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент запуска перемещения к сущьности.
    /// </summary>
    [Serializable]
    public class StartMoveToEntity : ComponentBase
    {
        /// <summary>
        /// Сущьность к которой нужно переместиться
        /// </summary>
        public Guid TargetEntityId;
    }
}
