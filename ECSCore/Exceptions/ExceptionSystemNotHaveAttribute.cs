using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Exceptions
{
    /// <summary>
    /// Исключение. Система не имеет необъодимый атрибут
    /// </summary>
    public class ExceptionSystemNotHaveAttribute : Exception {
        public ExceptionSystemNotHaveAttribute(Type attributeType, Type systemType) {
            AttributeType = attributeType;
            SystemType = systemType;
        }

        public Type AttributeType { get; set; }
        public Type SystemType { get; set; }
    }
}