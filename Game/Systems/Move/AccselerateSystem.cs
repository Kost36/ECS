using ECSCore.Attributes;
using ECSCore.Enums;
using ECSCore.System;
using Game.Components.ObjectStates;
using Game.Components.Propertys;
using Game.Components.Tasks;

namespace Game.Systems.Move
{
    //[AttributeSystemCalculate(SystemCalculateInterval.Sec1Once)]
    //[AttributeSystemPriority(1)]
    //public class AccselerateSystem : SystemBase
    //{
    //    public FilterAccelerate Filter;

    //    public override void Initialization()
    //    {
    //        Filter = (FilterAccelerate)ManagerFilters.GetFilter<FilterAccelerate>();
    //    }
    //    public override void PreAсtion()
    //    {
    //        Filter.Сalculate();
    //    }
    //    public override void Aсtion()
    //    {
    //        foreach (SpeedSV speedSV in Filter.ComponentsT0.Values)
    //        {
    //            AсtionUser(speedSV, Filter.ComponentsT1[speedSV.Id], Filter.ComponentsT2[speedSV.Id], Filter.ComponentsT3[speedSV.Id]);
    //        }
    //        //for (int i = 0; i < Filter.Count; i++)
    //        //{
    //        //    if( Filter.ComponentsT3[i].EnargyFact> Filter.ComponentsT1[i].EnargyUse)
    //        //    {
    //        //        //Ускорение
    //        //        if (Filter.ComponentsT0[i].dXSV < Filter.ComponentsT2[i].dX)
    //        //        {
    //        //            Filter.ComponentsT2[i].dX = Filter.ComponentsT2[i].dX - Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dXSV > Filter.ComponentsT2[i].dX)
    //        //            {
    //        //                Filter.ComponentsT2[i].dX = Filter.ComponentsT0[i].dXSV;
    //        //            }
    //        //        }
    //        //        else if (Filter.ComponentsT0[i].dXSV > Filter.ComponentsT2[i].dX)
    //        //        {
    //        //            Filter.ComponentsT2[i].dX = Filter.ComponentsT2[i].dX + Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dXSV < Filter.ComponentsT2[i].dX)
    //        //            {
    //        //                Filter.ComponentsT2[i].dX = Filter.ComponentsT0[i].dXSV;
    //        //            }
    //        //        }
    //        //        if (Filter.ComponentsT0[i].dYSV < Filter.ComponentsT2[i].dY)
    //        //        {
    //        //            Filter.ComponentsT2[i].dY = Filter.ComponentsT2[i].dY - Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dYSV > Filter.ComponentsT2[i].dY)
    //        //            {
    //        //                Filter.ComponentsT2[i].dY = Filter.ComponentsT0[i].dYSV;
    //        //            }
    //        //        }
    //        //        else if (Filter.ComponentsT0[i].dYSV > Filter.ComponentsT2[i].dY)
    //        //        {
    //        //            Filter.ComponentsT2[i].dY = Filter.ComponentsT2[i].dY + Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dYSV < Filter.ComponentsT2[i].dY)
    //        //            {
    //        //                Filter.ComponentsT2[i].dY = Filter.ComponentsT0[i].dYSV;
    //        //            }
    //        //        }
    //        //        if (Filter.ComponentsT0[i].dZSV < Filter.ComponentsT2[i].dZ)
    //        //        {
    //        //            Filter.ComponentsT2[i].dZ = Filter.ComponentsT2[i].dZ - Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dZSV > Filter.ComponentsT2[i].dZ)
    //        //            {
    //        //                Filter.ComponentsT2[i].dZ = Filter.ComponentsT0[i].dZSV;
    //        //            }
    //        //        }
    //        //        else if (Filter.ComponentsT0[i].dZSV > Filter.ComponentsT2[i].dZ)
    //        //        {
    //        //            Filter.ComponentsT2[i].dZ = Filter.ComponentsT2[i].dZ + Filter.ComponentsT1[i].Acc;
    //        //            Filter.ComponentsT3[i].EnargyFact = Filter.ComponentsT3[i].EnargyFact - Filter.ComponentsT1[i].EnargyUse;
    //        //            if (Filter.ComponentsT0[i].dZSV < Filter.ComponentsT2[i].dZ)
    //        //            {
    //        //                Filter.ComponentsT2[i].dZ = Filter.ComponentsT0[i].dZSV;
    //        //            }
    //        //        }
    //        //    }

    //        //    //Проверка достижения максимальной скорости
    //        //    if (Filter.ComponentsT2[i].dX == Filter.ComponentsT0[i].dXSV)
    //        //    {
    //        //        if (Filter.ComponentsT2[i].dY == Filter.ComponentsT0[i].dXSV)
    //        //        {
    //        //            if (Filter.ComponentsT2[i].dZ == Filter.ComponentsT0[i].dXSV)
    //        //            {
    //        //                ECSCore.ECS.Instance.RemoveComponent<Acceleration>(Filter.ComponentsT0[i].Id, null); //Удалить компонент ускорения
    //        //            }
    //        //        }
    //        //    }
    //        //}
    //    }
    //    private void AсtionUser(SpeedSV speedSV, Acceleration acceleration, Speed speed, Enargy enargy)
    //    {
    //        if (enargy.EnargyFact > acceleration.EnargyUse)
    //        {
    //            //Ускорение
    //            if (speedSV.dXSV < speed.dX)
    //            {
    //                speed.dX = speed.dX - acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dXSV > speed.dX)
    //                {
    //                    speed.dX = speedSV.dXSV;
    //                }
    //            }
    //            else if (speedSV.dXSV > speed.dX)
    //            {
    //                speed.dX = speed.dX + acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dXSV < speed.dX)
    //                {
    //                    speed.dX = speedSV.dXSV;
    //                }
    //            }
    //            if (speedSV.dYSV < speed.dY)
    //            {
    //                speed.dY = speed.dY - acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dYSV > speed.dY)
    //                {
    //                    speed.dY = speedSV.dYSV;
    //                }
    //            }
    //            else if (speedSV.dYSV > speed.dY)
    //            {
    //                speed.dY = speed.dY + acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dYSV < speed.dY)
    //                {
    //                    speed.dY = speedSV.dYSV;
    //                }
    //            }
    //            if (speedSV.dZSV < speed.dZ)
    //            {
    //                speed.dZ = speed.dZ - acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dZSV > speed.dZ)
    //                {
    //                    speed.dZ = speedSV.dZSV;
    //                }
    //            }
    //            else if (speedSV.dZSV > speed.dZ)
    //            {
    //                speed.dZ = speed.dZ + acceleration.Acc;
    //                enargy.EnargyFact = enargy.EnargyFact - acceleration.EnargyUse;
    //                if (speedSV.dZSV < speed.dZ)
    //                {
    //                    speed.dZ = speedSV.dZSV;
    //                }
    //            }
    //        }

    //        //Проверка достижения максимальной скорости
    //        if (speed.dX == speedSV.dXSV)
    //        {
    //            if (speed.dY == speedSV.dXSV)
    //            {
    //                if (speed.dZ == speedSV.dXSV)
    //                {
    //                    ECSCore.ECS.Instance.RemoveComponent<Acceleration>(speedSV.Id, null); //Удалить компонент ускорения
    //                }
    //            }
    //        }
    //    }
    //}
}