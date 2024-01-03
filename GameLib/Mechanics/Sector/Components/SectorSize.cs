using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Sector.Components
{
    /// <summary>
    /// Размер сектора
    /// </summary>
    public class SectorSize : ComponentBase
    {
        /// <summary>
        /// Граница сектора по оси X.
        /// От 0 до value
        /// </summary>
        public long LimitX = Constants.SectorSize.Width;
        /// <summary>
        /// Граница сектора по оси Y.
        /// От 0 до value
        /// </summary>
        public long LimitY = Constants.SectorSize.Height;
        /// <summary>
        /// Граница сектора по оси Z.
        /// От 0 до value
        /// </summary>
        public long LimitZ = Constants.SectorSize.Depth;
    }
}
