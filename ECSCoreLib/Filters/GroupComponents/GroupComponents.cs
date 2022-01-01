using ECSCore.BaseObjects;
using ECSCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCore.Filters.GroupComponents
{
    public class GroupComponentsExist<ExistComponent_1> : Interfaces.GroupComponents
           where ExistComponent_1 : IComponent
    {
        public ExistComponent_1 ExistComponent1;
        public override bool TryAddComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1))
                {
                    ExistComponent1 = existComponent_1;
                    return true;
                }
            } //Если сущьность есть
            return false;
        }
        public override bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        public override List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>();
            typesComponents.Add(typeof(ExistComponent_1));
            return typesComponents;
        }
        public override List<Type> GetTypesWithoutComponents()
        {
            List<Type> typesComponents = new List<Type>();
            return typesComponents;
        }
    }
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2> : IGroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
    {
        internal ExistComponent_1 ExistComponent1;
        internal ExistComponent_2 ExistComponent2;
        public bool TryAddComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
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
        public bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 existComponent_2) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        public List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>();
            typesComponents.Add(typeof(ExistComponent_1));
            typesComponents.Add(typeof(ExistComponent_2));
            return typesComponents;
        }
        public List<Type> GetTypesWithoutComponents()
        {
            List<Type> typesComponents = new List<Type>();
            return typesComponents;
        }
    }
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3> : IGroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
    {
        internal ExistComponent_1 ExistComponent1;
        internal ExistComponent_2 ExistComponent2;
        internal ExistComponent_3 ExistComponent3;
        public bool TryAddComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
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
        public bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 existComponent_2) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 existComponent_3) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        public List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>();
            typesComponents.Add(typeof(ExistComponent_1));
            typesComponents.Add(typeof(ExistComponent_2));
            typesComponents.Add(typeof(ExistComponent_3));
            return typesComponents;
        }
        public List<Type> GetTypesWithoutComponents()
        {
            List<Type> typesComponents = new List<Type>();
            return typesComponents;
        }
    }
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3, ExistComponent_4> : IGroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
        where ExistComponent_4 : IComponent
    {
        internal ExistComponent_1 ExistComponent1;
        internal ExistComponent_2 ExistComponent2;
        internal ExistComponent_3 ExistComponent3;
        internal ExistComponent_4 ExistComponent4;
        public bool TryAddComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
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
        public bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 existComponent_2) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 existComponent_3) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_4 existComponent_4) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        public List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>();
            typesComponents.Add(typeof(ExistComponent_1));
            typesComponents.Add(typeof(ExistComponent_2));
            typesComponents.Add(typeof(ExistComponent_3));
            typesComponents.Add(typeof(ExistComponent_4));
            return typesComponents;
        }
        public List<Type> GetTypesWithoutComponents()
        {
            List<Type> typesComponents = new List<Type>();
            return typesComponents;
        }
    }
    public class GroupComponentsExist<ExistComponent_1, ExistComponent_2, ExistComponent_3, ExistComponent_4, ExistComponent_5> : IGroupComponents
        where ExistComponent_1 : IComponent
        where ExistComponent_2 : IComponent
        where ExistComponent_3 : IComponent
        where ExistComponent_4 : IComponent
        where ExistComponent_5 : IComponent
    {
        internal ExistComponent_1 ExistComponent1;
        internal ExistComponent_2 ExistComponent2;
        internal ExistComponent_3 ExistComponent3;
        internal ExistComponent_4 ExistComponent4;
        internal ExistComponent_5 ExistComponent5;
        public bool TryAddComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
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
        public bool TryRemoveComponentForEntity(int entityId, IECSSystem eCS)
        {
            if (eCS.GetEntity(entityId, out Entity entity))
            {
                if (entity.Get(out ExistComponent_1 existComponent_1) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_2 existComponent_2) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_3 existComponent_3) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_4 existComponent_4) == false)
                {
                    return true;
                }
                if (entity.Get(out ExistComponent_5 existComponent_5) == false)
                {
                    return true;
                }
                return false;
            } //Если сущьность есть
            return true;
        }
        public List<Type> GetTypesExistComponents()
        {
            List<Type> typesComponents = new List<Type>();
            typesComponents.Add(typeof(ExistComponent_1));
            typesComponents.Add(typeof(ExistComponent_2));
            typesComponents.Add(typeof(ExistComponent_3));
            typesComponents.Add(typeof(ExistComponent_4));
            typesComponents.Add(typeof(ExistComponent_5));
            return typesComponents;
        }
        public List<Type> GetTypesWithoutComponents()
        {
            List<Type> typesComponents = new List<Type>();
            return typesComponents;
        }
    }
}