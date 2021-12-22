using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components.Propertys
{
    /// <summary>
    /// Компонент скорости
    /// (Value/Sec)
    /// </summary>
    public class Speed : ComponentBase
    {
        public float dX;
        public float dY;
        public float dZ;
    }
    /// <summary>
    /// Компонент ускорения
    /// (Value/Sec)
    /// </summary>
    public class Acceleration : ComponentBase
    {
        public float Value;
    }
    /// <summary>
    /// Компонент замедления
    /// (Value/Sec)
    /// </summary>
    public class Deceleration : ComponentBase
    {
        public float Value;
    }
    /// <summary>
    /// Компонент регенерации энергии
    /// Value/сек
    /// </summary>
    public class EnargyReGeneration
    {
        public float Value;
    }
    /// <summary>
    /// Компонент регенерации щита
    /// Value/сек
    /// </summary>
    public class ShildReGeneration
    {
        public float Value;
    }
    /// <summary>
    /// Компонент ремонта/исцеления
    /// </summary>
    public class HealthReGeneration
    {
        public float Value;
    }
}
