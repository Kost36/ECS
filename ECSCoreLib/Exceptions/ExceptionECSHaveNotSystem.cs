using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. Нет проинициализированных систем
    /// </summary>
    public class ExceptionECSHaveNotSystem : Exception
    {
        internal ExceptionECSHaveNotSystem(string msg) : base(message: msg)
        {
        }
    }
}