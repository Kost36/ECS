using ECSCore.BaseObjects;
using System;
using System.Diagnostics;

namespace ECSCore.Systems
{
    /// <summary>
    /// Контроллер системы
    /// </summary>
    internal class JobSystem
    {
        private Stopwatch _stopwatch = new Stopwatch();

        public JobSystem(SystemBase system, long ticksPoint)
        {
            System = system;
            TicksOldRun = ticksPoint;
            TicksNextRun = ticksPoint;
            CalculateNextRun();
        }

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

        /// <summary>
        /// Расчет deltaTime 
        /// </summary>
        /// <param name="ticksPoint"> Метка фактического времени </param>
        /// <param name="speedRun"> Множитель скорости выполнения </param>
        /// <returns> Интервал времени в секундах </returns>
        private float CalculateDeltaTime(long ticksPoint, float speedRun)
        {
            float deltaTimeInSec = ((float)(ticksPoint - TicksOldRun) / (float)TimeSpan.TicksPerSecond) * speedRun;
            TicksOldRun = ticksPoint;
            return deltaTimeInSec;
        }

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
            System.CalculateFilter(limitTimeTicks);
            SystemStatistic.AddFilterCalculateStatistic(_stopwatch.ElapsedTicks);
            _stopwatch.Stop();
        }

        /// <summary>
        /// Действие системы
        /// </summary>
        /// <param name="ticksPoint"> Метка времени, на которой выполняестся система </param>
        /// <param name="ticksWorkManagerSystem"> Общее время работы приложения в тиках </param>
        /// <param name="speedRun"> Множитель скорости выполнения </param>
        public void Run(long ticksPoint, long ticksWorkManagerSystem, float speedRun)
        {
            _stopwatch.Restart();
            System.CalculateFilter();
            SystemStatistic.AddFilterCalculateStatistic(_stopwatch.ElapsedTicks);

            _stopwatch.Restart();
            System.DeltaTime = CalculateDeltaTime(ticksPoint, speedRun);

            if (System.IsAction)
            {
                System.RunAction(); //TODO Фиксация объектов => потокобезопасность
            }

            SystemStatistic.AddRunStatistic(_stopwatch.ElapsedTicks, ticksWorkManagerSystem);
            _stopwatch.Stop();
        }
    }
}