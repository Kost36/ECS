using GameLib.Attributes;
using GameLib.Enums;
using GameLib.Exceptions;
using System;
using System.Reflection;

namespace GameLib.Providers
{
    /// <summary>
    /// Поставщик типа продукта
    /// </summary>
    public static class ProductTypeProvider
    {
        /// <summary>
        /// Получить тип продукта
        /// </summary>
        /// <param name="type"> Тип объекта </param>
        public static ProductType GetProductType(Type type)
        {
            ProductTypeAttribute attribute = type.GetCustomAttribute<ProductTypeAttribute>();
            if (attribute == null)
            {
                throw new ExceptionProductNotHaveAttribute($"Сlass [{type.FullName}] does not have AttributeProductType");
            }
            return attribute.ProductType;
        }
    }
}

//TODO Генерировать список при инициализации библиотеки 
//TODO Далее брать из заранее подготовленного списка
