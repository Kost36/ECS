using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. Нет проинициализированных систем
    /// </summary>
    public sealed class ECSDoesNotHaveAnySystemException : Exception
    {
        internal ECSDoesNotHaveAnySystemException(string msg) : base(message: msg)
        {
        }
    }
}