using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. ECS уже проинициализирован
    /// </summary>
    public sealed class ECSIsAlreadyInitializedException : Exception
    {
        internal ECSIsAlreadyInitializedException(string msg) : base(message: msg)
        {
        }
    }
}