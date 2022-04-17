using ECSCore.BaseObjects;
using ECSCore.Interfaces.Components;
using ECSCore.Interfaces.ECS;
using System;
using System.Collections.Generic;

namespace ECSCore.GroupComponents
{
    /// <summary>
    /// Группа компонентов, имеющая 1 включающий компонент (Исключающие компоненты задаются через атрибут к системе)
    /// </summary>
    /// <typeparam name="ExistComponent_1"> Generic тип включающего компонента №1 </typeparam>
    public class GroupComponentsExist<ExistComponent_1> : BaseObjects.GroupComponents
           where ExistComponent_1 : IComponent
    {
        /// <summary>
        /// Включающий компонент №1
        /// </summary>
        internal ExistComponent_1 ExistComponent1;

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    ExistComponent1 = existComponent_1;
                    return true;
                }
            } //Если сущьность есть
            return false;
        }
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 _) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>
            {
                typeof(ExistComponent_1)
            };
            return typesComponents;
        }
    }

    /// <summary>
    /// Группа компонентов, имеющая 2 включающих компонента (Исключающие компоненты задаются через атрибут к системе)
    /// </summary>
    /// <typeparam name="ExistComponent_1"> Generic тип включающего компонента №1 </typeparam>
    /// <typeparam name="ExistComponent_2"> Generic тип включающего компонента №2 </typeparam>
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2> : BaseObjects.GroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
    {

        /// <summary>
        /// Включающий компонент №1
        /// </summary>
        internal ExistComponent_1 ExistComponent1;
        /// <summary>
        /// Включающий компонент №2
        /// </summary>
        internal ExistComponent_2 ExistComponent2;

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    if (entity.Get(out ExistComponent_2 existComponent_2))
                    {
                        ExistComponent1 = existComponent_1;
                        ExistComponent2 = existComponent_2;
                        return true;
                    }
                }
            } //Если сущьность есть
            return false;
        }
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 _) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>
            {
                typeof(ExistComponent_1),
                typeof(ExistComponent_2)
            };
            return typesComponents;
        }
    }

    /// <summary>
    /// Группа компонентов, имеющая 3 включающих компонента (Исключающие компоненты задаются через атрибут к системе)
    /// </summary>
    /// <typeparam name="ExistComponent_1"> Generic тип включающего компонента №1 </typeparam>
    /// <typeparam name="ExistComponent_2"> Generic тип включающего компонента №2 </typeparam>
    /// <typeparam name="ExistComponent_3"> Generic тип включающего компонента №3 </typeparam>
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3> : BaseObjects.GroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
    {
        /// <summary>
        /// Включающий компонент №1
        /// </summary>
        internal ExistComponent_1 ExistComponent1;
        /// <summary>
        /// Включающий компонент №2
        /// </summary>
        internal ExistComponent_2 ExistComponent2;
        /// <summary>
        /// Включающий компонент №3
        /// </summary>
        internal ExistComponent_3 ExistComponent3;

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    if (entity.Get(out ExistComponent_2 existComponent_2))
                    {
                        if (entity.Get(out ExistComponent_3 existComponent_3))
                        {
                            ExistComponent1 = existComponent_1;
                            ExistComponent2 = existComponent_2;
                            ExistComponent3 = existComponent_3;
                            return true;
                        }
                    }
                }
            } //Если сущьность есть
            return false;
        }
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 _) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>
            {
                typeof(ExistComponent_1),
                typeof(ExistComponent_2),
                typeof(ExistComponent_3)
            };
            return typesComponents;
        }
    }

    /// <summary>
    /// Группа компонентов, имеющая 4 включающих компонента (Исключающие компоненты задаются через атрибут к системе)
    /// </summary>
    /// <typeparam name="ExistComponent_1"> Generic тип включающего компонента №1 </typeparam>
    /// <typeparam name="ExistComponent_2"> Generic тип включающего компонента №2 </typeparam>
    /// <typeparam name="ExistComponent_3"> Generic тип включающего компонента №3 </typeparam>
    /// <typeparam name="ExistComponent_4"> Generic тип включающего компонента №4 </typeparam>
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3, ExistComponent_4> : BaseObjects.GroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
        where ExistComponent_4 : IComponent
    {
        /// <summary>
        /// Включающий компонент №1
        /// </summary>
        internal ExistComponent_1 ExistComponent1;
        /// <summary>
        /// Включающий компонент №2
        /// </summary>
        internal ExistComponent_2 ExistComponent2;
        /// <summary>
        /// Включающий компонент №3
        /// </summary>
        internal ExistComponent_3 ExistComponent3;
        /// <summary>
        /// Включающий компонент №4
        /// </summary>
        internal ExistComponent_4 ExistComponent4;

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    if (entity.Get(out ExistComponent_2 existComponent_2))
                    {
                        if (entity.Get(out ExistComponent_3 existComponent_3))
                        {
                            if (entity.Get(out ExistComponent_4 existComponent_4))
                            {
                                ExistComponent1 = existComponent_1;
                                ExistComponent2 = existComponent_2;
                                ExistComponent3 = existComponent_3;
                                ExistComponent4 = existComponent_4;
                                return true;
                            }
                        }
                    }
                }
            } //Если сущьность есть
            return false;
        }
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_4 _) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>
            {
                typeof(ExistComponent_1),
                typeof(ExistComponent_2),
                typeof(ExistComponent_3),
                typeof(ExistComponent_4)
            };
            return typesComponents;
        }
    }

    /// <summary>
    /// Группа компонентов, имеющая 5 включающих компонента (Исключающие компоненты задаются через атрибут к системе)
    /// </summary>
    /// <typeparam name="ExistComponent_1"> Generic тип включающего компонента №1 </typeparam>
    /// <typeparam name="ExistComponent_2"> Generic тип включающего компонента №2 </typeparam>
    /// <typeparam name="ExistComponent_3"> Generic тип включающего компонента №3 </typeparam>
    /// <typeparam name="ExistComponent_4"> Generic тип включающего компонента №4 </typeparam>
    /// <typeparam name="ExistComponent_5"> Generic тип включающего компонента №5 </typeparam>
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3, ExistComponent_4, ExistComponent_5> : BaseObjects.GroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
        where ExistComponent_4 : IComponent
        where ExistComponent_5 : IComponent
    {

        /// <summary>
        /// Включающий компонент №1
        /// </summary>
        internal ExistComponent_1 ExistComponent1;
        /// <summary>
        /// Включающий компонент №2
        /// </summary>
        internal ExistComponent_2 ExistComponent2;
        /// <summary>
        /// Включающий компонент №3
        /// </summary>
        internal ExistComponent_3 ExistComponent3;
        /// <summary>
        /// Включающий компонент №4
        /// </summary>
        internal ExistComponent_4 ExistComponent4;
        /// <summary>
        /// Включающий компонент №5
        /// </summary>
        internal ExistComponent_5 ExistComponent5;

        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию добавления ее группы компонент в фильтр
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    if (entity.Get(out ExistComponent_2 existComponent_2))
                    {
                        if (entity.Get(out ExistComponent_3 existComponent_3))
                        {
                            if (entity.Get(out ExistComponent_4 existComponent_4))
                            {
                                if (entity.Get(out ExistComponent_5 existComponent_5))
                                {
                                    ExistComponent1 = existComponent_1;
                                    ExistComponent2 = existComponent_2;
                                    ExistComponent3 = existComponent_3;
                                    ExistComponent4 = existComponent_4;
                                    ExistComponent5 = existComponent_5;
                                    return true;
                                }
                            }
                        }
                    }
                }
            } //Если сущьность есть
            return false;
        }
        /// <summary>
        /// Проверить, удовлетворяет ли сущьность условию удаления ее группы компонент из фильтра
        /// </summary>
        /// <param name="entityId"> Идентификатор сущьности </param>
        /// <param name="eCS"> ссылка на ECS </param>
        /// <param name="entity"> Ссылка на сущьность </param>
        /// <returns> </returns>
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS, out Entity entity)
        {
            if (eCS.GetEntity(entityId, out entity))
            {
                if (entity.Get(out ExistComponent_1 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_4 _) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_5 _) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        /// <summary>
        /// Получить список типов компонент, которые должны быть на сущьности
        /// </summary>
        /// <returns></returns>
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>
            {
                typeof(ExistComponent_1),
                typeof(ExistComponent_2),
                typeof(ExistComponent_3),
                typeof(ExistComponent_4),
                typeof(ExistComponent_5)
            };
            return typesComponents;
        }
    }
}