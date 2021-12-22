using ECSCore.BaseObjects;
using ECSCore.Interfaces;

namespace ECSCore.Filters
{
    /// <summary>
    /// Задание на добавление компонента
    /// </summary>
    public class JobTryAdd : IJobToFilter
    {
        public JobTryAdd(ComponentBase component, EntityBase entity)
        {
            Component = component;
            Entity = entity;
        }
        ComponentBase Component { get; set; }
        EntityBase Entity { get; set; }

        public void Action(FilterBase filter)
        {
            filter.TryAdd(Component, Entity);
        }
    }
    /// <summary>
    /// Задание на удаление компонента
    /// </summary>
    public class JobTryRemove : IJobToFilter
    {
        public JobTryRemove(ComponentBase component, EntityBase entity)
        {
            Component = component;
            Entity = entity;
        }
        ComponentBase Component { get; set; }
        EntityBase Entity { get; set; }
        public void Action(FilterBase filter)
        {
            filter.TryRemove(Component, Entity);
        }
    }
    /// <summary>
    /// Задание на удаление компонентов по Id
    /// </summary>
    public class JobTryRemoveId : IJobToFilter
    {
        public JobTryRemoveId(int id)
        {
            Id = id;
        }
        int Id { get; set; }
        public void Action(FilterBase filter)
        {
            filter.TryRemove(Id);
        }
    }
}