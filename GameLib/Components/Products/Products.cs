using ECSCore.BaseObjects;
using ECSCore.Interfaces;

namespace GameLib.Components.Products
{
    #region Базовый объект товара
    /// <summary>
    /// Продукт
    /// </summary>
    public abstract class Product : ComponentBase
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

        /// <summary>
        /// Максимальный процент заполнения склада продуктом
        /// </summary>
        public float PercentMaxFillWarehouse;
        /// <summary>
        /// Минимальная цена покупки
        /// </summary>
        public float MinPriceBuy;
        /// <summary>
        /// Максимальная цена покупки
        /// </summary>
        public float MaxPriceBuy;
        /// <summary>
        /// Минимальная цена продажи
        /// </summary>
        public float MinPriceSell;
        /// <summary>
        /// Максимальная цена продажи
        /// </summary>
        public float MaxPriceSell;

        /// <summary>
        /// Вес одной еденицы товара.
        /// Кг.
        /// </summary>
        public abstract float Weight { get; }
        /// <summary>
        /// Объем одной еденицы товара.
        /// М.Куб.
        /// </summary>
        public abstract float Volume { get; }
    }
    #endregion

    #region Восполняемое
    /// <summary>
    /// Аккумулятор / батарея без энергии.
    /// 3 кВт
    /// </summary>
    public class BatteryEmpty : Product
    {
        private static readonly float _weight = 74;
        private static readonly float _volume = 0.04f;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //За основу взят:
        //               DELTA DTM 12250 i Пересмотреть в сторону аккумулятора Tesla TODO
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
    }
    /// <summary>
    /// Аккумулятор / батарея с энергией.
    /// 3 кВт
    /// </summary>
    public class Battery : Product
    {
        private static readonly float _weight = 74;
        private static readonly float _volume = 0.04f;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //За основу взят:
        //               DELTA DTM 12250 i Пересмотреть в сторону аккумулятора Tesla TODO
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
    }

    /// <summary>
    /// Грязная вода
    /// </summary>
    public class WaterDirty : Product
    {
        private static readonly float _weight = 1700;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Water      => TODO (При использовании воды, часть переходит в грызную воду)

        //Производимый товар: 
        //                   Water      => TODO
    }
    /// <summary>
    /// Вода
    /// </summary>
    public class Water : Product
    {
        private static readonly float _weight = 1700;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Hydrogen   => TODO 
        //                   Carbon     => TODO 
        //                   Battery    => TODO  

        //Сырье:
        //                   WaterDirty => TODO  

        //Производимый товар: 
        //                   WaterDirty   => TODO
        //                   Grain        => TODO
        //                   Vegetables   => TODO
        //                   Fruit        => TODO
        //                   CompoundFeed => TODO
        //                   Fish         => TODO
        //                   Meat         => TODO
    }
    #endregion

    #region Сырьё 0 уровень (Добываемое)
    /// <summary>
    /// Руда
    /// </summary>
    public class Ore : Product
    {
        private static readonly float _weight = 1700;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ 1700  кг
        //          Стандартная цена     ~ 112,99 USD

        //Производимый товар: 
        //                   Iron      => TODO
        //                   Сopper    => TODO
        //                   Aluminum  => TODO
        //                   Sulfur    => TODO
    }
    /// <summary>
    /// Песок
    /// </summary>
    public class Sand : Product
    {
        private static readonly float _weight = 1600;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ 1600 кг
        //          Стандартная цена     ~ 6,83 USD

        //Производимый товар: 
        //                   Silicon => 1т кремния из 20т Sand
        //                   Glass   => TODO
    }
    /// <summary>
    /// Углерод
    /// </summary>
    public class Carbon : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   Water      => TODO
        //                   Elastic    => TODO
        //                   Fertilizer => TODO
        //                   Plastic    => TODO
    }
    /// <summary>
    /// Водород
    /// </summary>
    public class Hydrogen : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   Water       => TODO
        //                   Fertilizer  => TODO
        //                   TODO        => TODO
    }
    /// <summary>
    /// Азот
    /// </summary>
    public class Nitrogen : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   Fertilizer => TODO
        //                   TODO       => TODO
        //                   TODO       => TODO
    }
    /// <summary>
    /// Метан
    /// </summary>
    public class Methane : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   Sulfur     => TODO
        //                   Elastic    => TODO
        //                   Fertilizer => TODO
    }
    /// <summary>
    /// Гелий
    /// </summary>
    public class Helium : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   Elastic => TODO
        //                   Glass   => TODO
        //                   TODO    => TODO
    }
    #endregion

    #region Материалы 1 уровень (Производимое)
    /// <summary>
    /// Железо
    /// </summary>
    public class Iron : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Ore     => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Metal   => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    /// <summary>
    /// Медь
    /// </summary>
    public class Сopper : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Ore     => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Wiring    => TODO
        //                   TODO      => TODO
        //                   TODO      => TODO
    }
    /// <summary>
    /// Алюминий
    /// </summary>
    public class Aluminum : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Ore     => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    /// <summary>
    /// Кремний
    /// </summary>
    public class Silicon : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Sand    => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Silicon    => TODO
        //                   TODO       => TODO
        //                   TODO       => TODO
    }
    /// <summary>
    /// Сера
    /// </summary>
    public class Sulfur : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Ore     => TODO 
        //                   Methane => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Rubber    => TODO
        //                   TODO      => TODO
        //                   TODO      => TODO
    }
    /// <summary>
    /// Амиак
    /// </summary>
    public class Ammonia : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Nitrogen => TODO 
        //                   Hydrogen => TODO 
        //                   Battery  => TODO  

        //Производимый товар: 
        //                   Rubber     => TODO
        //                   TODO       => TODO
        //                   TODO       => TODO
    }
    /// <summary>
    /// Каучук
    /// </summary>
    public class Elastic : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Carbon  => TODO 
        //                   Helium  => TODO 
        //                   Methane => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Rubber    => TODO
        //                   Plastic   => TODO
        //                   TODO      => TODO
    }
    #endregion

    #region Материалы 2 уровень (Производимое)
    /// <summary>
    /// Металл
    /// </summary>
    public class Metal : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Iron    => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Body      => TODO
        //                   Sheathing => TODO
        //                   TODO      => TODO
    }
    /// <summary>
    /// Резина
    /// </summary>
    public class Rubber : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Elastic => TODO 
        //                   Sulfur  => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Wiring        => TODO
        //                   Sheathing     => TODO
        //                   TODO          => TODO
        //                   TODO          => TODO
    }
    /// <summary>
    /// Пластик
    /// </summary>
    public class Plastic : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Elastic => TODO 
        //                   Carbon  => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Electronics      => TODO
        //                   ControlSystems   => TODO
        //                   Body             => TODO
        //                   Sheathing        => TODO
    }
    /// <summary>
    /// Провода
    /// </summary>
    public class Wiring : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Сopper  => TODO 
        //                   Rubber  => TODO 
        //                   Battery => TODO  

        //Производимый товар: 
        //                   Electronics    => TODO
        //                   ControlSystems => TODO
        //                   TODO           => TODO
    }
    /// <summary>
    /// Кремниевая пластина
    /// </summary>
    public class SiliconWafer : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Silicon  => TODO 
        //                   Battery  => TODO  

        //Производимый товар: 
        //                   Electronics => TODO
        //                   TODO        => TODO
        //                   TODO        => TODO
    }
    /// <summary>
    /// Удобрение
    /// </summary>
    public class Fertilizer : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Carbon   => TODO 
        //                   Methane  => TODO 
        //                   Nitrogen => TODO 
        //                   Hydrogen => TODO 
        //                   Battery  => TODO  

        //Производимый товар: 
        //                   Grain       => TODO
        //                   Vegetables  => TODO
        //                   Fruit       => TODO
        //                   TODO        => TODO
    }
    /// <summary>
    /// Стекло
    /// </summary>
    public class Glass : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Sand     => TODO 
        //                   Helium   => TODO 
        //                   Battery  => TODO  

        //Производимый товар: 
        //                   Sheathing    => TODO
        //                   TODO         => TODO
        //                   TODO         => TODO
    }
    #endregion

    #region Товары 3 уровень (Производимое) => Высокотехнологичные
    /// <summary>
    /// Электроника
    /// </summary>
    public class Electronics : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   SiliconWafer    => TODO 
        //                   Wiring          => TODO  
        //                   Plastic         => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   ControlSystems  => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Системы управления
    /// </summary>
    public class ControlSystems : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Electronics     => TODO 
        //                   Wiring          => TODO  
        //                   Plastic         => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    /// <summary>
    /// Корпус
    /// </summary>
    public class Body : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Metal           => TODO 
        //                   Plastic         => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    /// <summary>
    /// Обшивка
    /// </summary>
    public class Sheathing : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Metal           => TODO 
        //                   Plastic         => TODO  
        //                   Rubber          => TODO  
        //                   Glass           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    #endregion

    #region Товары 4 уровень (Производимое) => Оружие, дроны, турели, модули
    /// <summary>
    /// 
    /// </summary>
    public class A : Product
    {
        private static readonly float _weight = 1700;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    #endregion

    #region Товары 5 уровень (Производимое) => Станции, корабли
    /// <summary>
    /// 
    /// </summary>
    public class S : Product
    {
        private static readonly float _weight = 1700;
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO USD

        //Производимый товар: 
        //                   TODO    => TODO
        //                   TODO    => TODO
        //                   TODO    => TODO
    }
    #endregion

    #region Еда (Производимое)
    /// <summary>
    /// Зерно
    /// </summary>
    public class Grain : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Fertilizer      => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   CompoundFeed    => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Овощи 
    /// </summary>
    public class Vegetables : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Fertilizer      => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO            => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Фрукты  
    /// </summary>
    public class Fruit : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Fertilizer      => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO            => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Комбикорм  
    /// </summary>
    public class CompoundFeed : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   Grain           => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   Fish            => TODO
        //                   Meat            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Рыба  
    /// </summary>
    public class Fish : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   CompoundFeed    => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO            => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    /// <summary>
    /// Мясо  
    /// </summary>
    public class Meat : Product
    {
        private static readonly float _weight = 0; //TODO
        private static readonly float _volume = 1;
        public override float Weight => _weight;
        public override float Volume => _volume;

        //Параметры:
        //          Тип:                   
        //          Объем                ~ 1 м³ 
        //          Вес                  ~ TODO  кг
        //          Стандартная цена     ~ TODO  USD

        //Сырье:             
        //                   CompoundFeed    => TODO 
        //                   Water           => TODO  
        //                   Battery         => TODO  

        //Производимый товар: 
        //                   TODO            => TODO
        //                   TODO            => TODO
        //                   TODO            => TODO
    }
    #endregion
}