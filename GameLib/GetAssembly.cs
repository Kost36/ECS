using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Получить ссылку на сборку
    /// </summary>
    public static class GetAssembly
    {
        /// <summary>
        /// Взять сборку
        /// </summary>
        /// <returns></returns>
        public static Assembly Get()
        {
            return typeof(GetAssembly).Assembly;
        }
    }
}
