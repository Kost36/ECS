using ECSCore.BaseObjects;

namespace Game.Components.Production
{
    public class Production : ComponentBase
    {
        //public List
    }


    /// <summary>
    /// Аккумулятор / батарея с энергией
    /// </summary>
    public class ProductionBattery : Production { }

    //Сырьё 0 уровень (Добываемое)
    /// <summary>
    /// Руда
    /// </summary>
    public class ProductionOre : Production  { }
    /// <summary>
    /// Кремний
    /// </summary>
    public class ProductionSilicon : Production  { }
    /// <summary>
    /// Метан
    /// </summary>
    public class ProductionMethane : Production  { }
    /// <summary>
    /// Водород
    /// </summary>
    public class ProductionHydrogen : Production  { }
    /// <summary>
    /// Гелий
    /// </summary>
    public class ProductionHelium : Production  { }

    //Продукты 1 уровня (Производимое)
    /// <summary>
    /// Железо
    /// </summary>
    public class ProductionIron : Production  { }
    /// <summary>
    /// Пластмасса
    /// </summary>
    public class ProductionPolymer : Production  { }

    //Продукты 2 уровня (Производимое)
    /// <summary>
    /// Пластик
    /// </summary>
    public class ProductionPlastic : Production  { }
    /// <summary>
    /// Резина
    /// </summary>
    public class ProductionRubber : Production  { }
}