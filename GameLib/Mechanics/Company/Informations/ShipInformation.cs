namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Информация об известном корабле
    /// </summary>
    public sealed class ShipInformation : Information
    {
        public ShipInformation(int entityId)
        {
            EntityId = entityId;
            TypeInformation = TypeInformation.Ship;
        }
    }
}