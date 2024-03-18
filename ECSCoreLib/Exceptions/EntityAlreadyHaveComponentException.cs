using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. У сущности уже есть данный компонент
    /// </summary>
    public sealed class EntityAlreadyHaveComponentException : Exception
    {
        internal EntityAlreadyHaveComponentException(string msg) : base(message: msg)
        {
        }
    }
}