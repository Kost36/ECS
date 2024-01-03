using ECSCore.BaseObjects;
using GameLib.Mechanics.Production.Datas;
using GameLib.Mechanics.Products.Enums;
using System.Collections.Generic;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Компонент производственного модуля
    /// </summary>
    public class ProductionModule : ComponentBase
    {
        /// <summary>
        /// Включен
        /// </summary>
        public bool Enable;
        /// <summary>
        /// Работа
        /// </summary>
        public bool Work;
        /// <summary>
        /// Тип производимого продукта
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Процент выполнения производственного цикла
        /// </summary>
        public float CycleCompletionPercentage;

        /// <summary>
        /// Количество продукта за производственный цикл
        /// </summary>
        public int CountProductOfCycle; //Todo Вынести в конфиг описание продизводимого продукта
        /// <summary>
        /// Время производственного цикла
        /// </summary>
        public int TimeCycleInSec; //Todo Вынести в конфиг описание продизводимого продукта
        /// <summary>
        /// Потребление сырьевых товаров за производственный цикл
        /// </summary>
        public Dictionary<ProductType, Expense> RawExpenses = new Dictionary<ProductType, Expense>(); //Todo Вынести в конфиг описание продизводимого продукта
    }
}
