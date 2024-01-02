using System;

namespace GameLib.Attributes
{
    /// <summary>
    /// Атрибут время хранения информации о сущьности / компоненнте
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LifeTimeInformation : Attribute
    {
        public LifeTimeInformation(int hours = 0, int minutes = 1, int seconds = 0)
        {
            LifeTime = new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        /// Время хранения информации
        /// </summary>
        public TimeSpan LifeTime { get; set; }
    }
}
