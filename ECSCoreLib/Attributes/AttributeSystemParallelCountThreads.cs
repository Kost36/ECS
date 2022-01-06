using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Attributes
{
    /// <summary>
    /// Атрибут количества потоков, для распараллеливания системы.
    /// Оптимальное количество потоков: 
    /// От колличества ядер процессора 
    /// До колличества визических потоков процессора
    /// </summary>
    public class AttributeSystemParallelCountThreads : Attribute
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countThreads"> Количество потоков, для распараллеливания </param>
        public AttributeSystemParallelCountThreads(int countThreads)
        {
            CountThreads = countThreads;
            if (countThreads > 128)
            {
                //Топовые серверные процессоры имеют 64 ядра по 2 физ. потока на ядро 
                CountThreads = 128;
            } //Верхний лимит
            else if (countThreads < 1)
            {
                CountThreads = 1;
            } //Нижний лимит
        }
        #endregion

        /// <summary>
        /// Интервал обработки системы (ms)
        /// </summary>
        public int CountThreads { get; private set; } = 1;
    }
}