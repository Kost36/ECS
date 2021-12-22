using ECSCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCoreCore.Attributes
{
    /// <summary>
    /// Атрибут частоты обработки системы 
    /// Задается одно из:
    /// * Желательный CPM (CalculatePerMinute)
    /// * Желательный интервал (мс)
    /// </summary>
    public class AttributeSystemCalculate : Attribute
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxСPM">
        /// Желательный CPM (CalculatePerMinute) 
        /// Пределы: 1-1800;
        /// 3600 -> 60 раз в сек (Максимальный CPS);
        /// 1800 -> 30 раз в сек;
        /// 600  -> 10 раз в сек;
        /// 300  -> 5  раз в сек;
        /// 60   -> 1  раз в сек;
        /// 30   -> раз в 2 сек;
        /// 10   -> раз в 6 сек;
        /// 5    -> раз в 12 сек;
        /// 2    -> раз в 30 сек;
        /// 1    -> раз в 60 сек;
        /// </param>
        public AttributeSystemCalculate(float maxСPM)
        {
            if (maxСPM > 60)
            {
                maxСPM = 60;
            } //Верхний лимит
            else if (maxСPM < 1)
            {
                maxСPM = 1;
            } //Нижний лимит
            CalculateInterval = (int)(1000 / maxСPM); //Расчет интервала в ms
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="interval">
        /// Желательный интервал (CalculateInterval) в милисекундах.  
        /// Пределы: 17-86.400.000;
        /// 17 ~ 60 раз в секунду;
        /// 33 ~ 30 раз в секунду;
        /// 100 ~ 10 раз в секунду;
        /// 1000 = 1 раз в секунду;
        /// 60.000 = 1 раз в минуту;
        /// 3.600.000 = 1 раз в час;
        /// 43.200.000 = 1 раз в 12 часов;
        /// 86.400.000 = 1 раз в сутки;
        /// </param>
        public AttributeSystemCalculate(int interval)
        {
            if (interval > 86400000)
            {
                interval = 86400000;
            } //Верхний лимит
            else if (interval < 17)
            {
                interval = 17;
            } //Нижний лимит
            CalculateInterval = interval; //Передадим интервал
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="systemCalculateInterval">
        /// Перечисление интервала обработки систем
        /// </param>
        public AttributeSystemCalculate(SystemCalculateInterval systemCalculateInterval)
        {
            CalculateInterval = (int)systemCalculateInterval; //Зададим интервал
        }
        #endregion

        /// <summary>
        /// Интервал обработки системы
        /// </summary>
        public int CalculateInterval { get; private set; } = (int)SystemCalculateInterval.Sec1Once;
    }
}
