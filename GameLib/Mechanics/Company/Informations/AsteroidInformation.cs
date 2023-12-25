namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Информация об известном астеройде
    /// </summary>
    public sealed class AsteroidInformation : Information
    {
        public AsteroidInformation(int entityId)
        {
            EntityId = entityId;
            TypeInformation = TypeInformation.Asteroid;
        }
    }
}