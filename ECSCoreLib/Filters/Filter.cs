using ECSCore.BaseObjects;
using ECSCore.Interfaces.GroupComponents;
using System;
using System.Collections.Generic;

namespace ECSCore.Filters
{
    /// <summary>
    /// Фильтр для группы с одним компонентом
    /// </summary>
    internal class Filter<TGroupComponents> : FilterBase
        where TGroupComponents : IGroupComponents
    {
        private TGroupComponents _groupComponents;

        public Filter()
        {
            Init();
        }

        public Dictionary<int, TGroupComponents> Collection { get; set; } = new Dictionary<int, TGroupComponents>();

        /// <summary>
        /// Количество отслеживаемых сущьностей в фильтре
        /// </summary>
        public override int Count => Collection.Count;

        /// <summary>
        /// Типы имеющихся компонент
        /// </summary>
        public override List<Type> TypesExistComponents { get; set; }

        /// <summary>
        /// Типы исключающихся компонент
        /// </summary>
        public override List<Type> TypesWithoutComponents { get; set; }

        /// <summary>
        /// Инициализация фильтра
        /// </summary>
        public override void Init()
        {
            _groupComponents = Activator.CreateInstance<TGroupComponents>();
            TypesExistComponents = _groupComponents.GetTypesExistComponents();
            TypesWithoutComponents = new List<Type>();// _groupComponents.GetTypesWithoutComponents();
        }

        /// <summary>
        /// Проверить наличие у сущьности исключающих в фильтре компонент
        /// </summary>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> true - у сущьности присутствуют исключающие компоненты </returns>
        private bool CheckHaveWithoutComponents(Entity entity)
        {
            foreach (Type typeWithoutComponent in TypesWithoutComponents)
            {
                if (entity.CheckExistComponent(typeWithoutComponent))
                {
                    return true;
                } //Если исключающий компонент есть на сущьности
            } //Проходимся по всем исключающим компонентам
            return false; 
        }

        /// <summary>
        /// Удалить группу компонент из коллекции фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        private void RemoveFromCollection(int entityId)
        {
            lock (Collection)
            {
                if (Collection.TryGetValue(entityId, out TGroupComponents groupComponents))
                {
                    foreach (SystemBase system in InterestedSystems)
                    {
                        if (system.IsEnable && system.IsActionRemove)
                        {
                            system.ActionRemove(entityId); //Вызов события удаления группы компонент из фильтра
                        } //Если система включена и имеет интерфейс ActionRemove
                    } //Проход по заинтересованным в фильтре системам
                    Collection.Remove(entityId);
                    return;
                } //Если группа компонент есть в коллекции фильтра
            }
        }

        public override void TryAdd(int entityId)
        {
            lock (Collection)
            {
                if (Collection.ContainsKey(entityId))
                {
                    return;
                } //Проверка наличия группы компонентов в фильтре
                lock (_groupComponents)
                {
                    if (_groupComponents.TryAddComponentForEntity(entityId, ECSSystem, out Entity entity))
                    {
                        if (CheckHaveWithoutComponents(entity))
                        {
                            return;
                        } //Если у сущьности присутствуют исключающие компоненты
                        foreach (SystemBase system in InterestedSystems)
                        {
                            if (system.IsEnable && system.IsActionAdd)
                            {
                                system.AсtionAdd(_groupComponents, entity);
                            } //Если система включена и реализует соответствующий интерфейс
                        } //Вызов AсtionAdd у заинтересованных систем
                        Collection.Add(entityId, _groupComponents);
                        _groupComponents = Activator.CreateInstance<TGroupComponents>();
                        return;
                    } //Проверка необходимости добавления списка компонентов в фильтр и добавление списка компонентов в фильтр
                }
            }
        }

        public override void TryRemove(int entityId)
        {
            lock (_groupComponents)
            {
                if (_groupComponents.TryRemoveComponentForEntity(entityId, ECSSystem, out Entity entity))
                {
                    RemoveFromCollection(entityId); // Удалить группу из коллекции фильтра
                    return;
                } //Если у сущьности нету хотя бы одного включающего компонента
                //Иначе, если у сущьности есть все включающие компоненты, то проверяем исключающие компоненты!
                else if (CheckHaveWithoutComponents(entity))
                {
                    RemoveFromCollection(entityId); // Удалить группу из коллекции фильтра
                    return;
                } //Если у сущьности есть хотя бы один исключающих компонент
            }
        }

        public override void TryRemoveEntity(int entityId)
        {
            lock (Collection)
            {
                Collection.Remove(entityId);
            }
        }
    }
}