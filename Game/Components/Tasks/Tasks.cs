using ECSCore.BaseObjects;
using Game.Components.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components.Tasks
{
    /// <summary>
    /// Компонент заданной позиции.
    /// Позиция, в которую нужно переместиться
    /// </summary>
    public class PozitionSV : ComponentBase
    {
        public float X;
        public float Y;
        public float Z;
    }
    /// <summary>
    /// Компонент заданной скорости
    /// (Value/Sec)
    /// </summary>
    public class SpeedSV : ComponentBase
    {
        public float dXSV;
        public float dYSV;
        public float dZSV;
        public float NeedSpeed;
    }
    /// <summary>
    /// Компонент вектора заданного перемещения
    /// </summary>
    public class Way : ComponentBase
    {
        public float Len;
        public float LenX;
        public float LenY;
        public float LenZ;
        public float NormX;
        public float NormY;
        public float NormZ;
    }
    /// <summary>
    /// Компонент задание покупки
    /// </summary>
    public class TaskBuy : ComponentBase
    {
        /// <summary>
        /// Идентификатор сущьности поставщика, у которой нужно совершить покупку
        /// </summary>
        public int IdEntityVendor;
        /// <summary>
        /// Товар, который нужно купить
        /// </summary>
        public Product Product;
        /// <summary>
        /// Количество товара для покупки
        /// </summary>
        public int Count;
    }
    /// <summary>
    /// Компонент задание продажи
    /// </summary>
    public class TaskSell : ComponentBase
    {
        /// <summary>
        /// Идентификатор сущьности покупателя, которой нужно продать товар
        /// </summary>
        public int IdEntityVendor;
        /// <summary>
        /// Товар, который нужно продать
        /// </summary>
        public Product Product;
        /// <summary>
        /// Количество товара для продажи
        /// </summary>
        public int Count;
    }
}
