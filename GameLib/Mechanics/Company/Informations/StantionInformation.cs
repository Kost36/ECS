using System;

namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Информация об известной станции
    /// </summary>
    public sealed class StantionInformation : Information
    {
        public StantionInformation(Guid entityId)
        {
            EntityId = entityId;
            TypeInformation = TypeInformation.Stantion;
        }
    }

}