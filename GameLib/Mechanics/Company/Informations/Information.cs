using System;

namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Информация об объекте в секторе
    /// </summary>
    public abstract class Information
    {
        /// <summary>
        /// Тип объекта информации
        /// </summary>
        public TypeInformation TypeInformation;
        /// <summary>
        /// Идентификатор сущьности
        /// </summary>
        public Guid EntityId;
        /// <summary>
        /// Срок годности информации
        /// </summary>
        public DateTimeOffset ExpirationTime;
    }
}