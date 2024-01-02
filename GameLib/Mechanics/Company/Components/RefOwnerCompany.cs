using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Company.Components
{
    /// <summary>
    /// Компонент ссылка на компанию
    /// </summary>
    public class RefOwnerCompany : ComponentBase
    {
        /// <summary>
        /// Ссылка на компанию
        /// </summary>
        public Entity Company;
    }
}
