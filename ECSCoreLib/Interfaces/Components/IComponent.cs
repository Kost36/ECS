namespace ECSCore.Interfaces.Components
{
    /// <summary>
    /// Интерфейс компонента
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Идентификатор сущьности, на которой находится компонент
        /// </summary>
        int Id { get; set; }
    }
}