using System;

namespace GameLib.Mechanics.Company.Informations
{
    /// <summary>
    /// Конвертер TypeInformation в срок хранения информации
    /// </summary>
    public static class TypeInformationToTimeSpanConverter
    {
        /// <summary>
        /// Конвертировать TypeInformation в срок хранения информации
        /// </summary>
        public static TimeSpan ToTimeSpan(TypeInformation typeInformation)
        {
            switch (typeInformation)
            {
                case TypeInformation.Asteroid:
                    return new TimeSpan(12, 0, 0);
                case TypeInformation.Stantion:
                    return new TimeSpan(6, 0, 0);
                case TypeInformation.Ship:
                    return new TimeSpan(0, 5, 0);
            }

            return new TimeSpan(0, 1, 0);
        }
    }
}