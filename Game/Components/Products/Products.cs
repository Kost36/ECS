using ECSCore.BaseObjects;

namespace Game.Components.Products
{
    ///// <summary>
    ///// Продукт
    ///// </summary>
    //public abstract class Product : Component
    //{
    //    /// <summary>
    //    /// Количество товара
    //    /// </summary>
    //    public int Count;
    //    /// <summary>
    //    /// Вес (кг.)
    //    /// </summary>
    //    public float Weight;
    //    /// <summary>
    //    /// Объем (м.куб.)
    //    /// </summary>
    //    public float Volume;
    //}

    ///// <summary>
    ///// Аккумулятор / батарея с энергией
    ///// </summary>
    //public class Battery : Product { }

    ////Сырьё 0 уровень (Добываемое)
    ///// <summary>
    ///// Руда
    ///// </summary>
    //public class Ore : Product { }
    ///// <summary>
    ///// Кремний
    ///// </summary>
    //public class Silicon : Product { }
    ///// <summary>
    ///// Метан
    ///// </summary>
    //public class Methane : Product { }
    ///// <summary>
    ///// Водород
    ///// </summary>
    //public class Hydrogen : Product { }
    ///// <summary>
    ///// Гелий
    ///// </summary>
    //public class Helium : Product { }

        //За основу взят:
        //               DELTA DTM 12250 i
        //Параметры:
        //          Выходное напряжение    12  В
        //          Емкость                250 А/ч
        //          Энергия                3 кВт
        //          Длина	               520 мм
        //          Ширина	               269 мм
        //          Высота	               288 мм
        //          Объем                ~ 0.04 м³ 
        //          Вес                    41  кг
        //          Стандартная цена     ~ 682,75 USD
    
    ///// <summary>
    ///// Аккумулятор / батарея с энергией.
    ///// 3 кВт
    ///// </summary>
    //public class Battery : Product
    //{
    //    private static readonly float _weight = 74;
    //    private static readonly float _volume = 0.04f;
    //    public override float Weight => _weight;
    //    public override float Volume => _volume;

    //    //За основу взят:
    //    //               DELTA DTM 12250 i
    //    //Параметры:
    //    //          Выходное напряжение    12  В
    //    //          Емкость                250 А/ч
    //    //          Энергия                3 кВт
    //    //          Длина	               520 мм
    //    //          Ширина	               269 мм
    //    //          Высота	               288 мм
    //    //          Объем                ~ 0.04 м³ 
    //    //          Вес                    41  кг
    //    //          Стандартная цена     ~ 682,75 USD
    //}

    ////Сырьё 0 уровень (Добываемое)

    ///// <summary>
    ///// Руда
    ///// </summary>
    //public class Ore : Product
    //{
    //    private static readonly float _weight = 1700;
    //    private static readonly float _volume = 1;
    //    public override float Weight => _weight;
    //    public override float Volume => _volume;

    //    //Параметры:
    //    //          Тип:                   
    //    //          Объем                ~ 1 м³ 
    //    //          Вес                  ~ 1700  кг
    //    //          Стандартная цена     ~ 112,99 USD

    //    //Производимый товар: 
    //    //                    => Железо
    //    //                    => Медь
    //    //                    => 
    //    //                    =>
    //}
    ///// <summary>
    ///// Песок
    ///// </summary>
    //public class Sand : Product
    //{
    //    private static readonly float _weight = 1600;
    //    private static readonly float _volume = 1;
    //    public override float Weight => _weight;
    //    public override float Volume => _volume;

    //    //Параметры:
    //    //          Тип:                   
    //    //          Объем                ~ 1 м³ 
    //    //          Вес                  ~ 1600 кг
    //    //          Стандартная цена     ~ 6,83 USD
        
    //    //Производимый товар: 
    //    //                   Кремний => 1т кремния из 20т песка
    //}

    ///// <summary>
    ///// Метан
    ///// </summary>
    //public class Methane : Product { }
    ///// <summary>
    ///// Водород
    ///// </summary>
    //public class Hydrogen : Product { }
    ///// <summary>
    ///// Гелий
    ///// </summary>
    //public class Helium : Product { }

    ////Продукты 1 уровня (Производимое)
    ///// <summary>
    ///// Железо
    ///// </summary>
    //public class Iron : Product { }
    ///// <summary>
    ///// Пластмасса
    ///// </summary>
    //public class Polymer : Product { }

    ////Продукты 2 уровня (Производимое)
    ///// <summary>
    ///// Пластик
    ///// </summary>
    //public class Plastic : Product { }
    ///// <summary>
    ///// Резина
    ///// </summary>
    //public class Rubber : Product { }

    //Продукты 3 уровня (Производимые)

    //Продукты 4 уровня (Производимые) => Высокотехнологичные

    //Продукты 5 уровня (Производимые) => Оружие, дроны, турели, модули

    //Продукты 6 уровня (Производимые) => Станции, корабли
}

/* //Код для копирования, при создании продукта

/// <summary>
///
/// </summary>
public class : Product { }

 */