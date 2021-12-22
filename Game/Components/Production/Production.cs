using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components.Production
{
    public class Production : ComponentBase
    {
        public List
    }


    /// <summary>
    /// Аккумулятор / батарея с энергией
    /// </summary>
    public class Battery : Product { }

    //Сырьё 0 уровень (Добываемое)
    /// <summary>
    /// Руда
    /// </summary>
    public class Ore : Product { }
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
}
}