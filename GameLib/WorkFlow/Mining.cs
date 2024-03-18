using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;

namespace GameLib.WorkFlow
{
    /// <summary>
    /// Команда дабывать серье с астеройда
    /// </summary>
    public class CommandMiningFromForStantion : ComponentBase
    {
        /// <summary>
        /// Тип добываемого минерала (От типа астеройда?)
        /// </summary>
        public MineralType MinralType;
        /// <summary>
        /// Идентификатор сущности астероида
        /// </summary>
        public int AsteroidEntityId;
        /// <summary>
        /// Количество для добычи (В настройках?)
        /// </summary>
        public int QuantityForMining;
        /// <summary>
        /// Идентификатор сущности станции для выгрузки (Не тут должен находиться)
        /// </summary>
        public int StantionEntityId;
    }

    /// <summary>
    /// Команда дабывать серье с астеройда на продажу
    /// </summary>
    public class CommandMiningFromForSale : ComponentBase
    {
        /// <summary>
        /// Тип добываемого минерала (От типа астеройда?)
        /// </summary>
        public MineralType MinralType;
        /// <summary>
        /// Идентификатор сущности астероида
        /// </summary>
        public int AsteroidEntityId;
        /// <summary>
        /// Количество для добычи (В настройках?)
        /// </summary>
        public int QuantityForMining;
    }
}
