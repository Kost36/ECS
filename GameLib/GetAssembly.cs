using System.Reflection;

namespace GameLib
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