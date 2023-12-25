using ECSCore.BaseObjects;
using GameLib.Enums;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Команда дабывать серье с астеройда
    /// </summary>
    public class CommandMiningFrom : ComponentBase
    {
        /// <summary>
        /// Тип добываемого минерала (От типа астеройда?)
        /// </summary>
        public MineralType MinralType;
    }

    /// <summary>
    /// Настройки добычи серья
    /// </summary>
    public class SettingMining : ComponentBase
    {
        /// <summary>
        /// Максимальный процент заполнения трюма сырьем
        /// </summary>
        public int MaxPercentageFillingHoldRaw;
    }
}
