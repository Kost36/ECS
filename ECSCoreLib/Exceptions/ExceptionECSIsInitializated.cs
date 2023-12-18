using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. ECS уже проинициализирован
    /// </summary>
    public class ExceptionECSIsInitializated : Exception
    {
        internal ExceptionECSIsInitializated(string msg) : base(message: msg)
        {
        }
    }
}