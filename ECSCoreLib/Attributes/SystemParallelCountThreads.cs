using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут количества потоков, для обработки системы.
    /// </summary>
    public sealed class SystemParallelCountThreads : Attribute
    {
        /// <summary>
        /// Кол-во параллельных потоков обработки системы
        /// </summary>
        public int CountThreads { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countThreads"> Количество потоков, для распараллеливания </param>
        public SystemParallelCountThreads(int countThreads)
        {
            if (countThreads > 128)
            {
                CountThreads = 128;
            } //Верхний лимит
            else if (countThreads < 1)
            {
                CountThreads = 1;
            } //Нижний лимит
            else
            {
                CountThreads = countThreads;
            }
        }
    }
}