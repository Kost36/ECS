using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Systems
{
    /// <summary>
    /// Контроллер системы
    /// </summary>
    internal class JobSystem
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        public JobSystem(SystemBase system, long ticksPoint)
        {
            System = system;
            TicksOldRun = ticksPoint;
            TicksNextRun = ticksPoint;
            CalculateNextRun();
        }
        #endregion

        #region Поля
        private Stopwatch _stopwatch = new Stopwatch();
        #endregion

        #region Свойства
        /// <summary>
        /// Система
        /// </summary>
        public SystemBase System { get; }
        /// <summary>
        /// Статистика системы
        /// </summary>
        public SystemStatistic SystemStatistic { get; } = new SystemStatistic();
        /// <summary>
        /// Метка времени возможного предварительного выполнения
        /// </summary>
        public long TicksEarlyExecution { get; set; }
        /// <summary>
        /// Метка времени предидущего выполнения
        /// </summary>
        public long TicksOldRun { get; set; }
        /// <summary>
        /// Метка времени следующего выполнения
        /// </summary>
        public long TicksNextRun { get; set; }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Расчет deltaTime 
        /// </summary>
        /// <param name="ticksPoint"> Метка фактического времени </param>
        /// <returns> Интервал времени в секундах </returns>
        private float CalculateDeltaTime(long ticksPoint)
        {
            float deltaTimeInSec = (ticksPoint - TicksOldRun) / TimeSpan.TicksPerSecond; //Интервал в секундах
            TicksOldRun = ticksPoint; //Метка времени последнего выполнения
            return deltaTimeInSec;
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Сбросить метки времени
        /// </summary>
        public void SetTicks(long ticksPoint)
        {
            TicksOldRun = ticksPoint;
            TicksNextRun = ticksPoint;
            TicksEarlyExecution = ticksPoint;
        }
        /// <summary>
        /// Вычислить время следующего выполнения системы
        /// </summary>
        public void CalculateNextRun()
        {
            TicksNextRun += System.IntervalTicks;
            TicksEarlyExecution = TicksNextRun + (-System.EarlyExecutionTicks);
        }
        /// <summary>
        /// Вычислять фильтр системы заданное время
        /// </summary>
        /// <param name="limitTimeTicks"> Лимит времени на обработку Ticks </param>
        /// <returns></returns>
        public void FilterCalculateTime(long limitTimeTicks = 0)
        {
            _stopwatch.Restart();
            System.CalculateFilter(limitTimeTicks); //Вычисляем фильтр некоторое (Свободное) время
            SystemStatistic.AddFilterCalculateStatistic(_stopwatch.ElapsedTicks); //Добавить в статистику
            _stopwatch.Stop();
        }
        /// <summary>
        /// Действие системы
        /// </summary>
        /// <param name="ticksPoint"> Метка времени, на которой выполняестся система </param>
        /// <param name="ticksWorkManagerSystem"> Общее время работы приложения в тиках </param>
        public void RunAsync(long ticksPoint, long ticksWorkManagerSystem)
        {
            //Предобработка фильтра
            _stopwatch.Restart();
            System.CalculateFilter();
            SystemStatistic.AddFilterCalculateStatistic(_stopwatch.ElapsedTicks); //Добавить в статистику

            //Расчет deltaTime
            _stopwatch.Restart();
            System.DeltaTime = CalculateDeltaTime(ticksPoint); //Расчет DeltaTime

            //Обработка системы
            System.Aсtion();
            SystemStatistic.AddRunStatistic(_stopwatch.ElapsedTicks, ticksWorkManagerSystem); //Добавить в статистику
            _stopwatch.Stop();
        }
        #endregion
    }
}