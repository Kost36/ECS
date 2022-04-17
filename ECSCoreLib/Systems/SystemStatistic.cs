using System;

namespace ECSCore.Systems
{
    /// <summary>
    /// Класс информации о системе в очереди
    /// </summary>
    internal class SystemStatistic
    {
        #region Поля
        /// <summary>
        /// Сумарное количество вычислений фильтра
        /// </summary>
        private int _sumFilterCalculateCount;
        /// <summary>
        /// Сумарное время вычисления фильтра
        /// </summary>
        private float _sumFilterCalculateTimeTikcs;
        /// <summary>
        /// Среднее время вычисления фильтра
        /// </summary>
        private float _timeAverFilterCalculateInTicks;
        /// <summary>
        /// Максимальное время вычисления фильтра
        /// </summary>
        private float _timeMaxFilterCalculateInTicks;
        /// <summary>
        /// Сумарное время выполнения
        /// </summary>
        private float _sumRunTimeTikcs;
        /// <summary>
        /// Среднее время выполнения системы в тиках
        /// </summary>
        private float _timeAverRunInTicks;
        /// <summary>
        /// Максимальное время выполнения системы в тиках
        /// </summary>
        private float _timeMaxRunInTicks;
        #endregion

        #region Свойства
        /// <summary>
        /// Сумарное количество выполнений
        /// </summary>
        public int SummUseCount { get; private set; }

        /// <summary>
        /// Процент времени использования от производительности
        /// </summary>
        public float PercentTimeUsePerformance { get; private set; }

        /// <summary>
        /// Среднее время выполнения системы в мс
        /// </summary>
        public float AverTimeUseMs { get { return _timeAverRunInTicks / TimeSpan.TicksPerMillisecond; } }

        /// <summary>
        /// Максимально время выполнения системы в мс
        /// </summary>
        public float MaxTimeUseMs { get { return _timeMaxRunInTicks / TimeSpan.TicksPerMillisecond; } }

        /// <summary>
        /// Среднее время вычисления фильтра системы в мс
        /// </summary>
        public float AverTimeFilterCalculateMs { get { return _timeAverFilterCalculateInTicks / TimeSpan.TicksPerMillisecond; } }

        /// <summary>
        /// Максимально время вычисления фильтра системы в мс
        /// </summary>
        public float MaxTimeFilterCalculateMs { get { return _timeMaxFilterCalculateInTicks / TimeSpan.TicksPerMillisecond; } }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Добавить статистику по выполнению
        /// </summary>
        /// <param name="timeRunInTicks"> Время выполнения системы в тиках </param>
        /// <param name="timeWorkManagerSystemTicks"> Время работы приложения в тиках </param>
        public void AddRunStatistic(long timeRunInTicks, long timeWorkManagerSystemTicks)
        {
            SummUseCount++;
            _sumRunTimeTikcs += timeRunInTicks;
            _timeAverRunInTicks = _sumRunTimeTikcs / SummUseCount;
            if (timeRunInTicks > _timeMaxRunInTicks) { _timeMaxRunInTicks = timeRunInTicks; }
            PercentTimeUsePerformance = (_sumRunTimeTikcs + _sumFilterCalculateTimeTikcs) / (float)timeWorkManagerSystemTicks;
        }

        /// <summary>
        /// Добавить статистику по вычислению фильтра
        /// </summary>
        /// <param name="timeFilterCalculateInTicks"> Время вычисления фильтра в тиках </param>
        public void AddFilterCalculateStatistic(long timeFilterCalculateInTicks)
        {
            _sumFilterCalculateCount++;
            _sumFilterCalculateTimeTikcs += timeFilterCalculateInTicks;
            _timeAverFilterCalculateInTicks = _sumFilterCalculateTimeTikcs / _sumFilterCalculateCount;
            if (timeFilterCalculateInTicks > _timeMaxFilterCalculateInTicks) { _timeMaxFilterCalculateInTicks = timeFilterCalculateInTicks; }
        }

        /// <summary>
        /// Очистить статистику системы
        /// </summary>
        public void ClearStatistic()
        {
            //Выполнение
            SummUseCount = 0;
            _sumRunTimeTikcs = 0;
            _timeMaxRunInTicks = 0;
            _timeAverRunInTicks = 0;
            //Фильтры
            _sumFilterCalculateCount = 0;
            _sumFilterCalculateTimeTikcs = 0;
            _timeMaxFilterCalculateInTicks = 0;
            _timeAverFilterCalculateInTicks = 0;
            //Производительность
            PercentTimeUsePerformance = 0;
        }
        #endregion
    }
}