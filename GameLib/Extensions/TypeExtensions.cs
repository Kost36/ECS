using System;
using System.Linq;

namespace GameLib.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Получить значение аттрибута
        /// </summary>
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type, 
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var attribute = type
                .GetCustomAttributes(typeof(TAttribute), false)
                .FirstOrDefault() as TAttribute;
            
            if (attribute == null)
            {
                throw new ArgumentNullException($"Attribute [{typeof(TAttribute).FullName}] not found at [{type.FullName}]");
            }

            return valueSelector(attribute);
        }
    }
}
