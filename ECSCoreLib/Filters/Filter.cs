using ECSCore.BaseObjects;
using ECSCore.Interfaces.GroupComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Filters
{
    /// <summary>
    /// Группа для 1 компонента
    /// </summary>
    internal class Filter<TGroupComponents> : FilterBase
        where TGroupComponents : IGroupComponents
    {
        #region Конструктор
        public Filter()
        {
            Init();
        }
        #endregion

        #region Поля
        /// <summary>
        /// Экземпляр объекта группы компонент, для взаимодействия
        /// </summary>
        private TGroupComponents _groupComponents;
        #endregion

        #region Свойства
        public Dictionary<int, TGroupComponents> Collection { get; set; } = new Dictionary<int, TGroupComponents>();
        #endregion

        #region IFilterDebug Реализация
        /// <summary>
        /// Количество отслеживаемых сущьностей в фильтре
        /// </summary>
        public override int Count => Collection.Count;
        #endregion

        #region IFilterInit Реализация
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
            TypesWithoutComponents = _groupComponents.GetTypesWithoutComponents();
        }
        #endregion

        #region IFilterActionGroup Реализация
        public override void TryAdd(int entityId)
        {
            lock (Collection)
            {
                if (Collection.ContainsKey(entityId))
                {
                    CountNotAdd_Have++;
                    return;
                } //Проверка наличия списка компонентов в фильтре
                lock (_groupComponents)
                {
                    if (_groupComponents.TryAddComponentForEntity(entityId, ECSSystem))
                    {
                        foreach (SystemBase system in InterestedSystems)
                        {
                            if (system.IsEnable && system.IsActionAdd)
                            {
                                system.AсtionAdd(entityId, _groupComponents);
                            }
                        }
                        Collection.Add(entityId, _groupComponents);
                        CountAdd++;
                        _groupComponents = Activator.CreateInstance<TGroupComponents>();
                        return;
                    } //Проверка необходимости добавления списка компонентов в фильтр и добавление списка компонентов в фильтр
                    CountNotAdd_TryAddComponentForEntity_IsFalse++;
                }
            }
        }
        public override void TryRemove(int entityId)
        {
            lock (_groupComponents)
            {
                if (_groupComponents.TryRemoveComponentForEntity(entityId, ECSSystem))
                {
                    lock (Collection)
                    {
                        if (Collection.TryGetValue(entityId, out TGroupComponents groupComponents))
                        {
                            foreach (SystemBase system in InterestedSystems)
                            {
                                if (system.IsEnable && system.IsActionRemove)
                                {
                                    system.AсtionRemove(entityId);
                                }
                            }
                            Collection.Remove(entityId);
                            CountRemove++;
                            return;
                        }
                    }
                }
            }
        }
        public override void TryRemoveEntity(int entityId)
        {
            lock (Collection)
            {
                Collection.Remove(entityId);
            }
        }
        #endregion
    }
}

//TODO Если в одном фильтре заинтересовано более 1 системы.
//То логика реализации ActionAdd, Action, ActionRemove у систем будет работать неверно. 
//Будет происходить вызов только у какой то 1 системы
//FIX : Производить вызов ActionAdd и ActionRemove. У всех заинтересованных систем, при обработке фильтра