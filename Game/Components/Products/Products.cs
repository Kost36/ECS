using ECSCore.BaseObjects;

namespace Game.Components.Products
{
    /// <summary>
    /// Продукт
    /// </summary>
    public abstract class Product : Component
    {
        /// <summary>
        /// Количество товара
        /// </summary>
        public int Count;
        /// <summary>
        /// Цена покупки товара
        /// </summary>
        public float PriceBuy;
        /// <summary>
        /// Цена продажи товара
        /// </summary>
        public float PriceSell;


        public abstract float Weight { get; }
        public abstract float Volume { get; }
    }

    /// <summary>
    /// Аккумулятор / батарея с энергией
    /// </summary>
    public class Battery : Product
    {
        private static readonly float _weight = 0;
        private static readonly float _volume = 0;

        /// <summary>
        /// Вес одной еденицы товара.
        /// Кг.
        /// </summary>
        public override float Weight  => _weight;
        /// <summary>
        /// Объем одной еденицы товара.
        /// М.Куб.
        /// </summary>
        public override float Volume => _volume;
    }

    //Сырьё 0 уровень (Добываемое)
    /// <summary>
    /// Руда
    /// </summary>
    public class Ore : Product
    {
        private static readonly float _weight = 0;
        private static readonly float _volume = 0;

        /// <summary>
        /// Вес одной еденицы товара.
        /// Кг.
        /// </summary>
        public override float Weight => _weight;

        /// <summary>
        /// Вес одной еденицы товара.
        /// Кг.
        /// </summary>
        public override float Volume => _volume;
    }
    /// <summary>
    /// Кремний
    /// </summary>
    public class Silicon : Product { }
    /// <summary>
    /// Метан
    /// </summary>
    public class Methane : Product { }
    /// <summary>
    /// Водород
    /// </summary>
    public class Hydrogen : Product { }
    /// <summary>
    /// Гелий
    /// </summary>
    public class Helium : Product { }

    //Продукты 1 уровня (Производимое)
    /// <summary>
    /// Железо
    /// </summary>
    public class Iron : Product { }
    /// <summary>
    /// Пластмасса
    /// </summary>
    public class Polymer : Product { }

    //Продукты 2 уровня (Производимое)
    /// <summary>
    /// Пластик
    /// </summary>
    public class Plastic : Product { }
    /// <summary>
    /// Резина
    /// </summary>
    public class Rubber : Product { }

    Продукты 3 уровня(Производимые)

    Продукты 4 уровня(Производимые) => Высокотехнологичные

    Продукты 5 уровня(Производимые) => Оружие, дроны, турели, модули

   Продукты 6 уровня(Производимые) => Станции, корабли
}

/* //Код для копирования, при создании продукта

/// <summary>
///
/// </summary>
public class : Product { }

 */