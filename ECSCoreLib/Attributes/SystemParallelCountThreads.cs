using System;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут количества потоков, для обработки системы.
    /// </summary>
    public class SystemParallelCountThreads : Attribute
    {
        /// <summary>
        /// Кол-во параллельных потоков обработки системы
        /// </summary>
        public int CountThreads { get; private set; } = 1;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countThreads"> Количество потоков, для распараллеливания </param>
        public SystemParallelCountThreads(int countThreads)
        {
            if (countThreads > 128)
            {
                countThreads = 128;
            } //Верхний лимит
            else if (countThreads < 1)
            {
                countThreads = 1;
            } //Нижний лимит

            CountThreads = countThreads;
        }
    }
}