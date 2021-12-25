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
        public float SpeedFact;
        public float SpeedMax;
    }
    /// <summary>
    /// Компонент скорости вращения
    /// (Value/Sec)
    /// </summary>
    public class SpeedRotation : ComponentBase
    {
        public float dX = 0.02f;
        public float dY = 0.02f;
        public float dZ = 0.02f;
    }
    /// <summary>
    /// Компонент ускорения
    /// (Value/Sec)
    /// </summary>
    public class Acceleration : ComponentBase
    {
        public float Acc = 0.05f;
        public float EnargyUse = 5;
    }
    /// <summary>
    /// Компонент замедления
    /// (Value/Sec)
    /// </summary>
    public class Deceleration : ComponentBase
    {
        public float Dec = 0.05f;
        public float EnargyUse = 5;
    }
    /// <summary>
    /// Компонент регенерации энергии
    /// Value/сек
    /// </summary>
    public class EnargyReGeneration : ComponentBase
    {
        public float EnargyReGen = 15;
    }
    /// <summary>
    /// Компонент регенерации щита
    /// Value/сек
    /// </summary>
    public class ShildReGeneration : ComponentBase
    {
        public float ShildReGen = 1;
        public float EnargyUse = 10;
    }
    /// <summary>
    /// Компонент ремонта/исцеления
    /// </summary>
    public class HealthReGeneration : ComponentBase
    {
        public float HealthReGen = 1;
        public float EnargyUse = 10;
    }
}