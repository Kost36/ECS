using ECSCore.BaseObjects;
using System;

namespace GameLib.Components
{
    /// <summary>
    /// Компонент трюма (вместимости в м³) 
    /// м³
    /// </summary>
    [Serializable]
    public class Hold : ComponentBase
    {
        /// <summary>
        /// Максимальная вместимость в м³
        /// </summary>
        public float Max;
        /// <summary>
        /// Занято емкости в м³
        /// </summary>
        public float Use;
    }
}
