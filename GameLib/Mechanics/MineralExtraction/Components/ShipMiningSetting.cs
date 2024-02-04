using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;
using GameLib.Mechanics.Products.Extensions;
using System;
using System.Collections.Generic;

namespace GameLib.Mechanics.MineralExtraction.Components
{
    /// <summary>
    /// Настройки майнинга
    /// </summary>
    public class ShipMiningSetting : ComponentBase
    {
        /// <summary>
        /// Максимальная дистанция для добычи минерала с астеройда
        /// </summary>
        public int MaxMiningDistance = 100;

        /// <summary>
        /// Максимальный процент заполнения добываемым материалом
        /// </summary>
        public int MaxPercentageFillingMaterial = 90;

        /// <summary>
        /// Минимальный процент энергии (сколько энергии должно оставаться у корабля при добыче)
        /// </summary>
        public int MinPercentageEnergy = 50;

        /// <summary>
        /// Разрешенное расстояние майнинга.
        /// Если корабль выполняет команду снабжения станции - контролируется расстояние от станции
        /// </summary>
        public int AllowedMiningDistance = int.MaxValue;

        /// <summary>
        /// Время следующего анализа.
        /// * Поиск астеройда для майнинга
        /// * Поиск станции для продажи сырья
        /// </summary>
        public DateTimeOffset TimeNextAnalys = DateTimeOffset.MinValue;

        /// <summary>
        /// Доступные минералы для добычи
        /// </summary>
        public List<MineralType> AvailableMineralsForMining = MineralType.Unknown.GetAllMinerals();
    }
}
