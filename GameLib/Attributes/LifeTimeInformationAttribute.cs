using System;

namespace GameLib.Attributes
{
    /// <summary>
    /// Атрибут время хранения информации о сущности / компоненнте
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LifeTimeInformationAttribute : Attribute
    {
        public LifeTimeInformationAttribute(int hours = 0, int minutes = 1, int seconds = 0)
        {
            LifeTime = new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        /// Время хранения информации
        /// </summary>
        public TimeSpan LifeTime { get; set; }
    }
}
