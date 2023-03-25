using System;

namespace GameLib.Exceptions
{
    /// <summary>
    /// Исключение - отсутствует атрибут типа продукта
    /// </summary>
    public sealed class ExceptionProductNotHaveAttribute : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message"> Сообщение </param>
        public ExceptionProductNotHaveAttribute(string message) : base(message) { }
    }
}
