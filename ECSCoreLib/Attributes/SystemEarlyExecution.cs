using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут разрешения предварительного выполнения системы.
    /// Лимит 0.0-50.0 %
    /// Если интервал системы задан 100 ms и у системы есть данный аттрибут с значением 20.0% =>
    /// В случае если:
    /// ECS имеет возможность выполнять системы, и нет готовых к выполнению систем.
    /// ECS выполнит систему, у которой есть данный атрибут, и время находится в заданном пороге;
    /// Пример: 
    /// OldRun   = 100000
    /// FactTime = 100060 => notRun
    /// EarlyRun = 100080
    /// FactTime = 100081 => RunSystem and Set OldRun = 100100, EarlyRun = 100180, NextRun = 100200
    /// NextRun  = 100100
    /// </summary>
    public sealed class SystemEarlyExecution : Attribute
    {
        /// <summary>
        /// Процент времени порога предварительного выполнения
        /// </summary>
        public float PercentThresholdTime { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="percentThresholdTime"> Процент времени порога раннего выполнения (Считается от интервала выполнения системы) </param>
        public SystemEarlyExecution(float percentThresholdTime = 0)
        {
            if (percentThresholdTime < 0)
            {
                PercentThresholdTime = 0;
            }
            else if (percentThresholdTime > 50)
            {
                PercentThresholdTime = 50f;
            }
            else
            {
                PercentThresholdTime = percentThresholdTime;
            }
        }
    }
}