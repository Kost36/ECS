using System;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. У сущьности уже есть данный компонент
    /// </summary>
    public class ExceptionEntityHaveComponent : Exception
    {
        internal ExceptionEntityHaveComponent(string msg) : base(message: msg)
        {
        }
    }
}