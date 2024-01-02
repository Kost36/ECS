using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.Mining.Components
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
        /// Идентификатор сущьности астероида
        /// </summary>
        public int AsteroidEntityId;
        /// <summary>
        /// Количество для добычи (В настройках?)
        /// </summary>
        public int QuantityForMining;
        /// <summary>
        /// Идентификатор сущьности станции для выгрузки (Не тут должен находиться)
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
        /// Идентификатор сущьности астероида
        /// </summary>
        public int AsteroidEntityId;
        /// <summary>
        /// Количество для добычи (В настройках?)
        /// </summary>
        public int QuantityForMining;
    }

    /// <summary>
    /// Команда переместиться к астеройду
    /// </summary>
    public class CommandMoveToAsteroid : ComponentBase
    {
        /// <summary>
        /// Идентификатор сущьности астероида
        /// </summary>
        public int AsteroidEntityId;
    }
}
